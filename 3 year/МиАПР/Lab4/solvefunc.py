from typing import *

class SolveFunc(object):
    def __init__(self, n: int, w_vec: List[float]) -> None:
        self.n: int = n
        self.w_vec: List[float] = w_vec

    def d(self, x_vec: List[float]) -> float:
        sum: float = 0
        assert len(x_vec) == len(self.w_vec)
        for x, w in zip(x_vec, self.w_vec):
            sum += x * w
        return sum
    
    def encourage(self, c: float, x_vec: List[float]) -> None:
        assert len(x_vec) == len(self.w_vec)
        for i in range(len(self.w_vec)):
            self.w_vec[i] = self.w_vec[i] + c * x_vec[i]         

    def punish(self, c: float, x_vec: List[float]) -> None:
        assert len(x_vec) == len(self.w_vec)
        for i in range(len(self.w_vec)):
            self.w_vec[i] = self.w_vec[i] - c * x_vec[i] 

if __name__ == '__main__':
    pass