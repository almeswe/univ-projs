#ifndef _PRINTERR_h
#define _PRINTERR_h

#include <stdio.h>
#include <stdlib.h>

#define printerr(format, ...)                     \
    if (fprintf(stderr, format, __VA_ARGS__) < 0) \
        perror("printerr"), exit(1)

#endif