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

        public void deleteBook(string isbn)
        {
            BookMapper.deleteBookByISBN(isbn);
        }
        public void updateBook(Book book)
        {
            BookMapper.updateBook(book);
        }
        public Book getBookByISBN(string isbn)
        {
            Book book = BookMapper.getBookByISBN(isbn);
            book.price /= 100.0m;
            return book;
        }

        public List<Book> getAllBooks()
        {
            List<Book> books = BookMapper.getAllBooks();
            foreach (var book in books)
            {
                book.price = book.price / 100.0m;  // 将 Price 转换为小数形式
            }
            return books;
        }

    }
}
