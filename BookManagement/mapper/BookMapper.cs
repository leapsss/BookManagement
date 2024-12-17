using BookManagement.entity;
using BookManagement.util;

namespace BookManagement.mapper
{
    public class BookMapper
    {
        public  void addBook(Book book)
        {
            DatabaseService.Instance.Db.Insertable(book).ExecuteCommand();
        }
        public  void updateBook(Book book)
        {
            DatabaseService.Instance.Db.Updateable(book).ExecuteCommand();
        }
        public  void deleteBookByISBN(string isbn)
        {
            DatabaseService.Instance.Db.Deleteable<Book>().Where(it => it.isbn == isbn).ExecuteCommand();
        }
        public  Book getBookByISBN(string isbn)
        {
            return DatabaseService.Instance.Db.Queryable<Book>().Where(it => it.isbn == isbn).First();
        }

        public  List<Book> getAllBooks()
        {
            return DatabaseService.Instance.Db.Queryable<Book>().ToList();
        }

    }
}
