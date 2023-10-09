#include "common.h"

static void help(const char* name) {
    fprintf(stderr, "usage: %s [out] [a] [f] [n] [p] [d] [w1, w2 ... wn]\n", name);
    fprintf(stderr, "  out - out file path.\n"
                    "  a   - amplitude of wave function.\n"
                    "  f   - frequency of wave function (in Hz).\n"
                    "  n   - sample rate (in Hz).\n"
                    "  p   - phase.\n"
                    "  d   - duration of .wav file to be generated.\n"
                    "  w   - wave function (vals: `sine`, `sawtooth`, `triangle`, `noise`, `pulse`).\n"
    );
}

static wav_fn_params parse_params(char** argv) {
    return (wav_fn_params){
        .x = 0,
        .d = 0.5,
        .a = strtod(argv[2], NULL),
        .f = strtod(argv[3], NULL),
        .n = strtol(argv[4], NULL, 10),
        .p = strtod(argv[5], NULL)
    };
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

int main(int argc, char** argv) {
    errno = 0;
    if (argc < 8) {
        help(argv[0]);
        exit(EXIT_FAILURE);
    }
    wav_fn_params params = parse_params(argv);
    int duration = strtol(argv[6], NULL, 10);
    if (errno != 0) {
        perror("argv (strtol/d): ");
        exit(EXIT_FAILURE);
    }
    int count = argc - 7;
    wav_fn** wavs = (wav_fn**)calloc(count, sizeof(wav_fn*));
    for (int i = 0; i < count; i++) {
        wavs[i] = parse_wave_fn(argv[i + 7]);
    }
    merge_wavs((wav_merge_params){
        .out = argv[1],
        .wavs = wavs,
        .count = count,
        .duration = duration,
        .wav_fn_params = params
    });
    free(wavs);
    return EXIT_SUCCESS;
}