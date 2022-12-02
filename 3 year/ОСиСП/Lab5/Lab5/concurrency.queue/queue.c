#include "queue.h"
#include <stdio.h>

QueueTask QueueSortTaskCreate(CHAR* line) {
	QueueTask task = { 0 };
	task.line = line;
	task.kind = TASK_SORT;
	task.size = strlen(line);
	return task;
}

QueueTask QueueDumpTaskCreate(CHAR** lines, SIZE_T size) {
	QueueTask task = { 0 };
	task.size = size;
	task.kind = TASK_DUMP;
	task.lines = lines;
	return task;
}

void QueueTaskRelease(QueueTask task) {
	if (task.line != NULL) {
		free(task.line);
	}
}

ConcurrentQueue* QueueCreate() {
	ConcurrentQueue* queue = new(ConcurrentQueue);
	queue->size = 0;
	queue->cursor = 0;
	queue->mutex = CreateMutex(NULL, FALSE, NULL);
	queue->capacity = 0;
	return queue;
}

BOOL QueueEmpty(const ConcurrentQueue* queue) {
	return queue && (queue->cursor == queue->size);
}

BOOL QueueEmptySafe(const ConcurrentQueue* queue) {
	WaitForSingleObject(queue->mutex, INFINITE);
	BOOL isEmpty = QueueEmpty(queue);
	return ReleaseMutex(queue->mutex), isEmpty;
}

QueueTask* QueuePop(ConcurrentQueue* queue) {
	if (queue->cursor < queue->size) {
		return &queue->tasks[queue->cursor++];
	}
	return NULL;
}

QueueTask* QueuePopSafe(ConcurrentQueue* queue) {
	DWORD status = WaitForSingleObject(queue->mutex, INFINITE);
	QueueTask* task = QueuePop(queue);
	return ReleaseMutex(queue->mutex), task;
}

ConcurrentQueue* QueuePut(ConcurrentQueue* queue, const QueueTask* task) {
	if (queue->capacity <= queue->size) {
		queue->capacity = queue->capacity == 0 ? CONCURRENT_QUEUE_INITIAL_CAPACITY : 
			(SIZE_T)(queue->capacity * CONCURRENT_QUEUE_SCALE);
		SIZE_T size = sizeof(ConcurrentQueue) + sizeof(QueueTask) * queue->capacity;
		queue = (ConcurrentQueue*)realloc(queue, size);
	}
	queue->tasks[queue->size++] = *task;
	return queue;
}

VOID QueuePutSafe(ConcurrentQueue** queue, const QueueTask* task) {
	DWORD status = WaitForSingleObject((*queue)->mutex, INFINITE);
	if (status != WAIT_FAILED) {
		*queue = QueuePut(*queue, task);
		ReleaseMutex((*queue)->mutex);
	}
}

VOID QueueRelease(ConcurrentQueue* queue) {
	if (queue != NULL) {
		ReleaseMutex(queue->mutex);
		CloseHandle(queue->mutex);
		free(queue);
	}
}