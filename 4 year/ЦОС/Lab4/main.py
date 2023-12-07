import numpy as np

from PIL import Image
from PIL import ImageOps
from tqdm import tqdm

from scipy.ndimage import shift

class ImageProcessing(object):
    def __init__(self, im1: Image, im2: Image = None) -> None:
        self.im1: Image = im1
        self.im2: Image = im2 
        self.gsim1: Image = ImageOps.grayscale(im1)
        self.im1_npa: np.ndarray = np.array(self.gsim1)
        if im2 != None: 
            self.gsim2: Image = ImageOps.grayscale(im2)
            self.im2_npa: np.ndarray = np.array(self.gsim2)

    def norm(self, npa: np.ndarray) -> np.ndarray:
        return (npa - np.mean(npa)) / np.std(npa)

    def corr(self) -> np.ndarray:
        w1, h1 = self.im1.size
        w2, h2 = self.im2.size
        n_im1_npa: np.ndarray = self.norm(self.im1_npa)
        n_im2_npa: np.ndarray = self.norm(self.im2_npa)
        corr_npa: np.ndarray = np.zeros(shape=(w1, h1))
        for x in tqdm(range(0, w1), leave=False):
            for y in tqdm(range(0, h1), leave=False):
                pn_im2_npa: np.ndarray = np.pad(
                    n_im1_npa[x:, y:],
                    [(0, abs(min(w1-x+w2, 0))), 
                     (0, abs(min(h1-y+h2, 0)))],
                    mode="constant",
                    constant_values=0
                )
                pn_im2_npa = np.resize(pn_im2_npa, n_im2_npa.shape)
                corr_npa[x, y] = np.sum(pn_im2_npa * n_im2_npa)
        return corr_npa

    def autocorr(self) -> np.ndarray:
        height, width = self.im1_npa.shape
        autocorr = np.zeros(shape=(height, width))
        n_im1_npa: np.ndarray = self.norm(self.im1_npa)
        for dy in tqdm(range(height), leave=False):
            for dx in tqdm(range(width), leave=False):
                autocorr[dy, dx] = np.sum(n_im1_npa * np.roll(n_im1_npa, shift=(dy, dx), axis=(0, 1)))
        return autocorr

    def show_corr(self, corr: np.ndarray) -> None:
        import matplotlib.pyplot as plt
        tw, th = self.im2.size
        y, x = np.unravel_index(np.argmax(corr), corr.shape)

        plt.imshow(corr, cmap='gray')
        plt.colorbar(label='corr')
        plt.show()

        plt.figure(figsize=(10, 10))
        plt.imshow(self.im1, cmap='gray')
        rect = plt.Rectangle((x, y), tw, th, edgecolor='r', facecolor='none')
        plt.gca().add_patch(rect)
        plt.show()

    def show_autocorr(self, autocorr: np.ndarray) -> None:
        import matplotlib.pyplot as plt
        plt.imshow(self.im1, cmap='gray')
        plt.show()
    
        plt.imshow(autocorr, cmap='hot')
        plt.colorbar(label='autocorr')
        plt.show()

    '''
    @staticmethod
    def fft(x: np.ndarray) -> np.ndarray:
        n: int = len(x)
        if n <= 1: 
            return x
        e: np.ndarray = ImageProcessing.fft(x[0::2])
        o: np.ndarray = ImageProcessing.fft(x[1::2])
        temp: np.ndarray = [np.exp(-2j*np.pi*k/n)*o[k] for k in range(n//2)]
        return np.array([e[k] + temp[k] for k in range(n//2)] + [e[k] - temp[k] for k in range(n//2)])

    @staticmethod
    def ifft(x: np.ndarray) -> np.ndarray:
        n: int = len(x)
        if n <= 1:
            return x
        e: np.ndarray = ImageProcessing.ifft(x[0::2])
        o: np.ndarray = ImageProcessing.ifft(x[1::2])
        temp: np.ndarray = [np.exp(2j*np.pi*k/n)*o[k] for k in range(n//2)]
        return np.array([e[k] + temp[k] for k in range(n//2)] + [e[k] - temp[k] for k in range(n//2)]) / 2

    @staticmethod
    def fft2d(x: np.ndarray) -> np.ndarray:
        fft_rows = np.array([ImageProcessing.fft(row) for row in x])
        fft_2d = np.array([ImageProcessing.fft(column) for column in fft_rows.T]).T
        return fft_2d
    '''
        
if __name__ == '__main__':
    import sys
    if len(sys.argv) == 2:
        imp = ImageProcessing(
            Image.open(sys.argv[1]),
            None
        )
        corr = imp.autocorr()
        imp.show_autocorr(corr)
    if len(sys.argv) == 3:
        imp = ImageProcessing(
            Image.open(sys.argv[1]),
            Image.open(sys.argv[2]),
        )
        corr = imp.corr()
        imp.show_corr(corr)
    exit(0)
    ImageProcessing(
        Image.open(sys.argv[1]),
        Image.open(sys.argv[2]),
    )
    #ImageProcessing(Image.open("./test_image.jpg")).autocorr()