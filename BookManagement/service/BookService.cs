using BookManagement.entity;
using BookManagement.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.service
{
    public class BookService
    {
        private readonly BookRepository bookRepository;
        public BookService()
        {
            bookRepository = new BookRepository();
        }

        public void Add(Book book)
        {
            bookRepository.Add(book);
        }

        public Book GetByISBN(string isbn)
        {
            return bookRepository.GetByISBN(isbn);
        }

        public List<Book> GetAllBooks()
        {
            return bookRepository.GetAll();
        }

    }
}
