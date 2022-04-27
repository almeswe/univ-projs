#include <stdio.h>
#include <stdlib.h>
    
#include <errno.h>
#include <unistd.h>
#include <dirent.h>
#include <libgen.h>
#include <string.h>
#include <stdint.h>
#include <stdbool.h>
#include <sys/stat.h>
#include <sys/types.h>

#include "sbuffer.h"

#define METADATA_SIZE 4096

#define frmt(source, format, ...) \
    sprintf(source, format, __VA_ARGS__), source

static char* module = NULL;
static __dev_t device = 0;
static ino_t* inodes = NULL;

struct dir_size {
    uint64_t disk_size;
    uint64_t logical_size;

    uint64_t local_disk_size;
    uint64_t local_logical_size;
};

struct file_size {
    uint64_t disk_size;
    uint64_t logical_size;
};

void perrorf(char* message);

void* xmalloc(size_t size) {
    void* memory = malloc(size);
    if (memory == NULL) {
        perrorf("Memory allocation failure."), exit(1);
    }
}

void* xcalloc(size_t count, size_t cell_size) {
    void* memory = calloc(count, cell_size);
    if (memory == NULL) {
        perrorf("Memory allocation failure."), exit(1);
    }
}

void* xrealloc(void* memblock, size_t size) {
    void* memory = realloc(memblock, size);
    if (memory == NULL) {
        perrorf("Memory allocation failure."), exit(1);
    }
}

void perrorf(char* message) {
    char* buffer = (char*)xcalloc(1,
        strlen(message) + strlen(module) + 3);
    sprintf(buffer, "%s: %s", module, message);
    perror(buffer), free(buffer);
}

bool is_used_inode(ino_t inode) {
    for (uint32_t i = 0; i < sbuffer_len(inodes); i++) {
        if (inodes[i] == inode) {
            return true;
        }
    }
    return false;
}

bool is_used_hard_link(const char* path) {
    struct stat st;
    if (stat(path, &st)) {
        return false;
    }
    if (st.st_nlink > 1) {
        if (is_used_inode(st.st_ino)) {
            return true;
        }
        sbuffer_add(inodes, st.st_ino);
    }
    return false;
}

struct file_size* get_file_size(const char* file) {
    struct stat st;
    if (lstat(file, &st)) {
        char* error_str = (char*)xcalloc(1, strlen(file) + 23);
        frmt(error_str, "Cannot get file size: %s", file);
        return perrorf(error_str), free(error_str), NULL;
    }
    if (st.st_dev != device || is_used_hard_link(file)) {
        return NULL;
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
        return perrorf(dir_path), NULL;
    }
    struct file_size* fsize;
    struct dir_size* child_dir_size;
    struct dir_size* dir_size = (struct dir_size*)
        xcalloc(1, sizeof (struct dir_size));
    // add up dir's metadata size to disk space  
    dir_size->disk_size = dir_size->local_disk_size = METADATA_SIZE;

    char* new_path = NULL;
    struct dirent* entity;
    bool is_hard_link = is_used_hard_link(dir_path);
    if (!is_hard_link) {
        while ((errno = 0, entity = readdir(dir)) != NULL) {
            size_t old_path_size = strlen(dir_path);
            new_path = xcalloc(1, 5+old_path_size+strlen(entity->d_name));
            strcpy(new_path, dir_path);
            if (strcmp(dir_path, "/") == 0) {
                strcpy(new_path+old_path_size, entity->d_name);
            } else {
                strcpy(new_path+old_path_size, "/");
                strcpy(new_path+old_path_size+1, entity->d_name);
            }
            switch (entity->d_type) {
                case DT_CHR:
                case DT_BLK:
                case DT_WHT:
                case DT_FIFO:
                case DT_SOCK:
                case DT_UNKNOWN:
                    break;
                case DT_LNK:
                case DT_REG:
                    fsize = get_file_size(new_path);
                    if (fsize) {
                        dir_size->disk_size += fsize->disk_size;
                        dir_size->logical_size += fsize->logical_size;
                        dir_size->local_disk_size += fsize->disk_size;
                        dir_size->local_logical_size += fsize->logical_size;
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
        if (entity == NULL && errno != 0) {
            return perrorf("Cannot read new entity using readdir"), NULL;
        }
    }
    if (closedir(dir) < 0) {
        return perrorf("Cannot close entity using closedir"), NULL;
    }
    if (!is_hard_link) {
        printf("%s %lu %lu\n", dir_path, dir_size->local_disk_size,
            dir_size->local_logical_size);
        free(new_path);
    }
    return dir_size;
}

__dev_t get_current_device(char* dir_path) {
    struct stat st;
    if (stat(dir_path, &st)) {
        return perrorf("Cannot get device"), 0;
    }
    return st.st_dev;
}

int main(int argc, char** argv) {
    if (argc != 2) {
        return fprintf(stderr, "%s\n", "Incorrect argument count passed"), 1;
    }
    struct dir_size* size;
    module = basename(argv[0]);
    char* abs_path_ptr = realpath(argv[1], NULL);
    if (!abs_path_ptr) {
        return perrorf(argv[1]), 1;
    }
    else {
        device = get_current_device(abs_path_ptr);
        if (!device) {
            return perrorf("Cannot get current device"), 1;
        }
        if (size = get_dir_size(abs_path_ptr)) {
            printf("%lu %lu %f%%\n\n", size->disk_size, size->logical_size,
                (((float)size->logical_size / size->disk_size) * 100));
            free(size);
        }
        free(abs_path_ptr);
        sbuffer_free(inodes);
    }
    return 0;
}