from distributions import *

r0 = 1
m = 2**32-1
a = 1103515245
n = 10000

'''
variant for 3rd lab: 20
'''

def iinput(name: str) -> int:
    i: str = input(f"{name}: ")
    while True:
        try:
            return int(i)
        except Exception:
            print("Incorrect input, try again.")

def finput(name: str) -> float:
    i: str = input(f"{name}: ")
    while True:
        try:
            return float(i)
        except Exception:
            print("Incorrect input, try again.")

def show_uniform() -> (generator, List[float]):
    print("UNIFORM DIST")
    g: generator = uniform(
        r0,
        finput("a"),
        finput("b")
    )
    return (g, g.producen(n, m, a))

def show_gauss() -> (generator, List[float]):
    print("GAUSS DIST")
    g: generator = gauss(
        r0,
        finput("m"),
        finput("σ"),
        iinput("ni")
    )
    return (g, g.producen(n, m, a))

def show_exp() -> (generator, List[float]):
    print("EXPONENTIAL DIST")
    g: generator = exponential(
        r0,
        finput("λ")
    )
    return (g, g.producen(n, m, a))

def show_gamma() -> (generator, List[float]):
    print("GAMMA DIST")
    g: generator = gamma(
        r0,
        finput("λ"),
        iinput("η")
    )
    return (g, g.producen(n, m, a))

def show_triangle() -> (generator, List[float]):
    print("TRIANGLE DIST")
    g: generator = triangle(
        r0,
        finput("a"),
        finput("b")
    )
    return (g, g.producen(n, m, a))

def show_simpson() -> (generator, List[float]):
    print("SIMPSON DIST")
    g: generator = simpson(
        r0,
        finput("a"),
        finput("b")
    )
    return (g, g.producen(n, m, a))

'''
    uniform(1, 5, 10)
    gauss(1, 5, 0.3, 20),
    exponential(1, 2),
    gamma(1, 2, 10),
    triangle(1, 5, 10),
    simpson(1, 5, 10)
'''

def main() -> None:
    fns = [
        show_uniform,
        show_gauss,
        show_exp,
        show_gamma,
        show_triangle,
        show_simpson
    ]
    for a in fns:
        (g, p) = a()
        g.show_histograms(p)

if __name__ == "__main__":
    main()