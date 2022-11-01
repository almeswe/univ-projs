#undef UNICODE

#include <stdio.h>
#include <stdbool.h>
#include <windows.h>

typedef DWORD NWORD;

#define REG_TEST_INT_FLAG_NAME		"IntFlag"
#define REG_TEST_STR_FLAG_NAME		"StrFlag"

#define DEFAULT_INT_FLAG_VALUE		(DWORD)0
#define DEFAULT_STR_FLAG_VALUE		"The quick brown fox jumps over the lazy dog"

HKEY OpenRootKey(LONG predefinedKey) {
	HKEY hKeyRoot = 0;
	LSTATUS status = RegOpenKeyExA(HKEY_CURRENT_USER, "SOFTWARE", 0, KEY_ALL_ACCESS, &hKeyRoot);
	if (status != ERROR_SUCCESS) {
		fprintf(stderr, "Cannot open predefined key (%ul), status: %ld\n", predefinedKey, status);
		ExitProcess(EXIT_FAILURE);
	}
	return hKeyRoot;
}

HKEY OpenKey(HKEY parentKey, const LPCSTR keyName) {
	HKEY hKey = 0;
	LSTATUS status = RegOpenKeyExA(parentKey, keyName, 0, KEY_ALL_ACCESS, &hKey);
	if (status != ERROR_SUCCESS) {
		fprintf(stderr, "Cannot open key (%s), status: %ld\n", keyName, status);
		ExitProcess(EXIT_FAILURE);
	}
	return hKey;
}

VOID SetDwordKeyFlag(HKEY hKey, DWORD data) {
	LSTATUS status = RegSetValueExA(hKey, REG_TEST_INT_FLAG_NAME, 0, REG_DWORD, &data, sizeof(DWORD));
	if (status != ERROR_SUCCESS) {
		fprintf(stderr, "Cannot set dword value, status: %ld.\n", status);
		ExitProcess(EXIT_FAILURE);
	}
}

VOID SetStringKeyFlag(HKEY hKey, const PCHAR str) {
	LSTATUS status = RegSetValueExA(hKey, REG_TEST_STR_FLAG_NAME, 0, REG_SZ, str, strlen(str)+1);
	if (status != ERROR_SUCCESS) {
		fprintf(stderr, "Cannot set string value, status: %ld.\n", status);
		ExitProcess(EXIT_FAILURE);
	}
}

PCHAR GetStringKeyFlagValue(HKEY hKey, const PCHAR keyName) {
	CHAR databuf[256] = { 0 };
	DWORD databufSize = sizeof databuf;
	LSTATUS status = RegGetValueA(hKey, keyName, REG_TEST_STR_FLAG_NAME, RRF_RT_REG_SZ, NULL, databuf, &databufSize);
	if (status != ERROR_SUCCESS) {
		fprintf(stderr, "Cannot get string value, status: %ld.\n", status);
		ExitProcess(EXIT_FAILURE);
	}
	PCHAR data = (PCHAR)calloc(databufSize, sizeof(CHAR));
	sprintf_s(data, databufSize, "%s", databuf);
	return data;
}

DWORD GetDwordKeyFlagValue(HKEY hKey, const PCHAR keyName) {
	CHAR databuf[sizeof(DWORD)] = { 0 };
	DWORD databufSize = sizeof databuf;
	LSTATUS status = RegGetValueA(hKey, keyName, REG_TEST_INT_FLAG_NAME, RRF_RT_REG_DWORD, NULL, databuf, &databufSize);
	if (status != ERROR_SUCCESS) {
		fprintf(stderr, "Cannot get dword value, status: %ld.\n", status);
		ExitProcess(EXIT_FAILURE);
	}
	return ((DWORD*)databuf)[0];
}

HKEY CreateKey(HKEY parentKey, const LPCSTR keyName) {
	HKEY hKey = 0;
	DWORD disposition = 0;
	LSTATUS status = RegCreateKeyExA(parentKey, keyName, 0, NULL, 
		REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, NULL, &hKey, &disposition);
	if (status != ERROR_SUCCESS) {
		fprintf(stderr, "Cannot create key (%s), status: %ld.\n", keyName, status);
		ExitProcess(EXIT_FAILURE);
	}
	if (disposition != REG_CREATED_NEW_KEY) {
		fprintf(stderr, "Key (%s) is opened, not created.\n", keyName);
		ExitProcess(EXIT_FAILURE);
	}
	SetDwordKeyFlag(hKey, DEFAULT_INT_FLAG_VALUE);
	SetStringKeyFlag(hKey, DEFAULT_STR_FLAG_VALUE);
	return hKey;
}

VOID DeleteKey(HKEY parentKey, const LPCSTR keyName) {
	LSTATUS status = RegDeleteKeyExA(parentKey, keyName, KEY_WOW64_64KEY, 0);
	if (status != ERROR_SUCCESS) {
		fprintf(stderr, "Cannot delete key (%s), status: %ld.\n", keyName, status);
		ExitProcess(EXIT_FAILURE);
	}
}

VOID CloseKey(HKEY hKey) {
	RegFlushKey(hKey);
	RegCloseKey(hKey);
}

VOID WaitForAnyUserInput() {
	printf("%s\n", "Press any key to continue...");
	(VOID)getc(stdin);
}

INT main(INT argc, CHAR** argv) {
	if (argc != 4) {
		return 1;
	}
	DWORD data = (DWORD)atoi(argv[2]);
	
	printf("%s\n", "Opening RootNode\\SOFTWARE...");
	HKEY hKeyRoot = OpenRootKey(HKEY_CURRENT_USER);
	printf("Creating key \"%s\"...\n", argv[1]);
	HKEY hKey = CreateKey(hKeyRoot, argv[1]);
	printf("Closing key \"%s\"...\n", argv[1]);
	CloseKey(hKey);
	
	WaitForAnyUserInput();

	printf("Opening key \"%s\"...\n", argv[1]);
	hKey = OpenKey(hKeyRoot, argv[1]);
	printf("Changing IntFlag of key \"%s\" to \"%d\"...\n", argv[1], data);
	SetDwordKeyFlag(hKey, data);
	printf("Changing StrFlag of key \"%s\" to \"%s\"...\n", argv[1], argv[3]);
	SetStringKeyFlag(hKey, argv[3]);
	PCHAR strVal = GetStringKeyFlagValue(hKeyRoot, argv[1]);
	DWORD intVal = GetDwordKeyFlagValue(hKeyRoot, argv[1]);
	printf("Read value from StrFlag: \"%s\"\n", strVal), free(strVal);
	printf("Read value from IntFlag: \"%d\"\n", intVal);
	WaitForAnyUserInput();
	
	printf("Deleting key \"%s\"...\n", argv[1]);
	DeleteKey(hKeyRoot, argv[1]);
	printf("Closing key \"%s\"...\n", argv[1]);
	CloseKey(hKey);
	printf("%s\n", "Closing root key...\n");
	CloseKey(hKeyRoot);
	return 0;
}