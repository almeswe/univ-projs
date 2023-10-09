from generator import *

class exponential(generator):
    def __init__(self, r0: int, l: float):
        super().__init__(r0)
        self.l = l
    
    def produce(self, m: int, a: int) -> float:
        import math
        r: float = super().produce(m, a)
        return - 1 / self.l * math.log(r, math.e)

if __name__ == "__main__":
    pass