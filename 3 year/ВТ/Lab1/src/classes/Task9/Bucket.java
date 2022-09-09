package classes.Task9;

import java.util.*;

public class Bucket {
    private final ArrayList<Ball> balls;

    public Bucket() {
        this.balls = new ArrayList<Ball>();
    }

    public void addBall(Ball ball) {
        this.balls.add(ball);
    }

    public float calcWeight() {
        float result = 0;
        for (int i = 0; i < this.balls.size(); i++) {
            result += this.balls.get(i).weight;
        }
        return result;
    }

    public int getCountOfBlueBalls() {
        int count = 0;
        for (int i = 0; i < this.balls.size(); i++) {
            if (this.balls.get(i).color == Color.Blue) {
                count += 1;
            }
        }
        return count;
    }
}
