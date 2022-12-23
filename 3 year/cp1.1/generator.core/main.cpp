#include <vector>
#include <iostream>
#include <windows.h>
#include <commctrl.h>
#include <intrin.h>	

#define WIN_X			100
#define WIN_Y			100
#define WIN_WIDTH		800
#define WIN_HEIGHT		600
#define WIN_CLASS_NAME "generator.class"

#define CYCLIC_TRAVERSAL_ARRAY_SIZE 1024

static CONST LONG size = 40;

static HWND tbModHwnd = NULL;
static HWND tbSizeHwnd = NULL;

static HWND btnGoHwnd = NULL;
static HWND btn—learHwnd = NULL;
static HWND btnSetModHwnd = NULL;
static HWND btnShuffleHwnd = NULL;

static std::vector<SSIZE_T> generatedNumbers;
static std::vector<SSIZE_T> traversalNumbers;

static CONST SSIZE_T tPad = 20;
static CONST SSIZE_T lPad = 30;

static RECT clientRect = { 0 };

static LONG scrollColOffset = 0;

static SSIZE_T seed = 0;
static SSIZE_T next = 0;

static SSIZE_T mod = 0;
static SSIZE_T index = 0;

VOID ErrorShow(const CHAR* message) {
	MessageBoxA(NULL, message, "error", MB_OK | MB_ICONERROR);
}

VOID GenerateNumbers(SSIZE_T sequence, SSIZE_T times) {
	while (sequence > 0) {
		SSIZE_T number = 0;
		for (SSIZE_T i = 0; i < times; i++) {
			number += traversalNumbers[index];
			index = traversalNumbers[index];
		}
		generatedNumbers.push_back(number % mod);
		sequence--;
	}
}

VOID ShuffleTraversalNumbers(SSIZE_T size) {
	traversalNumbers.clear();
	for (SSIZE_T i = 0; i < size; i++) {
		traversalNumbers.push_back((i + 1) % size);
	}
	for (SSIZE_T i = 0; i < size; i++) {
		SSIZE_T index = rand() % size;
		SSIZE_T temp = traversalNumbers[i];
		traversalNumbers[i] = traversalNumbers[index];
		traversalNumbers[index] = temp;
	}
}

LRESULT WinDrawColNumber(HDC hdc, LONG col) {
	CHAR buffer[32];
	RECT colNumRect;
	colNumRect.top = col * size + tPad;
	colNumRect.bottom = colNumRect.top + size;
	colNumRect.left = 0;
	colNumRect.right = colNumRect.left + lPad;
	UINT format = DT_RIGHT | DT_SINGLELINE | DT_VCENTER;
	sprintf_s(buffer, "%ld", col + scrollColOffset + 1);
	DrawTextA(hdc, buffer, -1, &colNumRect, format);
	return (LRESULT)0;
}

LRESULT WinDrawGridWithArray(HDC hdc) {
	CHAR buffer[128];
	RECT numRect = { 0 };
	LONG rows = (clientRect.right - lPad) / size;
	LONG cols = (clientRect.bottom - tPad) / size;
	SIZE_T arrayIndex = scrollColOffset * rows;
	for (LONG col = 0; col < cols; col++) {
		numRect.top = col * size + tPad;
		numRect.bottom = numRect.top + size;
		for (LONG row = 0; row < rows; row++) {
			numRect.left = row * size + lPad;
			numRect.right = numRect.left + size;
			Rectangle(hdc, numRect.left, numRect.top,
				numRect.right, numRect.bottom);
			if (arrayIndex < generatedNumbers.size()) {
				sprintf_s(buffer, "%lld", generatedNumbers[arrayIndex++]);
				UINT format = DT_CENTER | DT_SINGLELINE | DT_VCENTER;
				DrawTextA(hdc, buffer, -1, &numRect, format);
			}
			WinDrawColNumber(hdc, col);
		}
	}
	return (LRESULT)0;
}

LRESULT WinDrawBackGround(HWND hWnd, HDC hdc) {
	HBRUSH hBrush = CreateSolidBrush(RGB(255, 255, 255));
	FillRect(hdc, &clientRect, hBrush);
	DeleteObject(hBrush);
	return (LRESULT)0;
}

LRESULT WinRedraw(HWND hWnd) {
	HDC hdc = GetDC(hWnd);
	WinDrawBackGround(hWnd, hdc);
	WinDrawGridWithArray(hdc);
	ReleaseDC(hWnd, hdc);
	InvalidateRect(hWnd, NULL, false);
	return (LRESULT)0;
}

LRESULT WinResize(HWND hWnd) {
	if (GetClientRect(hWnd, &clientRect)) {
		return WinRedraw(hWnd);
	}
	return (LRESULT)0;
}

LRESULT WinScroll(HWND hWnd, WPARAM wParam) {
	LONG scrolledBy = (((SHORT)HIWORD(wParam)) / WHEEL_DELTA);
	scrollColOffset = max(0, scrollColOffset - scrolledBy);
	return WinRedraw(hWnd);
}

LRESULT WinBtnProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	CHAR sizeBuffer[128];
	if (HIWORD(wParam) == BN_CLICKED) {
		if ((HWND)lParam == btnSetModHwnd) {
			GetWindowTextA(tbModHwnd, sizeBuffer, sizeof sizeBuffer);
			if (strcmp(sizeBuffer, "") == 0) {
				return ErrorShow("Specified MOD cannot be empty!"), (LRESULT)1;
			}
			for (SIZE_T i = 0; i < strlen(sizeBuffer); i++) {
				if (!isdigit(sizeBuffer[i])) {
					return ErrorShow("Specified MOD is not acceptable!"), (LRESULT)1;
				}
			}
			mod = atol(sizeBuffer);
		}
		if ((HWND)lParam == btn—learHwnd) {
			generatedNumbers.clear();
		}
		if ((HWND)lParam == btnGoHwnd) {
			GetWindowTextA(tbSizeHwnd, sizeBuffer, sizeof sizeBuffer);
			if (strcmp(sizeBuffer, "") == 0) {
				return ErrorShow("Specified size cannot be empty!"), (LRESULT)1;
			}
			for (SIZE_T i = 0; i < strlen(sizeBuffer); i++) {
				if (!isdigit(sizeBuffer[i])) {
					return ErrorShow("Specified size is not acceptable!"), (LRESULT)1;
				}
			}
			LONG size = atol(sizeBuffer);
			GenerateNumbers(size, rand() % CYCLIC_TRAVERSAL_ARRAY_SIZE);
		}
		if ((HWND)lParam == btnShuffleHwnd) {
			ShuffleTraversalNumbers(CYCLIC_TRAVERSAL_ARRAY_SIZE);
		}
		WinRedraw(hWnd);
	}
	return (LRESULT)(0);
}

LRESULT CALLBACK WinProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	switch (message) {
	case WM_COMMAND:
		if ((HWND)lParam == btnGoHwnd ||
			(HWND)lParam == btn—learHwnd ||
			(HWND)lParam == btnSetModHwnd ||
			(HWND)lParam == btnShuffleHwnd) {
			WinBtnProc(hWnd, message, wParam, lParam);
		}
		return (LRESULT)(0);
	case WM_DESTROY:
		return PostQuitMessage(EXIT_SUCCESS), (LRESULT)(0);
	case WM_SIZE:
		return WinResize(hWnd);
	case WM_CREATE:
		return WinRedraw(hWnd);
	case WM_MOUSEWHEEL:
		return WinScroll(hWnd, wParam);
	}
	return DefWindowProcA(hWnd, message, wParam, lParam);
}

VOID CreateGoButton(HWND hWnd) {
	CONST LONG width = 150;
	CONST LONG height = tPad;
	DWORD dwStyle = WS_VISIBLE | WS_BORDER | WS_CHILD;
	btnGoHwnd = CreateWindowExA(0, WC_BUTTONA, "Go!", dwStyle,
		150, 0, width, height, hWnd, NULL, NULL, NULL);
}

VOID CreateClearButton(HWND hWnd) {
	CONST LONG width = 150;
	CONST LONG height = tPad;
	DWORD dwStyle = WS_VISIBLE | WS_BORDER | WS_CHILD;
	btn—learHwnd = CreateWindowExA(0, WC_BUTTONA, "Clear", dwStyle,
		320, 0, width, height, hWnd, NULL, NULL, NULL);
}

VOID CreateShuffleButton(HWND hWnd) {
	CONST LONG width = 75;
	CONST LONG height = tPad;
	DWORD dwStyle = WS_VISIBLE | WS_BORDER | WS_CHILD;
	btnShuffleHwnd = CreateWindowExA(0, WC_BUTTONA, "Shuffle", dwStyle,
		470, 0, width, height, hWnd, NULL, NULL, NULL);
}

VOID CreateSetModuleButton(HWND hWnd) {
	CONST LONG width = 75;
	CONST LONG height = tPad;
	DWORD dwStyle = WS_VISIBLE | WS_BORDER | WS_CHILD;
	btnSetModHwnd = CreateWindowExA(0, WC_BUTTONA, "Set MOD", dwStyle,
		630, 0, width, height, hWnd, NULL, NULL, NULL);
}

VOID CreateArraySizeTextBox(HWND hWnd) {
	CONST LONG width = 150;
	CONST LONG height = tPad;
	DWORD dwStyle = WS_VISIBLE | WS_BORDER | WS_CHILD;
	tbSizeHwnd = CreateWindowExA(0, WC_EDITA, "5", dwStyle,
		0, 0, width, height, hWnd, NULL, NULL, NULL);
}

VOID CreateModuleValueTextBox(HWND hWnd) {
	mod = 100;
	CONST LONG width = 75;
	CONST LONG height = tPad;
	DWORD dwStyle = WS_VISIBLE | WS_BORDER | WS_CHILD;
	tbModHwnd = CreateWindowExA(0, WC_EDITA, "100", dwStyle,
		555, 0, width, height, hWnd, NULL, NULL, NULL);
}

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PSTR nCmdLine, INT nCmdShow) {
	MSG message = { 0 };
	WNDCLASSA wc = { 0 };
	seed = (SSIZE_T)time(NULL);
	wc.lpfnWndProc = WinProc;
	wc.lpszClassName = WIN_CLASS_NAME;
	if (RegisterClassA(&wc) == 0) {
		ExitProcess(EXIT_FAILURE);
	}
	DWORD dwStyle = WS_OVERLAPPEDWINDOW;
	clientRect.right = WIN_WIDTH;
	clientRect.bottom = WIN_HEIGHT;
	clientRect.top = clientRect.left = 0;
	RECT windowRect = clientRect;
	if (!AdjustWindowRect(&windowRect, dwStyle, false)) {
		ExitProcess(EXIT_FAILURE);
	}
	LONG width = windowRect.right - windowRect.left;
	LONG height = windowRect.bottom - windowRect.top;
	HWND hWnd = CreateWindowExA(0, WIN_CLASS_NAME, WIN_CLASS_NAME, dwStyle,
		WIN_X, WIN_Y, width, height, NULL, NULL, NULL, NULL);
	index = rand() % CYCLIC_TRAVERSAL_ARRAY_SIZE;
	ShuffleTraversalNumbers(CYCLIC_TRAVERSAL_ARRAY_SIZE);
	if (hWnd == NULL) {
		ExitProcess(EXIT_FAILURE);
	}
	CreateGoButton(hWnd);
	CreateClearButton(hWnd);
	CreateShuffleButton(hWnd);
	CreateSetModuleButton(hWnd);
	CreateArraySizeTextBox(hWnd);
	CreateModuleValueTextBox(hWnd);
	ShowWindow(hWnd, nCmdShow);
	while (GetMessageA(&message, NULL, 0, 0)) {
		TranslateMessage(&message);
		DispatchMessageA(&message);
	}
	return EXIT_SUCCESS;
}