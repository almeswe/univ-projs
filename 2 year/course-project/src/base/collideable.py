import pygame

from abc import ABC
from typing import Tuple

from constants import *

class Collideable(ABC):
    def __init__(self, size: Tuple[int, int]) -> None:
        self.x: int = 0
        self.y: int = 0
        self.size: Tuple[int, int] = size
        self.surface: pygame.Surface = pygame.Surface(self.size)

    def get_rect(self) -> pygame.Rect:
        return pygame.Rect(self.x, self.y, *self.size)

    def get_position(self) -> Tuple[int, int]:
        return (self.x, self.y)

    def set_position(self, position: Tuple[int, int]) -> None:
        self.x, self.y = position

    def collide(self, rect: pygame.Rect) -> bool:
        return self.get_rect().colliderect(rect)

if __name__ == '__main__':
    print('Try to run main.py')