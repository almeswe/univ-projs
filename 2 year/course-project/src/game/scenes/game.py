from src.base.scene import *

from src.game.table import *
from src.game.ui.buttons import *

class GameScene(Scene):
    def __init__(self, surface: Surface) -> None:
        super().__init__('game', surface)
        self.__init_constants()
        self.__init_game_objects()

    def __init_constants(self) -> None:
        self.upper_board_height: int = self.surface.get_size()[1] * 0.1

    def __init_game_objects(self) -> None:
        self.game_table: Table = Table(self)
        self.back_to_menu_button: Button = MenuButton('DISCONNECT', (250, 50),
            (10, self.surface.get_height()-self.game_table.bottom_board_height+10))
        self.register_control(self.back_to_menu_button)

    def render(self) -> None:
        self.surface.fill((32, 32, 32))
        super().render()
        self.game_table.render()

if __name__ == '__main__':
    print('Try to run main.py')