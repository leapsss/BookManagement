using BookManagement.entity;
using BookManagement.mapper;

namespace BookManagement.service
{
    public class BookService
    {
        public BookService() { }

        public void addBook(Book book)
        {
            BookMapper.addBook(book);
        }

        public Book getBookByISBN(string isbn)
        {
            return BookMapper.getBookByISBN(isbn);
        }

        public List<Book> getAllBooks()
        {
            return BookMapper.getAllBooks();
        }

    }
}
