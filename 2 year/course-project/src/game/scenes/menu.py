import time

from src.base.scene import *
from src.game.ui.buttons import *

from threading import Thread

class MenuScene(Scene):
    def __init__(self, surface: Surface) -> None:
        super().__init__('menu', surface)
        self.__init_temp_data()
        self.__init_controls()

    def __init_temp_data(self) -> None:
        self.__image: Surface = None
        self.__image_update_delay: int = 3
        self.__background_image_index: int = 0
        self.__background_images: List[str] = [
            'imgs/background/bg1.jpg',
            'imgs/background/bg2.jpg',
            'imgs/background/bg3.jpg',
            'imgs/background/bg4.jpg',
        ]
        self.updater_is_active: bool = True
        self.updater_thread: Thread = Thread(
            target=self.__render_background_image)
        self.updater_thread.start()

    def __init_controls(self) -> None:
        self.play_button    : Button = MenuButton('PLAY',   (180, 80), (50, 50))
        self.connect_button : Button = MenuButton('CONNECT', (180, 80), (50, 150))
        self.quit_button    : Button = MenuButton('QUIT',    (180, 80), (50, 250)) 
        self.register_control(self.play_button)
        self.register_control(self.connect_button)
        self.register_control(self.quit_button)
    
    def __render_background_image(self) -> None:
        while self.updater_is_active:
            if self.current:
                self.__background_image_index = (self.__background_image_index + 1) % \
                    len(self.__background_images)
                current_image_path: str = self.__background_images[self.__background_image_index]
                self.__image: Surface = pygame.image.load(current_image_path)
                self.__image = pygame.transform.scale(self.__image, self.surface.get_size())
                if self.updater_is_active:
                    time.sleep(self.__image_update_delay)
            time.sleep(0.2)

    def stop(self) -> None:
        super().stop()

    def release(self) -> None:
        self.updater_is_active = False
        self.updater_thread.join()

    def render(self) -> None:
        if self.__image and not self.__image.get_locked():
            if self.updater_is_active and pygame.get_init():
                self.surface.blit(self.__image, (0, 0))
        super().render()

if __name__ == '__main__':
    print('Try to run main.py')