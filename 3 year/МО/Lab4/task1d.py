a = 0
b = 8
n = 16
f = lambda x: round(x*x-11*x+5, 3)

x1n = 0
x2n = 0

PHI1 = 0.382
PHI2 = 0.618 

def golden_ratio_method_table():
    global a, b
    global x1n, x2n
    yield ('', 'x1j', 'x2j', 'f1j', 'f2j', 'aj', 'bj')
    yield (0, '-', '-', '-', '-', a, b)
    for j in range(1, n):
        x1 = x1n
        x2 = x2n
        if x1n == 0:
            x1 = a+PHI1*(b-a)
        if x2n == 0:
            x2 = a+PHI2*(b-a)
        x1, x2 = round(x1, 3), round(x2, 3)
        x1n, x2n = 0, 0 
        if f(x1) <= f(x2):
            b, x2n = x2, x1
        else:
            a, x1n = x1, x2
        yield (j, x1, x2, f(x1), f(x2), a, b)

rows = []
for row in golden_ratio_method_table():
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