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
using BookManagement.entity.Dto;
using BookManagement.util;
using System.Data;
namespace BookManagement.page
{
    /// <summary>
    /// PurchaseOrderDetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class PurchaseOrderDetailPage : Page
    {
        private readonly PurchaseOrderDetailService _dbOps;
        private List<PurchaseOrderDetailDto> allOrderDetails;
        private int currentPage = 1;
        private int pageSize = 15;
        private string role;
        public PurchaseOrderDetailPage()
        {
            InitializeComponent();
            _dbOps = new PurchaseOrderDetailService();
            int userId = (int)Session.GetCurrentUserId();
            role = UserService.GetUserById(userId).role;
            LoadPurchaseOrderDetails();
            LoadPurchaseOrderDetails();
        }
        private void LoadPurchaseOrderDetails()
        {
            if (role == "purchaser")
            {
                try
                {
                    PurchaserIdTextBox.Text = Session.GetCurrentUserId().ToString();
                    PurchaserIdTextBox.IsEnabled = false;
                    PurchaserNameTextBox.Text = UserService.GetUserById((int)Session.GetCurrentUserId()).username;
                    PurchaserNameTextBox.IsEnabled = false;
                    LoadPageData();

                }
                catch (Exception e)
                {
                    MessageBox.Show("加载采购订单详情失败: " + e.Message);
                }

            }
            else if (role == "admin")
            {
                try
                {
                    LoadPageData();
                }
                catch (Exception e)
                {
                    MessageBox.Show("加载采购订单详情失败: " + e.Message);
                }

            }
        }

     

        private List<PurchaseOrderDetailDto> FilterData()
        {
            string orderId = OrderIdTextBox.Text;
            string isbn = ISBNTextBox.Text;
            string supplierName = SupplierNameTextBox.Text;
            string supplierId = SupplierIdTextBox.Text;
            string purchaserId = PurchaserIdTextBox.Text;
            string purchaserName = PurchaserNameTextBox.Text;
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            decimal? minPrice = null;
            decimal? maxPrice = null;
            if (decimal.TryParse(MinPriceTextBox.Text, out decimal parsedMinPrice))
            {
                minPrice = parsedMinPrice;
            }

            if (decimal.TryParse(MaxPriceTextBox.Text, out decimal parsedMaxPrice))
            {
                maxPrice = parsedMaxPrice;
            }
            try
            {
                return _dbOps.QueryPurchaseOrderDetailDtos(orderId, isbn, supplierName, supplierId, purchaserId, purchaserName, minPrice, maxPrice, startDate, endDate,currentPage,pageSize);
            }
            catch (Exception e)
            {
                MessageBox.Show("查询采购订单详情失败: " + e.Message);
                return new List<PurchaseOrderDetailDto>();
            }
        }

        private int totalPages=0;
        private void LoadPageData()
        {
            var filteredData = FilterData();
            var pagedData = filteredData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            PurchaseOrderDetailDataGrid.ItemsSource = pagedData;
            totalPages = (int)Math.Ceiling(filteredData.Count / (double)pageSize);
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
