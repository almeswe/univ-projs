a = 0
b = 8
e = 0.1

n1 = 16
n2 = 17

f = lambda x: round(x*x-11*x+5, 3)

x1_fun_memory = 0

# it = iteration
def x1(it):
    global x1_fun_memory
    calc_it = it-x1_fun_memory
    val = a+((b-a)/(n1/2+1)*calc_it) + ((-e/2) if it % 2 != 0 else (e/2))
    if it % 2 == 1:
        x1_fun_memory += 1
    return round(val, 3)

def x2(it):
    val = a+((b-a)/(n2+1))*it
    return round(val, 3)

f1arg = [x1(i) for i in range(1, n1+1)]
f2arg = [x2(i) for i in range(1, n2+1)]
f1res = [f(x) for x in f1arg]
f2res = [f(x) for x in f2arg]

[print(f'|{x:<10}', end='') for x in f1arg]; print()
[print(f'|{y:<10}', end='') for y in f1res]; print()
f1min = min(f1res)
index = f1res.index(f1min)
print(f'min  : {f1min}, {index+1}')
print(f'delta: [{f1arg[index-1]}; {f1arg[index+1]}]')
print(f'x*   : {f1arg[index]}')
print(f'f*   : {f1res[index]}')
print()

[print(f'|{x:<10}', end='') for x in f2arg]; print()
[print(f'|{y:<10}', end='') for y in f2res]; print()
f2min = min(f2res)
index = f2res.index(f2min)
print(f'min  : {f2min}, {index+1}')
print(f'delta: [{f2arg[index-1]}; {f2arg[index+1]}]')
print(f'x*   : {f2arg[index]}')
print(f'f*   : {f2res[index]}')