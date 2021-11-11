#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <assert.h>

#include "queue.h"

#define new(type) \
	(type*)malloc(sizeof(type))

#define cnew(type, count) \
	(type*)calloc(count, sizeof(type))

#define TICK 3

// first element is size of sequence
int tasks_for_first[]  = { 8, 8, 11, 5, 4, 1, 9, 11, 8 };
int tasks_for_second[] = { 10, 2, 3, 2, 1, 3, 4, 5, 1, 2, 1 };
int tasks_for_third[]  = { 9, 6, 3, 8, 2, 1, 4, 6, 8, 7 };
int tasks_for_forth[]  = { 9, 5, 4, 3, 4, 2, 1, 5, 4, 3 };
int tasks_for_fifth[]  = { 10, 8, 6, 8, 2, 3, 2, 1, 3, 12, 7 };
int tasks_for_sixth[]  = { 10, 7, 6, 5, 7, 11, 3, 7, 8, 6, 4 };

typedef struct 
{
	char id;
	int* tasks;
	int task_count;
	int current_task;
	int remaining_in_current_task;
} Process;

Process* process_new(char id, int* tasks)
{
	Process* p = new(Process);
	assert(p);
	p->id = id;
	p->tasks = tasks;
	p->current_task = 1;
	p->task_count = tasks[0];
	p->remaining_in_current_task = tasks[1];
	return p;
}

int process_completed(Process* process)
{
	return process->current_task > process->task_count+1;
}

void process_increment_task(Process* process)
{
	process->current_task++;
	if (!process_completed(process))
		process->remaining_in_current_task = process->tasks[process->current_task];
}

void process_processes(Process** processes, int process_count)
{
#define calc_remainder(x)                \
	current->remaining_in_current_task = \
		current->remaining_in_current_task - (x)

#define print_state \
	printf("------------------------------------\n"); \
	printf("PROCESS                   (%d)\n", current->id); \
	printf("CURRENT TASK              (%d/%d)\n", current->current_task, current->task_count+1); \
	printf("REMAINING IN CURRENT TASK (%d)\n", current->remaining_in_current_task); \
	printf("------------------------------------\n"); 
	
	int remainder = 0;
	int skip_until_finish = 0;

	Queue* q = queue_new();

	for (int i = 0; i < process_count; i++)
		queue_push(q, processes[i]);

	Process* current = queue_pop(q);

	while (current)
	{
		if (!skip_until_finish)
		{
			char buffer[50];
			printf(">"); char* input = gets();
			if (strcmp(input, "uf") == 0)
				skip_until_finish = 1;
		}

		while (!process_completed(current))
		{
			if (remainder < 0)
				remainder = calc_remainder(-remainder);
			else
			{
				print_state;
				remainder = calc_remainder(TICK);
			}

			if (remainder == 0)
				process_increment_task(current);

			else if (remainder < 0)
			{
				process_increment_task(current);
				if (!process_completed(current))
					queue_push(q, current);
				break;
			}
		}
		current = queue_pop(q);
	}
}

int main(int argc, char** argv)
{
	Process* p1 = process_new(1, tasks_for_first);
	Process* p2 = process_new(2, tasks_for_second);
	Process* p3 = process_new(3, tasks_for_third);
	Process* seq1[] = { p1, p2, p3 };
	process_processes(seq1, 3);

	Process* p4 = process_new(4, tasks_for_forth);
	Process* seq2[] = { p4 };
	process_processes(seq2, 1);

	Process* p5 = process_new(5, tasks_for_fifth);
	Process* p6 = process_new(6, tasks_for_sixth);
	Process* seq3[] = { p5, p6 };
	process_processes(seq3, 2);

	return 1;
}