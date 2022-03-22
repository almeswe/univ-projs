#ifndef _XMALLOC_H
#define _XMALLOC_H

#include <stdio.h>
#include <stdlib.h>

void* xmalloc(size_t size);
void* xcalloc(size_t count, size_t size);

#endif