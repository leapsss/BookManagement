using BookManagement.entity;
using BookManagement.service;
using System.Windows;
using System.Windows.Controls;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookManagement.page
{
    /// <summary>
    /// BookManagementPage.xaml 的交互逻辑
    /// </summary>
    public partial class BookManagementPage : Page
    {
        private BookService _bookService = new BookService();

        public BookManagementPage()
        {
            InitializeComponent();
            showBookInfo();
        }

        private void showBookInfo()
        {
           
            List<Book> books = _bookService.getAllBooks();
            BookDataGrid.ItemsSource = null; 
            BookDataGrid.ItemsSource = books;
        }

        public void addBookButton_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            bool? result = addBookWindow.ShowDialog();

            if (result == true)
            {
                showBookInfo();
            }
        }

        public void editBookButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = BookDataGrid.SelectedItem as Book;

            if (selectedBook != null)
            {              
                EditBookWindow editBookWindow = new EditBookWindow(selectedBook);
                bool? result = editBookWindow.ShowDialog();

                if (result == true)
                {
                    showBookInfo();
                }
            }
            else
            {
                MessageBox.Show("请选择一本书籍进行编辑！");
            }
        }
        
        public void deleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = BookDataGrid.SelectedItem as Book;

            if (selectedBook != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "确认删除吗？", 
                    "确认", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Question); 

                if (result == MessageBoxResult.Yes)
                {                   
                    BookService bookService = new BookService();
                    bookService.deleteBook(selectedBook.isbn);
                    showBookInfo(); 
                }
            }
            else
            {
                MessageBox.Show("请选择一本书籍进行编辑！");
            }
        }

        public void getBookButton_Click(object sender, RoutedEventArgs e)
        {
            string isbn = ISBNTextBox.Text;
            if (isbn != "")
            {               
                List<Book> books = new List<Book>();
                books.Add(_bookService.getBookByISBN(isbn));
                BookDataGrid.ItemsSource = books;
            }
            else{ 
                showBookInfo();
            }
            
        }
    }
}
