import random

from solvefunc import *

CLASSES = 10

TRAIN_LEARNING_RATE     = 1
TRAIN_VOLUME_FOR_CLASS  = 3
TRAIN_DATA_VALUES_RANGE = 3

class Perceptron(object):
    def __init__(self, n: int) -> None:
        self._n: int = n
        self._init_solve_funcs()
        self._init_training_set()

    def _init_training_set(self) -> None:
        self.training_set: List[Tuple[List[float], float]] = []
        #print(f'generating training set..')
        for n in range(self._n):
            #print(f'generating training set for class: {n}.. ', flush=True, end='')
            for _ in range(TRAIN_VOLUME_FOR_CLASS):
                subrange: int = TRAIN_DATA_VALUES_RANGE
                treshold_top : int = (subrange * self._n) - subrange * n
                treshold_down: int = (subrange * self._n) - subrange * (n + 1)
                self.training_set.append((
                    [random.randint(treshold_down, treshold_top) \
                        for _ in range(self._n)], n
                ))

    def _init_solve_funcs(self) -> None:
        self.sfuncs: List[SolveFunc] = []
        w_vec_init: List[float] = [0 for _ in range(self._n)]
        for n in range(self._n):
            self.sfuncs.append(SolveFunc(n, w_vec_init.copy()))

    def _print_iter(self, iter: int) -> None:
        print(f'\r{" " * 25}', end='')
        print(f'\riteration #{iter}', end='')

    def train(self) -> List[SolveFunc]:
        iter: int = 0
        trained: bool = False
        while not trained:
            trained = True
            for training_data in self.training_set:
                n: int = training_data[1]
                x: List[float] = training_data[0]
                r: List[float] = [
                    f.d(x) for f in self.sfuncs
                ]
                encouraged = False
                for i in range(len(r)):
                    if (i != n) and (r[i] >= r[n]):
                        self.sfuncs[i].punish(TRAIN_LEARNING_RATE, x)
                        if not encouraged:
                            self.sfuncs[n].encourage(TRAIN_LEARNING_RATE, x)
                            encouraged = True
                        trained = False
            iter += 1
            self._print_iter(iter)
        print()
        return self.sfuncs

    def classify(self, x: List[float]) -> int:
        assert len(x) == self._n
        r: List[float] = [
            f.d(x) for f in self.sfuncs
        ]
        print(r)
        m: float = max(r)
        return r.index(m)

if __name__ == '__main__':
    p: Perceptron = Perceptron(CLASSES)
    for training_data in p.training_set: 
        print(training_data)
    print()
    for dfunc in p.train():
        print(f'd({dfunc.n}): {dfunc.w_vec}')
    while True:
        x_vec: List[float] = []
        for _ in range(CLASSES):
            x_vec.append(float(input('> ')))
        print(f'input         : {x_vec}')
        print(f'assigned class: {p.classify(x_vec)}')