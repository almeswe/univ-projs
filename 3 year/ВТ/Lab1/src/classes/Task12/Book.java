package classes.Task12;

public class Book implements Comparable<Book> {
    private int isbn;
    private int price;

    private String title;
    private String author;

    private static int edition;

    public int getIsbn() {
        return this.isbn;
    }

    public Integer getPrice() {
        return this.price;
    }

    public String getTitle() {
        return this.title;
    }

    public String getAuthor() {
        return this.author;
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (obj.getClass() != this.getClass()) {
            return false;
        }
        Book b = (Book)obj;
        return b.title.equals(this.title)   &&
               b.author.equals(this.author) &&
               b.price == this.price;
    }

    @Override
    public int hashCode() {
        int result = (price+edition)*257;
        if (this.title != null) {
            result += this.title.hashCode();
        }
        if (this.author != null) {
            result += this.author.hashCode();
        }
        return result;
    }

    @Override
    public String toString() {
        return String.format("title: %s, author: %s, price: %d, edition: %d",
                this.title, this.author, this.price, Book.edition);
    }

    @Override
    public Object clone() {
        var book = new Book();
        book.title = this.title;
        book.author = this.author;
        book.price = this.price;
        return book;
    }

    @Override
    public int compareTo(Book o) {
        if (o == null) {
            return 1;
        }
        return o.isbn > this.isbn ? -1 : 1;
    }
}