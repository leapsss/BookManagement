using BookManagement.entity;
using BookManagement.mapper;

namespace BookManagement.service
{
    public class BookService
    {
        private readonly BookMapper bookMapper;
        public BookService() {
            bookMapper = new BookMapper();
                }

        public void addBook(Book book)
        {
            bookMapper.addBook(book);
        }

        public Book getBookByISBN(string isbn)
        {
            return bookMapper.getBookByISBN(isbn);
        }

        public List<Book> getAllBooks()
        {
            return bookMapper.getAllBooks();
        }

    }
}
