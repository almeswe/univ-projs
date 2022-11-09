#ifndef _RAM_CELL_H
#define _RAM_CELL_H

#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

#define RAM_CELL_VALUE_SIZE		8
#define RAM_CELL_ACTION_SIZE	16

static const char* ram_type_kind_str[] = {
	"CFid\n",
	"CFin\n",
	"NONE\n"
};

typedef enum ram_type_kind {
	RAM_CELL_KIND_CFID = 0,
	RAM_CELL_KIND_CFIN = 1,
	RAM_CELL_KIND_NONE = 2
} ram_type_kind_t;

typedef enum ram_iterator_state {
	RAM_IT_INIT_STATE = 0b00000001,
	RAM_IT_ACTV_STATE = 0b00000010,
	RAM_IT_FINI_STATE = 0b00000100,
} ram_iterator_state_t;

typedef struct ram_cell {
	int address;
	ram_type_kind_t kind;
	char action[RAM_CELL_ACTION_SIZE];
	char values[3][RAM_CELL_VALUE_SIZE];
} ram_cell_t;

typedef struct ram_iterator {
	FILE* file;
	int state;
	ram_cell_t value;
} ram_iterator_t;

ram_iterator_t it_init(FILE* file);
ram_iterator_t it_next(ram_iterator_t* it);
bool it_fini(const ram_iterator_t* it);

#endif 