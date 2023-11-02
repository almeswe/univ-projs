import abc
from PIL import Image
from PIL import ImageOps
from typing import List
from typing import Union
from typing import Tuple

class ImagePixel(abc.ABC):
    def __init__(self, r: int, g: int, b: int) -> None:
        self.r: int = r
        self.g: int = g
        self.b: int = b

    def __add__(self, x: object) -> object:
        self.r += x.r
        self.g += x.g
        self.b += x.b
        return self
    
    def __mul__(self, w: float) -> object:
        self.r = int(self.r * w)
        self.g = int(self.g * w)
        self.b = int(self.b * w)
        return self

    def __truediv__(self, x: int) -> object:
        self.r = int(self.r / x)
        self.g = int(self.g / x)
        self.b = int(self.b / x)
        return self
    
    def as_tuple(self) -> Tuple[int, int, int]:
        return (self.r, self.g, self.b)

    @staticmethod
    def get_median(pixels: List[object]) -> object:
        pixels_pivot: int = len(pixels) // 2
        pixels.sort(key=lambda p: p.r)
        m_r: int = pixels[pixels_pivot].r
        pixels.sort(key=lambda p: p.g)
        m_g: int = pixels[pixels_pivot].g
        pixels.sort(key=lambda p: p.b)
        m_b: int = pixels[pixels_pivot].b
        return ImagePixel(m_r, m_g, m_b)

    @staticmethod
    def get_neighbours(around: int) -> List[Tuple[int]]:
        assert around % 2 == 1
        dist: int = (around - 1) // 2
        map: List[Tuple[int]] = [
            *[(x-dist, y-dist) for x in range(0, around)
                for y in range(0, around)]
        ]
        return map

class ImageWrap(object):
    def __init__(self, path: str) -> None:
        self.path: str = path
        self.width: int = 0
        self.height: int = 0
        self.__read()

    def __read(self) -> None:
        self.orig: Image = Image.open(self.path)
        self.pix: List[List[int]] = self.orig.load()
        self.width, self.height = self.orig.size

    def save(self, to: str) -> None:
        import os
        if os.path.exists(to):
            os.remove(to)
        if self.orig != None:
            if len(to.split('.')) != 2:
                to = f"{to}.jpg"
            self.orig.save(to)
            print(f'saved to `{to}`')

    def get_pixel(self, x: int, y: int) -> ImagePixel:
        if (x < 0 or x >= self.width) or\
           (y < 0 or y >= self.height):
            return None
        return ImagePixel(*self.pix[x, y])
    
    def get_gs_pixel(self, x: int, y: int) -> int:
        if (x < 0 or x >= self.width) or\
           (y < 0 or y >= self.height):
            return 0
        return self.pix[x, y]

class ImageWrapProc(ImageWrap):
    def __init__(self, path: str, kernel: int = 3) -> None:
        assert kernel % 2 == 1
        super().__init__(path)
        self.kernel: int = kernel
        self.pix_neighbours: List[Tuple[int]] =\
            ImagePixel.get_neighbours(self.kernel)

    @abc.abstractclassmethod
    def get_name(self) -> str:
        raise NotImplemented

    @abc.abstractclassmethod
    def blur_pix(self, x: int, y: int) -> Union[int, ImagePixel]:
        raise NotImplemented

    def apply(self) -> ImageWrap:
        copy_image: Image = self.orig.copy() 
        copy_pix = copy_image.load()
        for x in range(0, self.width):
            for y in range(0, self.height):
                copy_pix[x, y] = self.blur_pix(x, y).as_tuple()
        self.orig = copy_image
        return self

def bootstrap(x: ImageWrapProc) -> None:
    import sys
    if len(sys.argv) == 3:
        path: str = sys.argv[1]
        kernel: int = int(sys.argv[2])
        y: ImageWrapProc = x(path, kernel)
        y.apply().save(f'{y.get_name()}_{path}')

if __name__ == '__main__':
    pass