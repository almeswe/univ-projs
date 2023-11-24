#include "gen.h"

double sine(double x, wave_fn_params* p) {
    return p->a * sin(
        2 * M_PI * p->f * x
                 / p->n
    );
}

double trig(double x, wave_fn_params* p){
    return 2 / M_PI * asin(
        sin(
            2 * M_PI * p->f * x
                     / p->n + p->p
        )
    );
}

double sawt(double x, wave_fn_params* p){
    return - 2 / M_PI * atan(
        1 / tan(
            M_PI * p->f * x 
                 / p->n + p->p
        )
    );
}

double* gen(gen_params p) {
    assert(p.amount > 0);
    assert(p.amount < MAX_WAVES_AMOUNT);
    wave* waves = p.waves;
    const int rate = p.n;
    const int duration = p.d;
    double* g = (double*)calloc(rate * duration, sizeof(double));
    assert(g != NULL);
    for (int w = 0; w < p.amount; w++) {
        assert(rate == waves[w].params.n);
        for (int x = 0; x < rate * duration; x++) {
            g[x] += waves[w].fn(x, &waves[w].params);
        }
    }
    return g;
}