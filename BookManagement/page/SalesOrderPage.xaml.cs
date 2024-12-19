using BookManagement.service;
using BookManagement.util;
using System;
using System.Windows;
using System.Windows.Controls;


namespace BookManagement.page
{
    public partial class SalesOrderPage : Page
    {
        private int _pageSize = 15;
        private int _currentPage = 1;
        private int _totalRecords = 0;
        private int _totalPages = 0;
        private int? _currentUserId;

        public SalesOrderPage()
        {
            InitializeComponent();
            _currentUserId = Session.GetCurrentUserId();
            if (!_currentUserId.HasValue)
            {
                MessageBox.Show("未检测到登录用户，请重新登录!");
                return;
            }
            ShowSalesOrderDetail();
        }

        private void ShowSalesOrderDetail()
        {
            try
            {
                int? salesOrderId = null;
                if (int.TryParse(SalesOrderIdTextBox.Text, out int parsedSalesOrderId))
                {
                    salesOrderId = parsedSalesOrderId;
                }

                string? isbn = !string.IsNullOrWhiteSpace(ISBNTextBox.Text) ? ISBNTextBox.Text : null;
                string? bookName = !string.IsNullOrWhiteSpace(BookNameTextBox.Text) ? BookNameTextBox.Text : null;

                DateOnly? startOrderDate = null;
                if (StartOrderDateTextBox.SelectedDate.HasValue)
                {
                    startOrderDate = DateOnly.FromDateTime(StartOrderDateTextBox.SelectedDate.Value);
                }

                DateOnly? endOrderDate = null;
                if (EndOrderDateTextBox.SelectedDate.HasValue)
                {
                    endOrderDate = DateOnly.FromDateTime(EndOrderDateTextBox.SelectedDate.Value);
                }

                _totalRecords = SalesOrderService.GetCompleteSalesOrderCountByUserId(
                    salesOrderId, isbn, bookName, startOrderDate, endOrderDate, _currentUserId.Value);
                _totalPages = (int)Math.Ceiling((double)_totalRecords / _pageSize);

                var completeSalesOrders = SalesOrderService.GetCompleteSalesOrdersByMultipleConditionsAndUserId(
                    _currentPage, _pageSize, salesOrderId, isbn, bookName, startOrderDate, endOrderDate, _currentUserId.Value);

                PageInfoText.Text = $"当前第{_currentPage}页，共{_totalPages}页";
                CompleteSalesOrderDataGrid.ItemsSource = completeSalesOrders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据时发生错误: {ex.Message}");
            }
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                ShowSalesOrderDetail();
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
                ShowSalesOrderDetail();
            }
            else
            {
                MessageBox.Show("已经是最后一页！");
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            ShowSalesOrderDetail();
        }
    }
}