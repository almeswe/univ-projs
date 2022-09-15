#include "xmemory.h"

void* xmalloc(size_t size) {
	void* memory = malloc(size);
	if (memory == NULL) {
		exit(1);
	}
	return memory;
}

void* xcalloc(size_t blocks, size_t block_size) {
	void* memory = calloc(blocks, block_size);
	if (memory == NULL) {
		exit(1);
	}
	return memory;
}

void* xrealloc(void* memblock, size_t size) {
	void* memory = realloc(memblock, size);
	if (memory == NULL) {
		exit(1);
	}
	return memory;
}