#ifndef STACK_H
#define STACK_H

#include <stdlib.h>
#include <stdint.h>
#include <assert.h>
#include <stdio.h>

#define new(type) \
	((type*)malloc(sizeof(type)))

#define cnew(type, count) \
	((type*)calloc(count, sizeof(type)))

typedef struct Stack
{
	uint32_t len;
	uint32_t cap;

	uint32_t* head;
	uint32_t** data;
} Stack;

Stack* stack_new();

int* pop(Stack* stack);
int* peek(Stack* stack);
void push(Stack* stack, int* data);

void print_stack(Stack* stack);

#endif