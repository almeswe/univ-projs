#ifndef _PRINTERR_H
#define _PRINTERR_H

/*
    Macro under fprintf with error check, to print error to stderr.
*/

#include <stdio.h>
#include <stdlib.h>

#define printerr(format, ...)                     \
    if (fprintf(stderr, format, __VA_ARGS__) < 0) \
        perror("printerr"), exit(1)

#endif // _PRINTERR_H