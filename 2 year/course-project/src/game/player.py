from src.base.collideable import *

class Player(Collideable):
    def __init__(self, color: Tuple[int, int, int]) -> None:
        self.dv: int = PLAYER_Y_AXIS_SPEED
        self.upper_bound: float = float('inf')
        self.bottom_bound: float = float('-inf')
        self.color: Tuple[int, int, int] = color
        super().__init__((10, 100))

    def move_up(self) -> None:
        if self.y > self.upper_bound:
            self.y -= self.dv

    def move_down(self) -> None:
        if self.y < self.bottom_bound:
            self.y += self.dv

    def y_axis_retrictions(self, bounds: Tuple[int, int]) -> None:
        self.upper_bound, self.bottom_bound = bounds

    def render(self) -> pygame.Surface:
        pygame.draw.rect(self.surface, self.color,
            self.surface.get_rect(), border_radius=2)
        return self.surface

if __name__ == '__main__':
    print('Try to run main.py')