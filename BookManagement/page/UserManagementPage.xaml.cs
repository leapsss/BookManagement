﻿using BookManagement.entity;
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
using BookManagement.service;
using System.Drawing.Printing;
namespace BookManagement.page
{
    /// <summary>
    /// UserManagementPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserManagementPage : Page
    {
        private readonly UserService userService;
        private int currentPage = 1;
        private const int pageSize = 15;
        private int totalPages = 0;
        public UserManagementPage()
        {
            InitializeComponent();
            userService = new UserService();
            LoadUsers();
        }

        // Load users from the database
        private void LoadUsers()
        {
            UserDataGrid.ItemsSource = userService.GetUsers(); // Bind users to DataGrid
            LoadPageData();
        }
        private void LoadPageData()
        {
            var pagedData = userService.GetPagedUsers(currentPage, pageSize);
            var totalData = userService.GetUsers();
            UserDataGrid.ItemsSource = pagedData;
            totalPages = (int)Math.Ceiling(totalData.Count / (double)pageSize);
            PageInfoText.Text = $"第 {currentPage} 页 / 共 {Math.Ceiling(totalData.Count / (double)pageSize)} 页";
        }
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage >= totalPages)
            {
                MessageBox.Show("已经是最后一页了");
            }
            else
            {
                currentPage++;
                LoadPageData();
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string userId = UserIdTextBox.Text;
            string username = UsernameTextBox.Text;
            string role = RoleTextBox.Text;

            var filteredOrders = userService.GetFilteredUsers(userId, username, role);
            UserDataGrid.ItemsSource = filteredOrders;
        }
        // 上一页
        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPageData();
            }
        }
        // Edit button click event
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement the Edit functionality here
            var user = ((Button)sender).DataContext as User;
            //打开编辑界面
            // Open edit dialog or navigate to the edit page with the selected user
            if (user != null)
            {
                // 创建并显示 UserUpdateWindow
                var updateWindow = new UserUpdateWindow(user);

                // 设置为模式对话框（等待用户操作完成）
                if (updateWindow.ShowDialog() == true)
                {
                    // 如果保存成功，可以在这里处理，例如更新数据库
                    MessageBox.Show($"User '{user.username}' updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // 重新加载数据（如果需要）
                    LoadUsers();
                }
            }
        }

        // Delete button click event
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var user = ((Button)sender).DataContext as User;
            if (user != null)
            {
                var result = MessageBox.Show($"你确定删除用户 {user.username}吗?", "Confirm Deletion", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    userService.DeleteUser(user.userId); // Delete user by userId
                    LoadUsers(); // Reload user list after deletion
                }
            }
        }

    }
}
