#include <errno.h>
#include <error.h>

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <signal.h>
#include <stdbool.h>

#include <dirent.h>
#include <libgen.h>
#include <unistd.h>
#include <sys/wait.h>
#include <sys/types.h>

#include "xmemory.h"
#include "sbuffer.h"

/*
    поменять подсчет (задание другое)
*/

#define bt(src, bit) \
    (((src) >> (bit)) & 1)

#define streq(str1, str2) \
    (strcmp(str1, str2) == 0)

enum _constants {
    IO_BUFFER_SIZE = 16777216 // 16 MiB 1024*1024*16
};

static int status = 0;
static int childs = 0;
static int max_childs;
static char* module = NULL;
static char io_buf[IO_BUFFER_SIZE];

#define perror_va(buf, size, format, ...)     \
    char* buf = (char*)xcalloc(1, (size)+1);  \
    sprintf(buf, format, __VA_ARGS__);        \
    perrorf(buf), free(buf)

void perrorf(const char* message) {
    char* pure_module_name = basename(module);
    char* buffer = (char*)xcalloc(1,
        strlen(message) + strlen(pure_module_name) + 3 + 64);
    sprintf(buffer, "%d %s: %s", getpid(), pure_module_name, message);
    perror(buffer), free(buffer);
}

char* make_path(const char* base, const char* path) 
{
    bool root = false;
    size_t new_path_size = strlen(base); 
    new_path_size += (root = streq(base, "/")) ?
        strlen(path) : (strlen(path) + 1);
    char* new_path = (char*)xcalloc(1, new_path_size+1);
    sprintf(new_path, root ? "%s%s" : "%s/%s", base, path);
    return new_path;
}

typedef struct _holder {
    bool binary;
    size_t length;
    size_t repeats;
} holder;

void find_periods(const char* abspath)
{
#define _manage_holder(new)                             \
    bool found = false;                                 \
    for (int h = 0; h < sbuffer_len(holders); h++) {    \
        if (holders[h].binary == current) {             \
            if (holders[h].length == seq_len) {         \
                found = true;                           \
                holders[h].repeats += 1;                \
                break;                                  \
            }                                           \
        }                                               \
    }                                                   \
    if (!found) {                                       \
        holder hldr = {                                 \
            .binary = current,                          \
            .length = seq_len,                          \
            .repeats = 1                                \
        };                                              \
        sbuffer_add(holders, hldr);                     \
    }                                                   \
    seq_len = 1;                                        \
    current = new

    size_t read = 0;
    size_t total = 0;
    size_t seq_len = 1;
    int8_t current = -1;
    bool eof_met = false;
    FILE* fd = fopen(abspath, "r");
    holder* holders = NULL;
    if (fd == NULL) {
        perrorf(abspath);
        return;
    }
    while (!eof_met) {          
        read = fread(io_buf, sizeof(char), sizeof io_buf, fd);
        if (read < sizeof io_buf && !(eof_met = feof(fd))) {
            perrorf("fread()");
            if (fclose(fd) == EOF) {
                perrorf("fclose()");
            }
            return;
        }
        total += read;
        for (size_t i = 0; i < read; i++) {
            for (int8_t bit = __CHAR_BIT__-1; bit >= 0; bit--) {
                if (current == -1) {
                    current = bt(io_buf[i], bit);
                }
                else if (current != bt(io_buf[i], bit)) {
                    _manage_holder(bt(io_buf[i], bit));                
                }
                else {
                    seq_len += 1;
                }
            }
        }
    }
    _manage_holder(-1);
    printf("%d %s %lu ", getpid(), abspath, total);
    for (size_t i = 0; i < sbuffer_len(holders); i++) {
        if (!holders[i].binary) {
            printf("%lu-%lu ", holders[i].length, holders[i].repeats);
        }
    }
    printf("| ");
    for (size_t i = 0; i < sbuffer_len(holders); i++) {
        if (holders[i].binary) {
            printf("%lu-%lu ", holders[i].length, holders[i].repeats);
        }
    }
    printf("\n");
    sbuffer_free(holders);
    if (fclose(fd) == EOF) {
        perrorf("fclose()");
    }
#undef _manage_holder
}

void walk(const char* path)
{
    DIR* stream = opendir(path);
    if (stream == NULL) {
        return perrorf(path), (void)1;
    }
    struct dirent* dirent = NULL;
    while ((errno = 0, dirent = readdir(stream)) != NULL) {
        int status = 0;
        char* abspath = make_path(path, dirent->d_name);
        switch (dirent->d_type) {
            case DT_REG:
                if (childs >= max_childs) {
                    if (wait(&status) < 0 && errno != ECHILD) {
                        perrorf("wait()"), exit(1);
                    } 
                } 
                if (fork() != 0) {
                    childs += 1;
                } 
                else {
                    find_periods(abspath);
                    free(abspath), exit(0);
                }
                break;
            case DT_DIR:
                if (streq(dirent->d_name, ".") || 
                    streq(dirent->d_name, "..")) {
                        continue;
                }
                walk(abspath);
                break;
        }
        free(abspath);
    }
    if (dirent == NULL && errno != 0) {
        perrorf("readdir()");
    }
    if (closedir(stream) < 0) {
        perrorf("closedir()");
    }
}

int main(int argc, char** argv)
{
    if (argc != 3) {
        printerr("%s\n", "Incorrect argument count passed.");
    } 
    else {
        module = argv[0];
        max_childs = atoi(argv[1]) - 1;
        if (max_childs <= 0) {
            printerr("%s\n", "Number of max processes must be greater than zero!");
        }
        else {
            char* abspath = realpath(argv[2], NULL);
            walk(abspath), free(abspath);
            while (wait(&status) > 0);
            if (errno != ECHILD) {
                perrorf("wait()");
            }
        }
    }
    return 0;
}