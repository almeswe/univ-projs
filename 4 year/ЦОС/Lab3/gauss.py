import math

from common import *

class GaussianBlur(ImageWrapProc):
    def __init__(self, path: str, kernel: int) -> None:
        super().__init__(path, kernel)
        self.__o: float = 0.84089642
        self.__weights: List[Tuple[int]] = [
            self.__get_weight(*xy) for xy in self.pix_neighbours 
        ]

    def __get_weight(self, x: int, y: int) -> float:
        return (1.0/(2*math.pi*self.__o**2))*math.e**-((x*x+y*y)/(2*self.__o**2))
    
    def get_name(self) -> str:
        return 'gaussian_blur'

    def blur_pix(self, x: int, y: int) -> ImagePixel:
        i: int = 0
        pixels: List[ImagePixel] = []
        for neighbour in self.pix_neighbours:
            pixel: ImagePixel = self.get_pixel(
                x + neighbour[0],
                y + neighbour[1]
            )
            if pixel != None:
                pixels.append(pixel*self.__weights[i])
            i += 1
        blur: ImagePixel = ImagePixel(0, 0, 0)
        for pixel in pixels:
            blur += pixel
        return blur

if __name__ == '__main__':
    bootstrap(GaussianBlur)