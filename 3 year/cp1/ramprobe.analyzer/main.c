#define _CRT_SECURE_NO_WARNINGS

#include "ramcell.h"

static ram_cell_t ram[4096] = { 0 };

int main(int argc, char** argv) {
	FILE* file = fopen("C:\\Users\\HP\\Desktop\\RAM_CFin_250_CFid_250.txt", "r");
	ram_iterator_t it = it_init(file);
	it = it_next(&it);
	for (int i = 0; (i < 4096) || (!it_fini(&it)); i++) {
		ram[i] = it.value;
		it = it_next(&it);
	}
	return 0;
}