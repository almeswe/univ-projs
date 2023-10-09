#include "wav.h"

typedef float (wav_fn2)(float, float);
typedef float (wav_fn_phi_frq)(const wav_fn_params*, float);

static float sine_wave2(float phi, float p) {
    return sinf(phi + p);
}

static float sine_wave_phi(const wav_fn_params* params) {
    return 2 * M_PI * params->f * params->x 
                    / params->n;
}

static float sine_wave_phi_frq(const wav_fn_params* params, float y_m) {
    return 2 * M_PI * params->f * (1 + y_m)
                    / params->n;
}

float sine_wave(wav_fn_params* params) {
    return params->a * sinf(sine_wave_phi(params) + params->p);
}

static float pulse_wave2(float phi, float d) {
    return (phi <= d) ? 1 : 0;
}

static float pulse_wave_phi(const wav_fn_params* params) {
    return fmodf(
        2 * M_PI * params->f * params->x
                 / params->n,
        2 * M_PI
    ) / (2 * M_PI);
}

static float pulse_wave_phi_frq(const wav_fn_params* params, float y_m) {
    return fmodf(
        2 * M_PI * params->f * (1 + y_m)
                 / params->n,
        2 * M_PI
    ) / (2 * M_PI);
}

float pulse_wave(wav_fn_params* params) {
    return (pulse_wave_phi(params) <= params->d) ? params->a : 0;
}

static float triangle_wave2(float phi, float p) {
    return 2 / M_PI * asinf(sinf(phi + p));    
}

static float triangle_wave_phi(const wav_fn_params* params) {
    return 2 * M_PI * params->f * params->x
                    / params->n;
}

static float triangle_wave_phi_frq(const wav_fn_params* params, float y_m) {
    return 2 * M_PI * params->f * (1 + y_m)
                    / params->n;
}

float triangle_wave(wav_fn_params* params) {
    return params->a * 2 / M_PI * asinf(
        sinf(triangle_wave_phi(params) + params->p)
    );
}

static float sawtooth_wave2(float phi, float p) {
    return - 2 / M_PI * atanf(1 / tanf(phi + p));    
}

static float sawtooth_wave_phi(const wav_fn_params* params) {
    return M_PI * params->f * params->x 
                / params->n;
}

static float sawtooth_wave_phi_frq(const wav_fn_params* params, float y_m) {
    return M_PI * params->f * (1 + y_m)
                / params->n;
}

float sawtooth_wave(wav_fn_params* params) {
    return - params->a * 2 / M_PI * atanf(
        1 / tanf(sawtooth_wave_phi(params) + params->p)
    );
}

float noise(wav_fn_params* params) {
    return params->a * (2 * ((float)rand() / RAND_MAX) - 1);
}

static wav_fn2* get_wav_fn2(wav_fn* fn) {
    if (fn == sine_wave) {
        return sine_wave2;
    }
    if (fn == triangle_wave) {
        return triangle_wave2;
    }
    if (fn == sawtooth_wave) {
        return sawtooth_wave2;
    }
    if (fn == pulse_wave) {
        return pulse_wave2;
    }
    return NULL;
}

static wav_fn_phi_frq* get_wav_phi_frq_fn(wav_fn* fn) {
    if (fn == sine_wave) {
        return sine_wave_phi_frq;
    }
    if (fn == triangle_wave) {
        return triangle_wave_phi_frq;
    }
    if (fn == sawtooth_wave) {
        return sawtooth_wave_phi_frq;
    }
    if (fn == pulse_wave) {
        return pulse_wave_phi_frq;
    }
    return NULL;
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

void gen_mod_wav(wav_mod_params params) {
    remove(params.out);
    int fd = open(params.out, O_CREAT | O_RDWR, 0666);
    if (fd == -1) {
        perror("open");
        exit(EXIT_FAILURE);
    }
    wav_hdr hdr = make_wav_hdr(
        params.duration,
        params.cparams.n
    );
    write(fd, (char*)&hdr, sizeof(wav_hdr));
    float init_ca = params.cparams.a;
    float c_phi = 0.0;
    for (int i = 0; i < hdr.sample_rate * params.duration; i++) {
        float y_m = 0.0, y_cm = 0.0;
        params.cparams.x = params.mparams.x = i;
        switch (params.type) {
            case MOD_AMPLITUDE: {
                y_m = params.modulation(&params.mparams);
                params.cparams.a = init_ca * (1 + y_m);
                y_cm = params.carrier(&params.cparams);
                break;
            }
            case MOD_FREQUENCY: {
                y_m = params.modulation(&params.mparams);
                c_phi += get_wav_phi_frq_fn(params.carrier)(&params.cparams, y_m);
                float temp_p_or_d = params.cparams.p;
                if (params.carrier == pulse_wave) {
                    temp_p_or_d = params.cparams.d;
                }
                y_cm = init_ca * get_wav_fn2(params.carrier)(c_phi, temp_p_or_d);
                break;
            }
        }
        int y = cut((INT_MAX) * y_cm);
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