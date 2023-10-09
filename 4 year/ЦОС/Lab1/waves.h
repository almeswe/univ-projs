#ifndef _WAVES_H
#define _WAVES_H

#include <math.h>
#include <stdint.h>
#include <limits.h>

#ifndef M_PI
    #define M_PI 3.14159265358979323846
#endif

typedef struct _wav_fn_params {
    float a;
    float x; 
    float p;
    float d;
    float f;
    int32_t n;
} wav_fn_params;

typedef float (wav_fn)(wav_fn_params*);

float sine_wave(wav_fn_params* params);
float pulse_wave(wav_fn_params* params);
float triangle_wave(wav_fn_params* params);
float sawtooth_wave(wav_fn_params* params);
float noise(wav_fn_params* params);

#endif