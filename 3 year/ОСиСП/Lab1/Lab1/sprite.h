#ifndef _SPRITE_H
#define _SPRITE_H

#include "xmemory.h"
#include "winscales.h"

#include <windows.h>

#define sPosX(s)	s->scales->left
#define sPosXr(s)	s->scales->right
#define sPosY(s)	s->scales->top
#define sPosYb(s)	s->scales->bottom

#define sWidth(s)	(sPosXr(s) - sPosX(s)) 
#define sHeight(s)	(sPosYb(s) - sPosY(s)) 

#define rWidth(r)		(r.right-r.left)
#define rHeight(r)		(r.bottom-r.top)

#define sBrushColor RGB(129, 199, 125)

typedef enum _SpriteDirection {
	SVEC_LEFT,
	SVEC_RIGHT,
	SVEC_TOP,
	SVEC_BOTTOM
} SpriteDirection;

typedef struct _Sprite {
	HBRUSH brush;
	PRECT scales;
	INT velocity;
} Sprite;

Sprite* sprite_new();
VOID sprite_move(Sprite* sprite, SpriteDirection svec);
VOID sprite_free(Sprite* sprite);
VOID sprite_render(Sprite* sprite, HDC hdc);

#endif