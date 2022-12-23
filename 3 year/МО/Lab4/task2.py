import random
import numpy as np

'''
vi = [700, 200, 500, 150, 800]
ki = [5, 5, 20, 3, 4]
si = [15, 4, 10, 2, 20]
fi = [4, 3, 5, 4, 4]
for i in range(5):
    print(f'{ki[i]*vi[i]}/q{i+1}+{1/2*si[i]}q{i+1}')
exit(0)
'''

e = 0.1

def f(q1, q2, q3, q4, q5):
    return 3500 /q1+7.50*q1+ \
           1000 /q2+2.00*q2+ \
           10000/q3+5.00*q3+ \
           450  /q4+1.00*q4+ \
           3200 /q5+10.0*q5

def fgrad(q1, q2, q3, q4, q5):
    return np.array([
        7.50-3500/q1**2,
        2.00-1000/q2**2,
        5.0-10000/q3**2,
        1.000-450/q4**2,
        10.0-3200/q5**2
    ])

def s(q1, q2, q3, q4, q5):
    grad = fgrad(q1, q2, q3, q4, q5)
    return -np.divide(grad, np.linalg.norm(grad))

def l(q1, q2, q3, q4, q5):
    step = e/10
    fval = float("inf")
    sval = s(q1, q2, q3, q4, q5)
    qval = np.array([q1, q2, q3, q4, q5])
    while True:
        qval = np.add(qval, np.multiply(step, sval))
        fnval = round(f(*qval), 4)
        if fnval >= fval:
            break
        fval = fnval 
        step *= 2
    return step

def graddecent(qinit):
    if len(qinit) != 5:
        return qinit
    r = 0
    q = np.array([*qinit])
    qn = np.multiply(2, q)
    print(f'initial: {qinit}')
    while np.linalg.norm(np.subtract(qn, q)) > e:
        q = qn
        qn = np.add(q, np.multiply(l(*q), s(*q)))
        print(f'{r+1}) {round(f(*q), 4):<10}:{qn}')
        r += 1
    return qn

def qrand(a, b):
    return np.array([
        random.randint(a, b),
        random.randint(a, b),
        random.randint(a, b),
        random.randint(a, b),
        random.randint(a, b)
    ])

# print(f'min: {graddecent(qrand(1.0, 3.0))}')
qmin = graddecent([20, 20, 20, 20, 20])
fmin = f(*qmin)
print(f'qres: {qmin}')
print(f'fmin: {fmin}')