#ifndef _MARCH_TEST_ANALYZER_CORE_H
#define _MARCH_TEST_ANALYZER_CORE_H

// #define _MARCH_TEST_ANALYZER_CORE_ENABLE_CAMEL_CASE

#define RAM_SIZE 4096

#include "ram_bit_cell.h"
#include "ram_bit_cells.h"

#ifdef _MARCH_TEST_ANALYZER_CORE_ENABLE_CAMEL_CASE
	#define MarchTestFinalize()				march_test_finalize()
	#define MarchTestAnalyze(fname)			march_test_analyze(fname)
	#define MarchTestGetAnalyzedCell(index) march_test_get_analyzed_cell(index)

	typedef struct _ram_bit_cell		RamBitCell;
	typedef struct _ram_bit_cells		RamBitCells;
	typedef enum _ram_bit_cell_fault	RamBitCellFault;
	typedef struct _ram_iterator		RamIterator;
	typedef enum _ram_iterator_state	RamIteratorState;
#endif

void march_test_finalize();
bool march_test_analyze(const char* filename);
const ram_bit_cell* march_test_get_analyzed_cell(int index);

#endif