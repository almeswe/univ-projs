import math
import numba
from common import *

from pprint import pprint

class GaussianBlur(ImageWrapProc):
    def __init__(self, path: str, kernel: int) -> None:
        self.__o: float = kernel
        self.__k: float = 6*self.__o+1
        super().__init__(path, self.__k)
        self.weights: List[float] = [
            self.__get_weight(*xy) for xy in self.pix_neighbours 
        ]
        '''
        s = sum(self.weights)
        for i in range(len(self.weights)):
            self.weights[i] /= s
        '''
        #pprint(self.weights)
        print(sum(self.weights))

    def __get_weight(self, x: int, y: int) -> float:
        t1 = -(x**2+y**2)/(2*(self.__o**2))
        return 1/(2*math.pi*(self.__o**2))*math.exp(t1)

    def get_name(self) -> str:
        return 'gaussian_blur'

    def blur_pix(self, x: int, y: int) -> ImagePixel:
        fblur: List[float] = [0.0, 0.0, 0.0]
        for neighbour, ni in zip(self.pix_neighbours, range(len(self.weights))):
            xc: int = x + neighbour[0]
            yc: int = y + neighbour[1]
            if xc not in range(0, self.width-1):
                xc = 0 if xc < 0 else self.width-1
            if yc not in range(0, self.height-1):
                yc = 0 if yc < 0 else self.height-1
            pixel: ImagePixel = self.get_pixel(xc, yc)
            fblur[0] += pixel.r*self.weights[ni]
            fblur[1] += pixel.g*self.weights[ni]
            fblur[2] += pixel.b*self.weights[ni]
        return ImagePixel(
            int(fblur[0]),# / len(self.weights)),
            int(fblur[1]),# / len(self.weights)),
            int(fblur[2]),# / len(self.weights)),
        )

if __name__ == '__main__':
    #path: str = 'image.jpg'
    #kernel: int = int(7)
    #y: GaussianBlur = GaussianBlur(path, kernel)
    #y.apply().save(f'{y.get_name()}_{path}')
    bootstrap(GaussianBlur)