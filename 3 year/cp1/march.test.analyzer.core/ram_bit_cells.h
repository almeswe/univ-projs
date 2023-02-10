#ifndef _RAM_BIT_CELLS_H
#define _RAM_BIT_CELLS_H

#include "ram_bit_cell.h"

#define cells_hdr(ptr) ((((char*)ptr) - offsetof(ram_bit_cells, data)))

typedef struct _ram_bit_cells {
	int64_t length;
	int64_t capacity;
	ram_bit_cell* data[0];
} ram_bit_cells;

void ram_bit_cells_free(ram_bit_cell** cells);
void ram_bit_cells_add(ram_bit_cell*** cells, const ram_bit_cell* cell);
int64_t ram_bit_cells_size(ram_bit_cell** cells);

#endif