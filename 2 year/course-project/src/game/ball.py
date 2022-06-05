from src.base.collideable import *

class Ball(Collideable):
    def __init__(self) -> None:
        self.dv_x: int = 10
        self.dv_y: int = 5
        super().__init__((20, 20))

    def reverse_x_velocity(self) -> None:
        self.dv_x *= -1

    def reverse_y_velocity(self) -> None:
        self.dv_y *= -1

    def move(self) -> None:
        self.x += self.dv_x
        self.y += self.dv_y

    def render(self) -> pygame.Surface:
        self.surface.fill((32, 32, 32))
        rect: pygame.Rect = self.surface.get_rect()
        pygame.draw.circle(self.surface, (255, 255, 255),
            rect.center, rect.width//2-1)
        return self.surface

if __name__ == '__main__':
    print('Try to run main.py')