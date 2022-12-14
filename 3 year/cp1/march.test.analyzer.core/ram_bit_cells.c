#include "ram_bit_cells.h"

static ram_bit_cells* ram_bit_cells_new() {
	int64_t length = 0;
	int64_t capacity = 8;
	int64_t size = capacity * sizeof(ram_bit_cell)+
		offsetof(ram_bit_cells, data);
	ram_bit_cells* cells = (ram_bit_cells*)malloc(size);
	cells->length = length;
	cells->capacity = capacity;
	return cells;
}

static ram_bit_cells* ram_bit_cells_resize(ram_bit_cells* cells) {
	int64_t capacity = cells->capacity * 2;
	int64_t size = capacity * sizeof(*cells->data) +
		offsetof(ram_bit_cells, data);
	cells = (ram_bit_cell*)realloc(cells, size);
	return cells;
}

static ram_bit_cells* ram_bit_cells_check(ram_bit_cells* cells) {
	if (cells == NULL) {
		return ram_bit_cells_new();
	}
	if (cells->length >= cells->capacity) {
		return ram_bit_cells_resize(cells);
	}
	return cells;
}

int64_t ram_bit_cells_size(ram_bit_cell** cells) {
	if (cells == NULL) {
		return 0;
	}
	ram_bit_cells* header = (ram_bit_cells*)cells_hdr(cells);
	return header->length;
}

void ram_bit_cells_add(ram_bit_cell*** cells, ram_bit_cell* cell) {
	ram_bit_cells* header = NULL;
	if (*cells != NULL) {
		header = (ram_bit_cells*)cells_hdr(*cells);
	}
	header = ram_bit_cells_check(header);
	header->data[header->length++] = cell;
	*cells = &header->data;
}

void ram_bit_cells_free(ram_bit_cell** cells) {
	if (cells != NULL) {
		free(cells_hdr(cells));
	}
}