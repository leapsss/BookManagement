using BookManagement.entity;
using BookManagement.util;

namespace BookManagement.mapper
{
    public class BookMapper
    {
        public static void addBook(Book book)
        {
            DatabaseService.Instance.Db.Insertable(book).ExecuteCommand();
        }
        public static void updateBook(Book book)
        {
            DatabaseService.Instance.Db.Updateable(book).ExecuteCommand();
        }
        public static void deleteBookByISBN(string isbn)
        {
            DatabaseService.Instance.Db.Deleteable<Book>().Where(it => it.isbn == isbn).ExecuteCommand();
        }
        public static Book getBookByISBN(string isbn)
        {
            return DatabaseService.Instance.Db.Queryable<Book>().Where(it => it.isbn == isbn).First();
        }

        public static List<Book> getAllBooks()
        {
            return DatabaseService.Instance.Db.Queryable<Book>().ToList();
        }

    }
}
