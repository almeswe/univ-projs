a = 0
b = 8
e = 0.1

n = 16

f = lambda x: round(x*x-11*x+5, 3)

def dichotomy_method_table():
    global a, b
    j = 0
    yield ('', 'x1j', 'x2j', 'f1j', 'f2j', 'aj', 'bj')
    yield (0, '-', '-', '-', '-', a, b)
    while j < (n/2) and (b-a >= e):
        x1 = round(0.5*(a+b)-e/2, 3)
        x2 = round(0.5*(a+b)+e/2, 3)
        if f(x1) <= f(x2):
            b = x2
        else:
            a = x1
        j += 1
        yield (j, x1, x2, f(x1), f(x2), a, b)

rows = []
for row in dichotomy_method_table():
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