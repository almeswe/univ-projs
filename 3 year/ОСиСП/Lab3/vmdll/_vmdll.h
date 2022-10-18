#ifndef _VMDLL_PRIVATE_H
#define _VMDLL_PRIVATE_H

#include "vmdlldefs.h"

vmdll_export void vmdll_init();
vmdll_export enum vmdll_error vmdll_geterr();
vmdll_export void vmdll_replace(const char* src, const char* pholder);
vmdll_export void vmdll_fini();

#endif