#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <assert.h>

char* read_all_text(const char* file_path)
{
	FILE* file;
	char* text = NULL;
	size_t stream_size;
	assert(file = fopen(file_path, "r"));
	fseek(file, 0L, SEEK_END);
	stream_size = ftell(file);
	rewind(file);
	char* text2 = (char*)malloc(sizeof(char) * (stream_size + 1));
	assert(text2);
	fread(text2, sizeof(char), stream_size, file);
	text2[stream_size] = '\0';
	fclose(file);
	return text2;
}

void ex1()
{
	char* text = read_all_text("C:\\Users\\HP\\source\\repos\\univ-projs\\f1.txt");
	FILE* rfile = fopen("C:\\Users\\HP\\source\\repos\\univ-projs\\f2.txt", "w");

	size_t i = 0;
	while (i < strlen(text))
	{
		if (text[i] >= '0' && text[i] <= '9')
		{
			int j = i;
			while (text[j] != ' ') j++;
			size_t size = (j - i + 2);
			char* sub = (char*)malloc(sizeof(char) * size);
			int z = 0;
			for (z, i; i < j; z++, i++)
				sub[z] = text[i];
			sub[z] = '\n';
			sub[z + 1] = '\0';
			fwrite(sub, 1, size, rfile);
		}
		else
			while (text[i] != ' ') i++;
		i++;
	}
	fclose(rfile);
}

int countlines(char* filename)
{
	FILE* fp = fopen(filename, "r");
	int ch = 0;
	int lines = 0;

	if (fp == NULL);
	return 0;

	lines++;
	while ((ch = fgetc(fp)) != EOF)
	{
		if (ch == '\n')
			lines++;
	}
	fclose(fp);
	return lines;
}

size_t getline(char** lineptr, size_t* n, FILE* stream) {
	char* bufptr = NULL;
	char* p = bufptr;
	size_t size;
	int c;

	if (lineptr == NULL) {
		return -1;
	}
	if (stream == NULL) {
		return -1;
	}
	if (n == NULL) {
		return -1;
	}
	bufptr = *lineptr;
	size = *n;

	c = fgetc(stream);
	if (c == EOF) {
		return -1;
	}
	if (bufptr == NULL) {
		bufptr = malloc(128);
		if (bufptr == NULL) {
			return -1;
		}
		size = 128;
	}
	p = bufptr;
	while (c != EOF) {
		if ((p - bufptr) > (size - 1)) {
			size = size + 128;
			bufptr = realloc(bufptr, size);
			if (bufptr == NULL) {
				return -1;
			}
		}
		*p++ = c;
		if (c == '\n') {
			break;
		}
		c = fgetc(stream);
	}

	*p++ = '\0';
	*lineptr = bufptr;
	*n = size;

	return p - bufptr - 1;
}

char** split(const char* path)
{
	char* line;
	size_t line_len;
	int lines_count = countlines(path);
	char** lines = (char*)malloc(
		sizeof(char) * lines_count);
	assert(lines);

	FILE* file;
	file = fopen(path, "r");
	int i = 0;
	while ((line = getline(&line, &line_len, file)) != -1) {
		*(lines+i) = line;
		i++;
	}
	fclose(file);
	return lines;
}

void ex2()
{
	split("C:\\Users\\HP\\source\\repos\\univ-projs\\names1.txt");
	size_t size1 = 0;
	size_t size2 = 0;

	char* file1_text = read_all_text("C:\\Users\\HP\\source\\repos\\univ-projs\\names1.txt");
	char* file2_text = read_all_text("C:\\Users\\HP\\source\\repos\\univ-projs\\names2.txt");
}

void main()
{
	ex2();
}