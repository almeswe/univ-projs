from subprocess import call
from src.base.app import *
from src.game.scenes.menu import MenuScene
from src.game.scenes.game import GameScene

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
        game: GameScene = GameScene(self.window)
        menu.play_button.on_key_up += [self.__on_play_button_click]
        menu.quit_button.on_key_up += [self.__on_quit_button_click]
        game.subscribe(pygame.KEYDOWN, callback=self.__on_game_key_down)
        game.subscribe(pygame.KEYUP, callback=game.game_table.key_press_handler)
        game.subscribe(pygame.KEYDOWN, callback=game.game_table.key_press_handler)
        self.register(menu)
        self.register(game)

    def __on_game_key_down(self, event: Event) -> None:
        if event.key == pygame.K_ESCAPE:
            self.switch('menu')

    def __on_play_button_click(self, event: Event) -> None:
        self.switch('game')

    def __on_quit_button_click(self, event: Event) -> None:
        for scene in self.scenes:
            scene.stop()
            scene.release()
        pygame.quit()

if __name__ == '__main__':
    print('Try to run main.py')