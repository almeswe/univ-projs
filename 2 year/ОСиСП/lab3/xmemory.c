#include "xmemory.h"

void* xmalloc(size_t size)
{
	void* memory = malloc(size);
	if (memory == NULL)
		printerr("%s", "xmalloc allocation error");
	return memory;
}

void* xcalloc(size_t blocks, size_t block_size)
{
	void* memory = calloc(blocks, block_size);
	if (memory == NULL)
		printerr("%s", "calloc allocation error");
	return memory;
}

void* xrealloc(void* memblock, size_t size)
{
	void* memory = realloc(memblock, size);
	if (memory == NULL)
		printerr("%s", "realloc allocation error");
	return memory;
}