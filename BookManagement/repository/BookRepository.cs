using BookManagement.entity;
using BookManagement.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.repository
{
    public class BookRepository
    {
        public void Add(Book book)
        {
            DatabaseService.Instance.Db.Insertable(book).ExecuteCommand();
        }
        public void Update(Book book)
        {
            DatabaseService.Instance.Db.Updateable(book).ExecuteCommand();
        }
        public void DeleteByISBN(string isbn)
        {
            DatabaseService.Instance.Db.Deleteable<Book>().Where(it => it.isbn == isbn).ExecuteCommand();
        }
        public Book GetByISBN(string isbn) {
            return DatabaseService.Instance.Db.Queryable<Book>().Where(it => it.isbn == isbn).First();
        }

        public List<Book> GetAll()
        {
            return DatabaseService.Instance.Db.Queryable<Book>().ToList();
        }

    }
}
