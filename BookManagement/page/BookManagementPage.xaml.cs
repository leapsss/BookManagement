using BookManagement.entity;
using BookManagement.service;
using BookManagement.util;
using System.Windows;
using System.Windows.Controls;

namespace BookManagement.page
{
    /// <summary>
    /// BookManagementPage.xaml 的交互逻辑
    /// </summary>
    public partial class BookManagementPage : Page
    {
        public BookManagementPage()
        {
            InitializeComponent();
            showBookInfo();
        }
        public void showBookInfo()
        {
            BookService bookService = new BookService();
            List<Book> books = bookService.getAllBooks();
            foreach (var book in books)
            {
                MessageBox.Show($"ISBN: {book.isbn}, 书名: {book.bookName}, 作者: {book.author}, 价格: {book.price}");
            }
            BooksListView.ItemsSource = books;
        }
        public void addBookButton_Click(object sender, RoutedEventArgs e)
        {

        }
        public void editBookButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
