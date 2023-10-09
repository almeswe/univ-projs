#include "common.h"

static void help(const char* name) {
    fprintf(stderr, "usage: %s [out] [w] {pd} [a] [f] [n] [p] [d]\n", name);
    fprintf(stderr, "  out - out file path.\n"
                    "  w   - wave function (vals: `sine`, `sawtooth`, `triangle`, `noise`, `pulse`).\n"
                    "  pd  - duty (in case when wave is pulse wave).\n"
                    "  a   - amplitude of wave function.\n"
                    "  f   - frequency of wave function (in Hz).\n"
                    "  n   - sample rate (in Hz).\n"
                    "  p   - phase.\n"
                    "  d   - duration of .wav file to be generated.\n"
    );
}

static wav_fn* parse_wave_fn(const char* arg) {
    if (strcmp(arg, NOISE_STR) == 0) {
        return noise;
    }    
    if (strcmp(arg, SINE_WAVE_STR) == 0) {
        return sine_wave;
    }
    if (strcmp(arg, PULSE_WAVE_STR) == 0) {
        return pulse_wave;
    }
    if (strcmp(arg, SAWTOOTH_WAVE_STR) == 0) {
        return sawtooth_wave;
    }
    if (strcmp(arg, TRIANGLE_WAVE_STR) == 0) {
        return triangle_wave;
    }
    fprintf(stderr, "cannot recognize wave: `%s`\n", arg);
    exit(EXIT_FAILURE);
    return NULL;
}

static wav_gen_params parse_params(char** argv) {
    int index = 1;
    wav_gen_params params = {0};
    params.out = argv[index++];
    params.wav_fn = parse_wave_fn(argv[index++]);
    if (params.wav_fn == pulse_wave) {
        params.wav_fn_params.d = strtod(argv[index++], NULL);
    }
    params.wav_fn_params.a = strtod(argv[index++], NULL);
    params.wav_fn_params.f = strtod(argv[index++], NULL);
    params.wav_fn_params.n = strtod(argv[index++], NULL);
    params.wav_fn_params.p = strtod(argv[index++], NULL);
    params.duration = atoi(argv[index++]);
    return params;
}

int main(int argc, char** argv) {
    errno = 0;
    srand(time(NULL));
    if (argc != 8 &&
        argc != 9) {
        help(argv[0]);
        exit(EXIT_FAILURE);
    }
    gen_wav(parse_params(argv));
    return EXIT_SUCCESS;
}