import pygame
import pygame.key
import pygame.event
import pygame.display

from pygame import Surface

pygame.init()

pygame.display.set_caption('Quick Start')
window_surface: Surface = pygame.display.set_mode((800, 600))

window_surface.fill(pygame.Color('#000000'))

is_running = True

while is_running:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            is_running = False
        elif event.type == pygame.MOUSEBUTTONUP:
            is_running = False
    window_surface.blit(window_surface, (0, 0))
    pygame.display.update()