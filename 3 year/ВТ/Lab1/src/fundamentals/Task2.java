package fundamentals;

public class Task2 {
    public static boolean solve(float x, float y) {
        if (x >= -4 && x <= 4) {
            if (y >= 0 && y <= 5) {
                return true;
            }
        }
        if (x >= -6 && x <= 6) {
            if (y >= -3 && y <= 0) {
                return true;
            }
        }
        return false;
    }
}
