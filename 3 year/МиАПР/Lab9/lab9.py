import gzip
import pickle
import random
import struct
import numpy as np
import matplotlib.pyplot as plt

def unpack_mnist(path):
    with gzip.open(path, 'rb') as f:
        nrows, ncols = None, None
        magic, count = struct.unpack('>II', f.read(8))
        if magic == 2051:
            nrows, ncols = struct.unpack('>II', f.read(8))
        data = np.frombuffer(f.read(), 
            dtype=np.dtype(np.uint8).newbyteorder('>'))
        if nrows and ncols:
            data = data.reshape(count, nrows*ncols, 1)
            data = np.divide(data, 255.0)
        else:
            data = data.reshape(count, 1)
        return data
    
def load_test_dataset():
    test_images = unpack_mnist('./t10k_images_idx3.gz')
    test_labels = unpack_mnist('./t10k_labels_idx1.gz')
    return zip(test_images, test_labels)

def load_train_dataset():
    train_images = unpack_mnist('./train_60k.gz')
    train_labels = unpack_mnist('./answers_60k.gz')
    return zip(train_images, train_labels)

def sigmoid(z):
    return 1 / (1 + np.exp(-z))

def dsigmoid(z):
    return sigmoid(z) * (1 - sigmoid(z))

def cost(y, a):
    return np.sum(a - y) ** 2

def one_hot(o):
    zeros = np.zeros(10).reshape(10, 1)
    zeros[o] = 1
    return zeros

test_data  = list(load_test_dataset())
train_data = list(load_train_dataset())
 
z_vec = []
a_vec = []
b_vec = [
    np.random.randn(16, 1),
    np.random.randn(10, 1)
]
w_vec = [
    np.random.randn(784, 16),
    np.random.randn(16, 10)
]

def forwprop(a):
    al = a
    z_vec.clear()
    a_vec.clear()
    a_vec.append(al)
    for wl, bl in zip(w_vec, b_vec):
        zl = np.dot(wl.T, al) + bl
        al = sigmoid(zl)
        z_vec.append(zl)
        a_vec.append(al)
    return al

def backprop(y, a, rate):
    delta = 2*(a-y)*dsigmoid(z_vec[1])
    dwl1 = delta @ a_vec[1].T 
    dbl1 = delta

    delta = w_vec[1] @ delta * dsigmoid(z_vec[0])
    dwl2 = delta @ a_vec[0].T 
    dbl2 = delta

    w_vec[1] = w_vec[1] - rate * dwl1.T
    b_vec[1] = b_vec[1] - rate * dbl1
    w_vec[0] = w_vec[0] - rate * dwl2.T
    b_vec[0] = b_vec[0] - rate * dbl2


def show(sample, answer):
    print(answer)
    arr = np.asarray(sample).reshape((28, 28))
    plt.imshow(arr, cmap='gray', vmin=0, vmax=1)
    plt.show()

def accuracy(data):
    correct = 0
    for a, y in data:
        if np.argmax(forwprop(a)) == y:
            correct += 1
    print(f"accuracy: {correct / len(data) * 100}%")

def train(iterations, rate):
    for i in range(iterations):
        random.shuffle(train_data)
        for a, y in train_data:
            a = forwprop(a)
            y = one_hot(y)
            backprop(y, a, rate)
        print(f'[{i}]', end=' ')
        #print(f'[{i}] cost: {cost(y, a) / len(a)}')
        accuracy(test_data)
#train(30, 3)