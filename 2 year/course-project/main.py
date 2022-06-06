from src.base.app import *
from src.game.scenes.menu import *
from src.game.scenes.game import *
from src.game.scenes.game_net import *

class Game(App):
    def __init__(self) -> None:
        super().__init__(APP_TITLE, (700, 700), 'icon.png')
        self.__register_scenes()

    def __register_scenes(self) -> None:
        self.menu: MenuScene = MenuScene(self.window, self)
        self.game: GameScene = GameScene(self.window, self)
        self.game_net: GameNetScene = GameNetScene(self.window, self)
        self.register(self.menu)
        self.register(self.game)
        self.register(self.game_net)

if __name__ == '__main__':
    Game().run()