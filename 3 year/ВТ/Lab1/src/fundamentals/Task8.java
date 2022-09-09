package fundamentals;

public class Task8 {
    public static int[] solve(int[] a, int[] b) {
        int cIndex = 0;
        int aIndex = 0;
        int[] c = new int[a.length+b.length];

        for (int i = 0; i < b.length; i++) {
            while (aIndex < a.length && a[aIndex] <= b[i]) {
                c[cIndex++] = a[aIndex++];
            }
            c[cIndex++] = b[i];
        }
        for (int i = aIndex; i < a.length; i++) {
            c[cIndex++] = a[i];
        }
        return c;
    }
}
