#ifndef _PRINTERR_H
#define _PRINTERR_H

#include <stdio.h>

#define printerr(format, ...)					    \
	if (fprintf(stderr, format, __VA_ARGS__) < 0) \
		perror("printerr"), exit(1)

#endif