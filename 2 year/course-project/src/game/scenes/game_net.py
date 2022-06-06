from src.game.scenes.game import *

class GameNetScene(GameScene):
    def __init__(self, surface: Surface, app: object) -> None:
        super().__init__(surface, app)
        self.name = 'game-net'

    def set_game_table(self) -> None:
        self.game_table: NetworkTable = NetworkTable(self)

    def start(self) -> None:
        self.game_table.init_client()
        super().start()

if __name__ == '__main__':
    print('Try to run main.py')