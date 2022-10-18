#include <stdio.h>
#include <windows.h>

#include "vmdll.h"

BOOL WINAPI DllMain(HINSTANCE hInstDll, DWORD fdwReason, LPVOID lpvReserved) {
    switch (fdwReason) {
        case DLL_PROCESS_ATTACH:
            vmdll_init();
            vmdll_replace("standart", "changed!");
            switch (vmdll_geterr()) {
            case VMDLL_SUCCESS:
                break;
            case VMDLL_MALLOC_ERROR:
                printf("%s\n", "malloc allocation failure!");
                break;
            case VMDLL_NEEDS_INIT:
            case VMDLL_OPROC_ERROR:
                printf("%s\n", "OpenProcess function failure!");
                break;
            }
            vmdll_fini();
            break;
        case DLL_THREAD_ATTACH:
        case DLL_THREAD_DETACH:
        case DLL_PROCESS_DETACH:
            break;
    }
    return TRUE;
}