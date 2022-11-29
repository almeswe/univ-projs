#ifndef _TASK_H
#define _TASK_H

#include <windows.h>

typedef enum QueueTaskKind {
	TASK_SORT,
	TASK_DUMP
} QueueTaskKind;

typedef struct QueueTask {
	SIZE_T size;
	union {
		PCHAR line;
		PCHAR* lines;
	};
	QueueTaskKind kind;
} QueueTask;

QueueTask QueueSortTaskCreate(CHAR* line);
QueueTask QueueDumpTaskCreate(CHAR** lines, SIZE_T size);

void QueueTaskRelease(QueueTask task);

#endif