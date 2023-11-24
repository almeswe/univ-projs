#include "parser.h"
#include <sys/stat.h>

typedef struct _main_params {
    f_fn* f;
    int v1, v2;
    const char* in;
    const char* out;
} main_params;

static void help(const char* name) {
    fprintf(stderr, "usage: %s [f] [in] [out] [v1] {v2}\n", name);
    fprintf(stderr, "restores original .wav from *_amp.bin and *_phs.bin files.\n");
    fprintf(stderr, "  f - type of filter (vals: `lp`, `hp`, `bp`)\n"
                    " in - input pattern.\n"
                    "out - out pattern.\n"
                    " v1 - value for filter (in Hz).\n"
                    " v2 - value for filter (in Hz).\n"
                    "      (optional, only in case of `bp`)\n"
    );
}

static main_params parse_params(int argc, char** argv) {
    if (argc != 5 && argc != 6) {
        help(argv[0]);
        exit(EXIT_FAILURE);
    }
    int index = 1;
    main_params params = {
        .f = parse_f_fn(argv[index++])
    };
    params.in = argv[index++];
    params.out = argv[index++];
    params.v1 = atoi(argv[index++]);
    if (params.f == bpf) {
        params.v2 = atoi(argv[index++]);        
    }
    if (argc - index != 0) {
        fprintf(stderr, "too much arguments passed.");
        exit(EXIT_FAILURE);
    }
    return params;
}

static void open_spectrums(const char* in, int* fds, size_t* sizes) {
    char paths[3][255] = {0};
    for (int i = 0; i < 3; i++) {
        struct stat st = {0};
        snprintf(paths[i], sizeof paths[i]-1, 
            "%s%s", in, postfixes[i]);
        if (stat(paths[i], &st) != -1) {
            sizes[i] = st.st_size;
        }
        fds[i] = open(paths[i], O_RDONLY, 0666);
        if (fds[i] == -1) {
            perror(paths[i]);
            exit(EXIT_FAILURE);
        }
    }
}

static void save_spectrums(const char* out, const f_res* res) {
    int fds[3];
    char paths[3][255] = {0};
    for (int i = 0; i < 3; i++) {
        snprintf(paths[i], sizeof paths[i]-1, 
            "%s%s", out, postfixes[i]);
        remove(paths[i]);
        fds[i] = open(paths[i], O_CREAT | O_RDWR, 0666);
        if (fds[i] == -1) {
            perror(paths[i]);
            exit(EXIT_FAILURE);
        }
    }
    for (size_t i = 0; i < res->count; i++) {
        write(fds[0], (char*)&res->f[i], sizeof(float));
        write(fds[1], (char*)&res->a[i], sizeof(float));
        write(fds[2], (char*)&res->p[i], sizeof(float));
    }
    for (int i = 0; i < 3; i++) {
        close(fds[i]);
    }
}

static void filter(const main_params* p) {
    int fds[3] = {0};
    size_t sizes[3] = {0};
    open_spectrums(p->in, fds, sizes);
    assert(sizes[0] == sizes[1]);
    assert(sizes[1] == sizes[2]);
    size_t k = sizes[0]/sizeof(float);
    float* frq = calloc(k, sizeof(float));
    float* amp = calloc(k, sizeof(float));
    float* phs = calloc(k, sizeof(float));
    for (size_t i = 0; i < k; i++) {
        read(fds[0], (char*)&frq[i], sizeof(float));
        read(fds[1], (char*)&amp[i], sizeof(float));
        read(fds[2], (char*)&phs[i], sizeof(float));
    }
    for (int i = 0; i < 3; i++) {
        close(fds[i]);
    }
    f_res res = p->f((f_params){
        .f = frq,
        .ia = amp,
        .ip = phs,
        .count = k,
        .v1 = p->v1,
        .v2 = p->v2,
    });
    save_spectrums(p->out, &res);
    free(amp);
    free(phs);
    free(frq);
    free(res.a);
    free(res.p);
    free(res.f);
}

int main(int argc, char** argv) {
    main_params p = parse_params(argc, argv);
    filter(&p);
    return EXIT_SUCCESS;
}