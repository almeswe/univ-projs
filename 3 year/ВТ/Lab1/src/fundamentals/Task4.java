package fundamentals;

public class Task4 {
    public static void solve(int[] array) {
        for (int i = 0; i < array.length; i++) {
            boolean isPrime = true;
            for (int j = 2; j <= Math.sqrt(array[i]); j++) {
                if (array[i] % j == 0) {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime) {
                System.out.printf("%d\n", array[i]);
            }
        }
    }
}
