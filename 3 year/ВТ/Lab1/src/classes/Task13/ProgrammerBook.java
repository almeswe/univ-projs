package classes.Task13;

import classes.Task12.Book;

public class ProgrammerBook extends Book {
    private String language;
    private int level;

    @Override
    public boolean equals(Object obj) {
        return this.hashCode() == super.hashCode();
    }

    @Override
    public int hashCode() {
        int result = super.hashCode();
        if (this.language != null) {
            result += this.language.hashCode();
        }
        result += this.level*7;
        return result;
    }

    @Override
    public String toString() {
        return String.format("%s language: %s level: %d",
                super.toString(), this.language, this.level);
    }
}
