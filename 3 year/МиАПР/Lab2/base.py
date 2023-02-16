import abc
import logging

from point import *

class Classificator(abc.ABC):
    def __init__(self, points: int) -> None:
        self.ct_len: int = 0
        self.pt_len: int = points
        logging.basicConfig(format='[%(asctime)s]: %(message)s',
            datefmt="%H:%M:%S", level=logging.INFO)

    def reset(self, xy: List[int]) -> None:
        self.iteration: int = 0
        self.points:   List[Point] = []
        self.clusters: List[Point] = []
        self.spread(xy)
    
    def spread(self, xy: List[int]) -> None:
        for _ in range(self.pt_len):
            self.points.append(genpt(xy))

    def info(self, msg: str) -> None:
        logging.info(msg)

    @abc.abstractmethod
    def iterate(self) -> None:
        raise NotImplemented

    @abc.abstractmethod
    def resolve(self, xy: List[int]) -> int:
        raise NotImplemented

if __name__ == '__main__':
    pass