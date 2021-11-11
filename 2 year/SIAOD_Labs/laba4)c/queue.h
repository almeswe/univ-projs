#ifndef QUEUE_H
#define QUEUE_H

#define QUEUE_DEFAULT_SIZE 1024

typedef struct Queue
{
	int size;
	int offset;
	int** entries;
} Queue;

Queue* queue_new();

int* queue_pop(Queue* queue);
void queue_push(Queue* queue, int* entry);

#endif QUEUE_H