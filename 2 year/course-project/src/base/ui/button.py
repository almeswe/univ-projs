from src.base.ui.control import *

class Button(UiControl):
    def __init__(self, size: Tuple[int, int], position: Tuple[int, int]) -> None:
        super().__init__(size, position)
        self.on_key_up    : List[Callable] = []
        self.on_key_down  : List[Callable] = []
        self.on_key_motion: List[Callable] = []
        self.map(pygame.MOUSEBUTTONUP, self.on_key_up)
        self.map(pygame.MOUSEBUTTONDOWN, self.on_key_down)
        self.map(pygame.MOUSEMOTION, self.on_key_motion)

    def notify(self, event: Event) -> None:
        if event.type in self.mapper.keys():
            if self.rect.collidepoint(event.pos):
                self.notify_handlers(event, self.mapper[event.type])

if __name__ == '__main__':
    print('Try to run main.py')