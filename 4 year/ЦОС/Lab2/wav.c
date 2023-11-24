#include "wav.h"

static int cut(long value) {
    if (value > INT_MAX) {
        value = INT_MAX;
    }
    if (value < INT_MIN) {
        value = INT_MIN;
    }
    return (int)value;
}

static wav_hdr mkhdr(int d, int n) {
    const int8_t nc = 1;
    const int16_t bps = 32;
    const int32_t ns = (int32_t)(d * n);
    wav_hdr hdr = {
        .chunk_id           = *(int32_t*)"RIFF",
        .format             = *(int32_t*)"WAVE",
        .sub_chunk1_id      = *(int32_t*)"fmt ",
        .sub_chunk1_size    = 16,
        .audio_format       = 1,
        .num_channels       = nc,
        .sample_rate        = n,
        .byte_rate          = n * nc * bps / 8,
        .block_align        = 1 * bps / 8,
        .bits_per_sample    = bps,
        .sub_chunk2_id      = *(int32_t*)"data",
        .sub_chunk2_size    = ns * nc * bps / 8,
    };
    hdr.chunk_size = 36 + hdr.sub_chunk2_size;
    return hdr;
}

void gen_wav(wav_gen_params params) {
    remove(params.out);
    int fd = open(params.out, O_CREAT | O_RDWR, 0666);
    if (fd == -1) {
        perror("open");
        exit(EXIT_FAILURE);
    }
    wav_hdr hdr = mkhdr(params.d, params.n);
    write(fd, (char*)&hdr, sizeof(wav_hdr));
    for (int i = 0; i < params.n * params.d; i++) {
        int y = cut((INT_MAX) * params.values[i]);
        write(fd, (char*)&y, sizeof(int));
    }
    close(fd);
}