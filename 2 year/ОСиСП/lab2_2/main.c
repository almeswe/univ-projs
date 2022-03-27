#include <stdio.h>
#include <stdlib.h>

#include <dirent.h>
#include <string.h>
#include <stdint.h>
#include <errno.h>

#include <sys/stat.h>

#define frmt(source, format, ...) \
    sprintf(source, format, __VA_ARGS__), source

#define create_buf(buf, size) \
    char buf[size]; memset(buf, 0, sizeof buf)

struct dir_size {
    uint64_t disk_size;
    uint64_t logical_size;

    uint32_t entities_count;
    uint32_t entities_processed; 
};

struct file_size {
    uint64_t disk_size;
    uint64_t logical_size;
};

struct file_size* get_file_size(const char* file) {
    create_buf(err_buf, 4096);    
    struct stat st;
    if (stat(file, &st)) {
        frmt(err_buf, "%s: %s\n", strerror(errno), file);
        return fputs((char*)err_buf, stderr), NULL;
    }
    struct file_size* size = (struct file_size*)
        calloc(1, sizeof (struct file_size));
    size->disk_size = st.st_blocks * 512; 
    size->logical_size = st.st_size;
    return size;
}

struct dir_size* get_dir_size(const char* dir_path) {
    create_buf(err_buf, 4096);    
    DIR* dir = opendir(dir_path);
    if (dir == NULL) {
        frmt(err_buf, "Dir \'%s\' does not exists!\n", dir_path);
        return fputs((char*)err_buf, stderr), NULL;
    }

    struct file_size* fsize;
    struct dir_size* child_dir_size;
    struct dir_size* dir_size = (struct dir_size*)
        calloc(1, sizeof (struct dir_size));
    // add up dir's metadata size to disk space  
    dir_size->disk_size = 4096;

    struct dirent* entity;
    while ((entity = readdir(dir)) != NULL) {
        create_buf(path_buf, 4096);    
        strcpy(path_buf, dir_path);
        strcat(path_buf, "/");
        strcat(path_buf, entity->d_name);  
        switch (entity->d_type) {
            case DT_CHR:
            case DT_BLK:
            case DT_LNK:
            case DT_WHT:
            case DT_SOCK:
            case DT_FIFO:
            case DT_UNKNOWN:
                break;
            case DT_REG:
                fsize = get_file_size(path_buf);
                if (fsize) {
                    dir_size->disk_size += fsize->disk_size;
                    dir_size->logical_size += fsize->logical_size;
                    free(fsize);
                }
                break;
            case DT_DIR:
                if (strcmp(".",  entity->d_name) == 0 ||
                    strcmp("..", entity->d_name) == 0) {
                        break;
                }

                child_dir_size = get_dir_size(path_buf);
                if (child_dir_size) {
                    dir_size->disk_size += child_dir_size->disk_size; 
                    dir_size->logical_size += child_dir_size->logical_size;
                    free(child_dir_size);
                }
                break;
        }
    }

    if (dir_size->disk_size != 0) {
        printf("Dir: %s\n", dir_path);
        printf("Disk size    : %lu (%luK)\n", dir_size->disk_size, dir_size->disk_size/1024);
        printf("Logical size : %lu (%luK)\n", dir_size->logical_size, dir_size->logical_size/1024);
        printf("Usage: %f%%\n\n", (((float)dir_size->logical_size / dir_size->disk_size) * 100));
    }
    return closedir(dir), dir_size;
}

int main(int argc, char** argv) {
    create_buf(err_buf, 4096);    
    create_buf(abs_path_buf, 4096);    
    if (argc != 2) {
        frmt(err_buf, "Incorrect argument count passed: %d.\n", argc);
        return fputs((char*)err_buf, stderr), 1;
    }
    char* abs_path_ptr;
    struct dir_size* size;
    abs_path_ptr = realpath(argv[1], abs_path_buf);
    if (!abs_path_ptr) {
        frmt(err_buf, "Specified path is incorrect: %s.\n", argv[1]);
        return fputs((char*)err_buf, stderr), 1;
    }
    else {
        if (size = get_dir_size(abs_path_ptr)) {
            free(size);
        }
    }
    return 0;
}