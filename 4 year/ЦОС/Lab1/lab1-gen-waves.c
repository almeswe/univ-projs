#include "common.h"

static void help(const char* name) {
    fprintf(stderr, "usage: %s [out] [w] [a] [f] [n] [p] [d]\n", name);
    fprintf(stderr, "  out - out file path.\n"
                    "  w   - wave function (vals: `sine`, `sawtooth`, `triangle`, `noise`).\n"
                    "  a   - amplitude of wave function.\n"
                    "  f   - frequency of wave function (in Hz).\n"
                    "  n   - sample rate (in Hz).\n"
                    "  p   - phase.\n"
                    "  d   - duration of .wav file to be generated.\n"
    );
}

static wav_fn_params parse_params(char** argv) {
    return (wav_fn_params){
        .x = 0,
        .a = strtod(argv[3], NULL),
        .f = strtol(argv[4], NULL, 10),
        .n = strtol(argv[5], NULL, 10),
        .p = strtod(argv[6], NULL)
    };
}

static wav_fn* parse_wave_fn(const char* arg) {
    if (strcmp(arg, NOISE_STR) == 0) {
        return noise;
    }    
    if (strcmp(arg, SINE_WAVE_STR) == 0) {
        return sine_wave;
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
    srand(time(NULL));
    if (argc != 8) {
        help(argv[0]);
        exit(EXIT_FAILURE);
    }
    wav_fn_params params = parse_params(argv);
    const char* out = argv[1];
    int duration = strtol(argv[7], NULL, 10);
    if (errno != 0) {
        perror("argv (strtol/d): ");
        exit(EXIT_FAILURE);
    }
    gen_wav((wav_gen_params){
        .out = out,
        .duration = duration,
        .wav_fn = parse_wave_fn(argv[2]),
        .wav_fn_params = params
    });
    return EXIT_SUCCESS;
}