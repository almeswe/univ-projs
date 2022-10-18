#ifndef _VMDLL_DEFINES_H
#define _VMDLL_DEFINES_H
#define WIN32_LEAN_AND_MEAN

#undef UNICODE

#define vmdll_import __declspec(dllimport)
#define vmdll_export __declspec(dllexport)

#include <stdlib.h>
#include <stdbool.h>

#include <aclapi.h>
#include <windows.h>

typedef enum vmdll_error {
	VMDLL_SUCCESS,
	VMDLL_NEEDS_INIT,
	VMDLL_OPROC_ERROR,
	VMDLL_VQUERY_ERROR,
	VMDLL_MALLOC_ERROR,
	VMDLL_UNHANDLED
} vmdll_error;

typedef struct vmdll_stats {
	DWORD pid;
	HANDLE handle;
	SYSTEM_INFO info;
	bool initialized;
} vmdll_stats;

#endif 