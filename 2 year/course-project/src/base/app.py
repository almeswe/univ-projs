import pygame
import pygame.key 
import pygame.event
import pygame.display

from pygame import Surface

from typing import List
from typing import Tuple

from abc import ABC
from src.base.scene import Scene

class AppException(Exception): 
    def __init__(self, *args: object) -> None:
        super().__init__(*args)

class App(ABC):
    def __init__(self, name: str, size: Tuple[int, int]) -> None:
        pygame.init()
        self.__init_scenes()
        self.__init_surface(name, size)

    def __init_surface(self, name: str, size: Tuple[int, int]) -> None:
        pygame.display.set_caption(name)
        self.window: Surface = pygame.display.set_mode(size)

    def __init_scenes(self) -> None:
        self.current: Scene = None
        self.scenes: List[Scene] = []

    def run(self) -> None:
        self.scenes[0].start()

    def switch(self, scene: str) -> None:
        if self.current != None:
            self.current.stop()
        if scene not in [s.name for s in self.scenes]:
            raise AppException(f'Scene \'{scene}\' is not registered.')
        self.current = self.scenes[[s.name for s in self.scenes].index(scene)]
        self.current.start()

    def register(self, scene: Scene) -> None:
        for s in self.scenes:
            if s.name == scene.name:
                raise AppException(f'Scene \'{scene.name}\' is registered.')
        self.scenes.append(scene)

if __name__ == '__main__':
    pass