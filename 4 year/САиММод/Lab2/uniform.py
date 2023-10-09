from generator import *

class uniform(generator):
    def __init__(self, r0: int, a: float, b: float) -> None:
        super().__init__(r0)
        self.a: float = a
        self.b: float = b
    
    def produce(self, m: int, a: int) -> float:
        r: float = super().produce(m, a)
        return self.a + (self.b - self.a) * r

if __name__ == "__main__":
    pass