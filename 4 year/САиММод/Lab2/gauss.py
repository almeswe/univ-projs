from generator import *

class gauss(generator):
    def __init__(self, r0: int, m: float, o: float, ni: int = 6):
        super().__init__(r0)
        self.m: float = m
        self.o: float = o
        self.ni: int = ni
    
    def produce(self, m: int, a: int) -> float:
        rsum: float = 0.0
        for _ in range(self.ni):
            rsum += super().produce(m, a)
        return self.m + self.o * (2 ** 0.5) * (rsum - 3)
    
if __name__ == "__main__":
    pass