#include <stdio.h>
#include <windows.h>

int main(int argc, char** argv) {
	char str[] = "standart print message!";
	while (1) {
		printf("%s\n", str);
		Sleep(2000);
	}
	return 1;
}