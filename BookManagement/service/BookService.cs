using BookManagement.entity;
using BookManagement.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.service
{
    public class BookService
    {
        private readonly BookMapper bookMapper;
        public BookService()
        {
            bookMapper = new BookMapper();
        }

        public void Add(Book book)
        {
            bookMapper.Add(book);
        }

        public Book GetByISBN(string isbn)
        {
            return bookMapper.GetByISBN(isbn);
        }

        public List<Book> GetAllBooks()
        {
            return bookMapper.GetAll();
        }

    }
}
