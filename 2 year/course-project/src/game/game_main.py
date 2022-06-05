from src.base.app import *
from src.game.scenes.menu import *
from src.game.scenes.game import *

class Game(App):
    def __init__(self) -> None:
        super().__init__("game", (700, 700))
        self.__register_scenes()

    def __register_scenes(self) -> None:
        self.menu: MenuScene = MenuScene(self.window)
        self.game: GameScene = GameScene(self.window)
        self.menu.play_button.on_key_up += [self.__on_play_button_click]
        self.menu.quit_button.on_key_up += [self.__on_quit_button_click]
        self.game.back_to_menu_button.on_key_up += [self.__on_back_to_menu_button_click]
        self.game.subscribe(pygame.KEYDOWN, callback=self.__on_game_key_down)
        self.game.subscribe(pygame.KEYDOWN, callback=self.game.game_table.key_press_event_provider)
        self.game.subscribe(pygame.USEREVENT, callback=self.__on_game_userevent)
        self.register(self.menu)
        self.register(self.game)

    def __on_game_key_down(self, event: Event) -> None:
        if event.key == pygame.K_ESCAPE:
            self.switch('menu')

    def __on_play_button_click(self, event: Event) -> None:
        self.game.game_table.reset()
        self.game.game_table.reset_scores()
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

    def __on_game_userevent(self, event: Event) -> None:
        if hasattr(event, 'pong_finish_game_request') != None:
            self.game.game_table.update_delay(1500)
            while not self.game.game_table.update_delay_finished():
                pygame.time.delay(200)
            self.switch('menu')

if __name__ == '__main__':
    print('Try to run main.py')