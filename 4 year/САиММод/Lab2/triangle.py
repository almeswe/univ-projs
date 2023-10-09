from generator import *

class triangle(generator):
    def __init__(self, r0: int, a: float, b: float):
        super().__init__(r0)
        self.a: float = a
        self.b: float = b
    
    def produce(self, m: int, a: int) -> float:
        r1: float = super().produce(m, a)
        r2: float = super().produce(m, a)
        return self.a + (self.b - self.a) * max(r1, r2)
    
if __name__ == "__main__":
    pass