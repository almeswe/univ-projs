package fundamentals;

import java.lang.Math;

public class Task1 {
    public static double solve(double x, double y) {
        double term1 = 1 + Math.pow(Math.sin(x+y), 2);
        double term2 = 2 + Math.abs(x-((2*x)/(1+x*x*y*y)));
        return term1/term2+x;
    }
}
