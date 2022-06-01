from src.base.app import *

class MenuScene(Scene):
    def __init__(self, surface: Surface) -> None:
        super().__init__('menu', surface)
        self.__init_controls()

    def __init_controls(self) -> None:
        size = (100, 50)
        menu_button: Button = Button(size, (150, 50))
        bg: Surface = Surface(size)
        bg.fill((255, 255, 255))
        menu_button.add_surface(bg, (0, 0))
        # prettyfy
        menu_button.on_key_up.append(self.__on_menu_button_up)
        menu_button.on_key_down.append(self.__on_menu_button_down)
        menu_button.on_key_motion.append(self.__on_menu_button_motion)
        #
        self.register_control(menu_button)

    def __on_menu_button_up(self, event: Event) -> None:
        print(f'button up: {event.pos}')

    def __on_menu_button_down(self, event: Event) -> None:
        print(f'button down: {event.pos}')

    def __on_menu_button_motion(self, event: Event) -> None:
        print(f'motion: {event.pos}')

class FirstScene(Scene):
    def __init__(self, surface: Surface) -> None:
        super().__init__("first_scene", surface)

    def render(self, event: Event) -> None:
        self.surface.fill((0, 0, 255))

class SecondScene(Scene):
    def __init__(self, surface: Surface) -> None:
        super().__init__("second_scene", surface)

    def render(self, event: Event) -> None:
        self.surface.fill((255, 0, 0))

class Game(App):
    def __init__(self) -> None:
        super().__init__("game", (500, 500))
        self.__register_scenes()
        #self.scenes[0].subscribe(pygame.MOUSEBUTTONUP, callback=lambda e: print(f'global: {e.pos}'))
    #    self.__subscribe_on_events()

    def __register_scenes(self) -> None:
        #scenes: List[Scene] = [FirstScene(self.window), SecondScene(self.window)]
        #for scene in scenes:
        #    self.register(scene)
        self.register(MenuScene(self.window))

    #def __subscribe_on_events(self) -> None:
    #    pass
        #first_scene: str = self.scenes[0].name
        #second_scene: str = self.scenes[1].name
        #self.scenes[0].subscribe(pygame.MOUSEBUTTONDOWN, callback=lambda e: self.switch(second_scene))
        #self.scenes[1].subscribe(pygame.MOUSEBUTTONDOWN, callback=lambda e: self.switch(first_scene))