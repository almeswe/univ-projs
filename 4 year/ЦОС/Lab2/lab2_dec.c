#include "parser.h"

typedef struct _main_params {
    int n, d, k;
    int fast;
    int amount;
    const char* out;
    wave waves[MAX_WAVES_AMOUNT];
} main_params;

static void help(const char* name) {
    fprintf(stderr, "usage: %s [out] [k] [n] [fast] [w1, w2 ... wn]\n", name);
    fprintf(stderr, "this program outputs three files *_amp.bin, *_phs.bin, *_frq.bin\n");
    fprintf(stderr, "which then can be used by other analyzing programs.\n");
    fprintf(stderr, "  out - out file path.\n"
                    "  k   - number of dft samples.\n"
                    "  n   - sample rate (in Hz).\n"
                    " fast - use fft instead of dft (1 or 0).\n"
                    "  w   - wave function (can be: `sine`, `trig`, `sawt`).\n"
                    "        specified in a format `a-f-p-w`.\n"
                    "        (a-amplitude, f-frequency, p-phase, w-wave function).\n"
    );
}

static main_params parse_params(int argc, char** argv) {
    int index = 1;
    main_params params = {
        .out = argv[index++]
    };
    if (argc < 6) {
        help(argv[0]);
        exit(EXIT_FAILURE);
    }
    params.k = atoi(argv[index++]);
    params.n = atoi(argv[index++]);
    params.fast = atoi(argv[index++]);
    params.d = 1;//atoi(argv[index++]);
    int count_of_waves = argc - index;
    if (count_of_waves <= 0) {
        fprintf(stderr, "specify at least one wave.\n");
        exit(EXIT_FAILURE);
    }
    for (int i = 0; i < count_of_waves; i++, params.amount++) {
        params.waves[params.amount] = parse_wave(argv[index++]);
        params.waves[params.amount].params.n = params.n;
    }
    return params;
}

static void decompose(main_params* p) {
    int fds[3] = {0};
    char paths[3][255] = {0};
    complex double* ft = NULL;
    double* g = gen((gen_params){
        .n = p->n,
        .d = p->d,
        .waves = p->waves,
        .amount = p->amount
    });
    gen_wav((wav_gen_params){
        .n = p->n,
        .d = p->d,
        .out = p->out,
        .values = g
    });
    if (p->fast) {
        ft = fft((fft_params){
            .k = p->k,
            .d = p->d,
            .w = g
        });
    }
    else {
        ft = dft((dft_params){
            .k = p->k,
            .d = p->d,
            .w = g
        });
    }
    for (int i = 0; i < 3; i++) {
        snprintf(paths[i], sizeof paths[i]-1, 
            "%s%s", p->out, postfixes[i]);
        remove(paths[i]);
        fds[i] = open(paths[i], O_CREAT | O_RDWR, 0666);
        if (fds[i] == -1) {
            perror(paths[i]);
            exit(EXIT_FAILURE);
        }
    }
    for (int k = 0; k < p->k; k++) {
        float y = k*p->n/(float)p->k;
        write(fds[0], (char*)&y, sizeof(float));
        y = cabsf(ft[k]) / p->k;
        write(fds[1], (char*)&y, sizeof(float));
        y = cargf(ft[k]);
        write(fds[2], (char*)&y, sizeof(float));
    }
    free(g);
    free(ft);
    for (int i = 0; i < 3; i++) {
        close(fds[i]);
    }
}

int main(int argc, char** argv) {
    main_params p = parse_params(argc, argv);
    assert(p.k <= p.n);
    decompose(&p);
    return EXIT_SUCCESS;
}