import os, sys
import struct
import matplotlib.pyplot as plt

from typing import *

def plot_mag_spectrum(name: str, n: int) -> None:
    datax: List[float] = []
    datay: List[float] = []
    with open(f'{name}_frq.bin', 'rb') as raw:
        datax = struct.unpack('f'*n, raw.read(4*n))
    with open(f'{name}_amp.bin', 'rb') as raw:
        datay = struct.unpack('f'*n, raw.read(4*n))
    plt.plot(datax, datay, 'ro-')
    plt.show()

def plot_phs_spectrum(name: str, n: int) -> None:
    datax: List[float] = []
    datay: List[float] = []
    with open(f'{name}_frq.bin', 'rb') as raw:
        datax = struct.unpack('f'*n, raw.read(4*n))
    with open(f'{name}_phs.bin', 'rb') as raw:
        datay = struct.unpack('f'*n, raw.read(4*n))
    plt.plot(datax, datay, 'ro-')
    plt.show()

if __name__ == '__main__':
    if len(sys.argv) == 2:
        name: str = sys.argv[1]
        n: int = int(os.path.getsize(f'{name}_frq.bin')/4)
        plot_mag_spectrum(name, n)
        plot_phs_spectrum(name, n)
