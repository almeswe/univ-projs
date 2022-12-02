#ifndef _QUEUE_H
#define _QUEUE_H

#undef UNICODE

#define WIN32_LEAN_AND_MEAN
#define _CRT_SECURE_NO_WARNINGS

#include "task.h"

#include <stdlib.h>

#include <windows.h>

#define new(type) (type*)malloc(sizeof(type))

#define CONCURRENT_QUEUE_SCALE				2
#define CONCURRENT_QUEUE_INITIAL_CAPACITY	8

typedef struct ConcurrentQueue {
	SIZE_T size;
	SIZE_T cursor;
	SIZE_T capacity;
	HANDLE mutex;
	QueueTask tasks[0];
} ConcurrentQueue;

ConcurrentQueue* QueueCreate();
VOID QueueRelease(ConcurrentQueue* queue);

BOOL QueueEmpty(const ConcurrentQueue* queue);
BOOL QueueEmptySafe(const ConcurrentQueue* queue);

QueueTask* QueuePop(ConcurrentQueue* queue);
QueueTask* QueuePopSafe(ConcurrentQueue* queue);

ConcurrentQueue* QueuePut(ConcurrentQueue* queue, const QueueTask* task);
VOID QueuePutSafe(ConcurrentQueue** queue, const QueueTask* task);

#endif