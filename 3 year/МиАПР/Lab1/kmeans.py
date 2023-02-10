import math
import random
import logging

from typing import List
from typing import Dict

class Point:
    x: int = 0
    y: int = 0
    c: List[int] = [255, 255, 255] 

    def __init__(self, x: int, y: int) -> None:
        self.x = x
        self.y = y

    def to_list(self) -> List[int]:
        return [self.x, self.y]

    def to_int(self) -> int:
        return                 \
            self.c[0] * 1000 + \
            self.c[1] * 100  + \
            self.c[2] * 10

class KMeans(object):
    def __init__(self, sectors: int, points: int) -> None:
        self.pt_len: int = points
        self.ct_len: int = sectors
        logging.basicConfig(format='[%(asctime)s]: %(message)s',
            datefmt="KMEANS %H:%M:%S", level=logging.INFO)

    def reset(self, xy: List[int]) -> None:
        self.finished: bool = False
        self.points: List[Point] = []
        self.centers: List[Point] = []
        self.colors: List[List[int]] = []
        self.iteration: int = 0
        if (xy[0] * xy[1]) < self.pt_len:
            raise Exception('Cannot place this amount of points in specified area.')
        self.spread(xy)
        self.show('reset performed')

    def genpt(self, xy: List[int]) -> Point:
        x: int = random.randint(0, xy[0])
        y: int = random.randint(0, xy[1])
        return Point(x, y)

    def genst(self, xy: List[int]) -> Point:
        return self.genpt(xy)

    def spread(self, xy: List[int]) -> None:
        for _ in range(self.pt_len):
            self.points.append(self.genpt(xy))
        for _ in range(self.ct_len):
            self.centers.append(self.genst(xy))
            self.colors.append([
                random.randint(0, 255),
                random.randint(0, 255),
                random.randint(0, 255)
            ])

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