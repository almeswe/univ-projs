package fundamentals;

public class Task5 {
    public static int solve(int[] numbers) {
        int maxCount = 0;
        int startIndex = 0;
        for (int i = 0; i < numbers.length; i++) {
            for (int j = i; j < numbers.length; j++) {
                int count = 1;
                int last = numbers[i];
                for (int k = j+1; k < numbers.length; k++) {
                    if (last < numbers[k]) {
                        count += 1;
                        last = numbers[k];
                    }
                }
                if (count > maxCount) {
                    startIndex = i;
                    maxCount = count;
                }
            }
        }
        System.out.printf("%d:%d\n", startIndex, maxCount);
        return numbers.length-maxCount;
    }
}
