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
using BookManagement.entity;
using BookManagement.entity.Dto;
using BookManagement.service;
namespace BookManagement.page
{
    /// <summary>
    /// PurchaseOrderPage.xaml 的交互逻辑
    /// </summary>
    public partial class PurchaseOrderPage : Page
    {
        private readonly PurchaseOrderService purchaseOrderService;
        public PurchaseOrderPage()
        {
            InitializeComponent();
            purchaseOrderService = new PurchaseOrderService();
            LoadPurchaseOrders();
        }
        public void LoadPurchaseOrders()
        {
            PurchaseOrderDataGrid.ItemsSource = purchaseOrderService.GetPurchaseOrders();
            LoadPageData();
        }
        private int currentPage = 1;
        private const int pageSize = 15;
        private int totalPages=0;
        private void LoadPageData()
        {
            var pagedData = purchaseOrderService.GetPagedPurchaseOrders(currentPage, pageSize);
            var totalData = purchaseOrderService.GetPurchaseOrders();
            try
            {
                var pagedDataDto = pagedData.Select(po => new PurchaseOrderDto
                {
                    PurchaseOrderId = po.PurchaseOrderId,
                    SupplierId = po.SupplierId,
                    SupplierName = purchaseOrderService.GetSupplierById((int)po.SupplierId).SupplierName, // Placeholder value for now
                    OrderDate = po.OrderDate,
                    PurchaserId = po.PurchaserId,
                    PurchaserName = purchaseOrderService.GetUserById((int)po.PurchaserId).username, // Placeholder value for now
                    price = purchaseOrderService.GetTotalPrice((int)po.PurchaseOrderId) // Placeholder value for now
                }).ToList();
                PurchaseOrderDataGrid.ItemsSource = pagedDataDto;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   
            
           
            
            totalPages = (int)Math.Ceiling(totalData.Count / (double)pageSize);
            PageInfoText.Text = $"第 {currentPage} 页 / 共 {Math.Ceiling(totalData.Count / (double)pageSize)} 页";
        }
        private void ToPurchaseOrderDetailWindowButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement the Edit functionality here
            var purchaseOrderDto = ((Button)sender).DataContext as PurchaseOrderDto;
            //打开编辑界面
            // Open edit dialog or navigate to the edit page with the selected user
            if (purchaseOrderDto != null)
            {
                // 创建并显示 UserUpdateWindow
                var purchaseOrderDetailWindow = new PurchaseOrderDetailWindow(purchaseOrderDto.PurchaseOrderId);

                // 设置为模式对话框（等待用户操作完成）

            }
        }
        // 下一页
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

        // 上一页
        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPageData();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string orderId = OrderIdTextBox.Text;
            string supplierId = SupplierIdTextBox.Text;
            string purchaserId = PurchaserIdTextBox.Text;  
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            string purchaserName = PurchaserNameTextBox.Text;
            string supplierName = SupplierNameTextBox.Text;
            string minPrice = MinPriceTextBox.Text;
            string maxPrice = MaxPriceTextBox.Text;
            try
            {
                var filteredOrders = purchaseOrderService.GetFilteredPurchaseOrders(orderId, supplierId, purchaserId, startDate, endDate, purchaserName, supplierName);
                PurchaseOrderDataGrid.ItemsSource = filteredOrders;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
