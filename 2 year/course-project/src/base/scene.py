import pygame
import pygame.key 
import pygame.time
import pygame.event
import pygame.display

from pygame import Surface

from typing import Dict
from typing import List
from typing import Callable

from src.base.ui.control import *

class Scene(ABC):
    def __init__(self, name: str, surface: Surface) -> None:
        self.name: str = name
        self.surface: Surface = surface
        self.controls: List[UiControl] = []
        self.__init_context()
        self.__init_base_events()

    def __init_context(self) -> None:
        self.current: bool = False
        self.callbacks: Dict[int, List[Callable]] = {}

    def __init_base_events(self) -> None:
        def __quit(event: Event) -> None:
            self.stop()
            self.release()
            pygame.quit()
        self.subscribe(pygame.QUIT, __quit)

    def stop(self) -> None:
        self.current = False

    def start(self) -> None:
        self.current = True
        self.__run_event_loop()

    def release(self) -> None:
        pass

    def render(self) -> None:
        if pygame.get_init():
            for control in self.controls:
                self.surface.blit(control.render(), control.position)

    def notify(self, event: Event) -> None: 
        if event.type in self.callbacks.keys():
            for callback in self.callbacks[event.type]:
                callback.__call__(event)
        self.notify_controls(event)

    def notify_controls(self, event: Event) -> None:
        for control in self.controls:
            control.notify(event)

    def subscribe(self, event: int, callback: Callable) -> None:
        if event in self.callbacks.keys():
            self.callbacks[event].append(callback)
        else:
            self.callbacks[event] = [callback]

    def unsubscribe(self, event: int, callback: Callable) -> None:
        if event in self.callbacks.keys():
            self.callbacks[event].remove(callback)

    def register_control(self, control: UiControl) -> None:
        self.controls.append(control)

    def __run_event_loop(self) -> None:
        self.clocks: pygame.time.Clock = pygame.time.Clock()
        while self.current:
            self.frame_dt: int = self.clocks.tick(60)
            for event in pygame.event.get():
                self.notify(event)
            self.render()
            if pygame.get_init():
                pygame.display.update()

if __name__ == '__main__':
    print('Try to run main.py')