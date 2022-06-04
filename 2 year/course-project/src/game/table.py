import enum

import pygame
import pygame.draw

from pygame.event import Event

from typing import Dict
from typing import Tuple

from src.base.scene import *

from src.game.ball import Ball

from abc import ABC
from abc import abstractmethod

class Player(ABC):
    def __init__(self, color: Tuple[int, int, int]) -> None:
        self.dv: int = 10
        self.x: int = 0
        self.y: int = 0
        self.size: Tuple[int, int] = (20, 100)
        self.color: Tuple[int, int, int] = color
        self.surface: pygame.Surface = pygame.Surface(self.size) 

    def set_position(self, position: Tuple[int, int]) -> None:
        self.x, self.y = position

    def move_up(self) -> None:
        self.y -= self.dv
    
    def move_down(self) -> None:
        self.y += self.dv

    def get_position(self) -> Tuple[int, int]:
        return (self.x, self.y)

    def render(self) -> pygame.Surface:
        pygame.draw.rect(self.surface, self.color,
            self.surface.get_rect(), border_radius=5)
        return self.surface

class LocalPlayer(Player):
    def __init__(self, game: Scene, position: Tuple[int, int], color: Tuple[int, int, int]) -> None:
        super().__init__(game, position, color)
        game.subscribe(pygame.K_w, self.__move_up)
        game.subscribe(pygame.K_s, self.__move_down)

    def __move_up(self, event: Event) -> None:
        super().move_up()

    def __move_down(self, event: Event) -> None:
        super().move_down()

class Orientation(enum.Enum):
    ORIENTATION_LEFT    = 0
    ORIENTATION_RIGHT   = 1
    ORIENTATION_TOP     = 2
    ORIENTATION_BOTTOM  = 3

class Table(object):
    def __init__(self, scene: Scene) -> None:
        self.game: Scene = scene
        self.surface: pygame.Surface = self.game.surface
        self.__init_constants()
        self.rect: pygame.Rect = scene.surface.get_rect()
        self.orientation: Orientation = Orientation.ORIENTATION_LEFT
        self.board: pygame.Rect = pygame.Rect(0, self.upper_board_height, 
            self.rect.width, self.rect.height-self.upper_board_height)
        self.upper_board: pygame.Rect = pygame.Rect(0, 0,
            self.rect.width, self.upper_board_height)
        self.__init_game_objects()
        self.reset()

    def __init_constants(self) -> None:
        self.upper_board_height: int = max(self.surface.get_height()*0.15, 50)

    def __init_game_objects(self) -> None:
        self.ball: Ball = Ball((250, 250))
        #
        self.ball.dv_x = 0
        self.ball.dv_y = 0
        #
        self.lplayer: Player = Player((255, 0, 0))
        self.rplayer: Player = Player((0, 0, 255))
        self.players: Dict[int, Player] = {
            Orientation.ORIENTATION_LEFT : self.lplayer,
            Orientation.ORIENTATION_RIGHT: self.rplayer,
        }

        self.borders: Dict[int, pygame.Rect] = {
            Orientation.ORIENTATION_LEFT   : pygame.Rect(0, self.upper_board_height, 1, self.board.height),
            Orientation.ORIENTATION_TOP    : pygame.Rect(0, self.upper_board_height, self.board.width, 1),
            Orientation.ORIENTATION_RIGHT  : pygame.Rect(self.board.width-1, self.upper_board_height, 1, self.board.height),
            Orientation.ORIENTATION_BOTTOM : pygame.Rect(0, self.rect.height-1, self.board.width, 1)
        }

    def reset(self) -> None:
        self.lplayer.set_position((5, self.board.height // 2))
        self.rplayer.set_position((self.board.width-self.rplayer.size[0]-5, self.board.height // 2))

    def render(self) -> pygame.Surface:
        self.surface.blit(self.ball.render(), (250, 250))
        for player in self.players.values():
            self.surface.blit(player.render(), player.get_position())
        for border in self.borders.values():
            pygame.draw.rect(self.surface, (255, 255, 255), border)

    def key_press_handler(self, event: Event) -> None:
        if event.type in [pygame.KEYUP, pygame.KEYDOWN]:
            under_control: Player = self.players[self.orientation] 
            if event.key == pygame.K_UP:
                if under_control.y > self.upper_board_height:
                    under_control.move_up()
                else:
                    under_control.y = self.upper_board_height
            if event.key == pygame.K_DOWN:
                if under_control.y < self.rect.height-under_control.size[1]:
                    under_control.move_down()
                else:
                    under_control.y = self.rect.height-under_control.size[1]