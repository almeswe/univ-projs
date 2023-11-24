#ifndef _PARAM_PARSER_H
#define _PARAM_PARSER_H

#include "fft.h"
#include "gen.h"
#include "wav.h"
#include "filter.h"

static const char* postfixes[] = {
    "_frq.bin", "_amp.bin", "_phs.bin"
};

f_fn* parse_f_fn(const char* arg);
wave_fn* parse_wave_fn(const char* arg);
wave parse_wave(const char* arg);

#endif