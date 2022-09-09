package fundamentals;

public class Task6 {
    public static float[][] solve(float[] numbers) {
        float[][] matrix = new float[numbers.length][numbers.length];
        for (int i = 0; i < numbers.length; i++) {
            int j = i;
            for (int k = 0; k < numbers.length; k++) {
                matrix[i][k] = numbers[j];
                j = (j+1)%numbers.length;
            }
        }
        return matrix;
    }
}
