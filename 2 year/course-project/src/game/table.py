import enum

from src.base.scene import *

from src.game.ball import *
from src.game.player import *

from datetime import datetime
from datetime import timedelta

from src.network.client import *

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
        self.lplayer: Player = Player(LP_THEME_COLOR)
        self.rplayer: Player = Player(RP_THEME_COLOR)
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

    def request_game_finish(self) -> None:
        self.update_delay(1000)
        while not self.update_delay_finished():
            pygame.time.delay(200)
        self.game.app.switch('menu')

    def update_ball(self):
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
                    self.request_game_finish()
                self.ball.reverse_x_velocity()
                self.update_delay(1000)
                self.reset()
                break

    def update_myself(self) -> None:
        pass

    def update_opponent(self) -> None:
        opponent: Player = self.players[Orientation(self.orientation.value^1)]
        if (self.orientation == Orientation.LEFT and self.ball.dv_x > 0) or (
            self.orientation == Orientation.RIGHT and self.ball.dv_x < 0):
            if opponent.y < self.ball.y:
                opponent.move_down()
            elif opponent.y > self.ball.y:
                opponent.move_up()

    def update(self) -> None:
        self.update_ball()
        self.update_myself()
        self.update_opponent()

    def __render_game_objects(self) -> None:
        self.surface.blit(self.ball.render(), self.ball.get_position())
        for player in self.players.values():
            self.surface.blit(player.render(), player.get_position())

    def __render_borders(self) -> None:
        for border in self.borders.values():
            pygame.draw.rect(self.surface, WHITE_COLOR, border)
        center_line_pos1: Tuple[int, int] = (self.board.centerx, self.upper_board_height)
        center_line_pos2: Tuple[int, int] = (self.board.centerx, self.rect.height-self.bottom_board_height)
        pygame.draw.line(self.surface, WHITE_COLOR, center_line_pos1, center_line_pos2)

    def __render_scores(self) -> None:
        lpscore: str = str(self.scores[self.lplayer]).zfill(2)
        rpscore: str = str(self.scores[self.rplayer]).zfill(2)
        score_text: Surface = self.__font.render(f'{lpscore} : {rpscore}', True, SCORE_THEME_COLOR)
        score_centered_rect: pygame.Rect = score_text.get_rect(center=self.scores_rect.center)
        self.surface.blit(score_text, score_centered_rect)

    def render(self) -> pygame.Surface:
        if self.update_delay_finished():
            self.update()
        self.__render_game_objects()
        self.__render_borders()
        self.__render_scores()

    def key_press_event_provider(self, event: Event) -> None:
        if event.type == pygame.KEYDOWN:
            under_control: Player = self.players[self.orientation] 
            if event.key == pygame.K_w:
                under_control.move_up()
            if event.key == pygame.K_s:
                under_control.move_down()

class NetworkTable(Table):
    def __init__(self, game: Scene) -> None:
        super().__init__(game)

    def init_client(self) -> None:
        self.client: PongClient = None
        try:
            self.client = PongClient()
            self.client.connect()
            self.client.verify_peer_node()
            self.client.set_recv_callback(self.__recv_handler)
            self.client.start_receiving()
            self.orientation = Orientation(self.client.orientation)
        except PongServerException as pse:
            print(f'Server error: {pse}')
            self.client = None
            self.request_game_finish()

    def __recv_handler(self, response: str) -> None:
        try:
            data: Dict[str, str] = json.loads(response)
            if int(data['pong_client_game_status']) == 0:
                print(f'__recv_handler: {data["pong_client_game_status_message"]}')
                self.client.disconnect()
            if int(data['pong_client_game_status']) == 1:
                if 'lplayer' in data['pong_client_sync_data'].keys():
                    self.lplayer.y = int(data['pong_client_sync_data']['lplayer'])
                if 'rplayer' in data['pong_client_sync_data'].keys():
                    self.rplayer.y = int(data['pong_client_sync_data']['rplayer'])
                if 'ball' in data['pong_client_sync_data'].keys():
                    position_str: List[str] = data['pong_client_sync_data']['ball'].split(',')
                    self.ball.set_position((int(position_str[0]), int(position_str[1])))
                if 'scores' in data['pong_client_sync_data'].keys():
                    scores_str: List[str] = data['pong_client_sync_data']['scores'].split(',')
                    self.scores[self.lplayer] = int(scores_str[0])
                    self.scores[self.rplayer] = int(scores_str[1])
        except Exception as e:
            print(f'__recv_handler: {e}')

    def request_game_finish(self) -> None:
        if self.client != None:
            self.client.disconnect()
        super().request_game_finish()

    def update_opponent(self) -> None:
        pass

    def update_ball(self) -> None:
        if self.orientation == Orientation.LEFT:
            super().update_ball()
            try:
                self.client.send(self.client.make_sync_response(
                    scores=[value for value in self.scores.values()]))
                self.client.send(self.client.make_sync_response(
                    ball=self.ball.get_position()))
            except Exception as e:
                print(f'update_ball: {e}')
                self.request_game_finish()

    def update_myself(self) -> None:
        try:
            if self.orientation == Orientation.LEFT:
                self.client.send(self.client.make_sync_response(
                    lplayer=self.lplayer.y))
            else:
                self.client.send(self.client.make_sync_response(
                    rplayer=self.rplayer.y))
        except Exception as e:
            print(f'update_myself: {e}')
            self.request_game_finish()

    def reset(self) -> None:
        super().reset()
        self.update_myself()

if __name__ == '__main__':
    print('Try to run main.py')