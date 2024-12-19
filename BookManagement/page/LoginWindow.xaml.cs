using BookManagement.entity;
using BookManagement.service;
using BookManagement.util;
using System.Diagnostics.Metrics;
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
            string userId = txtUserID.Text.Trim();
            string password = txtPassword.Password;

            if (!ValidateInput(userId, password))
                return;

            User user = new User();

            try
            {
                user = UserService.GetUserByUserIdAndPassword(userId, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询用户时出错:\n{ex.Message}");
                return;
            }

            if (user != null)
            {                
                // 保存当前用户ID到 Session
                Session.SetCurrentUserId(user.id);
                Session.SetCurrentUser(user);

                LayoutWindow l = new LayoutWindow();
                l.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
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
                MessageBox.Show("密码不能为+空！");
                return false;
            }

            return true;
        }

    }
}
