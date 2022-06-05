from subprocess import call
from src.base.app import *
from src.game.scenes.menu import MenuScene
from src.game.scenes.game import GameScene

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
        super().__init__("game", (600, 600))
        self.__register_scenes()

    def __register_scenes(self) -> None:
        self.menu: MenuScene = MenuScene(self.window)
        self.game: GameScene = GameScene(self.window)
        self.menu.play_button.on_key_up += [self.__on_play_button_click]
        self.menu.quit_button.on_key_up += [self.__on_quit_button_click]
        self.game.back_to_menu_button.on_key_up += [self.__on_back_to_menu_button_click]
        self.game.subscribe(pygame.KEYDOWN, callback=self.__on_game_key_down)
        self.game.subscribe(pygame.KEYDOWN, callback=self.game.game_table.key_press_handler)
        self.register(self.menu)
        self.register(self.game)

    def __on_game_key_down(self, event: Event) -> None:
        if event.key == pygame.K_ESCAPE:
            self.switch('menu')

    def __on_play_button_click(self, event: Event) -> None:
        self.game.game_table.reset()
        self.game.game_table.update_delay(1500)
        self.switch('game')

    def __on_quit_button_click(self, event: Event) -> None:
        for scene in self.scenes:
            scene.stop()
            scene.release()
        pygame.quit()

    def __on_back_to_menu_button_click(self, event: Event) -> None:
        self.game.game_table.update_delay(1500)
        while not self.game.game_table.update_delay_finished():
            pygame.time.delay(200)
        self.switch('menu')

if __name__ == '__main__':
    print('Try to run main.py')