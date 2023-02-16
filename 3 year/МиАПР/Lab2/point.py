import math
import random

from typing import List

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
    
    def dist(self, to: __name__) -> float:
        return math.dist(self.to_list(), to.to_list())
        
def genpt(xy: List[int]) -> Point:
    x: int = random.randint(0, xy[0])
    y: int = random.randint(0, xy[1])
    return Point(x, y)

if __name__ == '__main__':
    pass