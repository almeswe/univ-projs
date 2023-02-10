#include "ram_bit_cell.h"

void it_modify_at_iteration0(ram_iterator* it, char* data) {
	it->value.address = (uint32_t)atoi(data);
}

void it_modify_at_iteration1(ram_iterator* it, char* data) {
	for (ram_bit_cell_fault fault = 0; fault < 3; fault++) {
		if (strcmp(data, ram_fault_type_str[fault]) == 0) {
			it->value.fault = (ram_bit_cell_fault)fault;
		}
	}
}

void it_modify_at_iteration2(ram_iterator* it, char* data) {
	switch (it->value.fault) {
		case RAM_CELL_FAULT_NONE:
			_B(memset(it->value.untyped.action, 0, RAM_CELL_ACTION_SIZE));
			break;
		case RAM_CELL_FAULT_CFID:
		case RAM_CELL_FAULT_CFIN:
			_B(memcpy(it->value.untyped.action, data, 
				min(strlen(data), RAM_CELL_ACTION_SIZE)));
	}
}

void it_modify_at_iterationx(ram_iterator* it, int x, char* data) {
	memcpy(it->value.untyped.values[x - 3], data, 
		min(strlen(data), RAM_CELL_VALUE_SIZE));
	if (x - 3 == 0 && it->value.fault != RAM_CELL_FAULT_NONE) {
		it->value.victim.agressor = (uint64_t)atoll(data);
	}
}

ram_iterator it_init(FILE* file) {
	ram_iterator it;
	it.file = file;
	it.state = RAM_IT_INIT_STATE;
	memset(&it.value, 0, sizeof(ram_bit_cell));
	return it;
}

ram_iterator it_next(ram_iterator* it) {
	char buf[256];
	it->state = RAM_IT_ACTV_STATE;
	for (int iteration = 0; iteration < 7; iteration++) {
		memset(buf, 0, sizeof buf);
		if (fgets(buf, sizeof buf, it->file) == NULL) {
			_B(it->state = RAM_IT_FINI_STATE);
		}
		else {
			for (int i = 0; i < strlen(buf); i++) {
				buf[i] = buf[i] != '\n' ? buf[i] : '\0';
			}
			switch (iteration) {
				case 0:	_B(it_modify_at_iteration0(it, buf));
				case 1: _B(it_modify_at_iteration1(it, buf));
				case 2: _B(it_modify_at_iteration2(it, buf));
				default:
					it_modify_at_iterationx(it, iteration, buf);
			}
		}
	}
	return *it;
}

bool it_fini(const ram_iterator* it) {
	return it->state == RAM_IT_FINI_STATE;
}