import pygame
from potential import *

WINDOW_CELL = 100
WINDOW_SIZE = (1200, 800)

class Window(object):
    def __init__(self) -> None:
        pygame.init()
        self._win: pygame.Surface = \
            pygame.display.set_mode(WINDOW_SIZE)
        pygame.display.set_caption('Potential method + visualization')
        self._pm: PotentialMethod = PotentialMethod()
        self._clock = pygame.time.Clock()
    
    def run(self) -> None:
        self.loop: bool = True
        self._pm.train(TRAINING_SET)
        self._render()
        while self.loop:
            self._clock.tick(60)
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    self.loop = False
                if event.type == pygame.MOUSEBUTTONDOWN:
                    self._render_user_pt()
            pygame.display.flip()

    def _normalize(self, x: float, y: float) -> List[float]:
        return [WINDOW_SIZE[0] / 2 + x * WINDOW_CELL,
                WINDOW_SIZE[1] / 2 - y * WINDOW_CELL]

    def _render_graph(self) -> None:
        startp: List[float] = self._normalize(-100, self._pm.fx(-100))
        for x in np.arange(-100, 100, 0.001):
            endp: List[float] = self._normalize(x, self._pm.fx(x))
            pygame.draw.line(self._win, (255, 0, 0), startp, endp, 2)
            startp = endp

    def _render_axes(self) -> None:
        w, h = WINDOW_SIZE[0], WINDOW_SIZE[1]
        pygame.draw.line(self._win, (0, 0, 0), (w/2, 0), (w/2, h), 1)
        pygame.draw.line(self._win, (0, 0, 0), (0, h/2), (w, h/2), 1)
        for i in range(w // WINDOW_CELL):
            pygame.draw.circle(self._win, (0, 0, 0), (i * WINDOW_CELL, h/2), 2)
        for i in range(h // WINDOW_CELL):
            pygame.draw.circle(self._win, (0, 0, 0), (w/2, i * WINDOW_CELL), 2)

    def _render_user_pt(self) -> None:
        pos: List[int] = pygame.mouse.get_pos()
        x, y = pos[0], pos[1]
        x = (x - WINDOW_SIZE[0] / 2) / WINDOW_CELL
        y = (y - WINDOW_SIZE[1] / 2) / WINDOW_CELL
        self._render_pt_of_c([x, -y], 0 if self._pm.kx([x, -y]) >= 0 else 1)

    def _render_pt_of_c(self, pt: List[float], c: int) -> None:
        color = (255, 255, 0) if c == 0 else (0, 255, 255)
        self._pm.info(f'Point: ({pt[0]}, {pt[1]}) -> {c}')
        pygame.draw.circle(self._win, color, self._normalize(*pt), 4)

    def _render_set(self) -> None:
        for t_data in TRAINING_SET:
            self._render_pt_of_c(*t_data)

    def _render(self) -> None:
        self._win.fill((33, 33, 33))
        self._render_axes()
        self._render_graph()
        self._render_set()

if __name__ == '__main__':
    Window().run()