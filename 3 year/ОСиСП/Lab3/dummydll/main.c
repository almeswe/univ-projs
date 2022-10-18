#include <stdio.h>
#include <windows.h>

BOOL WINAPI DllMain(HINSTANCE hInstDll, DWORD fdwReason, LPVOID lpvReserved) {
    CHAR path[1024] = { 0 };
    CHAR format[1024] = { 0 };
    switch (fdwReason) {
        case DLL_PROCESS_ATTACH:
            printf("%s\n", "attached");
            GetModuleFileNameA(NULL, path, sizeof path);
            sprintf_s(format, sizeof format, "%s loaded! Pid %d! Reason: %d",
                path, GetCurrentProcessId(), fdwReason);
            MessageBoxA(NULL, format, "Caption", MB_OK);
            break;
        case DLL_THREAD_ATTACH:
        case DLL_THREAD_DETACH:
        case DLL_PROCESS_DETACH:
            break;
    }
    return TRUE;
}