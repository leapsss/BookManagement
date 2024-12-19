using BookManagement.service;
using System.Windows;
using System.Windows.Controls;
using static BookManagement.service.SalesOrderService;

namespace BookManagement.page
{
    /// <summary>
    /// SalesOrderDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class SalesOrderDetailPage : Page
    {

        private int _pageSize = 15;
        private int _currentPage = 1;
        private int _totalRecords = 0; 
        private int _totalPages = 0;

        public SalesOrderDetailPage()
        {
            InitializeComponent();
            ShowSalesOrderDetail();
        }

        private void ShowSalesOrderDetail()
        {
            try { 
                _totalRecords = SalesOrderService.GetgetAllCompleteSalesOrderCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);

            int? salesOrderId = null;
            if (int.TryParse(SalesOrderIdTextBox.Text, out int parsedSalesOrderId))
            {
                salesOrderId = parsedSalesOrderId;
            }
            string? isbn = ISBNTextBox.Text;
            string? userId = UserIdTextBox.Text;

            DateOnly? startOrderDate = null;
            if (DateOnly.TryParse(StartOrderDateTextBox.Text, out DateOnly parsedStartOrderDate))
            {
                startOrderDate = parsedStartOrderDate;
            }
            DateOnly? endOrderDate = null;

            if (DateOnly.TryParse(EndOrderDateTextBox.Text, out DateOnly parsedEndOrderDate))
            {
                endOrderDate = parsedEndOrderDate;
            }
            List<CompleteSalesOrder> completeSalesOrders = new List<CompleteSalesOrder>();
            try { 
                completeSalesOrders = SalesOrderService.GetCompleteSalesOrdersByMultipleConditions(_currentPage, _pageSize, salesOrderId, isbn, userId, startOrderDate, endOrderDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            PageInfoText.Text = $"当前第{_currentPage}页，共{_totalPages}页";
            CompleteSalesOrderDataGrid.ItemsSource = completeSalesOrders;
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                ShowSalesOrderDetail(); // 重新加载数据
            }
            else
            {
                MessageBox.Show("已经是第一页！");
            }
        }
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                ShowSalesOrderDetail(); // 重新加载数据
            }
            else
            {
                MessageBox.Show("已经是最后一页！");
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSalesOrderDetail();
        }

        
    }

}
