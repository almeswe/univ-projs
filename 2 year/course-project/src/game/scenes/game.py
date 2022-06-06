from src.base.scene import *

from src.game.table import *
from src.game.ui.buttons import *

class GameScene(Scene):
    def __init__(self, surface: Surface, app: object) -> None:
        super().__init__('game', surface, app)
        self.set_game_table()
        self.__init_game_objects()
        self.__init_events()

    def __init_events(self) -> None:
        self.subscribe(pygame.KEYDOWN, self.__on_key_down)
        self.subscribe(pygame.KEYDOWN, self.game_table.key_press_event_provider)
        self.disconnect_button.on_key_up += [self.__on_back_to_menu_button_click]

    def set_game_table(self) -> None:
        self.game_table: Table = Table(self)

    def __init_game_objects(self) -> None:
        self.disconnect_button: Button = MenuButton('DISCONNECT', (250, 50),
            (10, self.surface.get_height()-self.game_table.bottom_board_height+10))
        self.register_control(self.disconnect_button)

    def __on_key_down(self, event: Event) -> None:
        if event.key == pygame.K_ESCAPE:
            self.__on_back_to_menu_button_click(event)

    def __on_back_to_menu_button_click(self, event: Event) -> None:
        self.game_table.request_game_finish()

    def start(self) -> None:
        self.game_table.reset()
        self.game_table.reset_scores()
        self.game_table.update_delay(1000)
        return super().start()

    def render(self) -> None:
        super().render()
        self.game_table.render()

if __name__ == '__main__':
    print('Try to run main.py')