#ifndef _XMEMORY_H
#define _XMEMORY_H

/*
    Implementation of wrappers under default malloc, calloc, realloc.
    To avoid memory allocation failures.
*/

#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include "printerr.h"

#define new(type)			   (type*)xmalloc(sizeof(type))        
#define newc(type, count)	   (type*)xmalloc(sizeof(type) * (count))
#define cnew(type, count)	   (type*)xcalloc(count, sizeof(type))
#define rnew(type, count, var) (type*)xrealloc(var, sizeof(type) * (count))

void* xmalloc(size_t size);
void* xcalloc(size_t blocks, size_t block_size);
void* xrealloc(void* memblock, size_t size);

#endif // _XMEMORY_H