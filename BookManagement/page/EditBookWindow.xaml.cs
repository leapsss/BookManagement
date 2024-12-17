using BookManagement.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookManagement.page
{
    /// <summary>
    /// EditBookWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class EditBookWindow : Window
    {
        private Book _book;

        public EditBookWindow(Book book)
        {
            InitializeComponent();

            _book = book;
            // 初始化文本框，显示当前选中的书籍信息
            ISBNTextBox.Text = _book.isbn;
            BookNameTextBox.Text = _book.bookName;
            AuthorTextBox.Text = _book.author;
            PressTextBox.Text = _book.press;
            PressDateTextBox.Text = _book.pressDate;
            ClcNameTextBox.Text = _book.clcName;
            PriceTextBox.Text = _book.price.ToString();
            BookDescTextBox.Text = _book.bookDesc;
        }
    }
}
