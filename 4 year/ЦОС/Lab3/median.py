from common import *

class MedianFilter(ImageWrapProc):
    def __init__(self, path: str, kernel: int) -> None:
        super().__init__(path, kernel)

    def get_name(self) -> str:
        return 'median_filter'

    def blur_pix(self, x: int, y: int) -> ImagePixel:
        pixels: List[ImagePixel] = []
        for neighbours in self.pix_neighbours:
            pixel: ImagePixel = self.get_pixel(
                x + neighbours[0],
                y + neighbours[1]
            )
            if pixel != None:
                pixels.append(pixel)
        return ImagePixel.get_median(pixels)
    
if __name__ == '__main__':
    bootstrap(MedianFilter)