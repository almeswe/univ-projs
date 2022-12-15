#include "analyzer.h"

static int64_t ramsize = 0;
static ram_bit_cell shared = { 0 };
static ram_bit_cell ram[RAM_SIZE] = { 0 };

//static ram_bit_cell** print_cells = NULL;
//
//static void print_bit_cell(ram_bit_cell* cell, int indent) {
//	if (indent == 0) {
//		ram_bit_cells_free(print_cells), print_cells = NULL;
//	}
//	for (int i = 0; i < indent; i++) {
//		printf("  ");
//	}
//	printf("%ld\n", cell->address);
//	for (int i = 0; i < ram_bit_cells_size(print_cells); i++) {
//		if (cell == print_cells[i]) {
//			return;
//		}
//	}
//	ram_bit_cells_add(&print_cells, cell);
//	int64_t size = ram_bit_cells_size(cell->victims);
//	for (int64_t i = 0; i < size; i++) {
//		print_bit_cell(cell->victims[i], indent + 1);
//	}
//}

void march_test_finalize() {
	for (int64_t i = 0; i < ramsize; i++) {
		ram_bit_cells_free(ram[i].victims);
	}
	ramsize = 0;
	memset(ram, 0, sizeof ram);
}

bool march_test_analyze(const char* filename) {
	FILE* file = NULL;
	fopen_s(&file, filename, "r");
	if (file == NULL) {
		return false;
	}
	ram_iterator it = it_init(file);
	it = it_next(&it);
	for (int64_t i = 0; (i < RAM_SIZE) && (!it_fini(&it)); i++) {
		ram[ramsize++] = it.value;
		it = it_next(&it);
	}
	for (int64_t i = 0; i < ramsize; i++) {
		if (ram[i].fault != RAM_CELL_FAULT_NONE) {
			int64_t agressor = ram[i].victim.agressor;
			ram_bit_cells_add(&ram[agressor].victims, &ram[i]);
		}
	}
	/*for (int64_t i = 0; i < ramsize; i++) {
		if (ram_bit_cells_size(ram[i].victims) > 0) {
			print_bit_cell(&ram[i], 0);
		}
	}*/
	return true;
}

const ram_bit_cell* march_test_get_analyzed_cell(int64_t index) {
	if (index < 0 || index >= ramsize) {
		return NULL;
	}
	shared = ram[index];
	return &shared;
}
//
//int main(int argc, char** argv) {
//	FILE* file = fopen(INPUT_FILE, "r");
//	ram_iterator it = it_init(file);
//	it = it_next(&it);
//	for (int i = 0; (i < RAM_SIZE) && (!it_fini(&it)); i++) {
//		ram[ramsize++] = it.value;
//		it = it_next(&it);
//	}
//	for (int i = 0; i < ramsize; i++) {
//		if (ram[i].fault != RAM_CELL_FAULT_NONE) {
//			int64_t agressor = ram[i].victim.agressor;
//			ram_bit_cells_add(&ram[agressor].victims, &ram[i]);
//		}
//	}
//	for (int i = 0; i < ramsize; i++) {
//		if (ram_bit_cells_size(ram[i].victims) > 0) {
//			print_bit_cell(&ram[i], 0);
//		}
//	}
//	return 0;
//}