#ifndef _FREQUENCY_FILTER_H
#define _FREQUENCY_FILTER_H

#define LPF_STR "lp"
#define HPF_STR "hp"
#define BPF_STR "bp"

#include <stdlib.h>

typedef enum _f_type {
    LP_FILTER,
    HP_FILTER,
    BP_FILTER
} f_type;

typedef struct _f_res {
    int count;
    float* a;
    float* p;
    float* f;
} f_res;

typedef struct _f_params {
    int count;        // count of f & ia & ip.
    int v1, v2;       // params for filtering (both are frequncy values)
    const float* f;  // frequencies
    const float* ia; // initial amplitudes
    const float* ip; // initial phases
} f_params;

typedef f_res(f_fn)(f_params);

f_res lpf(f_params p);
f_res hpf(f_params p);
f_res bpf(f_params p);

#endif