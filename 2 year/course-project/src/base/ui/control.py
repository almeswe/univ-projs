import pygame

from pygame import Surface
from pygame.event import Event

from abc import ABC
from abc import abstractmethod

from typing import Dict
from typing import List
from typing import Tuple
from typing import Callable

class UiControl(ABC):
    def __init__(self, size: Tuple[int, int], position: Tuple[int, int]) -> None:
        self.__init_scales(size, position)
        self.__init_context()

    def __init_scales(self, size: Tuple[int, int], position: Tuple[int, int]) -> None:
        self.size: Tuple[int, int] = size
        self.position: Tuple[int, int] = position
        self.rect: pygame.Rect = pygame.Rect(*self.position, *self.size)

    def __init_context(self) -> None:
        self.canvas: Surface = Surface(self.size)
        self.mapper: Dict[int, List[Callable]] = {}
        self.layers: List[Tuple[Surface, Tuple[int, int]]] = []

    def add_surface(self, surface: Surface, position: Tuple[int, int]) -> Surface:
        self.layers.append((surface, position))

    def surface(self) -> Surface:
        for layer in self.layers:
            self.canvas.blit(*layer)
        return self.canvas

    @abstractmethod
    def notify(event: Event) -> None:
        pass

    def map(self, event: int, handler: List[Callable]) -> None:
        self.mapper[event] = handler

    def notify_handlers(self, event: Event, handlers: List[Callable]) -> None:
        for handler in handlers:
            handler(event)

if __name__ == '__main__':
    pass