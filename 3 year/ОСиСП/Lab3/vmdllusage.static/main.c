#define _CRT_SECURE_NO_WARNINGS

#include "vmdll.h"
#include <stdio.h>

char cgstr[] = "failed for global array variable";
static char scgstr[] = "failed for static global array variable";

const char ccgstr[] = "failed for const global array variable";
const static char sccgstr[] = "failed for static const global array variable";

char* pcgstr = "failed for pointer to variable";
const char* cpcgstr = "failed for pointer to const variable";

int main(int argc, char** argv) {
    char* hstr = (char*)malloc(128);
    char lstr[] = "failed for stack variable";
    const char clstr[] = "failed for const stack variable";
    sprintf(hstr, "%s", "failed for heap variable");

    vmdll_init();
    vmdll_replace("failed", "workss");
    switch (vmdll_geterr()) {
    case VMDLL_SUCCESS:
        printf("%s\n", "done");
        break;
    case VMDLL_MALLOC_ERROR:
        printf("%s\n", "malloc allocation failure!");
        break;
    case VMDLL_NEEDS_INIT:
    case VMDLL_OPROC_ERROR:
        printf("%s\n", "OpenProcess function failure!");
        break;
    }
    vmdll_fini();
    printf("%s\n%s\n%s\n%s\n%s\n", lstr, cgstr, hstr, scgstr, clstr);
    printf("%s\n%s\n%s\n%s\n", ccgstr, sccgstr, pcgstr, cpcgstr);
    free(hstr);
    return 0;
}