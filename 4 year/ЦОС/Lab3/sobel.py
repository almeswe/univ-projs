import math
import numpy as np

from common import *

class SobelOperator(ImageWrapProc):
    def __init__(self, path: str, kernel: int) -> None:
        assert kernel == 3
        super().__init__(path, kernel)
        self.pix_neighbours = np.array(self.pix_neighbours)
        self.__vborders: List[int] = np.array([
            -1, -2, -1,
             0,  0,  0,
             1,  2,  1
        ])
        self.__hborders: List[int] = np.array([
            -1, 0, 1,
            -2, 0, 2,
            -1, 0, 1
        ])

    def get_name(self) -> str:
        return 'sobel_operator'

    def blur_pix(self, x: int, y: int) -> int:
        pixels_r: List[int] = []
        pixels_g: List[int] = []
        pixels_b: List[int] = []
        for neighbour in self.pix_neighbours:
            xc: int = x + neighbour[0]
            yc: int = y + neighbour[1]
            if xc not in range(0, self.width-1):
                xc = 0 if xc < 0 else self.width-1
            if yc not in range(0, self.height-1):
                yc = 0 if yc < 0 else self.height-1
            pixel: ImagePixel = self.get_pixel(xc, yc)
            pixels_r.append(pixel.r)
            pixels_g.append(pixel.g)
            pixels_b.append(pixel.b)
            
        h_matrix_r: List[int] = np.multiply(self.__hborders, pixels_r)
        v_matrix_r: List[int] = np.multiply(self.__vborders, pixels_r)
        h_sum_r: int = np.sum(h_matrix_r)
        v_sum_r: int = np.sum(v_matrix_r)
        r = int(math.sqrt(h_sum_r**2 + v_sum_r**2))

        h_matrix_g: List[int] = np.multiply(self.__hborders, pixels_g)
        v_matrix_g: List[int] = np.multiply(self.__vborders, pixels_g)
        h_sum_g: int = np.sum(h_matrix_g)
        v_sum_g: int = np.sum(v_matrix_g)
        g = int(math.sqrt(h_sum_g**2 + v_sum_g**2))

        h_matrix_b: List[int] = np.multiply(self.__hborders, pixels_b)
        v_matrix_b: List[int] = np.multiply(self.__vborders, pixels_b)
        h_sum_b: int = np.sum(h_matrix_b)
        v_sum_b: int = np.sum(v_matrix_b)
        b = int(math.sqrt(h_sum_b**2 + v_sum_b**2))
        return ImagePixel(r, g, b)
    '''    
    def blur_pix(self, x: int, y: int) -> int:
        pixels: List[int] = []
        for neighbour in self.pix_neighbours:
            pixel: int = self.get_gs_pixel(
                x + neighbour[0],
                y + neighbour[1]
            )
            pixels.append(pixel)
        h_matrix: List[int] = np.multiply(self.__hborders, pixels)
        v_matrix: List[int] = np.multiply(self.__vborders, pixels)
        h_sum: int = np.sum(h_matrix)
        v_sum: int = np.sum(v_matrix)
        return int(math.sqrt(h_sum**2 + v_sum**2))

    def apply(self) -> ImageWrap:
        orig_gs: Image = ImageOps.grayscale(self.orig)
        self.pix = orig_gs.load()
        blur_image: Image = orig_gs.copy()
        blur = blur_image.load()
        for x in range(0, self.width):
            for y in range(0, self.height):
                blur[x, y] = self.blur_pix(x, y)
        self.orig = blur_image
        return self
    '''
if __name__ == '__main__':
    bootstrap(SobelOperator)