#ifndef _RAM_BIT_CELL_H
#define _RAM_BIT_CELL_H

#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

#define RAM_CELL_VALUE_SIZE		8
#define RAM_CELL_ACTION_SIZE	16

#define _B(expr) expr; break

typedef enum _ram_bit_cell_fault {
	RAM_CELL_FAULT_CFID = 0,
	RAM_CELL_FAULT_CFIN = 1,
	RAM_CELL_FAULT_NONE = 2
} ram_bit_cell_fault;

static const char* ram_fault_type_str[] = {
	[RAM_CELL_FAULT_CFID] = "CFid",
	[RAM_CELL_FAULT_CFIN] = "CFin",
	[RAM_CELL_FAULT_NONE] = "NONE"
};

typedef enum _ram_iterator_state {
	RAM_IT_INIT_STATE = 0b00000001,
	RAM_IT_ACTV_STATE = 0b00000010,
	RAM_IT_FINI_STATE = 0b00000100,
} ram_iterator_state;

typedef struct _ram_bit_cell {
	uint32_t address;
	ram_bit_cell_fault fault;
	struct _ram_bit_cell** victims;
	union _ram_bit_cell_data {
		struct _ram_bit_untyped_data {
			char action[RAM_CELL_ACTION_SIZE];
			char values[3][RAM_CELL_VALUE_SIZE];
		} untyped;
		struct _ram_bit_victim_data {
			char influence[RAM_CELL_ACTION_SIZE];
			uint64_t agressor;
			char _unused[2][RAM_CELL_VALUE_SIZE];
		} victim;
	};
} ram_bit_cell;

typedef struct _ram_iterator {
	FILE* file;
	int state;
	ram_bit_cell value;
} ram_iterator;

ram_iterator it_init(FILE* file);
ram_iterator it_next(ram_iterator* it);
bool it_fini(const ram_iterator* it);

#endif 