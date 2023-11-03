#ifndef _WAV_H_
#define _WAV_H_

#include <stdio.h>
#include <fcntl.h>
#include <unistd.h>
#include <stdlib.h>
#include <stdbool.h>
#include <assert.h>
#include <limits.h>
#include <stdint.h>

#define BUF_SIZE 1024

#pragma pack(push)
#pragma pack(1)
typedef struct _wav_hdr {
    int32_t chunk_id;
    int32_t chunk_size;
    int32_t format;
    int32_t sub_chunk1_id;
    int32_t sub_chunk1_size;
    int16_t audio_format;
    int16_t num_channels;
    int32_t sample_rate;
    int32_t byte_rate;
    int16_t block_align;
    int16_t bits_per_sample;
    int32_t sub_chunk2_id;
    int32_t sub_chunk2_size;
} wav_hdr;
#pragma pack(pop)

typedef struct _wav_gen_params {
    int n, d;
    const char* out;
    double* values;
} wav_gen_params;

void gen_wav(wav_gen_params params);

#endif