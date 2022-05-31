import pygame

from pygame import Surface
from pygame.event import Event

from src.base.app import App
from src.base.scene import Scene

from typing import List

class FirstScene(Scene):
    def __init__(self, surface: Surface) -> None:
        super().__init__("first_scene", surface)

    def render(self, event: Event) -> None:
        self.surface.fill((0, 0, 255))

class SecondScene(Scene):
    def __init__(self, surface: Surface) -> None:
        super().__init__("second_scene", surface)

    def render(self, event: Event) -> None:
        self.surface.fill((255, 0, 0))

class Game(App):
    def __init__(self) -> None:
        super().__init__("game", (500, 500))
        self.__register_scenes()
        self.__subscribe_on_events()

    def __register_scenes(self) -> None:
        scenes: List[Scene] = [FirstScene(self.window), SecondScene(self.window)]
        for scene in scenes:
            self.register(scene)

    def __subscribe_on_events(self) -> None:
        first_scene: str = self.scenes[0].name
        second_scene: str = self.scenes[1].name
        self.scenes[0].subscribe(pygame.MOUSEBUTTONDOWN, callback=lambda e: self.switch(second_scene))
        self.scenes[1].subscribe(pygame.MOUSEBUTTONDOWN, callback=lambda e: self.switch(first_scene))