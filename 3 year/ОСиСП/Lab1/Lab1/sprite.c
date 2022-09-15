#include "sprite.h"

#define sMoveX(s, vel) s->scales->left += (vel), s->scales->right += (vel)
#define sMoveY(s, vel) s->scales->top += (vel), s->scales->bottom += (vel)

VOID sprite_render(Sprite* sprite, HDC hdc) {
	FillRect(hdc, sprite->scales, sprite->brush);
}

VOID sprite_move(Sprite* sprite, SpriteDirection svec) {
	INT vel = sprite->velocity;
	switch (svec) {
		case SVEC_LEFT:
			if (sPosX(sprite) > 0) {
				sMoveX(sprite, -vel);
			}
			break;
		case SVEC_RIGHT:
			if (sPosXr(sprite) < WIN_WIDTH) {
				sMoveX(sprite, vel);
			}
			break;
		case SVEC_TOP:
			if (sPosY(sprite) > 0) {
				sMoveY(sprite, -vel);
			}
			break;
		case SVEC_BOTTOM:
			if (sPosYb(sprite) < WIN_HEIGHT) {
				sMoveY(sprite, vel);
			}
			break;
	}
}

Sprite* sprite_new() {
	Sprite* s = (Sprite*)xmalloc(sizeof(Sprite));
	s->brush = CreateSolidBrush(sBrushColor);
	s->scales = (PRECT)xmalloc(sizeof(RECT));
	sPosX(s) = 100, sPosY(s) = 100;
	sPosXr(s) = sPosX(s) + 50;
	sPosYb(s) = sPosY(s) + 50;
	s->velocity = 10;
	return s;
}

VOID sprite_free(Sprite* sprite) {
	if (sprite != NULL) {
		free(sprite->scales);
		free(sprite);
	}
}