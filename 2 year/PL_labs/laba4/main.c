#define CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <string.h>

int base_index = 0;

#define SIZE 60
#define GETS(x) char buffer[SIZE]; x = gets(buffer)

char* get_next_word()
{
	char* base = _strdup("S_DICH'U_YA_POLAGAU_GLAVA_PREDPRIYATIYA_PO_SVEDENIYAM_O_MYXOBOIKAX_FAZANNIX_KYROCHEK");
	int len = 0;
	while (base[base_index] != '_')
	{
		len++;
		base_index += 1;
	}
	base_index -= len;
	char* str = (char*)malloc(sizeof(char) * (len + 1));
	for (int i = 0; i < len; i++)
	{
		str[i] = base[base_index];
		base_index += 1;
	}
	str[len] = '\0';
	base_index++;
	return str;
}

void ex()
{
	char* str; GETS(str);
	const int size = 1024;

	char* new_buffer = (char*)malloc(sizeof(char) * size);

	int i = 0;
	int j = 0;
	while (j < strlen(str))
	{
		new_buffer[i] = str[j];
		if (str[j] == '_')
		{
			i++;
			for (int f = 0; f < 2; f++)
			{
				char* word = get_next_word();
				int word_size = strlen(word);
				for (int z = 0; z < word_size; z++, i++)
					new_buffer[i] = word[z];
				new_buffer[i] = '_';
				i++;
			}
		}
		else
			i++;
		j++;
	}
	new_buffer[i] = '\0';
	printf("%s\n", new_buffer);
}

int main(int argc, char** argv)
{
	ex();
	return 0;
}