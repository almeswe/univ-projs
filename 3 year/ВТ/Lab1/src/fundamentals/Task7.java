package fundamentals;

public class Task7 {
    public static int[] solve(int[] numbers) {
        for (int gap = numbers.length/2; gap > 0; gap /= 2) {
            for (int i = gap; i < numbers.length; i += 1) {
                int j;
                int temp = numbers[i];
                for (j = i; j >= gap && numbers[j - gap] > temp; j -= gap) {
                    numbers[j] = numbers[j - gap];
                }
                numbers[j] = temp;
            }
        }
        return numbers;
    }
}
