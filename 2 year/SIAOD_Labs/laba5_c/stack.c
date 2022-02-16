#include "stack.h"

Stack* stack_new()
{
	Stack* stack = new(Stack);
	assert(stack);
	stack->cap = 8;
	stack->len = 0;
	stack->head = NULL;
	stack->data = cnew(int*, stack->cap);
	assert(stack->data);
	return stack;
}

void stack_resize(Stack* stack)
{
	stack->cap *= 2;
	assert(stack->data);
	stack->data = (int*)realloc(stack->data,
		stack->cap * sizeof(int*));
	assert(stack->data);
}

int* pop(Stack* stack)
{
	assert(stack->len != 0);
	return stack->data[--stack->len];
}

int* peek(Stack* stack)
{
	assert(stack->len != 0);
	return stack->data[stack->len - 1];
}

void push(Stack* stack, int* data)
{
	if (stack->len == stack->cap)
		stack_resize(stack);
	stack->data[stack->len++] = data;
}

void print_stack(Stack* stack)
{
	printf("---------------------------\n");
	for (int i = 0; i < stack->len; i++)
		printf("%d) %d\n", i + 1, stack->data[i]);
	printf("---------------------------\n");
}