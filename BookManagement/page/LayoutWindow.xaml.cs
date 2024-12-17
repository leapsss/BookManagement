using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookManagement.page
{
    /// <summary>
    /// LayoutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LayoutWindow : Window
    {
        public LayoutWindow()
        {
            InitializeComponent();
        }
        private void Router(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            string s = btn.Content.ToString();
            switch (s)
            {
                case "书籍管理":
                    hosFrame.Source = new Uri("BookManagementPage.xaml", UriKind.Relative);                   
                    break;
                case "供货商管理":
                    hosFrame.Source = new Uri("SupplierManagementPage.xaml", UriKind.Relative);
                    break;
                case "库存管理":
                    hosFrame.Source = new Uri("InventoryManagementPage.xaml", UriKind.Relative);
                    break;
            }
        }
    }
}
