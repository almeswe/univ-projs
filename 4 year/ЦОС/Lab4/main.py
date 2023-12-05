import numpy as np

from PIL import Image
from PIL import ImageOps
from tqdm import tqdm

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
        from scipy import fftpack
        n_im1_npa: np.ndarray = self.norm(self.im1_npa)
        f: np.ndarray = fftpack.fftn(n_im1_npa)
        fps: np.ndarray = np.abs(f) ** 2
        return np.array(fftpack.ifftn(fps).real)

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
        plt.imshow(autocorr, cmap='hot')
        plt.colorbar(label='autocorr')
        plt.show()

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
    '''
    ImageProcessing(
        Image.open(sys.argv[1]),
        Image.open(sys.argv[2]),
    )
    '''