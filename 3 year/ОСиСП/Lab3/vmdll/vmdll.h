#ifndef _VMDLL_H
#define _VMDLL_H

#include "vmdlldefs.h"

typedef void VMDLL_INIT();
typedef vmdll_error VMDLL_GETERR();
typedef void VMDLL_REPLACE(const char*, const char*);
typedef void VMDLL_FINI();

#define ld_vmdll_init(module)		(VMDLL_INIT*)GetProcAddress(module, "vmdll_init");
#define ld_vmdll_fini(module)		(VMDLL_FINI*)GetProcAddress(module, "vmdll_fini");
#define ld_vmdll_geterr(module)		(VMDLL_GETERR*)GetProcAddress(module, "vmdll_geterr");
#define ld_vmdll_replace(module)	(VMDLL_REPLACE*)GetProcAddress(module, "vmdll_replace");

vmdll_import void vmdll_init();
vmdll_import vmdll_error vmdll_geterr();
vmdll_import void vmdll_replace(const char*, const char*);
vmdll_import void vmdll_fini();

#endif