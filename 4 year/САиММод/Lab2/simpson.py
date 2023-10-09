from generator import *

class simpson(generator):
    def __init__(self, r0: int, a: float, b: float):
        import uniform
        super().__init__(r0)
        self.a: float = a
        self.b: float = b
        self.u: uniform.uniform = \
            uniform.uniform(r0, a / 2, b / 2)
    
    def produce(self, m: int, a: int) -> float:
        r1: float = self.u.produce(m, a)
        r2: float = self.u.produce(m, a)
        return r1 + r2
    
if __name__ == "__main__":
    pass