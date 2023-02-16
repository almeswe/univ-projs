import math
import logging

from point import *
from maxmin import *

from typing import List
from typing import Dict

class KMeans(object):
    def __init__(self, c: Classificator) -> None:
        self.c = c
        self.pt_len: int = self.c.pt_len

    def reset(self, xy: List[int]) -> None:
        self.resolve(xy)
        self.finished: bool = False
        self.colors: List[List[int]] = []
        self.iteration: int = 0
        if (xy[0] * xy[1]) < self.pt_len:
            raise Exception('Cannot place this amount of points in specified area.')
        for _ in range(len(self.centers)):
            self.colors.append([
                random.randint(0, 255),
                random.randint(0, 255),
                random.randint(0, 255)
            ])
        self.show('reset performed')

    def resolve(self, xy: List[int]) -> None:
        self.ct_len: int = self.c.resolve(xy)
        self.points: List[Point] = self.c.points
        self.centers: List[Point] = self.c.clusters
        self.show(f'used \'{self.c.__class__}\' as a classificator')
        self.show(f'{self.c.ct_len} clusters set')

    def relocate(self) -> None:
        relocated: bool = False
        cxvalues: Dict[int, int] = {}        
        cyvalues: Dict[int, int] = {}
        ckvalues: Dict[int, int] = {}
        for point in self.points:
            ptint: int = point.to_int() 
            if ptint not in cxvalues:
                cxvalues[ptint] = 0
                cyvalues[ptint] = 0 
                ckvalues[ptint] = 0
            cxvalues[ptint] += point.x 
            cyvalues[ptint] += point.y
            ckvalues[ptint] += 1
        for ct in self.centers:
            ctint: int = ct.to_int()
            ctx: int = int(cxvalues[ctint] / ckvalues[ctint])
            cty: int = int(cyvalues[ctint] / ckvalues[ctint])
            if (ct.x != ctx) or (ct.y != cty):
                ct.x, ct.y = ctx, cty
                relocated = True
        if not relocated:
            self.finished = True
            self.show('simulation finished.')

    def iterate(self) -> None:
        if not self.finished:
            self.iteration += 1
            self.show(f'iteration #{self.iteration}')
            if self.iteration > 1:
                self.relocate()
            for point in self.points:
                distances: List[float] = [
                    math.dist(point.to_list(), center.to_list()) \
                        for center in self.centers
                ]
                index: int = distances.index(min(distances))
                point.c = self.colors[index]

    def show(self, msg: str) -> None:
        logging.info(msg)

if __name__ == '__main__':
    pass