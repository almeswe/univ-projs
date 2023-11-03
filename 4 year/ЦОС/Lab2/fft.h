#ifndef _FFT_H_
#define _FFT_H_

#include "dft.h"

typedef dft_params fft_params; 
typedef idft_params ifft_params;

complex double* fft(fft_params params);
double* ifft(idft_params params);

#endif