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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static BookManagement.service.SalesOrderService;

namespace BookManagement.page
{
    /// <summary>
    /// SalesOrderDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class SalesOrderDetailPage : Page
    {
        private SalesOrderService _salesOrderService = new SalesOrderService();

        public SalesOrderDetailPage()
        {
            InitializeComponent();
            ShowSalesOrderDetail();
        }

        public void ShowSalesOrderDetail()
        {
            List<CompleteSalesOrder> completeSalesOrders = _salesOrderService.getAllCompleteSalesOrder();
            CompleteSalesOrderDataGrid.ItemsSource = completeSalesOrders;
        }

        public void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
