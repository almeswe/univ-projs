#include "_vmdll.h"

#define vmdll_report_error(err) vmdll_glob_error = err; return

static vmdll_stats vmdll_glob = {0};
static vmdll_error vmdll_glob_error = VMDLL_SUCCESS;

vmdll_export void vmdll_init() {
	GetSystemInfo(&vmdll_glob.info);
	DWORD access = PROCESS_ALL_ACCESS;
	vmdll_glob.pid = GetCurrentProcessId();
	vmdll_glob.handle = OpenProcess(access, false, vmdll_glob.pid);
	if (vmdll_glob.handle == NULL) {
		vmdll_report_error(VMDLL_OPROC_ERROR);
	}
	vmdll_glob.initialized = true;
}

vmdll_export void vmdll_fini() {
	if (vmdll_glob.handle != NULL) {
		CloseHandle(vmdll_glob.handle);
	}
}

vmdll_export vmdll_error vmdll_geterr() {
	return vmdll_glob_error;
}

vmdll_export void vmdll_replace(const char* src, const char* pholder) {
	char* curaddr = NULL;
	size_t srclen = strlen(src);
	if (!vmdll_glob.initialized) {
		vmdll_report_error(VMDLL_NEEDS_INIT);
	}
	while (true) {
		MEMORY_BASIC_INFORMATION mbase = {0};
		if (VirtualQueryEx(vmdll_glob.handle, curaddr, &mbase, sizeof mbase) != sizeof mbase) {
			if (GetLastError() == ERROR_INVALID_PARAMETER) {
				return;
			}
			vmdll_report_error(VMDLL_VQUERY_ERROR);
		}
		curaddr += mbase.RegionSize;
		if (mbase.Protect == PAGE_READWRITE /* | PAGE_READONLY */ && mbase.State == MEM_COMMIT) {
			char* buffer = (char*)malloc(mbase.RegionSize);
			if (buffer == NULL) {
				vmdll_report_error(VMDLL_MALLOC_ERROR);
			}
			size_t bytes = 0;
			if (ReadProcessMemory(vmdll_glob.handle, mbase.BaseAddress, buffer, mbase.RegionSize, &bytes) != 0) {
				for (size_t i = 0; i < bytes-srclen; i++) {
					const char* offset = (char*)(buffer + i);
 					if (memcmp(offset, src, srclen) == 0) {
						memcpy((char*)mbase.BaseAddress+i, pholder, srclen);
					}
				}
			}
			free(buffer);
		}
	}
}