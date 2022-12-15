#include "analyzer.h"

static int64_t ramsize = 0;
static ram_bit_cell shared = { 0 };
static ram_bit_cell ram[RAM_SIZE] = { 0 };

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
		return perror("fopen_s"), false;
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
	fclose(file);
	return true;
}

const ram_bit_cell* march_test_get_analyzed_cell(int64_t index) {
	if (index < 0 || index >= ramsize) {
		return NULL;
	}
	shared = ram[index];
	return &shared;
}