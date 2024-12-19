using System;
using System.Collections.Generic;
using System.Data;
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
using BookManagement.util;
namespace BookManagement.page
{
    /// <summary>
    /// PurchaseOrderPage.xaml 的交互逻辑
    /// </summary>
    public partial class PurchaseOrderPage : Page
    {
        private readonly PurchaseOrderService purchaseOrderService;
        private string role;
        public PurchaseOrderPage()
        {
            InitializeComponent();
            purchaseOrderService = new PurchaseOrderService();
            int userId = (int)Session.GetCurrentUserId();
            role = UserService.GetUserById(userId).role;
            LoadPurchaseOrders();
            
        }
        public void LoadPurchaseOrders()
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
                    MessageBox.Show("加载采购订单失败: " + e.Message);
                }
            }
            else if (role == "admin")
            {
                try
                {
                    SupplierIdTextBox.IsEnabled = true;
                    SupplierNameTextBox.IsEnabled = true;
                    PurchaserIdTextBox.IsEnabled = true;
                    PurchaserNameTextBox.IsEnabled = true;
                    StartDatePicker.IsEnabled = true;
                    EndDatePicker.IsEnabled = true;
                    MinPriceTextBox.IsEnabled = true;
                    MaxPriceTextBox.IsEnabled = true;
                    PurchaseOrderDataGrid.ItemsSource = purchaseOrderService.GetPurchaseOrders();
                    LoadPageData();
                }
                catch (Exception e)
                {
                    MessageBox.Show("加载采购订单失败: " + e.Message);
                }
            }
            PurchaseOrderDataGrid.ItemsSource = purchaseOrderService.GetPurchaseOrders();
            LoadPageData();
        }
        private int currentPage = 1;
        private const int pageSize = 15;
        private int totalPages=0;
        private void LoadPageData()
        {
            string orderId = OrderIdTextBox.Text;
            string supplierId = SupplierIdTextBox.Text;
            string purchaserId = PurchaserIdTextBox.Text;
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            string purchaserName = PurchaserNameTextBox.Text;
            string supplierName = SupplierNameTextBox.Text;
            string minPriceText = MinPriceTextBox.Text;
            string maxPriceText = MaxPriceTextBox.Text;

            decimal? minPrice = null;
            decimal? maxPrice = null;

            // 解析 minPrice 和 maxPrice
            if (!string.IsNullOrEmpty(minPriceText))
            {
                if (decimal.TryParse(minPriceText, out decimal parsedMinPrice))
                {
                    minPrice = parsedMinPrice;
                }
                else
                {
                    MessageBox.Show("请输入正确的最低价格");
                    return;
                }

            }

            if (!string.IsNullOrEmpty(maxPriceText))
            {
                if (decimal.TryParse(maxPriceText, out decimal parsedMaxPrice))
                {
                    maxPrice = parsedMaxPrice;
                }
                else
                {
                    MessageBox.Show("请输入正确的最低价格");
                    return;
                }

                try
                {
                    // 获取所有分页数据（没有价格过滤）
                    var pagedData = purchaseOrderService.GetFilteredPurchaseOrders(orderId, supplierId, purchaserId, startDate, endDate, purchaserName, supplierName, currentPage, pageSize);

                    // 转换成 DTO 列表
                    var pagedDataDto = new List<PurchaseOrderDto>();

                    foreach (var po in pagedData)
                    {
                        var dto = new PurchaseOrderDto
                        {
                            PurchaseOrderId = po.PurchaseOrderId,
                            SupplierId = po.SupplierId,
                            SupplierName = purchaseOrderService.GetSupplierById((int)po.SupplierId).SupplierName, // Placeholder value for now
                            OrderDate = po.OrderDate,
                            PurchaserId = po.PurchaserId,
                            PurchaserName = purchaseOrderService.GetUserById((int)po.PurchaserId).username, // Placeholder value for now
                            Price = purchaseOrderService.GetTotalPrice((int)po.PurchaseOrderId) // Placeholder value for now
                        };

                        // 过滤价格
                        if (minPrice.HasValue && dto.Price < minPrice.Value)
                            continue;  // 如果价格小于 minPrice，跳过该项

                        if (maxPrice.HasValue && dto.Price > maxPrice.Value)
                            continue;  // 如果价格大于 maxPrice，跳过该项

                        // 将符合条件的 DTO 添加到列表
                        pagedDataDto.Add(dto);
                    }

                    // 设置数据源
                    PurchaseOrderDataGrid.ItemsSource = pagedDataDto;
                    var totalData = purchaseOrderService.GetPurchaseOrders();
                    totalPages = (int)Math.Ceiling(totalData.Count / (double)pageSize);
                    PageInfoText.Text = $"第 {currentPage} 页 / 共 {totalPages} 页";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
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
            LoadPageData();

        }
    }
}
