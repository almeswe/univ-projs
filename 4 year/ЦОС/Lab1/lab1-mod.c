#include "common.h"

static void help(const char* name) {
    fprintf(stderr, "usage: %s [out] [c] {cd} [m] {md} [t] [ca] [cf] [ma] [mf] [n] [p] [d]\n", name);
    fprintf(stderr, "  out - out file path.\n"
                    "  c   - carrier wave (vals: `sine`, `sawtooth`, `triangle`, `pulse`, `noise`).\n"
                    "  cd  - duty (if carrier wave is pulse wave).\n"
                    "  m   - modulation wave (vals: same as carrier).\n"
                    "  md  - duty (if modulation wave is pulse wave).\n"
                    "  t   - type of modulation. (vals: `amp`, `frq`).\n"
                    "  ca  - amplitude of carrier function.\n"
                    "  cf  - frequency of carrier function (in Hz).\n"
                    "  ma  - amplitude of modulation function.\n"
                    "  mf  - frequency of modulation function (in Hz).\n"
                    "  n   - sample rate (in Hz).\n"
                    "  p   - phase.\n"
                    "  d   - duration of .wav file to be generated.\n"
    );
}

static wav_mod_type parse_mod_type(const char* arg) {
    if (strcmp(arg, AMP_MODULATION) == 0) {
        return MOD_AMPLITUDE;
    }    
    if (strcmp(arg, FRQ_MODULATION) == 0) {
        return MOD_FREQUENCY;
    }
    fprintf(stderr, "cannot recognize modulation type: `%s`\n", arg);
    exit(EXIT_FAILURE);
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

static wav_mod_params parse_params(char** argv) {
    int index = 1;
    wav_mod_params params = { 0 };
    params.out = argv[index++];
    params.carrier = parse_wave_fn(argv[index++]);
    if (params.carrier == pulse_wave) {
        params.cparams.d = strtod(argv[index++], NULL);
    }
    params.modulation = parse_wave_fn(argv[index++]);
    if (params.modulation == pulse_wave) {
        params.mparams.d = strtod(argv[index++], NULL);
    }
    params.type = parse_mod_type(argv[index++]);
    params.cparams.a = strtod(argv[index++], NULL);
    params.cparams.f = strtod(argv[index++], NULL);
    params.mparams.a = strtod(argv[index++], NULL);
    params.mparams.f = strtod(argv[index++], NULL);
    params.cparams.n = params.mparams.n = strtod(argv[index++], NULL);
    params.cparams.p = params.mparams.p = strtod(argv[index++], NULL);
    params.duration = atoi(argv[index++]);
    return params;
}


int main(int argc, char** argv) {
    errno = 0;
    srand(time(NULL));
    if (argc != 12 && 
        argc != 13 &&
        argc != 14) {
        help(argv[0]);
        exit(EXIT_FAILURE);
    }
    gen_mod_wav(parse_params(argv));
    return EXIT_SUCCESS;
}