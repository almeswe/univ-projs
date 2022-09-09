package fundamentals;

import java.util.*;
import java.lang.*;

public class Task3 {
    public static HashMap<Double, Double> solve(double a, double b, double h) {
        var table = new HashMap<Double, Double>();
        for (double x = a; x <= b; x += h) {
            table.put(x, Math.tan(x));
        }
        return table;
    }
}
