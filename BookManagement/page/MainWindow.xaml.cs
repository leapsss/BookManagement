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
        }
        private void ToUserManagement_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UserManagementPage());
        }

    }
}