#undef UNICODE

#define WIN32_LEAN_AND_MEAN
#define _CRT_SECURE_NO_WARNINGS
#define _MARCH_TEST_ANALYZER_CORE_ENABLE_CAMEL_CASE

#include <stdio.h>
#include <stdlib.h>
#include <windows.h>

#include "uidefs.h"
#include "analyzer.h"

VOID ErrorPrintWin32() {
	CHAR buf[256] = { 0 };
	DWORD error = GetLastError();
	if (error != 0) {
		DWORD dwFlags = FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS;
		DWORD dwLangId = MAKELANGID(LANG_ENGLISH, SUBLANG_DEFAULT);
		if (FormatMessageA(dwFlags, NULL, error, dwLangId, buf, sizeof buf, NULL) != 0) {
			fprintf(stderr, "%s\n", buf);
		}
	}
}

VOID ErrorPanicWin32() {
	ErrorPrintWin32();
	ExitProcess(EXIT_FAILURE);
}

LRESULT CALLBACK WinProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	return DefWindowProcA(hWnd, message, wParam, lParam);
}

INT WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PWSTR* pCmdLine, int nCmdShow) {
	//MarchTestAnalyze(INPUT_FILE);
	//const RamBitCell* cell = MarchTestGetAnalyzedCell(123);
	//MarchTestFinalize();
	WNDCLASS wc = { 0 };
	wc.lpfnWndProc = WinProc;
	wc.hInstance = hInstance;
	wc.lpszClassName = WIN_CLASS_NAME;
	ATOM class = RegisterClassA(&wc);
	if (class == 0) {
		ErrorPanicWin32();
	}
	DWORD dwStyle = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU;
	HWND hWnd = CreateWindowExA(0, WIN_CLASS_NAME, WIN_NAME, dwStyle, WIN_X, WIN_Y,
		WIN_WIDTH, WIN_HEIGHT, NULL, NULL, NULL, NULL);
	if (hWnd == NULL) {
		ErrorPanicWin32();
	}
	MSG message = { 0 };
	ShowWindow(hWnd, nCmdShow);
	while (GetMessageA(&message, hWnd, 0, 0)) {
		TranslateMessage(&message);
		DispatchMessageA(&message);
	}
	return EXIT_SUCCESS;
}