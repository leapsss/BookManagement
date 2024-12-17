using BookManagement.page;
using BookManagement.util;
using System;
using System.Windows;

namespace BookManagement.page
{
    public partial class LoginWindow : Window
    {
        private readonly DatabaseService _databaseService;

        public LoginWindow()
        {
            InitializeComponent();
            _databaseService = DatabaseService.Instance;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userID = txtUserID.Text.Trim();
            string password = txtPassword.Password;

            if (!ValidateInput(userID, password))
                return;

            if (int.TryParse(userID, out int parsedUserId))
            {
                var user = GetUserById(parsedUserId);

                if (user == null)
                {
                    MessageBox.Show("用户ID不存在！");
                    return;
                }

                if (user.password == password)
                {
                    // 保存当前用户ID到 Session
                    Session.SetCurrentUserId(user.userId);

                    MessageBox.Show("登录成功！");
                    LayoutWindow layoutWindow = new LayoutWindow();
                    layoutWindow.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("密码错误！");
                }
            }
            else
            {
                MessageBox.Show("请输入有效的用户ID！");
            }
        }

        private bool ValidateInput(string userID, string password)
        {
            if (string.IsNullOrEmpty(userID))
            {
                MessageBox.Show("用户ID不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("密码不能为空！");
                return false;
            }

            return true;
        }

        private BookManagement.entity.users GetUserById(int userId)
        {
            try
            {
                return _databaseService.Db.Queryable<BookManagement.entity.users>()
                                           .First(u => u.userId == userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询用户时出错: {ex.Message}");
                return null;
            }
        }

    }
}
