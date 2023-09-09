#include "wav.h"

float sine_wave(wav_fn_params* params) {
    return params->a * sinf(
        2 * M_PI * params->f * params->x 
                 / params->n 
        + params->p
    );
}

float pulse_wave(wav_fn_params* params) {
    float value = fmodf(
        2 * M_PI * params->f * params->x
                 / params->n
        + params->p, 2 * M_PI
    ) / (2 * M_PI);
    return (value <= params->d) ?
        params->a : -params->a;
}

float triangle_wave(wav_fn_params* params) {
    return params->a * 2 / M_PI * asinf(
        sinf(
            2 * M_PI * params->f * params->x
                     / params->n
            + params->p
        )
    );
}

float sawtooth_wave(wav_fn_params* params) {
    return params->a * 2 * M_PI * atanf(
        tanf(2 * M_PI * params->f * params->x 
                      / params->n
        )
    );
}

float noise(wav_fn_params* params) {
    return params->a * rand() / RAND_MAX;
}

static wav_hdr make_wav_hdr(int duration, int n) {
    const int8_t nc = 1;
    const int16_t bps = 32;
    const int32_t ns = (int32_t)(duration * n);
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

static int cut(long value) {
    if (value > INT_MAX) {
        value = INT_MAX;
    }
    if (value < INT_MIN) {
        value = INT_MIN;
    }
    return (int)value;
}

void gen_wav(wav_gen_params params) {
    remove(params.out);
    int fd = open(params.out, O_CREAT | O_RDWR, 0666);
    if (fd == -1) {
        perror("open");
        exit(EXIT_FAILURE);
    }
    wav_hdr hdr = make_wav_hdr(
        params.duration,
        params.wav_fn_params.n
    );
    write(fd, (char*)&hdr, sizeof(wav_hdr));
    for (int i = 0; i < hdr.sample_rate * params.duration; i++) {
        params.wav_fn_params.x = i;
        int y = cut((INT_MAX) * params.wav_fn(&params.wav_fn_params));
        write(fd, (char*)&y, sizeof(int));
    }
    close(fd);
}

void merge_wavs(wav_merge_params params) {
    remove(params.out);
    int fd = open(params.out, O_CREAT | O_RDWR, 0666);
    if (fd == -1) {
        perror("open");
        exit(EXIT_FAILURE);
    }
    wav_hdr hdr = make_wav_hdr(
        params.duration,
        params.wav_fn_params.n
    );
    write(fd, (char*)&hdr, sizeof(wav_hdr));
    for (int i = 0; i < hdr.sample_rate * params.duration; i++) {
        params.wav_fn_params.x = i;
        long temp = 0;
        for (int j = 0; j < params.count; j++) {
            temp += (INT_MAX) * params.wavs[j](&params.wav_fn_params);
        }
        int y = cut(temp);
        write(fd, (char*)&y, sizeof(int));
    }
    close(fd);
}