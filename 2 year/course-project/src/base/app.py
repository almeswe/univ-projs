from src.base.scene import *

class AppException(Exception): 
    def __init__(self, *args: object) -> None:
        super().__init__(*args)

class App(ABC):
    def __init__(self, name: str, size: Tuple[int, int], icon: str = None) -> None:
        pygame.init()
        self.__init_icon(icon)
        self.__init_scenes()
        self.__init_surface(name, size)

    def __init_icon(self, icon: str) -> None:
        if icon != None:
            pygame.display.set_icon(pygame.transform.scale(
                pygame.image.load(icon), (32, 32)))

    def __init_surface(self, name: str, size: Tuple[int, int]) -> None:
        pygame.display.set_caption(name)
        self.window: Surface = pygame.display.set_mode(size)

    def __init_scenes(self) -> None:
        self.current: Scene = None
        self.scenes: List[Scene] = []

    def run(self) -> None:
        self.scenes[0].start()

    def switch(self, scene: str) -> None:
        if self.current != None:
            self.current.stop()
        if scene not in [s.name for s in self.scenes]:
            raise AppException(f'Scene \'{scene}\' is not registered.')
        self.current = self.scenes[[s.name for s in self.scenes].index(scene)]
        self.current.start()

    def register(self, scene: Scene) -> None:
        for s in self.scenes:
            if s.name == scene.name:
                raise AppException(f'Scene \'{scene.name}\' is registered.')
        self.scenes.append(scene)

if __name__ == '__main__':
    print('Try to run main.py')