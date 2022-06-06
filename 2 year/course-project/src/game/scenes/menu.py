from constants import *
from src.base.scene import *
from src.game.ui.buttons import *

class MenuScene(Scene):
    def __init__(self, surface: Surface, app: object) -> None:
        super().__init__('menu', surface, app)
        self.__init_controls()
        self.__init_events()
        self.__init_font()

    def __init_controls(self) -> None:
        self.play_button: Button = MenuButton('PLAY', (180, 80), (50, 50))
        self.conn_button: Button = MenuButton('CONNECT', (180, 80), (50, 150))
        self.quit_button: Button = MenuButton('QUIT', (180, 80), (50, 250)) 
        self.register_control(self.play_button)
        self.register_control(self.conn_button)
        self.register_control(self.quit_button)
    
    def __init_events(self) -> None:
        self.play_button.on_key_up += [self.__on_play_button_click]
        self.quit_button.on_key_up += [self.__on_quit_button_click]
        self.conn_button.on_key_up += [self.__on_connect_button_click]

    def __init_font(self) -> None:
        self.__font: pygame.font.Font = pygame.font.Font('fonts/CollegiateheavyoutlineMedium-B0yx.ttf', 36)

    def __on_play_button_click(self, event: Event) -> None:
        self.app.switch('game')

    def __on_connect_button_click(self, event: Event) -> None:
        self.app.switch('game-net')

    def __on_quit_button_click(self, event: Event) -> None:
        for scene in self.app.scenes:
            scene.stop()
            scene.release()
        pygame.quit()

    def stop(self) -> None:
        super().stop()

    def render(self) -> None:
        self.surface.fill(BG_THEME_COLOR)
        self.surface.blit(self.__font.render(
            f'ping pong {VERSION}', True, (144,144,144)), (400, 650))
        super().render()

if __name__ == '__main__':
    print('Try to run main.py')