from generator import *

class gamma(generator):
    def __init__(self, r0: int, l: float, nu: int):
        assert l > 0
        assert nu > 0
        super().__init__(r0)
        self.l: float = l
        self.nu: int = nu
    
    def produce(self, m: int, a: int) -> float:
        import math
        rpower: float = 1.0
        for _ in range(self.nu):
            rpower *= super().produce(m, a)
        return - 1 / self.l * math.log(rpower, math.e)
    
if __name__ == "__main__":
    pass