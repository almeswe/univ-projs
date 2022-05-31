import pygame
import pygame.key 
import pygame.event
import pygame.display

from pygame import Surface
from pygame.event import Event

from typing import Dict
from typing import List
from typing import Callable

from abc import ABC

class Scene(ABC):
    def __init__(self, name: str, surface: Surface) -> None:
        self.name: str = name
        self.surface: Surface = surface
        self.__init_context()
        self.__init_base_events()

    def __init_context(self) -> None:
        self.current: bool = False
        self.callbacks: Dict[int, List[Callable]] = {}

    def __init_base_events(self) -> None:
        def __quit(event: Event) -> None:
            self.current = False
            pygame.quit()
        self.subscribe(pygame.QUIT, callback=__quit)

    def start(self) -> None:
        self.current = True
        self.__run_event_loop()

    def stop(self) -> None:
        self.current = False

    def render(self, event: Event) -> None:
        pass

    def notify(self, event: Event) -> None: 
        if event.type in self.callbacks.keys():
            for callback in self.callbacks[event.type]:
                callback.__call__(event)

    def subscribe(self, event: int, callback: Callable) -> None:
        if event in self.callbacks.keys():
            self.callbacks[event].append(callback)
        else:
            self.callbacks[event] = [callback]

    def unsubscribe(self, event: int, callback: Callable) -> None:
        if event in self.callbacks.keys():
            self.callbacks[event].remove(callback)

    def __run_event_loop(self) -> None:
        while self.current:
            for event in pygame.event.get():
                self.notify(event)
                if not self.current:
                    break
                self.render(event)
                pygame.display.update()

if __name__ == '__main__':
    pass