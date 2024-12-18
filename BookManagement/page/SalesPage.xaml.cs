using BookManagement.entity;
using BookManagement.service;
using System.Windows;
using System.Windows.Controls;
using BookManagement.util;

namespace BookManagement.page
{
    /// <summary>
    /// Interaction logic for SalesOrderPage.xaml
    /// </summary>
    public partial class SalesOrderPage : Page
    {
        // 存储已添加的订单详情
        private List<SalesOrderService.SalesOrderDetailDTO> salesOrderDetailDTOList = new List<SalesOrderService.SalesOrderDetailDTO>();

        public SalesOrderPage()
        {
            InitializeComponent();
            SalesOrderDetailGrid.ItemsSource = salesOrderDetailDTOList;
        }

        /// <summary>
        /// 添加按钮事件：添加书籍详情到列表
        /// </summary>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // 输入验证
            if (string.IsNullOrWhiteSpace(TxtISBN.Text) ||
                !int.TryParse(TxtAmount.Text, out int amount) ||
                !decimal.TryParse(TxtPrice.Text, out decimal price))
            {
                MessageBox.Show("请输入有效的 ISBN、数量 和 单价。", "输入错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // 根据 ISBN 构建 SalesOrderDetailDTO
                var salesOrderDetail = new SalesOrderDetail
                {
                    isbn = TxtISBN.Text,
                    Amount = amount,
                    Price = price
                };

                if (insertToTable(salesOrderDetail))
                {
                    // 刷新 DataGrid
                    refreshData();

                    // 清空输入框
                    TxtISBN.Clear();
                    TxtAmount.Text = "1";
                    TxtPrice.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 提交订单按钮事件
        /// </summary>
        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (salesOrderDetailDTOList.Count == 0)
            {
                MessageBox.Show("请先添加订单明细。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {

                // 创建销售订单
                var salesOrder = new SalesOrder
                {
                    SalespersonId = (int)Session.GetCurrentUserId(),
                    OrderDate = DateTime.Now
                };

                // 转换为 SalesOrderDetail 列表
                var salesOrderDetails = new List<SalesOrderDetail>();
                foreach (var dto in salesOrderDetailDTOList)
                {
                    salesOrderDetails.Add(new SalesOrderDetail
                    {
                        isbn = dto.isbn,
                        Amount = dto.amount,
                        Price = dto.price
                    });
                }

                // 调用 Service 提交订单
                SalesOrderService.addSalesOrder(salesOrder, salesOrderDetails);

                MessageBox.Show("订单提交成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);

                // 清空列表和界面
                salesOrderDetailDTOList.Clear();
                SalesOrderDetailGrid.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"订单提交失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool insertToTable(SalesOrderDetail salesOrderDetail) // return true if success
        {
            var salesOrderDetailDTOToInsert = SalesOrderService.salesOrderDetailToDTO(salesOrderDetail);
            if (salesOrderDetailDTOToInsert == null)
            {
                MessageBox.Show("这本书不存在");
                return false;
            }

            if (salesOrderDetailDTOToInsert.amount <= 0)
            {
                MessageBox.Show("数量必须大于0");
                return false;
            }

            if (salesOrderDetailDTOToInsert.price <= 0)
            {
                salesOrderDetailDTOToInsert.price = salesOrderDetailDTOToInsert.originalPrice;
            }

            if (salesOrderDetailDTOToInsert.inventory < salesOrderDetailDTOToInsert.amount)
            {
                MessageBox.Show("库存不足");
                return false;
            }

            // 判断是否已经存在
            foreach (var salesOrderDetailDTO in salesOrderDetailDTOList)
            {
                if (salesOrderDetailDTOToInsert.isbn == salesOrderDetailDTO.isbn && salesOrderDetailDTOToInsert.price == salesOrderDetailDTO.price)
                {
                    if (salesOrderDetailDTOToInsert.amount + salesOrderDetailDTO.amount > salesOrderDetailDTO.inventory)
                    {
                        MessageBox.Show("库存不足");
                        return false;
                    }

                    salesOrderDetailDTO.amount += salesOrderDetail.Amount;
                    return true;
                }
            }

            salesOrderDetailDTOList.Add(salesOrderDetailDTOToInsert);
            return true;
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            refreshData();
        }

        private void refreshData()
        {
            SalesOrderDetailGrid.ItemsSource = null;
            SalesOrderDetailGrid.ItemsSource = salesOrderDetailDTOList;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // 获取点击按钮对应的行数据
            var button = sender as Button;
            if (button?.Tag is SalesOrderService.SalesOrderDetailDTO dtoToRemove)
            {
                // 从列表中移除选中的对象
                salesOrderDetailDTOList.Remove(dtoToRemove);

                // 刷新 DataGrid 显示
                refreshData();
            }
        }

    }
}
