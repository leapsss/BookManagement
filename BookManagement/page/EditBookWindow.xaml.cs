using BookManagement.entity;
using BookManagement.service;
using System.Windows;

namespace BookManagement.page
{
    /// <summary>
    /// EditBookWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class EditBookWindow : Window
    {
        private Book _book;
        private BookService _bookService = new BookService();

        public EditBookWindow(Book book)
        {
            InitializeComponent();

            _book = book;
            // 初始化文本框，显示当前选中的书籍信息
            ISBNTextBox.Text = _book.isbn;
            bookNameTextBox.Text = _book.bookName;
            authorTextBox.Text = _book.author;
            pressTextBox.Text = _book.press;
            pressDateTextBox.Text = _book.pressDate;
            clcNameTextBox.Text = _book.clcName;
            priceTextBox.Text = _book.price.ToString();
            bookDescTextBox.Text = _book.bookDesc;
        }
        public void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            _book.isbn = ISBNTextBox.Text;
            _book.bookName = bookNameTextBox.Text;
            _book.author = authorTextBox.Text;
            _book.press = pressTextBox.Text;
            _book.pressDate = pressDateTextBox.Text;
            _book.clcName = clcNameTextBox.Text;
            _book.price = (int)(Convert.ToDecimal(priceTextBox.Text) * 100);
            _book.bookDesc = bookDescTextBox.Text;


            _bookService.updateBook(_book);
            this.DialogResult = true;
            this.Close();
        }
    }
}
