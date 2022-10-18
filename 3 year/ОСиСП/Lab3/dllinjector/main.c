#include <stdio.h>
#include <string.h>
#include <stdbool.h>
#include <windows.h>

#define kernel				"kernel32.dll"
#define kernelLoadLibrary	"LoadLibraryA"

#define getch() (void)getc(stdin)

enum bufsizes {
	PIDBUF_SIZE = 128,
	DLLBUF_SIZE	= 1024
};

typedef struct injinfo {
	HANDLE hThread;
	HANDLE hProcess;
	HANDLE hProcModule;
	DWORD threadId;
	DWORD processId;
	DWORD paramSize;
	PCHAR paramValue;
	LPVOID procAddr;
	LPVOID paramAddr;
} injinfo;

static injinfo info = { 0 };

VOID error(const PCHAR message) {
	fprintf(stderr, "error occured: %s, error code: %d\n",
		message, GetLastError());
	getch();
	ExitProcess(1);
}

VOID validatePid(const PCHAR pid) {
	size_t size = strlen(pid);
	for (size_t i = 0; i < size; i++) {
		if (pid[i] < '0' || pid[i] > '9') {
			error("specified pid is incorrect");
		}
	}
}

VOID validatePath(const PCHAR path) {
	OFSTRUCT fstruct = { 0 };
	HFILE hFile = OpenFile(path, &fstruct, OF_READ);
	if (hFile == HFILE_ERROR) {
		error("Cannot open specified .dll module");
	}
	CloseHandle(hFile);
}

VOID initInfo(const PCHAR dllBuf, const PCHAR pidBuf) {
	validatePid(pidBuf);
	validatePath(dllBuf);
	info.processId = atoi(pidBuf);
	info.paramSize = strlen(dllBuf) + 1;
	info.paramValue = (PCHAR)malloc(info.paramSize);
	if (info.paramValue == NULL) {
		error("malloc() failed");
	}
	sprintf_s(info.paramValue, info.paramSize, "%s", dllBuf);
	info.hProcModule = GetModuleHandleA(kernel);
	if (info.hProcModule == NULL) {
		error("Failed to obtain info for kernel32.dll");
	}
	info.procAddr = GetProcAddress(info.hProcModule, kernelLoadLibrary);
	if (info.procAddr == NULL) {
		error("GetProcAddress() failed for LoadLibraryA");
	}
}

VOID finiInfo() {
	free(info.paramValue);
	CloseHandle(info.hThread);
	CloseHandle(info.hProcess);
	CloseHandle(info.hProcModule);
	VirtualFreeEx(info.hProcess, info.paramAddr, 0, MEM_RELEASE);
}

VOID interact() {
	CHAR pidBuf[PIDBUF_SIZE] = { 0 };
	CHAR dllBuf[DLLBUF_SIZE] = { 0 };
	printf("path to dll: "); gets(dllBuf);
	printf("id of target process: "); gets(pidBuf);
	initInfo(dllBuf, pidBuf);
}

INT main(INT argc, CHAR** argv) {
	interact();
	DWORD hProcAccess = PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | 
		PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ;
	info.hProcess = OpenProcess(hProcAccess, false, info.processId);
	if (info.hProcess == 0) {
		error("OpenProcess() failed");
	}
	info.paramAddr = VirtualAllocEx(info.hProcess, NULL, info.paramSize, 
		MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);
	if (info.paramAddr == NULL) {
		error("VirtualAllocEx() failed");
	}
	size_t written = 0;
	printf("allocated space at: %p\n", info.paramAddr);
	if (!WriteProcessMemory(info.hProcess, info.paramAddr, info.paramValue, info.paramSize, &written)) {
		error("WriteProcessMemory() failed");
	}
	printf("written %lu bytes to process %ld\n", written, info.processId);
	info.hThread = CreateRemoteThread(info.hProcess, NULL, 0, info.procAddr, info.paramAddr, 0, &info.threadId);
	if (info.hThread == NULL) {
		error("CreateRemoteThread() failed");
	}
	WaitForSingleObject(info.hThread, INFINITE);
	finiInfo();
	return getch(), 1;
}