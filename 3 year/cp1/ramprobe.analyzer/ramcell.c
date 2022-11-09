#include "ramcell.h"

ram_iterator_t it_init(FILE* file) {
	ram_iterator_t it;
	it.file = file;
	it.state = RAM_IT_INIT_STATE;
	memset(&it.value, 0, sizeof(ram_cell_t));
	return it;
}

ram_iterator_t it_next(ram_iterator_t* it) {
	char buf[RAM_CELL_ACTION_SIZE*2];
	it->state = RAM_IT_ACTV_STATE;
	for (int iteration = 0; iteration < 7; iteration++) {
		if (fgets(buf, sizeof buf, it->file) == NULL) {
			it->state = RAM_IT_FINI_STATE;
			break;
		}
		else {
			if (iteration >= 3) {
				memcpy(it->value.values[iteration - 3], buf, RAM_CELL_VALUE_SIZE);
			}
			else {
				switch (iteration) {
				case 0:
					it->value.address = atoi(buf);
					break;
				case 1:
					for (ram_type_kind_t kind = 0; kind < 3; kind++) {
						if (strcmp(buf, ram_type_kind_str[kind]) == 0) {
							it->value.kind = (ram_type_kind_t)kind;
						}
					}
					break;
				case 2:
					switch (it->value.kind) {
					case RAM_CELL_KIND_NONE:
						memset(it->value.action, 0, RAM_CELL_ACTION_SIZE);
						break;
					case RAM_CELL_KIND_CFID:
					case RAM_CELL_KIND_CFIN:
						memcpy(it->value.action, buf, RAM_CELL_ACTION_SIZE);
						break;
					}
				}
			}
		}
	}
	return *it;
}


bool it_fini(const ram_iterator_t* it) {
	//it->file == NULL || feof(it->file) != 0
	if (it->state == RAM_IT_FINI_STATE) {
		return true;
	}
	return false;
}