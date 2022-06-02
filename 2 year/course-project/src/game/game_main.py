from src.base.app import *
from src.game.scenes.menu import MenuScene

# todo: do something with layer representation (probably change name, or store like independent object instead of (Surface, size) )

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

    def __register_scenes(self) -> None:
        menu: MenuScene = MenuScene(self.window)
        menu.quit_button.on_key_up += [self.__on_quit_button_click]
        self.register(menu)

    def __on_quit_button_click(self, event: Event) -> None:
        self.scenes[0].stop()
        pygame.quit()
