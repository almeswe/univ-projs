#ifndef _LFSR_H
#define _LFSR_H

#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include <assert.h>
#include "xmalloc.h"

struct lfsr_header {
	uint64_t seed;
	uint8_t seed_bits;

	uint32_t* taps;
	uint32_t taps_count;

	uint64_t seq_len;
};

char* lfsr_generic_gen(struct lfsr_header header);
char* lfsr_40bits_gen(uint64_t seed, uint64_t seq_len);

#endif