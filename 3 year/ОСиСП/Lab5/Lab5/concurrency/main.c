#define _CRT_SECURE_NO_WARNINGS

#include "task.h"
#include "queue.h"

#include <stdio.h>
#include <stdbool.h>

#define BUF_SIZE			128
#define THREADS_MAX_COUNT	32
#define OUTPUT_FILE_NAME    "C:\\Users\\HP\\Desktop\\output_file.txt"

typedef struct Dispatcher {
	ConcurrentQueue* dump;
	ConcurrentQueue* sort;
	FILE* inFile;
	FILE* outFile;
	PCHAR filePath;
	LONG threadsCount;
	HANDLE threads[THREADS_MAX_COUNT];
} Dispatcher;

static Dispatcher dispatcher = { 0 };

VOID PrintError(const PCHAR message) {
	if (fprintf(stderr, "%s\n", message) < 0) {
		perror("PrintError: ");
	}
}

VOID PrintErrorWin32() {
	CHAR buf[256] = { 0 };
	DWORD error = GetLastError();
	if (error != 0) {
		DWORD dwFlags = FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS;
		DWORD dwLangId = MAKELANGID(LANG_ENGLISH, SUBLANG_DEFAULT);
		if (FormatMessageA(dwFlags, NULL, error, dwLangId, buf, sizeof buf, NULL) != 0) {
			PrintError(buf);
		}
	}
}

PCHAR* DispatcherMergeParts(PCHAR* lPart, SIZE_T lSize, PCHAR* rPart, SIZE_T rSize) {
	SIZE_T rOffset = 0;
	SIZE_T lOffset = 0;
	SIZE_T tempSize = 0;
	PCHAR* tempPart = (PCHAR*)malloc((lSize + rSize) * sizeof(PCHAR));
	while ((rOffset < rSize) || (lOffset < lSize)) {
		if ((rOffset >= rSize) || (lOffset >= lSize)) {
			tempPart[tempSize++] = (lOffset >= lSize) ? 
				rPart[rOffset++] : lPart[lOffset++];
		}
		else {
			INT result = strcmp(lPart[lOffset], rPart[rOffset]);
			tempPart[tempSize++] = (result <= 0) ?
				lPart[lOffset++] : rPart[rOffset++];
		}
	}
	for (SIZE_T i = 0; i < tempSize; i++) {
		lPart[i] = tempPart[i];
	}
	return free(tempPart), lPart;
}

PCHAR* DispatcherSortLines(PCHAR* data, SIZE_T size) {
	if (size == 1) {
		return data;
	}
	else {
		SIZE_T lSize = size / 2;
		SIZE_T rSize = size - lSize;
		PCHAR* lPart = DispatcherSortLines(data, lSize);
		PCHAR* rPart = DispatcherSortLines(data + lSize, rSize);
		return DispatcherMergeParts(lPart, lSize, rPart, rSize);
	}
}

VOID DispatcherCreate(LONG threads, PCHAR filePath) {
	FILE* file = NULL;
	if (threads <= 0 || threads >= THREADS_MAX_COUNT) {
		PrintError("Cannot set this amount of threads (use range: [1; 31])");
		ExitProcess(EXIT_FAILURE);
	}
	fopen_s(&dispatcher.inFile, filePath, "r");
	if (dispatcher.inFile == NULL) {
		perror("Cannot open input file: ");
		ExitProcess(EXIT_FAILURE);
	}
	fopen_s(&dispatcher.outFile, OUTPUT_FILE_NAME, "w");
	if (dispatcher.outFile == NULL) {
		perror("Cannot open or create output file: ");
		ExitProcess(EXIT_FAILURE);
	}
	dispatcher.filePath = filePath;
	dispatcher.threadsCount = threads;
	dispatcher.dump = QueueCreate();
	dispatcher.sort = QueueCreate();
	memset(dispatcher.threads, 0, sizeof dispatcher.threads);
}

VOID DispatcherRelease() {
	QueueRelease(dispatcher.sort);
	QueueRelease(dispatcher.dump);
	if (dispatcher.inFile != NULL) {
		fclose(dispatcher.inFile);
	}
	if (dispatcher.outFile != NULL) {
		fclose(dispatcher.outFile);
	}
}

VOID DispatcherWaitForAllThreads() {
	WaitForMultipleObjects(dispatcher.threadsCount, 
		dispatcher.threads, true, INFINITE);
}

VOID DispatcherThreadsRelease() {
	for (LONG i = 0; i < THREADS_MAX_COUNT; i++) {
		if (dispatcher.threads[i] != NULL) {
			CloseHandle(dispatcher.threads[i]);
			dispatcher.threads[i] = NULL;
		}
	}
}

VOID DispatcherAppendThread(HANDLE thread) {
	for (LONG i = 0; i < THREADS_MAX_COUNT; i++) {
		if (dispatcher.threads[i] == NULL) {
			dispatcher.threads[i] = thread;
			return;
		}
	}
	PrintError("Threads buffer overflow!");
	ExitProcess(EXIT_FAILURE);
}

DWORD WINAPI StartSortRoutine(PVOID param) {
	SIZE_T tasksSize = 0;
	PCHAR* tasks = (PCHAR*)malloc(
		dispatcher.sort->size*sizeof(PCHAR));
	while (!QueueEmptySafe(dispatcher.sort)) {
		QueueTask* task = QueuePopSafe(dispatcher.sort);
		if (task != NULL && task->kind == TASK_SORT) {
			tasks[tasksSize++] = task->line;
		}
	}
	tasks = DispatcherSortLines(tasks, tasksSize);
	QueueTask task = QueueDumpTaskCreate(tasks, tasksSize);
	QueuePutSafe(&dispatcher.dump, &task);
	return 0;
}

VOID DispatcherStartSortThread() {
	HANDLE sort = CreateThread(NULL, 0, 
		StartSortRoutine, NULL, 0, NULL);
	DispatcherAppendThread(sort);
}

PCHAR DispatcherReadLineFrom(FILE* file) {
	PCHAR text = NULL;
	SIZE_T textLen = 0;
	SIZE_T readLen = BUF_SIZE - 1;
	CHAR readBuf[BUF_SIZE] = { 0 };
	while (readLen == (BUF_SIZE - 1)) {
		fgets(readBuf, sizeof readBuf, file);
		readLen = strlen(readBuf);
		text = (text == NULL) ? (char*)malloc(readLen + 1) :
			(char*)realloc(text, textLen + readLen + 1);
		strcpy(text + textLen, readBuf);
		memset(readBuf, 0, sizeof readBuf);
		textLen += readLen;
	}
	text[textLen] = '\0';
	return text;
}

VOID DispatcherMakeSortTasks() {
	while (!feof(dispatcher.inFile)) {
		PCHAR data = DispatcherReadLineFrom(dispatcher.inFile);
		QueueTask task = QueueSortTaskCreate(data);
		QueuePutSafe(&dispatcher.sort, &task);
	}
}

VOID DispatcherMergeAndDump() {
	SIZE_T tasksSize = 0;
	PCHAR* tasks = (PCHAR*)malloc(
		dispatcher.sort->size * sizeof(PCHAR));
	while (!QueueEmptySafe(dispatcher.dump)) {
		QueueTask* task = QueuePopSafe(dispatcher.dump);
		if (task != NULL && task->kind == TASK_DUMP) {
			for (SIZE_T i = 0; i < task->size; i++) {
				tasks[tasksSize++] = task->lines[i];
			}
			QueueTaskRelease(*task);
		}
	}
	tasks = DispatcherSortLines(tasks, tasksSize);
	for (SIZE_T i = 0; i < tasksSize; i++) {
		fwrite(tasks[i], sizeof(CHAR), strlen(tasks[i]), dispatcher.outFile);
		free(tasks[i]);
	}
	free(tasks);
}

VOID DispatcherStart() {
	DispatcherMakeSortTasks();
	DispatcherThreadsRelease();
	for (LONG i = 0; i < dispatcher.threadsCount; i++) {
		DispatcherStartSortThread();
	}
	DispatcherWaitForAllThreads();
	DispatcherThreadsRelease();
	DispatcherMergeAndDump();
}

INT main(INT argc, CHAR** argv) {
	if (argc != 3) {
		PrintError("You need to filename and max thread count!");
	}
	else {
		DispatcherCreate(atol(argv[1]), argv[2]);
		DispatcherStart();
		DispatcherRelease();
	}
	return 0;
}