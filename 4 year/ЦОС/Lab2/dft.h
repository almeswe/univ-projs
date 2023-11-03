#ifndef _DFT_H_
#define _DFT_H_

#include <stdlib.h>
#include "common.h"

typedef struct _dft_params {
    int k, d;
    double* w;
} dft_params;

typedef struct _idft_params {
    int k, d;
    complex double* c;
} idft_params;

typedef struct _rft_params {
    int n, d;
    float* a;
    float* p;
} rft_params;

complex double* dft(const dft_params p);
double* idft(const idft_params p);
double* rft(const rft_params p);

#endif