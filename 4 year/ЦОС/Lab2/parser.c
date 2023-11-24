#include "parser.h"

f_fn* parse_f_fn(const char* arg) {
    if (strcmp(arg, LPF_STR) == 0) {
        return lpf;
    }
    if (strcmp(arg, HPF_STR) == 0) {
        return hpf;
    }
    if (strcmp(arg, BPF_STR) == 0) {
        return bpf;
    }
    fprintf(stderr, "unknown filter: `%s`\n", arg);
    exit(EXIT_FAILURE);
    return NULL;
}

wave_fn* parse_wave_fn(const char* arg) {
    if (strcmp(arg, "sine") == 0) {
        return sine;
    }
    if (strcmp(arg, "trig") == 0) {
        return trig;
    }
    if (strcmp(arg, "sawt") == 0) {
        return sawt;
    }
    fprintf(stderr, "unknown wave: `%s`\n", arg);
    exit(EXIT_FAILURE);
    return NULL;
}

wave parse_wave(const char* arg) {
    char buf[5] = {0};
    wave wave = {0};
    sscanf(arg, "%lf-%lf-%lf-%4s",
        &wave.params.a, 
        &wave.params.f, 
        &wave.params.p,
        buf
    );
    if (errno != 0) {
        perror(arg);
        exit(EXIT_FAILURE);
    }
    wave.fn = parse_wave_fn(buf);
    return wave;
}