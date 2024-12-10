using BookManagement.entity;
using BookManagement.util;
using System.Windows;

namespace BookManagement.page
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestDatabaseService();

        }

        private void TestDatabaseService()
        {
            try
            {
                // 获取单例实例
                var db = DatabaseService.Instance.Db;

                // 测试查询所有书籍
                List<Book> books = db.Queryable<Book>().ToList();

                // 打印查询结果到控制台
                foreach (var book in books)
                {
                    MessageBox.Show($"ISBN: {book.isbn}, 书名: {book.bookName}, 作者: {book.author}, 价格: {book.price}");
                }

               /* // 测试插入新书
                var newBook = new Book
                {
                    Title = "测试书籍",
                    Author = "测试作者",
                    Price = 19.99m
                };

                db.Insertable(newBook).ExecuteCommand();
                MessageBox.Show("新书已插入成功！");*/
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"数据库操作失败：{ex.Message}");
            }
        }
    }
}