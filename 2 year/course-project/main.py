from src.game.game_main import Game

def test(): 
    import pygame
    import pygame.font
    import pygame.draw
    import pygame.event
    import pygame.display

    pygame.init()

    window = pygame.display.set_mode((200, 200))
    font = pygame.font.SysFont('consolas', 24)
    
    text = font.render('TEXT', True, (255, 0, 0))
    rect = text.get_rect()
    parent_rect = pygame.Rect(20, 20, 100, 50)
    centered_rect = text.get_rect(center=window.get_rect().center)

    pygame.draw.rect(window, (0, 0, 255), parent_rect)
    pygame.draw.rect(window, (0, 255, 0), centered_rect)
    window.blit(text, centered_rect.topleft)
    pygame.display.update()

    while True:
        for event in pygame.event.get():
            pass

if __name__ == '__main__':
    #test()
    Game().run()