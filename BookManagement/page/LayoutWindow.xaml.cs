using BookManagement.entity;
using BookManagement.util;
using System.Windows;
using System.Windows.Controls;

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
            PermissionControl();
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
                case "进货单信息":
                    hosFrame.Source = new Uri("PurchaseOrderPage.xaml", UriKind.Relative);
                    break;
                case "进货单明细":
                    hosFrame.Source = new Uri("PurchaseOrderDetailPage.xaml", UriKind.Relative);
                    break;
                case "员工管理":
                    hosFrame.Source = new Uri("UserManagementPage.xaml", UriKind.Relative);
                    break;
                case "库存管理":
                    hosFrame.Source = new Uri("InventoryManagementPage.xaml", UriKind.Relative);
                    break;
                case "销售管理":
                    hosFrame.Source = new Uri("SalesPage.xaml", UriKind.Relative);
                    break;
                case "销售单信息":
                    hosFrame.Source = new Uri("SalesOrderPage.xaml", UriKind.Relative);
                    break;
                case "销售单明细":
                    hosFrame.Source = new Uri("SalesOrderDetailPage.xaml", UriKind.Relative);
                    break;
                case "报表中心":
                    hosFrame.Source = new Uri("ReportStatisticsPage.xaml", UriKind.Relative);
                    break;
                case "进货管理":
                    hosFrame.Source = new Uri("PurchasePage.xaml", UriKind.Relative);
                    break;
            }
        }

        private void PermissionControl()
        {
            User? user = Session.GetCurrentUser();
           
            switch (user.role)
            {
                case "admin":
                    ShowAdminButtons();
                    break;
                case "purchaser":
                    ShowPurchaserButtons();
                    break;
                case "saler":
                    ShowSalerButtons();
                    break;
                default:
                    ShowDefaultButtons();
                    break;
            }
            
        }
        private void ShowAdminButtons()
        {
            UserManagementButton.Visibility = Visibility.Visible;
            InventoryManagementButton.Visibility = Visibility.Visible;
            BookManagementButton.Visibility = Visibility.Collapsed;
            SupplierManagementButton.Visibility = Visibility.Collapsed;
            PurchaseManagementButton.Visibility = Visibility.Collapsed;
            PurchaseOrderButton.Visibility = Visibility.Collapsed;
            PurchaseOrderDetailButton.Visibility = Visibility.Visible;
            SalesManagementButton.Visibility = Visibility.Collapsed;
            SalesOrderDetailButton.Visibility = Visibility.Visible;
            ReportStatisticsButton.Visibility = Visibility.Visible;
        }

        // 显示进货员角色的按钮
        private void ShowPurchaserButtons()
        {
            UserManagementButton.Visibility = Visibility.Collapsed;
            InventoryManagementButton.Visibility = Visibility.Collapsed;
            BookManagementButton.Visibility = Visibility.Visible;
            SupplierManagementButton.Visibility = Visibility.Visible;
            PurchaseManagementButton.Visibility = Visibility.Visible;
            PurchaseOrderButton.Visibility = Visibility.Visible;
            PurchaseOrderDetailButton.Visibility = Visibility.Collapsed;
            SalesManagementButton.Visibility = Visibility.Collapsed;
            SalesOrderDetailButton.Visibility = Visibility.Collapsed;
            ReportStatisticsButton.Visibility = Visibility.Collapsed;
        }

        // 显示售货员角色的按钮
        private void ShowSalerButtons()
        {
            UserManagementButton.Visibility = Visibility.Collapsed;
            InventoryManagementButton.Visibility = Visibility.Visible;
            BookManagementButton.Visibility = Visibility.Collapsed;
            SupplierManagementButton.Visibility = Visibility.Collapsed;
            PurchaseManagementButton.Visibility = Visibility.Collapsed;
            PurchaseOrderButton.Visibility = Visibility.Collapsed;
            PurchaseOrderDetailButton.Visibility = Visibility.Collapsed;
            SalesManagementButton.Visibility = Visibility.Visible;
            SalesOrderDetailButton.Visibility = Visibility.Collapsed;
            ReportStatisticsButton.Visibility = Visibility.Collapsed;
        }

        // 显示默认按钮
        private void ShowDefaultButtons()
        {
            UserManagementButton.Visibility = Visibility.Visible;
            InventoryManagementButton.Visibility = Visibility.Visible;
            BookManagementButton.Visibility = Visibility.Visible;
            SupplierManagementButton.Visibility = Visibility.Visible;
            PurchaseManagementButton.Visibility = Visibility.Visible;
            PurchaseOrderButton.Visibility = Visibility.Visible;
            PurchaseOrderDetailButton.Visibility = Visibility.Visible;
            SalesManagementButton.Visibility = Visibility.Visible;
            SalesOrderDetailButton.Visibility = Visibility.Visible;
            ReportStatisticsButton.Visibility = Visibility.Visible;
        }
        
    }
}
