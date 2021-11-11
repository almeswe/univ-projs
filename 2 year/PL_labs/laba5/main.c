#include <time.h>
#include "stack.h"

Stack* create_stack(int size)
{
	int buff;
	int* array = cnew(int, size);
	Stack* stack = stack_new();

	for (int i = 0; i < size; i++)
		array[i] = rand() % 9;

	for (int i = 0; i < size; i++)
		for (int j = i + 1; j < size; j++)
			if (array[i] > array[j])
				buff = array[i], array[i] = array[j],
					array[j] = buff;

	for (int i = 0; i < size; i++)
		push(stack, array[i]);

	free(array);
	return stack;
}

Stack* merge_stacks(Stack* stack1, Stack* stack2)
{
	int size = stack1->len;
	Stack* general_stack = stack_new();

	while (stack1->len)
	{
		while (stack2->len && peek(stack2) >= peek(stack1))
			push(general_stack, pop(stack2));
		push(general_stack, pop(stack1));
	}

	if (size = stack2->len)
		for (int i = 0; i < size; i++)
			push(general_stack, pop(stack2));

	int* buffer = cnew(int, size = general_stack->len);
	assert(buffer);
	for (int i = 0; i < size; i++)
		buffer[i] = pop(general_stack);
	for (int i = 0; i < size; i++)
		push(general_stack, buffer[i]);

	return general_stack;
}

int main(int argc, char** argv)
{
	srand((uint32_t)(time(NULL) / 2));

	Stack* stack1 = create_stack(6);
	Stack* stack2 = create_stack(10);
	print_stack(stack1);
	print_stack(stack2);

	Stack* stack3 = merge_stacks(stack1, stack2);
	print_stack(stack3);

	return 1;
}