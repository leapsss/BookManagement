using BookManagement.entity;
using BookManagement.service;
using BookManagement.util;
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

            try
            {
                var user = GetUserById(userID);

                if (user == null)
                {
                    MessageBox.Show("用户ID不存在！");
                    return;
                }

                if (user.password == password)
                {
                    // 保存当前用户ID到 Session
                    Session.SetCurrentUserId(user.userId);

                    //MessageBox.Show("登录成功！");

                    LayoutWindow l = new LayoutWindow();
                    l.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("密码错误！");
                }
            }
            catch (Exception ex) { 

                MessageBox.Show("错误信息："+ ex.Message);
            }
        }


        //判断账号或者密码是否为空
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


        //调用service中的方法，通过id查询，并返回查询到的用户
        private User GetUserById(string userId)
        {
            try
            {
                return UserService.getUserById(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询用户时出错: {ex.Message}");
                return null;
            }
        }
    }
}
