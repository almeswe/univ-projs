from typing import List

class lemer(object):
    def __init__(self, rn: int): 
        self.rn: int = 1
        self.set(rn)

    def set(self, rn: int) -> None:
        self.rn = rn

    def produce_int(self, m: int, a: int) -> int:
        self.rn = (a * self.rn) % m
        return self.rn

    def produce_ints(self, n: int, m: int, a: int) -> List[int]:
        return [self.produce_int(m, a) for _ in range(n)]

    def produce(self, m: int, a: int) -> float:
        self.rn = (a * self.rn) % m
        return self.rn / float(m)

    def produces(self, n: int, m: int, a: int) -> List[float]:
        return [self.produce(m, a) for _ in range(n)]

    @staticmethod
    def get_m(points: List[float]) -> float:
        return sum(points) / len(points)

    @staticmethod   
    def get_d(points: List[float]) -> float:
        s: float = 0.0
        m: float = lemer.get_m(points)
        for x in points:
            s += (x-m)**2
        return s / len(points)

    @staticmethod
    def get_p(points: List[float]) -> float:
        k: int = 0
        even: bool = False
        if len(points) % 2 == 0:
            even = True
        for i in range(0, len(points), 2):
            if points[i]**2 + points[i+1]**2 < 1:
                k += 1
        return 2*k/len(points)

    @staticmethod
    def get_o(points: List[float]) -> float:
        return lemer.get_d(points)**0.5 

    @staticmethod
    def get_l(v: int, r0: int, m: int, a: int) -> int:
        i3: int = 0
        g1: lemer = lemer(r0)
        g2: lemer = lemer(r0)
        gen1: List[float] = g1.produce_ints(int(v), m, a)
        gen2: List[float] = []
        xr: List[int] = []
        xv: float = gen1[-1]
        g1.set(r0)
        i: int = 0
        while len(xr) != 2:
            xi: int = g1.produce_int(m, a)
            if xi == xv:
               xr.append(i)
            gen2.append(xi)
            i += 1
        p: int = abs(xr[1] - xr[0])
        print(f"p={p}")
        g1 = lemer(r0)
        g2 = lemer(gen2[p-1])
        while True:
            x1 = g1.produce_int(m, a)
            x2 = g2.produce_int(m, a)
            if x1 == x2:
                break
            i3 += 1
        return i3 + p

    @staticmethod
    def show_plot(points: List[float]) -> None:
        import numpy as np
        import matplotlib.pyplot as plt
        xps: List[float] = np.array([i for i in range(len(points))])
        yps: List[float] = np.array(points)
        plt.plot(xps, yps, 'ro', markersize=1)
        plt.show()

    @staticmethod
    def show_histograms(points: List[float]) -> None:
        import matplotlib.pyplot as plt
        '''
        xmin: float = min(points)
        xmax: float = max(points)
        rvar: float = xmax - xmin
        d: float = rvar/20
        edges: List[List[float]] = [
            [i*d, i*(d+1)] for i in range(20) 
        ]
        freqs: List[int] = [
            0 for _ in range(20)
        ]
        for x in points:
            for edge in edges:
                if x >= edge[0] or x < edge[1]:
                    freqs[edges.index(edge)] += 1
        '''
        plt.hist(points, density=False, bins=20, edgecolor="black")
        plt.xlabel(
            f"m={lemer.get_m(points)}, " \
            f"d={lemer.get_d(points)}, " \
            f"σ={lemer.get_o(points)}"
        )
        plt.show()
'''
    m = 2**31 (RAND_MAX+1)
    a = 1103515245
    https://opensource.apple.com/source/Libc/Libc-166/stdlib.subproj/rand.c.auto.html

    when L > 50000:
        r0 = 67499
        m  = 301493
        a  = 12345
'''

def manual() -> None:
    n: int = int(input("n : "))
    assert n >= 1
    r: int = eval(input("r0: "))
    assert r >= 1
    m: int = eval(input("m : "))
    assert m >= 1
    a: int = eval(input("a : "))
    assert a >= 1
    g: lemer = lemer(r)
    p: List[float] = g.produces(n, m, a)
    lemer.show_histograms(p)
    p: float = lemer.get_p(p)
    print(f'P={p}')
    print(f'L={lemer.get_l(10.0e+6/5, r, m, a)}')

def main() -> None:
    manual()

if __name__ == '__main__':
    main()