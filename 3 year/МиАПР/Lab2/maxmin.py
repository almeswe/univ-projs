from base import *
from point import *

from typing import Dict

class MaxMin(Classificator):
    def __init__(self, points: int) -> None:
        super().__init__(points)

    def resolve(self, xy: List[int]) -> int:
        self.reset(xy)
        self.finished: bool = False
        assert len(self.points) >= 1
        self.clusters = [
            self.points[0],
            self.getfar(self.points[0])
        ]
        while not self.finished:
            self.iterate()
        return self.ct_len

    def treshold(self) -> float:
        assert len(self.clusters) >= 2
        return self.clusters[0] \
            .dist(self.clusters[1]) / 2

    def getfar(self, to: Point) -> Point:
        dists: List[float] = [
            to.dist(point) \
                for point in self.points
        ]
        return self.points[dists.index(max(dists))]

    def iterate(self) -> None:
        found: bool = False
        groups: Dict[Point, List[Point]] = {}
        for cluster in self.clusters:
            groups[cluster] = []
        for point in self.points:
            dists: List[float] = [
                point.dist(cluster) \
                    for cluster in self.clusters
            ]
            index: int = dists.index(min(dists))
            groups[self.clusters[index]].append(point)
        for cluster in groups:
            points: List[Point] = groups[cluster]
            dists: List[float] = [
                cluster.dist(point) \
                    for point in points
            ]
            index: int = dists.index(max(dists))
            if dists[index] > self.treshold():
                self.clusters.append(points[index])
                found = True; break;
        if not found:
            self.finished = True
        self.ct_len = len(self.clusters)

if __name__ == '__main__':
    pass