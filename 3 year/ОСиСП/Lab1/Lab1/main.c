#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

#undef UNICODE
#include <windows.h>

#include "sprite.h"
#include "winscales.h"

Sprite* sprite = NULL;
HBRUSH backSolidBrush = { 0 };

VOID WINAPI WinHandleMouseInput(UINT message, WPARAM wParam, LPARAM lParam) {
	SpriteDirection svec;
	WORD hWord = HIWORD(wParam);
	WORD lWord = LOWORD(wParam);
	if (lWord == MK_SHIFT) {
		svec = hWord == WHEEL_DELTA ?
			SVEC_RIGHT : SVEC_LEFT;
	}
	else {
		svec = hWord == WHEEL_DELTA ?
			SVEC_TOP : SVEC_BOTTOM;
	}
	sprite_move(sprite, svec);
}

VOID WINAPI WinHandleKeyboardInput(UINT message, WPARAM wParam, LPARAM lParam) {
	switch (message) {
	case WM_CHAR:
	case WM_KEYDOWN:
		switch (wParam) {
		case 'w':
		case VK_UP:
			sprite_move(sprite, SVEC_TOP);
			break;
		case 's':
		case VK_DOWN:
			sprite_move(sprite, SVEC_BOTTOM);
			break;
		case 'a':
		case VK_LEFT:
			sprite_move(sprite, SVEC_LEFT);
			break;
		case 'd':
		case VK_RIGHT:
			sprite_move(sprite, SVEC_RIGHT);
			break;
		}
	}
}

VOID WINAPI WinRender(HWND hWnd) {
	RECT cr;
	HDC hdc = GetDC(hWnd);
	GetClientRect(hWnd, &cr);
	FillRect(hdc, &cr, backSolidBrush);
	sprite_render(sprite, hdc);
}

LRESULT CALLBACK WinProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
	switch (message) {
		case WM_CHAR:
		case WM_KEYDOWN:
			WinHandleKeyboardInput(message, wParam, lParam);
		case WM_MOUSEWHEEL:
			if (message == WM_MOUSEWHEEL) {
				WinHandleMouseInput(message, wParam, lParam);
			}
			PostMessage(hWnd, WM_PAINT, 0, 0);
			break;
		case WM_PAINT:
			WinRender(hWnd);
			break;
		case WM_DESTROY:
			PostQuitMessage(0);
			break;
	}
	return DefWindowProc(hWnd, message, wParam, lParam);
}

INT APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PWSTR pCmdLine, int nCmdShow) {
	WNDCLASSA wnd;
	memset(&wnd, 0, sizeof wnd);
	wnd.lpfnWndProc = WinProc;
	wnd.lpszClassName = winClassName;
	if (!RegisterClass(&wnd)) {
		return 1;
	}
	DWORD dwStyle = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU;
	RECT winRect = { 0, 0, WIN_WIDTH, WIN_HEIGHT };
	if (!AdjustWindowRect(&winRect, dwStyle, false)) {
		return 1;
	}
	HWND hWnd = CreateWindowEx(0, winClassName, winName, dwStyle, WIN_START_POSX, WIN_START_POSY,
		rWidth(winRect), rHeight(winRect), NULL, NULL, NULL, NULL);
	if (hWnd == NULL) {
		return 1;
	}
	MSG message;
	sprite = sprite_new();
	backSolidBrush = CreateSolidBrush(RGB(33, 33, 33));	
	ShowWindow(hWnd, nCmdShow);
	while (GetMessage(&message, NULL, 0, 0)) {
		TranslateMessage(&message);
		DispatchMessage(&message);
	}
	sprite_free(sprite);
	return 0;
}