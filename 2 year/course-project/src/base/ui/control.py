import pygame
import pygame.display

from abc import ABC
from typing import Tuple

class UiControl(ABC):
    def __init__(self, size: Tuple[int, int]) -> None:
        self.surface: pygame.Surface = pygame.Surface(size)

    def render(self) -> None:
        pass

if __name__ == '__main__':
    pass