import math
import pygame

from pygame import Surface
from typing import Tuple

class Ball(object):
    def __init__(self, position: Tuple[int, int]) -> None:
        self.dv_x: int = 20
        self.dv_y: int = 18
        self.x: int = position[0]
        self.y: int = position[1]
        self.surface: Surface = Surface((20, 20))
        self.rect: pygame.Rect = self.surface.get_rect()

    def reverse_x_velocity(self) -> None:
        self.dv_x *= -1

    def reverse_y_velocity(self) -> None:
        self.dv_y *= -1

    def render(self) -> Surface:
        pygame.draw.circle(self.surface, (255, 255, 255),
            self.rect.center, self.rect.width//2-1)
        return self.surface

if __name__ == '__main__':
    print('Try to run main.py')