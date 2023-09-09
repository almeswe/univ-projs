#include "common.h"

static void help(const char* name) {
    fprintf(stderr, "usage: %s [out] [a] [duty] [f] [n] [p] [d]\n", name);
    fprintf(stderr, " out  - out file path.\n"
                    "  a   - amplitude of pulse wave function.\n"
                    " duty - duty of the pulse wave.\n"
                    "  f   - frequency of pulse wave function (in Hz).\n"
                    "  n   - sample rate (in Hz).\n"
                    "  p   - phase.\n"
                    "  d   - duration of .wav file to be generated.\n"
    );
}

static wav_fn_params parse_params(char** argv) {
    return (wav_fn_params){
        .x = 0,
        .a = strtod(argv[2], NULL),
        .d = strtod(argv[3], NULL),
        .f = strtol(argv[4], NULL, 10),
        .n = strtol(argv[5], NULL, 10),
        .p = strtod(argv[6], NULL)
    };
}

int main(int argc, char** argv) {
    errno = 0;
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
        .wav_fn = pulse_wave,
        .wav_fn_params = params
    });
    return EXIT_SUCCESS;
}