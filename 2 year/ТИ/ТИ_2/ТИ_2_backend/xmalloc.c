#include "xmalloc.h"

void* xcalloc(size_t count, size_t size) {
	void* allocated;
	if (!(allocated = calloc(count, size))) {
		printf("Cannot allocate %lu bytes\n", size), exit(1);
	}
	return allocated;
}

void* xmalloc(size_t size) {
	void* allocated;
	if (!(allocated = malloc(size))) {
		printf("Cannot allocate %lu bytes\n", size), exit(1);
	}
	return allocated;
}