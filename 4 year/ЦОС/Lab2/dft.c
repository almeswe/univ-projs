#include "dft.h"

complex double* dft(const dft_params p) {
    complex double* t = calloc(p.k, sizeof(complex double));
    for (int k = 0; k < p.k; k++) {
        for (int n = 0; n < p.k; n++) {
            t[k] += p.w[n]*cexp(-2*M_PI*I*k*n/(double)p.k);
        }
    }
    return t;
}

double* idft(const idft_params p) {
    double* wave = calloc(p.k, sizeof(double));
    for (int n = 0; n < p.k; n++) {
        for (int k = 0; k < p.k; k++) {
            wave[n] += p.c[k]*cexp(2*M_PI*I*k*n/(double)p.k);
        }
        wave[n] /= p.k;
    }
    return wave;
}

double* rft(const rft_params p) {
    double* restored = calloc(p.n, sizeof(double));
    for (int i = 0; i < p.n; i++) {
        for (int j = 0; j < p.n; j++) {
            restored[i] += p.a[j]*-cosf(2*M_PI*i*j/p.n-p.p[j]);
        }
    }
    return restored;
}