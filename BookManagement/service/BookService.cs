using BookManagement.entity;
using BookManagement.mapper;

namespace BookManagement.service
{
    public class BookService
    {
        private readonly BookMapper bookMapper;

        public BookService()
        {
            bookMapper = new BookMapper();
        }

        public void addBook(Book book)
        {
            bookMapper.Add(book);
        }

        public Book getByISBN(string isbn)
        {
            return bookMapper.GetByISBN(isbn);
        }

        public List<Book> getAllBooks()
        {
            return bookMapper.GetAll();
        }

    }
}
