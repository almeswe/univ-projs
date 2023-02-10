import pygame
import kmeans

from typing import List

WINDOW_SIZE = (1200, 800)

CLASSES = 10
POINTS  = 30000

class Window(object):
    def __init__(self) -> None:
        pygame.init()
        self.surface: pygame.Surface = \
            pygame.display.set_mode(WINDOW_SIZE)
        pygame.display.set_caption('KMeans simulation')
        self.km: kmeans.KMeans = kmeans.KMeans(CLASSES, POINTS)
        self.clock = pygame.time.Clock()

    def run(self) -> None:
        self.loop: bool = True
        self.km.reset(WINDOW_SIZE)
        while self.loop:
            self.clock.tick(60)
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    self.loop = False
                if event.type == pygame.KEYDOWN:
                    if event.key == pygame.K_SPACE:
                        self.km.iterate()
                    if event.key == pygame.K_c:
                        while not self.km.finished:
                            self.km.iterate()
                            self.render()
                    if event.key == pygame.K_q:
                        self.loop = False
            self.render()
        self.km.show('finished')

    def render(self) -> None:
        self.surface.fill((0, 0, 0))
        for point in self.km.points:
            self.renderpt(point)
        for color, point in zip(self.km.colors, self.km.centers):
            point.c = color
            self.renderct(point, color)
        pygame.display.flip()

    def renderpt(self, pt: kmeans.Point) -> None:
        pygame.draw.circle(self.surface, pt.c, pt.to_list(), 1)

    def renderct(self, ct: kmeans.Point, c: List[int ]) -> None:
        rect: pygame.Rect = pygame.Rect(*ct.to_list(), 10, 10)
        pygame.draw.rect(self.surface, c, rect, 10)
        pygame.draw.rect(self.surface, (255, 255, 255), rect, 2)

if __name__ == '__main__':
    w: Window = Window()
    w.run()