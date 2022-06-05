import enum
import socket
import threading

from src.base.scene import *

from src.game.ball import *
from src.game.player import *

from datetime import datetime
from datetime import timedelta

class Orientation(enum.Enum):
    LEFT   = 0
    RIGHT  = 1
    TOP    = 2
    BOTTOM = 3

class Table(object):
    def __init__(self, scene: Scene) -> None:
        self.__init_scene(scene)
        self.__init_constants()
        self.__init_board()
        self.__init_game_objects()
        self.__init_scores()

    def __init_scene(self, scene: Scene) -> None:
        self.game: Scene = scene
        self.surface: pygame.Surface = self.game.surface
        self.rect: pygame.Rect = scene.surface.get_rect()

    def __init_scores(self) -> None:
        self.__max_score: int = 10
        self.__font_size: int = 56
        self.__font_path: str = 'fonts/CollegiateheavyoutlineMedium-B0yx.ttf'
        self.__font: pygame.font.Font = pygame.font.Font(self.__font_path, self.__font_size)
        self.scores_rect: pygame.Rect = pygame.Rect(0, 0, self.rect.width, self.upper_board_height)
        self.scores: Dict[Player, int] = {}
        self.reset_scores()

    def __init_board(self) -> None:
        self.orientation: Orientation = Orientation.LEFT
        self.board: pygame.Rect = pygame.Rect(0, self.upper_board_height, 
            self.rect.width, self.rect.height-self.upper_board_height-self.bottom_board_height)
        self.upper_board: pygame.Rect = pygame.Rect(0, 0,
            self.rect.width, self.upper_board_height)

    def __init_constants(self) -> None:
        self.border_margin: int = 10
        self.__update_ts: datetime = datetime.now()
        self.upper_board_height: int = max(self.surface.get_height()*0.15, 50)
        self.bottom_board_height: int = max(self.surface.get_height()*0.10, 25)

    def __init_game_objects(self) -> None:
        self.ball: Ball = Ball()
        self.lplayer: Player = Player((205, 62, 64))
        self.rplayer: Player = Player((81, 154, 186))
        self.players: Dict[int, Player] = {
            Orientation.LEFT : self.lplayer,
            Orientation.RIGHT: self.rplayer,
        }
        for player in self.players.values():
            player.y_axis_retrictions((self.upper_board_height, self.board.height))
        self.borders: Dict[int, pygame.Rect] = {
            Orientation.LEFT   : pygame.Rect(0, self.upper_board_height, 1, self.board.height),
            Orientation.TOP    : pygame.Rect(0, self.upper_board_height, self.board.width, 1),
            Orientation.RIGHT  : pygame.Rect(self.board.width-1, self.upper_board_height, 1, self.board.height),
            Orientation.BOTTOM : pygame.Rect(0, self.rect.height-self.bottom_board_height, self.board.width, 1)
        }
        self.reset()

    def update_delay(self, ms: int) -> None:
        self.__update_ts = datetime.now()+timedelta(milliseconds=ms)

    def update_delay_finished(self) -> bool:
        return datetime.now() >= self.__update_ts

    def reset(self) -> None:
        self.ball.set_position((self.board.center[0]-self.ball.size[0]//2,
            self.board.center[1]-self.ball.size[1]//2))
        self.lplayer.set_position((self.border_margin, self.board.center[1]-self.lplayer.size[1]//2))
        self.rplayer.set_position((self.board.width-self.rplayer.size[0]-\
            self.border_margin, self.board.center[1]-self.rplayer.size[1]//2))

    def reset_scores(self) -> None:
        self.scores = {
            self.lplayer: 0,
            self.rplayer: 0
        }

    def __request_game_finish(self) -> None:
        pygame.event.post(Event(pygame.USEREVENT, {
            'pong_finish_game_request': 1,
        }))

    def __update_ball(self):
        self.ball.move()
        if (self.ball.collide(self.lplayer.get_rect())) or (
            self.ball.collide(self.rplayer.get_rect())):
            self.ball.reverse_x_velocity()
        if (self.ball.collide(self.borders[Orientation.TOP])) or (
            self.ball.collide(self.borders[Orientation.BOTTOM])):
            self.ball.reverse_y_velocity()
        for orientation in [Orientation.LEFT, Orientation.RIGHT]:
            inverse_orientation_player: Player = self.players[Orientation(orientation.value^1)]
            if (self.ball.collide(self.borders[orientation])):
                self.scores[inverse_orientation_player] += 1
                if self.scores[inverse_orientation_player] >= self.__max_score:
                    self.__request_game_finish()
                self.ball.reverse_x_velocity()
                self.update_delay(1000)
                self.reset()
                break

    def __update_myself(self) -> None:
        pass

    def __update_opponent(self) -> None:
        opponent: Player = self.players[Orientation(self.orientation.value^1)]
        if (self.orientation == Orientation.LEFT and self.ball.dv_x > 0) or (
            self.orientation == Orientation.RIGHT and self.ball.dv_x < 0):
            if opponent.y < self.ball.y:
                opponent.move_down()
            elif opponent.y > self.ball.y:
                opponent.move_up()

    def update(self) -> None:
        self.__update_ball()
        self.__update_myself()
        self.__update_opponent()

    def __render_game_objects(self) -> None:
        self.surface.blit(self.ball.render(), self.ball.get_position())
        for player in self.players.values():
            self.surface.blit(player.render(), player.get_position())

    def __render_borders(self) -> None:
        for border in self.borders.values():
            pygame.draw.rect(self.surface, (255, 255, 255), border)
        center_line_pos1: Tuple[int, int] = (self.board.centerx, self.upper_board_height)
        center_line_pos2: Tuple[int, int] = (self.board.centerx, self.rect.height-self.bottom_board_height)
        pygame.draw.line(self.surface, (255, 255, 255), center_line_pos1, center_line_pos2)

    def __render_scores(self) -> None:
        lpscore: str = str(self.scores[self.lplayer]).zfill(2)
        rpscore: str = str(self.scores[self.rplayer]).zfill(2)
        score_text: Surface = self.__font.render(f'{lpscore} : {rpscore}', True, (185, 122, 87))
        score_centered_rect: pygame.Rect = score_text.get_rect(center=self.scores_rect.center)
        self.surface.blit(score_text, score_centered_rect)

    def render(self) -> pygame.Surface:
        if self.update_delay_finished():
            self.update()
        self.__render_game_objects()
        self.__render_borders()
        self.__render_scores()

    def key_press_event_provider(self, event: Event) -> None:
        if event.type in [pygame.KEYDOWN]:
            under_control: Player = self.players[self.orientation] 
            if event.key == pygame.K_w:
                under_control.move_up()
            if event.key == pygame.K_s:
                under_control.move_down()

class NetworkTable(Table):
    def __init__(self, dest_host: Tuple[str, int], orientation: Orientation) -> None:
        super().__init__()
        self.orientation: Orientation = orientation
        self.dest_host: Tuple[str, int] = dest_host

    def __init_socket(self) -> None:
        self.sockfd: int = socket.socket(
            socket.AF_INET, socket.SOCK_STREAM)

if __name__ == '__main__':
    print('Try to run main.py')