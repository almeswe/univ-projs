import logging
import numpy as np

from typing import List
from typing import Tuple

TRAINING_SET = [
    ([-2, 0], 0),
    ([-1, 0], 0),
    ([1,  1], 0),
    ([2,  0], 1),
    ([3,  0], 1),
    ([1, -2], 1),
]

Polynomial = List[float]

TrainingData = Tuple[List[float], int]
TrainingSet  = List[TrainingData]

class PotentialMethod(object):
    def __init__(self) -> None:
        self._kx: Polynomial = [0, 0, 0, 0]
        logging.basicConfig(format='[%(asctime)s]: %(message)s',
            datefmt="%H:%M:%S", level=logging.INFO)

    def fx(self, x: float) -> float:
        return (-self._kx[0] - self._kx[1] * x) / (self._kx[2] + self._kx[3] * x)

    def kx(self, x_vec: List[float]) -> float:
        x1, x2 = x_vec[0], x_vec[1]
        return  self._kx[0] +      \
                self._kx[1] * x1 + \
                self._kx[2] * x2 + \
                self._kx[3] * x1 * x2
    
    def kx_p(self, p: int, kxi: Polynomial) -> Polynomial:
        return np.add(self._kx, np.multiply(p, kxi))

    def kxi_p(self, xi_vec: List[float]) -> Polynomial:
        xi1, xi2 = xi_vec[0], xi_vec[1]
        return [
            1, 
            4 * xi1,
            4 * xi2,
            16 * xi1 * xi2
        ]

    def p(self, t_data: TrainingData) -> int:
        x_vec, c = t_data[0], t_data[1]
        if (c == 0) and (self.kx(x_vec) <= 0):
            return 1
        if (c == 1) and (self.kx(x_vec) > 0):
            return -1
        return 0

    def train(self, t_set: TrainingSet) -> None:
        for t_data in t_set:
            p: int = self.p(t_data)
            self._kx = self.kx_p(p, 
                self.kxi_p(t_data[0]))
        self.info(f"k(x) = {self._kx[0]} + ({self._kx[1]}*x1) + ({self._kx[2]}*x2) + ({self._kx[3]}*x1*x2)")

    def info(self, msg: str) -> None:
        logging.info(msg)

if __name__ == '__main__':
    pass