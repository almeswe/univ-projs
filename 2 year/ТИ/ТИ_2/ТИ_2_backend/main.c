#include "lfsr.h"

#include <time.h>
#include <stdio.h>

void elapsed(clock_t start) {
	double time_spent = (float)(clock() - start) / CLOCKS_PER_SEC;
	printf("Done: %fs\n", time_spent);
}

int main(int argc, char** argv) {
	// 18
	uint32_t taps_count = 4;
	uint32_t* taps = (uint32_t)xmalloc(
		sizeof(uint32_t) * taps_count);
	
	taps[0] = 39;
	taps[1] = 20;
	taps[2] = 18;
	taps[3] = 1;

	struct lfsr_header header = {
		.seed = ULLONG_MAX,
		.seed_bits = 40,
		.seq_len = (uint64_t)(20*1024*1024),
		.taps = taps,
		.taps_count = taps_count
	};

	// 15
	//uint32_t taps_count = 4;
	//uint32_t* taps = (uint32_t)xmalloc(
	//	sizeof(uint32_t) * taps_count);

	//taps[0] = 36;
	//taps[1] = 11;
	//taps[2] = 9;
	//taps[3] = 1;

	//struct lfsr_header header = {
	//	.seed = ULLONG_MAX,
	//	.seed_bits = 37,
	//	.seq_len = 3,
	//	.taps = taps,
	//	.taps_count = taps_count
	//};

	// 17
	//uint32_t taps_count = 2;
	//uint32_t* taps = (uint32_t)xmalloc(
	//	sizeof(uint32_t) * taps_count);

	//taps[0] = 38;
	//taps[1] = 3;

	//struct lfsr_header header = {
	//	.seed = ULLONG_MAX,
	//	.seed_bits = 39,
	//	.seq_len = 4,
	//	.taps = taps,
	//	.taps_count = taps_count
	//};

	char* seq;
	clock_t start = clock();

	seq = lfsr_40bits_gen(ULLONG_MAX, header.seq_len);
	elapsed(start), start = clock();
	free(seq);

	seq = lfsr_generic_gen(header);
	elapsed(start);
	free(seq);
	free(taps);

	return 0;
}