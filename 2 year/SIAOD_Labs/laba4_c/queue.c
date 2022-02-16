#include "queue.h"
#include <assert.h>

Queue* queue_new()
{
	Queue* q = (Queue*)malloc(sizeof(Queue));
	assert(q);
	q->size = 0;
	q->offset = 0;
	q->entries = (int*)calloc(
		sizeof(int*), QUEUE_DEFAULT_SIZE);
	return q;
}

int* queue_pop(Queue* queue)
{
	if (!queue->size)
		return NULL;
	queue->size--;
	return queue->entries[queue->offset++];
}

void queue_push(Queue* queue, int* entry)
{
	queue->size++;
	queue->entries[queue->size-1 + queue->offset] = entry;
}