from src.game.game_main import Game
from src.game.table import *

def test(): 
    import pygame
    import pygame.font
    import pygame.draw
    import pygame.event
    import pygame.display

    pygame.init()

    p = Player((255, 123, 31))

    window = pygame.display.set_mode((600, 600))
    font = pygame.font.SysFont('consolas', 24)

    window.blit(p.render(), (0, 200))    
    pygame.display.update()

    while True:
        for event in pygame.event.get():
            pass

if __name__ == '__main__':
    #test()
    Game().run()