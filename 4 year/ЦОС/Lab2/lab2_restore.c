#include "parser.h"
#include <sys/stat.h>

typedef struct _main_params {
    const char* in;
    const char* out;
} main_params;

static void help(const char* name) {
    fprintf(stderr, "usage: %s [in] [out]\n", name);
    fprintf(stderr, "restores original .wav from *_amp.bin and *_phs.bin files.\n");
    fprintf(stderr, "  in - input pattern. program will search for\n"
                    "       (`in`_amp.bin and `in`_phs.bin) files.\n"
                    " out - path to restored file from spectrums.\n"
    );
}

static main_params parse_params(int argc, char** argv) {
    if (argc != 3) {
        help(argv[0]);
        exit(EXIT_FAILURE);
    }
    return (main_params){
        .in = argv[1],
        .out = argv[2]
    };
}

static void restore(const main_params* p) {
    int fds[2] = {0};
    size_t sizes[2] = {0};
    char paths[2][255] = {0};
    for (int i = 0; i < 2; i++) {
        struct stat st = {0};
        snprintf(paths[i], sizeof paths[i]-1, 
            "%s%s", p->in, postfixes[i+1]);
        if (stat(paths[i], &st) != -1) {
            sizes[i] = st.st_size;
        }
        fds[i] = open(paths[i], O_RDONLY, 0666);
        if (fds[i] == -1) {
            perror(paths[i]);
            exit(EXIT_FAILURE);
        }
    }
    assert(sizes[0] == sizes[1]);
    size_t k = sizes[0]/sizeof(float);
    float* amp = calloc(k, sizeof(float));
    float* phs = calloc(k, sizeof(float));
    for (size_t i = 0; i < k; i++) {
        read(fds[0], (char*)&amp[i], sizeof(float));
        read(fds[1], (char*)&phs[i], sizeof(float));
    }
    for (int i = 0; i < 2; i++) {
        close(fds[i]);
    }
    double* restored = rft((rft_params){
        .a = amp,
        .p = phs,
        .n = k,
        .d = 1
    });
    remove(p->out);
    gen_wav((wav_gen_params){
        .d = 1,
        .n = k,
        .out = p->out,
        .values = restored,
    });
    free(amp);
    free(phs);
    free(restored);
}

int main(int argc, char** argv) {
    main_params p = parse_params(argc, argv);
    restore(&p);
    return EXIT_SUCCESS;
}