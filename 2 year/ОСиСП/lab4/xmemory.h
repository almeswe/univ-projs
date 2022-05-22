#ifndef _XMEMORY_H
#define _XMEMORY_H

#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include "printerr.h"

void* xmalloc(size_t size);
void* xcalloc(size_t blocks, size_t block_size);
void* xrealloc(void* memblock, size_t size);

#endif