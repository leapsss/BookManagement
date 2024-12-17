using BookManagement.entity;
using BookManagement.service;
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
    /// UserUpdateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserUpdateWindow : Window
    {
        public User CurrentUser { get; set; }
        private readonly UserService userService;
        public UserUpdateWindow(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            userService = new UserService();
            // Populate the fields with the user data
            UsernameTextBox.Text = user.username;
            PasswordBox.Password = user.password;
            RoleTextBox.Text = user.role;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Update the user object with the modified values
            CurrentUser.username = UsernameTextBox.Text;
            CurrentUser.password = PasswordBox.Password;
            CurrentUser.role = RoleTextBox.Text;
            userService.UpdateUser(CurrentUser);
            // Close the window
            this.DialogResult = true;
            this.Close();
        }
    }

}
