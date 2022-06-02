from src.base.ui.button import *

class MenuButton(Button):
    def __init__(self, text: str, size: Tuple[int, int], position: Tuple[int, int]) -> None:
        self.text: str = text
        super().__init__(size, position)
        self.__init_handlers()
        self.__init_constants()
        self.__init_layer_context()

    def __init_constants(self) -> None:
        self.__font_size: int = 36
        self.__font_path: str = 'fonts/CollegiateheavyoutlineMedium-B0yx.ttf'
        self.__font: pygame.font.Font = pygame.font.Font(self.__font_path, self.__font_size)
        self.__text_color: Tuple[int, int, int] = (255, 255, 255)
        self.__button_default_image_path: str = 'imgs/menu_button_default2.png'
        self.__button_pressed_image_path: str = 'imgs/menu_button_pressed2.png'

    def __init_layer_context(self) -> None:
        default_image_surface: Surface = pygame.image.load(self.__button_default_image_path)
        pressed_image_surface: Surface = pygame.image.load(self.__button_pressed_image_path)

        default_image_surface: Surface = pygame.transform.scale(
            default_image_surface, self.size)
        pressed_image_surface: Surface = pygame.transform.scale(
            pressed_image_surface, self.size)

        button_text_surface: Surface = self.__font.render(self.text, True, self.__text_color)
        centered_rect: pygame.Rect = button_text_surface.get_rect(center=self.rect.center)

        center_point: Tuple[int, int] = (centered_rect.left-self.position[0], centered_rect.top-self.position[1])
        center_point_pressed: Tuple[int, int] = (center_point[0], center_point[1]+5)

        self.__idle_layers: List[Surface] = [
            (default_image_surface, (0, 0)),
            (button_text_surface, center_point)
        ]
        self.__key_up_layers : List[Surface] = self.__idle_layers

        self.__key_down_layers : List[Surface] = [
            (pressed_image_surface, (0, 0)),
            (button_text_surface, center_point_pressed)
        ]
        self.layers = self.__idle_layers

    def __init_handlers(self) -> None:
        self.on_key_up   += [self.__on_key_up_handler]
        self.on_key_down += [self.__on_key_down_handler]

    def __on_key_up_handler(self, event: Event) -> None:
        self.layers = self.__key_up_layers

    def __on_key_down_handler(self, event: Event) -> None:
        self.layers = self.__key_down_layers

    def render(self) -> Surface:
        self.surface.set_colorkey((0, 0, 0))
        if not self.layers:
            self.layers = self.__idle_layers
        super().render()
        self.layers = None
        return self.surface
    
if __name__ == '__main__':
    print('Try to run main.py')