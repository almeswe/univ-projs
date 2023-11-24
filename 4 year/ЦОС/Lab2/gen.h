#ifndef _GEN_WAVES_H
#define _GEN_WAVES_H

#include "common.h"
#include <assert.h>
#include <stdlib.h>

#define MAX_WAVES_AMOUNT 16

typedef struct _wave_fn_params {
    int n;
    double a, f, p;
} wave_fn_params;

typedef double (wave_fn)(double, wave_fn_params*);

typedef struct _wave {
    wave_fn* fn;
    wave_fn_params params;
} wave;

typedef struct _gen_params {
    int n, d;
    int amount;
    wave* waves;
} gen_params;

double sine(double x, wave_fn_params* p);
double trig(double x, wave_fn_params* p);
double sawt(double x, wave_fn_params* p);
//double impl(double x, wave_fn_params* p);

double* gen(gen_params p);

#endif