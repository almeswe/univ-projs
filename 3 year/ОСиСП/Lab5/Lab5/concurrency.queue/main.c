#include "task.h"
#include "queue.h"

#include <stdio.h>
#include <stdbool.h>

static ConcurrentQueue* queue = NULL;

PVOID thread_routine(PVOID data) {
	QueueTask task = QueueTaskCreate("taskname");
	QueuePutSafe(&queue, &task);
	ExitThread(0);
}

INT main(INT argc, CHAR** argv) {
	QueueTask task1 = QueueTaskCreate("task 1");
	QueueTask task2 = QueueTaskCreate("task 2");
	QueueTask task3 = QueueTaskCreate("task 3");
	queue = QueueCreate();
	HANDLE thread1 = CreateThread(NULL, 0, thread_routine, NULL, 0, 0);
	HANDLE thread2 = CreateThread(NULL, 0, thread_routine, NULL, 0, 0);
	WaitForSingleObject(thread2, INFINITE);
	WaitForSingleObject(thread1, INFINITE);
	printf("%s\n", QueuePop(queue)->data);
	printf("%s\n", QueuePop(queue)->data);
	CloseHandle(thread1);
	CloseHandle(thread2);
	QueueRelease(queue);
	return 0;
}