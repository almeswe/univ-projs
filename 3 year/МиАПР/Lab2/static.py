from base import *
from point import *

class Static(Classificator):
    def __init__(self, points: int, clusters: int) -> None:
        super().__init__(points)
        self.ct_len = clusters

    def iterate(self) -> None:
        pass

    def resolve(self, xy: List[int]) -> int:
        self.reset(xy)
        self.clusters = [genpt(xy) \
            for _ in range(self.ct_len)]
        return len(self.clusters)

if __name__ == '__main__':
    pass