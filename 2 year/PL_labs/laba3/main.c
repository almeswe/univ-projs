#define CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <string.h>

#define SIZE 40
#define GETS(x) char buffer[SIZE]; x = gets(buffer)

void ex1()
{
	char* str;
	GETS(str);

	int* map = (int*)calloc(1, 26 * sizeof(int));

	for (int i = 0; i < SIZE; i++)
		if (str[i] != '_' && (str[i] <= 'Z' && str[i] >= 'A'))
			map[str[i] - 'A'] += 1;

	for (int i = 0; i < 26; i++)
		if (map[i] > 0)
			printf("%c%d", (char)('A'+i), map[i]);
}

void ex2()
{
#define is_lower(x) ((x) >= 'a' && (x) <= 'z')
	char* str;
	GETS(str);

	const char offset = 'A' - 'a';
	char* new_buffer = (char*)calloc(SIZE, sizeof(char));

	int index_in_new_buffer = 0;
	for (int i = 0; i < SIZE; i++)
	{
		str[i] += is_lower(str[i]) ? offset : 0;
		if (str[i] >= 'Q' && str[i] <= 'Z')
			new_buffer[index_in_new_buffer++] = str[i];
	}

	printf("%s\n", str);

	int buff;
	for (int i = 0; i < index_in_new_buffer; i++)
	{
		for (int j = i + 1; j < index_in_new_buffer; j++)
		{
			if (new_buffer[i] > new_buffer[j])
			{
				buff = new_buffer[i];
				new_buffer[i] = new_buffer[j];
				new_buffer[j] = buff;
			}
		}
	}
	new_buffer[index_in_new_buffer] = '\0';
	printf("%s\n", new_buffer);
}

int input(const char* msg)
{
	char s[20];
	printf("%s\n", msg);
	char* input = gets(s);
	int num_input = 0;
	for (int i = 0; i < strlen(input); i++)
		num_input = num_input * 10 + (input[i] - '0');
	return num_input;
}

void ex3()
{
	char* str1 = _strdup("abcdeadsjwqiehgqdbhasdhqwuieq");
	char* str2 = _strdup("11111111111111111111111111111");
	const int size = 30;
	int k = input("Enter k: ");

	for (int i = k; i < (30 - k); i++)
		str1[i] = str2[i-k];

	printf("%s\n", str1);
}

int main(int argc, char** argv)
{
	//ex1();
	ex2();
	ex3();
	return 0;
}