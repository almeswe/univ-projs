#include "fft.h"

complex double* subfft(int n, const complex double* v) {
    complex double* y = NULL;
    if (n == 2) {
        y = calloc(2, sizeof(complex double));
        y[0] = v[0] + v[1];
        y[1] = v[0] - v[1];
    }
    else {
        complex double* o = calloc(n/2, sizeof(complex double));
        complex double* e = calloc(n/2, sizeof(complex double));
        for (int k = 0; k < n/2; k++) {
            e[k] = v[2*k];
            o[k] = v[2*k+1];
        }
        complex double* ye = subfft(n/2, e);
        complex double* yo = subfft(n/2, o);
        y = calloc(n, sizeof(complex double));
        for (int k = 0; k < n/2; k++) {
            complex double wk = cexp(-2.0*M_PI*I*k/n);
            y[  k  ] = ye[k] + wk * yo[k];
            y[k+n/2] = ye[k] - wk * yo[k];
        }
        free(e); 
        free(o);
        free(ye); 
        free(yo);
    }
    return y;
}

complex double* fft(fft_params params) {
    complex double* tr = NULL;
    complex double* ti = calloc(params.k, sizeof(complex double));
    for (int k = 0; k < params.k; k++) {
        ti[k] = params.w[k];
    }
    tr = subfft(params.k, ti);
    free(ti);
    return tr;
}

complex double* subifft(int n, const complex double* v) {
    complex double* y = NULL;
    if (n == 2) {
        y = calloc(2, sizeof(complex double));
        y[0] = v[0] + v[1];
        y[1] = v[0] - v[1];
    }
    else {
        complex double* o = calloc(n/2, sizeof(complex double));
        complex double* e = calloc(n/2, sizeof(complex double));
        for (int k = 0; k < n/2; k++) {
            e[k] = v[2*k];
            o[k] = v[2*k+1];
        }
        complex double* ye = subifft(n/2, e);
        complex double* yo = subifft(n/2, o);
        y = calloc(n, sizeof(complex double));
        for (int k = 0; k < n/2; k++) {
            complex double wk = cexp(2.0*M_PI*I*k/n);
            y[  k  ] = (ye[k] + wk * yo[k]) / n;
            y[k+n/2] = (ye[k] - wk * yo[k]) / n;
        }
        free(e); 
        free(o);
        free(ye); 
        free(yo);
    }
    return y;
}

double* ifft(idft_params params) {
    complex double* tcr = subifft(params.k, params.c);
    double* tr = calloc(params.k, sizeof(double));
    for (int k = 0; k < params.k; k++) {
        tr[k] = creal(tcr[k]);
    }
    free(tcr);
    return tr;
}