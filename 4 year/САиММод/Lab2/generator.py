from typing import List

class generator(object):
    def __init__(self, r0: int): 
        self.rn: float = r0

    def set(self, rn: int) -> None:
        self.rn = float(rn)

    def produce(self, m: int, a: int) -> float:
        self.rn = (a * self.rn) % m
        return self.rn / float(m)

    def producen(self, n: int, m: int, a: int) -> List[float]:
        return [self.produce(m, a) for _ in range(n)]

    def get_m(self, points: List[float]) -> float:
        return sum(points) / len(points)
    
    def get_d(self, points: List[float]) -> float:
        s: float = 0.0
        m: float = self.get_m(points)
        for x in points:
            s += (x - m) ** 2
        return s / len(points)

    def get_o(self, points: List[float]) -> float:
        return self.get_d(points) ** 0.5 

    def show_plot(self, points: List[float]) -> None:
        import numpy as np
        import matplotlib.pyplot as plt
        xps: List[float] = np.array([i for i in range(len(points))])
        yps: List[float] = np.array(points)
        plt.plot(xps, yps, 'ro', markersize=1)
        plt.show()

    def show_histograms(self, points: List[float]) -> None:
        import matplotlib.pyplot as plt
        plt.hist(points, density=False, bins=20, edgecolor="black")
        plt.xlabel(
            f"m={self.get_m(points)}, " \
            f"d={self.get_d(points)}, " \
            f"σ={self.get_o(points)}"
        )
        plt.show()

if __name__ == "__main__":
    pass