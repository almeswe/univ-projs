#include "lfsr.h"
#include <windows.h>

char* lfsr_generic_gen(struct lfsr_header header) {
	assert(header.seed);
	assert(header.seed_bits <= 64);
	assert(header.taps_count > 0);

	char* buffer = (char*)xcalloc(
		1, header.seq_len);

	//printf("%llu\n", header.seed);
	for (size_t byte = 0; byte < header.seq_len; byte++) {
		for (size_t bit = 0; bit < 8; bit++) {
			uint8_t xored = (header.seed >> header.taps[0]) & 1;
			for (uint32_t tap = 1; tap < header.taps_count; tap++) {
				xored ^= (header.seed >> (header.taps[tap]));
				if (tap == header.taps_count - 1)
					xored &= 1;
			}
			header.seed = (header.seed << 1) | xored;
			buffer[byte] = (buffer[byte] << 1) | xored;
			//printf("%llu\n", header.seed);
		}
	}
	return buffer;
}

char* lfsr_40bits_gen(uint64_t seed, uint64_t seq_len) {
	char* sequence = (char*)xmalloc(seq_len);
	for (size_t byte = 0; byte < seq_len; byte++) {
		for (size_t bit = 0; bit < 8; bit++) {
			uint8_t xored = ((seed >> 39) ^ (seed >> 20) ^
				(seed >> 18) ^ (seed >> 1)) & 1;
			seed = ((seed << 1) | xored);
			sequence[byte] = ((sequence[byte] << 1) | xored);
		}
	}
	return sequence;
}