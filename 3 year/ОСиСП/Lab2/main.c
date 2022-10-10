#undef UNICODE

#include <time.h>
#include <stdbool.h>

#include <windows.h>
#include <commctrl.h>

#include "sbuffer.h"

#define rectWidth(r)	(r.right-r.left)
#define rectHeight(r)	(r.bottom-r.top)

typedef struct _Font {
	LOGFONTA font;
	TEXTMETRICA metrics;
} Font;

typedef struct _WindowClientInfo {
	HPEN gridPen;
	HBRUSH backGroundBrush;
	
	COLORREF gridColor;
	COLORREF backGroundColor;
	
	RECT clientArea;
	HWND cbhWnd;

	HFONT hFont;
	Font* sysFonts;
} WindowClientInfo;

typedef struct _WindowMatrixInfo {
	LONG rows;
	LONG cols;
	TCHAR* text;
} WindowMatrixInfo;

typedef enum _WindowClientSizeScales {
	WIN_STARTPOS_X = 100,
	WIN_STARTPOS_Y = 100,
	WIN_WIDTH = 500,
	WIN_HEIGHT = 500,
	CB_WIDTH = WIN_WIDTH,
	CB_HEIGHT = 250
} WindowClientSizeScales;

static const TCHAR* windowName = TEXT("window name");
static const TCHAR* windowClassName = TEXT("WindowClassNameLab2");
static const TCHAR* comboBoxClassName = TEXT("WindowFontComboBox");

static WindowClientInfo winfo = { 0 };
static WindowMatrixInfo minfo = { 10, 1 };

VOID WinfoInitialize() {
	winfo.clientArea.top = 0;
	winfo.clientArea.left = 0;
	winfo.clientArea.right = WIN_WIDTH;
	winfo.clientArea.bottom = WIN_HEIGHT;

	winfo.backGroundColor = RGB(255, 255, 255);
	winfo.backGroundBrush = CreateSolidBrush(winfo.backGroundColor);

	winfo.gridColor = RGB(0, 0, 0);
	winfo.gridPen = CreatePen(PS_SOLID, 2, winfo.gridColor);
	winfo.sysFonts = NULL;
}

VOID WinfoRelease() {
	DeleteObject(winfo.gridPen);
	DeleteObject(winfo.backGroundBrush);
	sbuffer_free(minfo.text);
	sbuffer_free(winfo.sysFonts);
}

VOID WinRedrawBg(HWND hWnd, HDC hdc) {
	if (GetClientRect(hWnd, &winfo.clientArea)) {
		FillRect(hdc, &winfo.clientArea, winfo.backGroundBrush);
	}
}

VOID ChangeCbFont(HWND hWnd, LONG fontIndex) {
	LONG length = sbuffer_len(winfo.sysFonts);
	HDC hdc = GetDC(hWnd);
	fontIndex %= length;
	Font* font = &winfo.sysFonts[fontIndex];
	winfo.hFont = CreateFontIndirect(&(font->font));
	ReleaseDC(hWnd, hdc);
}

VOID CALLBACK LoadFontCallBack(const LOGFONTA* lf, const TEXTMETRICA* metrics, DWORD dWord, LPARAM lParam) {
	Font font = {
		.font = *lf,
		.metrics = *metrics
	};
	sbuffer_add(winfo.sysFonts, font);
}

VOID LoadFonts(HWND hWnd) {
	LOGFONT lf = { 0 };
	HDC hdc = GetDC(hWnd);
	EnumFontFamiliesEx(hdc, &lf, LoadFontCallBack, 0, 0);
	ReleaseDC(hWnd, hdc);
}

HWND CreateMainWindow() {
	RECT rect = winfo.clientArea;
	DWORD dwStyle = WS_OVERLAPPEDWINDOW;
	if (!AdjustWindowRect(&rect, dwStyle, false)) {
		exit(1);
	}
	HWND hWnd = CreateWindowEx(0, windowClassName, windowName, dwStyle, WIN_STARTPOS_X, WIN_STARTPOS_Y,
		rectWidth(rect), rectHeight(rect), NULL, NULL, NULL, NULL);
	return hWnd;
}

HWND CreateFontCb(HWND hWnd) {
	LoadFonts(hWnd);
	DWORD dwStyle = CBS_DROPDOWNLIST | CBS_HASSTRINGS |
		WS_CHILD | WS_OVERLAPPED | WS_VISIBLE | WS_VSCROLL;
	HWND cbhWnd = CreateWindowEx(0, WC_COMBOBOX, NULL, dwStyle, 
		0, 0, CB_WIDTH, CB_HEIGHT, hWnd, NULL, NULL, NULL);
	for (LONG i = 0; i < sbuffer_len(winfo.sysFonts); i += 1) {
		TCHAR* faceName = winfo.sysFonts[i].font.lfFaceName;
		SendMessage(cbhWnd, (UINT)CB_ADDSTRING, (WPARAM)0, (LPARAM)faceName);
	}
	LONG index = __rdtsc() % sbuffer_len(winfo.sysFonts);
	SendMessage(cbhWnd, CB_SETMINVISIBLE, (WPARAM)0x14, (LPARAM)0x0);
	SendMessage(cbhWnd, CB_SETCURSEL, (WPARAM)index, (LPARAM)0x0);
	ChangeCbFont(cbhWnd, index);
	return cbhWnd;
}

VOID WinDrawGrid(HDC hdc) {
	TCHAR buffer[16];
	memset(buffer, 0, sizeof buffer);
	RECT cbRect = { 0 };
	RECT rdRect = winfo.clientArea;
	rdRect.right -= 50;
	rdRect.bottom -= 50;
	LONG rowWidth = (LONG)(rdRect.right / minfo.rows);
	LONG colWidth = (LONG)(rdRect.bottom / minfo.cols);
	HPEN newPen = winfo.gridPen;
	HPEN oldPen = (HPEN)SelectObject(hdc, newPen);
	if (GetWindowRect(winfo.cbhWnd, &cbRect)) {
		LONG cbHeight = cbRect.bottom - cbRect.top;
		rdRect.top = cbHeight;
		rdRect.bottom += cbHeight;
		RECT textRect = { 0, cbHeight, rowWidth, colWidth+cbHeight };
		for (LONG i = 0, j = -1; i < sbuffer_len(minfo.text); i++) {
			if (i % minfo.rows == 0) {
				j += 1;
			}
			buffer[0] = minfo.text[i];
			textRect.left = rowWidth * (i % minfo.rows);
			textRect.right = textRect.left + rowWidth;
			textRect.top = cbHeight + colWidth * j;
			textRect.bottom = textRect.top + colWidth;
			DrawText(hdc, buffer, -1, &textRect, DT_CENTER | DT_SINGLELINE | DT_VCENTER);
		}
		for (LONG i = 0; i <= minfo.rows; i++) {
			MoveToEx(hdc, rowWidth * i, cbHeight, NULL);
			LineTo(hdc, rowWidth * i, rdRect.bottom);
		}
		for (LONG i = 0; i <= minfo.cols; i++) {
			MoveToEx(hdc, 0, i * colWidth + cbHeight, NULL);
			LineTo(hdc, rdRect.right, i * colWidth + cbHeight);
		}
	}
	SelectObject(hdc, oldPen);
	DeleteObject(newPen);
}

LRESULT WinRedraw(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	PAINTSTRUCT ps = { 0 };
	HDC hdc = GetDC(hWnd);
	SetBkMode(hdc, TRANSPARENT);
	WinRedrawBg(hWnd, hdc);
	SelectObject(hdc, winfo.hFont);
	WinDrawGrid(hdc);
	ReleaseDC(hWnd, hdc);
	InvalidateRect(NULL, NULL, true);
	return (LRESULT)0;
}

LRESULT WinResize(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	SetWindowPos(winfo.cbhWnd, HWND_TOP, 0, 0, 
		winfo.clientArea.right, CB_HEIGHT, SWP_SHOWWINDOW);
	return (LRESULT)0;
}

LRESULT WinCbProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	if (HIWORD(wParam) == CBN_SELCHANGE) {
		if (lParam == winfo.cbhWnd) {
			LRESULT itemIndex = SendMessage((HWND)lParam,
				CB_GETCURSEL, (WPARAM)0x0, (LPARAM)0x0);
			ChangeCbFont((HWND)lParam, (LONG)itemIndex);
			WinRedraw(hWnd, message, wParam, lParam);
			SetFocus(hWnd);
		}
	}
	return (LRESULT)0;
}

LRESULT CALLBACK WinProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	switch (message) {
		case WM_COMMAND:
			return WinCbProc(hWnd, message, wParam, lParam);
		case WM_CHAR:
			sbuffer_add(minfo.text, (TCHAR)wParam);
			if (sbuffer_len(minfo.text) % minfo.rows == 0) {
				minfo.cols += 1;
			}
			return WinRedraw(hWnd, message, wParam, lParam);
		case WM_DESTROY:
			PostQuitMessage(0);
			return (LRESULT)0;
		case WM_SIZE:
			WinResize(hWnd, message, wParam, lParam);
		case WM_CREATE:
			return WinRedraw(hWnd, message, wParam, lParam);
	}
	return DefWindowProc(hWnd, message, wParam, lParam);
}

INT WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PWSTR pCmdLine, INT nCmdShow) {
	WNDCLASSEX wnd = { 0 };
	WinfoInitialize();
	wnd.lpfnWndProc = WinProc;
	wnd.lpszClassName = windowClassName;
	if (!RegisterClass(&wnd)) {
		exit(1);
	}
	MSG message;
	HWND hWnd = CreateMainWindow();
	winfo.cbhWnd = CreateFontCb(hWnd);
	ShowWindow(hWnd, nCmdShow);
	while (GetMessage(&message, NULL, 0, 0)) {
		TranslateMessage(&message);
		DispatchMessage(&message);
	}
	WinfoRelease();
	return 0;
}