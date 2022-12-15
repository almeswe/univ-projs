#undef UNICODE

#define WIN32_LEAN_AND_MEAN
#define _MARCH_TEST_ANALYZER_CORE_ENABLE_CAMEL_CASE

#include <stdio.h>
#include <stdlib.h>
#include <windows.h>
#include <commctrl.h>

#include "uidefs.h"
#include "analyzer.h"

static SSIZE_T index = 0;
static HWND hThis = NULL;
static HWND hBitCb = NULL;

static RECT thisRect = { 0 };
static RamBitCell** traverseData = NULL;
static RamBitCell** traverseTable[TRAVERSE_DEPTH_SIZE] = { 0 };

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

VOID ErrorShow(const PCHAR message) {
	MessageBoxA(NULL, message, "error", MB_OK | MB_ICONWARNING);
}

VOID TraverseNode(const RamBitCell* node, int64_t depth) {
	SSIZE_T size = RamBitCellsSize(traverseData);
	for (SSIZE_T i = 0; i < size; i++) {
		if (node == traverseData[i]) {
			return;
		}
	}
	RamBitCellsAdd(&(traverseData), node);
	RamBitCellsAdd(&(traverseTable[depth]), node);
	size = RamBitCellsSize(node->victims);
	for (SSIZE_T i = 0; i < size; i++) {
		TraverseNode(node->victims[i], depth + 1);
	}
}

SSIZE_T TraverseDataDepth() {
	SSIZE_T size = 0;
	for (SSIZE_T i = 0; i < TRAVERSE_DEPTH_SIZE; i++) {
		size += (traverseTable[i] != NULL);
	}
	return size;
}

VOID TraverseDataFree() {
	for (SSIZE_T i = 0; i < TRAVERSE_DEPTH_SIZE; i++) {
		RamBitCellsFree(traverseTable[i]);
		traverseTable[i] = NULL;
	}
	RamBitCellsFree(traverseData);
	traverseData = NULL;
}

VOID DrawNode(HDC hdc, RamBitCell* cell, PRECT nodeRect) {
	CHAR strbuf[BUF_SIZE] = { 0 };
	Ellipse(hdc, nodeRect->left, nodeRect->top, 
		nodeRect->right, nodeRect->bottom);
	if (cell->fault != RAM_CELL_FAULT_NONE) {
		sprintf_s(strbuf, BUF_SIZE, "%d %s", cell->address,
			ram_fault_type_str[cell->fault]);
	}
	else {
		sprintf_s(strbuf, BUF_SIZE, "%d", cell->address);
	}
	UINT textFormat = DT_CENTER | DT_SINGLELINE | DT_VCENTER;
	DrawTextA(hdc, strbuf, -1, nodeRect, textFormat);
}

VOID DrawDependencyArrow(HDC hdc, SSIZE_T node, PRECT nodeRect, SSIZE_T depth) {
	if (depth > 0) {
		SSIZE_T pNodes = RamBitCellsSize(traverseTable[depth - 1]);
		SSIZE_T pRowWidth = thisRect.right / pNodes;
		SSIZE_T pRowHeight = thisRect.bottom / TraverseDataDepth();
		for (SSIZE_T pNode = 0; pNode < pNodes; pNode++) {
			RECT pRect = {
				.top    = (LONG)((depth - 1) * pRowHeight + pRowHeight / 2 - NODE_RECT_SIZE_HALF),
				.bottom = (LONG)((depth - 1) * pRowHeight + pRowHeight / 2 + NODE_RECT_SIZE_HALF),
				.left  = (LONG)((pNode * pRowWidth) + pRowWidth / 2 - NODE_RECT_SIZE_HALF),
				.right = (LONG)((pNode * pRowWidth) + pRowWidth / 2 + NODE_RECT_SIZE_HALF)
			};
			SSIZE_T pNodeChilds = RamBitCellsSize(traverseTable[depth - 1][pNode]->victims);
			for (SSIZE_T pNodeChild = 0; pNodeChild < pNodeChilds; pNodeChild++) {
				if (traverseTable[depth - 1][pNode]->victims[pNodeChild] == traverseTable[depth][node]) {
					MoveToEx(hdc, pRect.left + NODE_RECT_SIZE_HALF,
						pRect.top + NODE_RECT_SIZE_HALF, NULL);
					LineTo(hdc, nodeRect->left + NODE_RECT_SIZE_HALF,
						nodeRect->top + NODE_RECT_SIZE_HALF);
					DrawNode(hdc, traverseTable[depth - 1][pNode], &pRect);
				}
			}
		}
	}
}

VOID DrawTree(HWND hWnd, HDC hdc, SSIZE_T rootIndex) {
	const RamBitCell* cell = 
		MarchTestGetAnalyzedCell(rootIndex);
	if (cell == NULL) {
		ErrorShow("Cannot open the analyze result for this bit!");
	}
	else {
		TraverseDataFree();
		TraverseNode(cell, 0);
		RECT rowRect = { 0 };
		SSIZE_T depth = TraverseDataDepth();
		SSIZE_T rowHeight = thisRect.bottom / depth;
		for (SSIZE_T row = 0; row < depth; row++) {
			rowRect.top = (LONG)(row * rowHeight);
			rowRect.bottom = (LONG)(rowRect.top + rowHeight);
			SSIZE_T nodes = RamBitCellsSize(traverseTable[row]);
			SSIZE_T rowWidth = thisRect.right;
			if (nodes != 0) {
				rowWidth /= nodes;
			}
			for (SSIZE_T node = 0; node < nodes; node++) {
				CHAR strbuf[BUF_SIZE] = { 0 };
				rowRect.left = (LONG)(node * rowWidth);
				rowRect.right = (LONG)(rowRect.left + rowWidth);
				RECT nodeRect = {
					.top    = (LONG)(rowRect.top + rowHeight / 2 - NODE_RECT_SIZE_HALF),
					.bottom = (LONG)(rowRect.top + rowHeight / 2 + NODE_RECT_SIZE_HALF),
					.left   = (LONG)(rowRect.left + rowWidth / 2 - NODE_RECT_SIZE_HALF),
					.right  = (LONG)(rowRect.left + rowWidth / 2 + NODE_RECT_SIZE_HALF)
				};
				DrawDependencyArrow(hdc, node, &nodeRect, row);
				DrawNode(hdc, traverseTable[row][node], &nodeRect);
			}
		}
	}
}

LRESULT WinRedraw(HWND hWnd) {
	HDC hdc = GetDC(hWnd);
	HBRUSH hBrush = CreateSolidBrush(RGB(255, 255, 255));
	FillRect(hdc, &thisRect, hBrush);
	DrawTree(hWnd, hdc, index);
	ReleaseDC(hWnd, hdc);
	DeleteObject(hBrush);
	InvalidateRect(hWnd, &thisRect, true);
	return (LRESULT)0;
}

LRESULT WinResize(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	GetClientRect(hWnd, &thisRect);
	SetWindowPos(hBitCb, HWND_TOP, 0, 0, thisRect.right,
		BIT_CB_HEIGHT, SWP_SHOWWINDOW);
	return (LRESULT)0;
}

LRESULT WinCbProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	if (HIWORD(wParam) == CBN_SELCHANGE) {
		if ((HWND)lParam == hBitCb) {
			LRESULT itemIndex = SendMessageA((HWND)lParam,
				CB_GETCURSEL, (WPARAM)0x0, (LPARAM)0x0);
			index = itemIndex;
			return WinRedraw(hWnd);
		}
	}
	return (LRESULT)0;
}

LRESULT CALLBACK WinProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	switch (message) {
		case WM_COMMAND:
			return WinCbProc(hWnd, message, wParam, lParam);
		case WM_DESTROY:
			PostQuitMessage(0);
			return (LRESULT)0;
		case WM_SIZE:
			WinResize(hWnd, message, wParam, lParam);
		case WM_CREATE:
			return WinRedraw(hWnd);
	}
	return DefWindowProcA(hWnd, message, wParam, lParam);
}

HWND CreateBitComboBox(HWND hWnd) {
	DWORD dwStyle = CBS_DROPDOWNLIST | CBS_HASSTRINGS |
		WS_CHILD | WS_OVERLAPPED | WS_VISIBLE | WS_VSCROLL;
	HWND cbhWnd = CreateWindowExA(0, WC_COMBOBOX, NULL, dwStyle,
		0, 0, BIT_CB_WIDTH, BIT_CB_HEIGHT, hThis, NULL, NULL, NULL);
	for (SSIZE_T i = 0; i < RAM_SIZE; i += 1) {
		CHAR strbuf[BUF_SIZE] = { 0 };
		const RamBitCell* cell = MarchTestGetAnalyzedCell(i);
		sprintf_s(strbuf, BUF_SIZE, "%lld (%lld)", i, RamBitCellsSize(cell->victims));
		SendMessageA(cbhWnd, (UINT)CB_ADDSTRING, (WPARAM)0, (LPARAM)strbuf);
	}
	SendMessageA(cbhWnd, CB_SETMINVISIBLE, (WPARAM)15, (LPARAM)0);
	SendMessageA(cbhWnd, CB_SETCURSEL, (WPARAM)0, (LPARAM)0);
	return cbhWnd;
}

HWND CreateMainWindow(PWNDCLASSA pWndClass) {
	ATOM class = RegisterClassA(pWndClass);
	if (class == 0) {
		ErrorPanicWin32();
	}
	DWORD dwStyle = WS_OVERLAPPEDWINDOW;
	thisRect = (RECT){ 0, 0, WIN_WIDTH, WIN_HEIGHT };
	RECT windowRect = thisRect;
	if (!AdjustWindowRect(&windowRect, dwStyle, false)) {
		ErrorPanicWin32();
	}
	HWND hWnd = CreateWindowExA(0, WIN_CLASS_NAME, WIN_NAME, dwStyle, WIN_X, WIN_Y,
		windowRect.right - windowRect.left, windowRect.bottom - windowRect.top, NULL, NULL, NULL, NULL);
	if (hWnd == NULL) {
		ErrorPanicWin32();
	}
	return hWnd;
}

INT WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, INT nCmdShow) {
	if (!MarchTestAnalyze(lpCmdLine)) {
		ErrorShow("Cannot analyze this file!");
		ExitProcess(EXIT_FAILURE);
	}
	MSG message = { 0 };
	WNDCLASSA wc = { 0 };
	wc.lpfnWndProc = WinProc;
	wc.lpszClassName = WIN_CLASS_NAME;
	hThis = CreateMainWindow(&wc);
	hBitCb = CreateBitComboBox(hThis);
	ShowWindow(hThis, nCmdShow);
	while (GetMessageA(&message, NULL, 0, 0)) {
		TranslateMessage(&message);
		DispatchMessageA(&message);
	}
	MarchTestFinalize();
	return EXIT_SUCCESS;
}