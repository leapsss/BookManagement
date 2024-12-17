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
