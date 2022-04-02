#include <stdio.h>
#include <stdlib.h>

#include <dirent.h>
#include <string.h>
#include <stdint.h>
#include <errno.h>

#include <sys/stat.h>

#define METADATA_SIZE 4096

#define frmt(source, format, ...) \
    sprintf(source, format, __VA_ARGS__), source

struct dir_size {
    uint64_t disk_size;
    uint64_t logical_size;
};

struct file_size {
    uint64_t disk_size;
    uint64_t logical_size;
};

void* xmalloc(size_t size) {
    void* memory = malloc(size);
    if (memory == NULL) {
        perror("Memory allocation failure."), exit(1);
    }
}

void* xcalloc(size_t count, size_t cell_size) {
    void* memory = calloc(count, cell_size);
    if (memory == NULL) {
        perror("Memory allocation failure."), exit(1);
    }
}

void* xrealloc(void* memblock, size_t size) {
    void* memory = realloc(memblock, size);
    if (memory == NULL) {
        perror("Memory allocation failure."), exit(1);
    }
}

struct file_size* get_file_size(const char* file) {
    struct stat st;
    if (stat(file, &st)) {
        return perror("Cannot get file size"), NULL;
    }
    struct file_size* size = (struct file_size*)
        xcalloc(1, sizeof (struct file_size));
    size->disk_size = st.st_blocks * 512; 
    size->logical_size = st.st_size;
    return size;
}

struct dir_size* get_dir_size(char* dir_path) {
    DIR* dir = opendir(dir_path);
    if (dir == NULL) {
        char* error_str = (char*)xcalloc(1, strlen(dir_path) + 35); // 29 is size of error string + null terminator
        frmt(error_str, "Specified path is incorrect: %s", dir_path);
        return perror(error_str), free(error_str), NULL;
    }
    struct file_size* fsize;
    struct dir_size* child_dir_size;
    struct dir_size* dir_size = (struct dir_size*)
        xcalloc(1, sizeof (struct dir_size));
    // add up dir's metadata size to disk space  
    dir_size->disk_size = METADATA_SIZE;

    char* new_path = NULL;
    struct dirent* entity;
    while ((errno=0, entity = readdir(dir)) != NULL) {
        if (errno != 0) {
            return perror("Cannot read new entity using readdir"), NULL;
        }
        size_t old_path_size = strlen(dir_path);
        new_path = xcalloc(1, 5+old_path_size+strlen(entity->d_name));
        strcpy(new_path, dir_path);
        strcpy(new_path+old_path_size, "/");
        strcpy(new_path+old_path_size+1, entity->d_name);
        switch (entity->d_type) {
            case DT_CHR:
            case DT_BLK:
            case DT_LNK:
            case DT_WHT:
            case DT_FIFO:
            case DT_UNKNOWN:
                break;
            case DT_REG:
            case DT_SOCK:
                fsize = get_file_size(new_path);
                if (fsize) {
                    dir_size->disk_size += fsize->logical_size;
                    dir_size->logical_size += fsize->logical_size;
                    free(fsize);
                }
                break;
            case DT_DIR:
                if (strcmp(".",  entity->d_name) == 0 ||
                    strcmp("..", entity->d_name) == 0) {
                        break;
                }
                child_dir_size = get_dir_size(new_path);
                if (child_dir_size) {
                    dir_size->disk_size += child_dir_size->disk_size; 
                    dir_size->logical_size += child_dir_size->logical_size;
                    free(child_dir_size);
                }
                break;
        }
    }
    if (closedir(dir) < 0) {
        return perror("Cannot close read entity using closedir"), NULL;
    }
    if (dir_size->disk_size != 0) {
        printf("%s %lu %lu\n", dir_path, 
            dir_size->disk_size, dir_size->logical_size);
    }
    free(new_path);
    return dir_size;
}

int main(int argc, char** argv) {
    if (argc != 2) {
        return perror("Incorrect argument count passed"), 1;
    }
    struct dir_size* size;
    char* abs_path_ptr = realpath(argv[1], NULL);
    if (!abs_path_ptr) {
        char* error_str = (char*)xcalloc(1, strlen(argv[1]) + 35); // 29 is size of error string + null terminator
        frmt(error_str, "Specified path is incorrect: %s", argv[1]);
        return perror(error_str), free(error_str), 1;
    }
    else {
        if (size = get_dir_size(abs_path_ptr)) {
            printf("%lu %lu %f%%\n\n", size->disk_size, size->logical_size, (((float)size->logical_size / size->disk_size) * 100));
            free(size);
        }
        free(abs_path_ptr);
    }
    return 0;
}