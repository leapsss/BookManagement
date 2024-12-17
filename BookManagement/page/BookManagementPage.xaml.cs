using BookManagement.entity;
using BookManagement.util;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }
        public void getBookInfo()
        {
            try
            {
                var db = DatabaseService.Instance.Db;
                List<Book> books = db.Queryable<Book>().ToList();
                BooksListView.ItemsSource = books;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"数据库操作失败：{ex.Message}");
            }
        }
    }
}
