#include "filter.h"

f_res lpf(f_params p) {
    float* ra = calloc(p.count, sizeof(float));
    float* rp = calloc(p.count, sizeof(float));
    float* rf = calloc(p.count, sizeof(float));
    for (int i = 0; i < p.count; i++) {
        rf[i] = p.f[i];
        ra[i] = (p.v1 >= p.f[i]) ? p.ia[i] : 0.0;
        rp[i] = (p.v1 >= p.f[i]) ? p.ip[i] : 0.0;
    }
    return (f_res){
        .f = rf,
        .a = ra,
        .p = rp,
        .count = p.count
    };
}

f_res hpf(f_params p) {
    float* ra = calloc(p.count, sizeof(float));
    float* rp = calloc(p.count, sizeof(float));
    float* rf = calloc(p.count, sizeof(float));
    for (int i = 0; i < p.count; i++) {
        rf[i] = p.f[i];
        ra[i] = (p.v1 < p.f[i]) ? p.ia[i] : 0.0;
        rp[i] = (p.v1 < p.f[i]) ? p.ip[i] : 0.0;
    }
    return (f_res){
        .f = rf,
        .a = ra,
        .p = rp,
        .count = p.count
    };
}

f_res bpf(f_params p) {
    float* ra = calloc(p.count, sizeof(float));
    float* rp = calloc(p.count, sizeof(float));
    float* rf = calloc(p.count, sizeof(float));
    for (int i = 0; i < p.count; i++) {
        rf[i] = p.f[i];
        ra[i] = (p.v2 >= p.f[i] && p.v1 <= p.f[i]) ? p.ia[i] : 0.0;
        rp[i] = (p.v2 >= p.f[i] && p.v1 <= p.f[i]) ? p.ip[i] : 0.0;
    }
    return (f_res){
        .f = rf,
        .a = ra,
        .p = rp,
        .count = p.count
    };
}