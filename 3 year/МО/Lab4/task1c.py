from fibonacci import FIBONACCI as F

a = 0
b = 8
n = 16
e = (b-a)/F[n+1]#0.2
f = lambda x: round(x*x-11*x+5, 3)

x1n = 0
x2n = 0

def fibonacci_method_table():
    global a, b
    global x1n, x2n
    yield ('', 'x1j', 'x2j', 'f1j', 'f2j', 'aj', 'bj')
    yield (0, '-', '-', '-', '-', a, b)
    for j in range(1, n):
        x1 = x1n
        x2 = x2n
        if x1n == 0:
            x1 = a+F[n-j-1]/F[n-j+1]*(b-a)-(((-1)**(n-j+1))/(F[n-j+1])*e)
        if x2n == 0:
            x2 = a+F[n-j]/F[n-j+1]*(b-a)+(((-1)**(n-j+1))/(F[n-j+1])*e)
        x1, x2 = round(x1, 3), round(x2, 3)
        x2n, x1n = 0, 0
        if f(x1) <= f(x2):
            b, x2n = x2, x1
        else:
            a, x1n = x1, x2
        yield (j, x1, x2, f(x1), f(x2), a, b) 

rows = []
for row in fibonacci_method_table():
    rows.append(row)
    [print(f'|{i:<12}', end='') for i in row]; print()

farg = []
fres = []
minx = 0
minf = 0
for row in rows[2:]:
    farg += [row[1], row[2]]
    fres += [row[3], row[4]]
print(f'f*   : {min(fres)}')
print(f'x*   : {farg[fres.index(min(fres))]}')
print(f'delta: [{rows[-1][5]}; {rows[-1][6]}]')