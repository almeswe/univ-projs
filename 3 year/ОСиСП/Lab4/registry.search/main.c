#undef UNCICODE

#include <stdio.h>
#include <stdbool.h>
#include <windows.h>

#include "sbuffer.h"

#define SUBKEY_DEF_BUF_SIZE 256

typedef struct path_pholder {
	bool is_finished;
	PCHAR path;
} PathHolder;

static PCHAR* paths = NULL;

__forceinline bool StringsAreEqual(const PCHAR s1, const PCHAR s2) {
	return (s1 && s2) && (strcmp(s1, s2) == 0);
}

PCHAR ConcatStrings(const PCHAR s1, const PCHAR s2) {
	size_t s1Size = strlen(s1);
	size_t s2Size = strlen(s2);
	PCHAR str = (PCHAR)calloc(s1Size + s2Size + 3, sizeof(CHAR));
	sprintf_s(str, s1Size + s2Size + 3, "%s\\%s", s1, s2);
	return str;
}

VOID SearchKeyInParent(HKEY hParentKey, const PSTR targetKeyName, const PSTR parentKeyName) {
	LSTATUS lStatus = ERROR_SUCCESS;
	CHAR keyNameBuf[SUBKEY_DEF_BUF_SIZE] = { 0 };
	for (DWORD dwIndex = 0; lStatus != ERROR_NO_MORE_ITEMS; dwIndex++) {
		DWORD keyNameBufSize = sizeof keyNameBuf;
		lStatus = RegEnumKeyExA(hParentKey, dwIndex, keyNameBuf, 
			&keyNameBufSize, NULL, NULL, NULL, NULL);
		PCHAR newPath = ConcatStrings(parentKeyName, keyNameBuf);
		sbuffer_add(paths, newPath);
		if (lStatus == ERROR_SUCCESS) {
			if (StringsAreEqual(keyNameBuf, targetKeyName)) {
				printf("Found at: root\%s\n", newPath);
				fflush(stdout);
			}
			else {
				HKEY hCurrentKey = 0;
				lStatus = RegOpenKeyExA(hParentKey, keyNameBuf, 0, KEY_READ, &hCurrentKey);
				if (lStatus == ERROR_SUCCESS) {
					SearchKeyInParent(hCurrentKey, targetKeyName, newPath);
				}
				RegCloseKey(hCurrentKey);
			}
		}
	}
}

/*
	атомарность операций
	типизированность данных из коробки
	парсинг бинарных данныз быстрее
	спецефичен для каждого пользователя
*/

INT main(INT argc, CHAR** argv) {
	if (argc != 2) {
		return 1;
	}
	printf("Searching for %s\n", argv[1]);
	SearchKeyInParent(HKEY_CURRENT_USER, argv[1], "");
	size_t size = sbuffer_len(paths);
	for (size_t i = 0; i < size; i++) {
		free(paths[i]);
	}
	sbuffer_free(paths);
	printf("%s\n", "Press any key to exit...");
	(VOID)getc(stdin);
}