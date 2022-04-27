#include <errno.h>
#include <error.h>

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

#include <dirent.h>
#include <sys/types.h>

#include "xmemory.h"

enum _constants {
    IO_BUFFER_SIZE = 16777216 // 16 MiB 1024*1024*16
};

struct periods {
    size_t total;
    size_t periods_0;
    size_t periods_1;
    const char* abspath;
};

static char* module = NULL;
static char io_buf[IO_BUFFER_SIZE];

#define perror_va(buf, size, format, ...)     \
    char* buf = (char*)xcalloc(1, (size)+1);  \
    sprintf(buf, format, __VA_ARGS__);        \
    perrorf(buf), free(buf)

void perrorf(const char* message) {
    char* buffer = (char*)xcalloc(1,
        strlen(message) + strlen(module) + 3);
    sprintf(buffer, "%s: %s", module, message);
    perror(buffer), free(buffer);
}

bool find_periods_info(const char* abspath, 
    struct periods* periods)
{
    size_t read = 0;
    periods->total = 0;
    periods->periods_0 = 0;
    periods->periods_1 = 0;
    periods->abspath = abspath;
    FILE* fd = fopen(abspath, "r");
    if (fd == NULL) {
        perror_va(buf, strlen(abspath), "%s", abspath);
        return false;
    }
    int8_t current = -1;
    while (fgets(io_buf, sizeof io_buf, fd) != NULL) {          // handle error from fgets
        read = strlen(io_buf);                                  // this is a proper way of determining the size of read bytes??
        periods->total += read;
        for (size_t i = 0; i < read; i++) {
            if (current == -1) {
                current = io_buf[i];
                continue;
            }
            if (current != io_buf[i]) {
                current == 1 ? (periods->periods_1 += 1) :
                   (periods->periods_0 += 1);
                current = io_buf[i];
            }
        }
    }
    if (fclose(fd) == NULL) {
        perror_va(buf, strlen(abspath)+8, "fclose->%s", abspath);
        return false;
    }
    return true;
}

bool walk(const char* path)
{
    DIR* stream = opendir(path);
    if (stream == NULL) {
        perror_va(buf, strlen(path), "%s", path);
        return false;
    }
    struct dirent* dirent = NULL;
    while ((errno = 0, dirent = readdir(stream)) != NULL) {
        switch (dirent->d_type) {
            case DT_REG:
                break;
            case DT_DIR:
                break;
        }
    }
    if (dirent == NULL && errno != 0) {
        perrorf("readdir()");
    }
    return true;
}

int main(int argc, char** argv)
{
    return 0;
}