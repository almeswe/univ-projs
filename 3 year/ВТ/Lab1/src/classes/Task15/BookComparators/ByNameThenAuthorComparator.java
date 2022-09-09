package classes.Task15.BookComparators;

import classes.Task12.Book;
import java.util.Comparator;

public class ByNameThenAuthorComparator implements Comparator<Book> {
    @Override
    public int compare(Book o1, Book o2) {
        var result = o1.getTitle().compareTo(o2.getTitle());
        if (result != 0) {
            return result;
        }
        return result + o1.getAuthor().compareTo(o2.getAuthor());
    }
}
