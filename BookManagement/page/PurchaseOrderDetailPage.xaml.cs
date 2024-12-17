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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookManagement.page
{
    /// <summary>
    /// PurchaseOrderDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class PurchaseOrderDetailPage : Page
    {
        private readonly PurchaseOrderDetailService _dbOps;
        private List<PurchaseOrderDetail> allOrderDetails;
        private int currentPage = 1;
        private int pageSize = 10;
        public PurchaseOrderDetailPage()
        {
            InitializeComponent();
            _dbOps = new PurchaseOrderDetailService();

            LoadPurchaseOrderDetails();
        }
        private void LoadPurchaseOrderDetails()
        {
            allOrderDetails = _dbOps.GetPurchaseOrderDetails();
            LoadPageData();
        }
        private List<PurchaseOrderDetail> FilterData()
        {
            var query = allOrderDetails.AsQueryable();

            if (!string.IsNullOrEmpty(OrderIdTextBox.Text))
            {
                query = query.Where(po => po.PurchaseOrderId.ToString().Contains(OrderIdTextBox.Text));
            }

            if (decimal.TryParse(MinPriceTextBox.Text, out decimal minPrice))
            {
                query = query.Where(po => po.Price >= minPrice);
            }
            if (decimal.TryParse(MaxPriceTextBox.Text, out decimal maxPrice))
            {
                query = query.Where(po => po.Price <= maxPrice);
            }

            return query.ToList();
        }
        private void LoadPageData()
        {
            var filteredData = FilterData();
            var pagedData = filteredData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            PurchaseOrderDetailDataGrid.ItemsSource = pagedData;
            PageInfoText.Text = $"第 {currentPage} 页 / 共 {Math.Ceiling(filteredData.Count / (double)pageSize)} 页";
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            currentPage = 1;
            LoadPageData();
        }
        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPageData();
            }
        }
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            var filteredData = FilterData();
            if (currentPage * pageSize < filteredData.Count)
            {
                currentPage++;
                LoadPageData();
            }
        }
    }
}
