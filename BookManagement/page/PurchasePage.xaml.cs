using BookManagement.entity;
using BookManagement.service;
using BookManagement.util;
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
    /// Interaction logic for PurchasePage.xaml
    /// </summary>
    public partial class PurchasePage : Page
    {
        private List<PurchaseOrderService.PurchaseOrderDetailDTO> purchaseOrderDetailDTOList = new List<PurchaseOrderService.PurchaseOrderDetailDTO>();

        public PurchasePage()
        {
            InitializeComponent();
            refreshData();
        }

        private void refreshData()
        {
            PurchaseOrderDetailGrid.ItemsSource = null;
            PurchaseOrderDetailGrid.ItemsSource = purchaseOrderDetailDTOList;
        }

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
                // 根据 ISBN 构建 PurchaseOrderDetailDTO
                var purchaseOrderDetail = new PurchaseOrderDetail
                {
                    Isbn = TxtISBN.Text,
                    Amount = amount,
                    Price = price
                };

                if (insertToTable(purchaseOrderDetail))
                {
                    // 刷新 DataGrid
                    refreshData();

                    // 清空输入框
                    TxtISBN.Clear();
                    TxtAmount.Clear();
                    TxtPrice.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (purchaseOrderDetailDTOList.Count == 0)
            {
                MessageBox.Show("请添加至少一本书籍。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                // 创建销售订单
                var purchaseOrder = new PurchaseOrder
                { 
                    //PurchaserId = int.Parse(Session.GetCurrentUserId()),
                    PurchaserId = 1,
                    OrderDate = DateTime.Now
                };

                // 转换为 PurchaseOrderDetail 列表
                var purchaseOrderDetails = new List<PurchaseOrderDetail>();
                foreach (var dto in purchaseOrderDetailDTOList)
                {
                    purchaseOrderDetails.Add(new PurchaseOrderDetail
                    {
                        Isbn = dto.isbn,
                        Amount = dto.amount,
                        Price = dto.price
                    });
                }

                // 调用 Service 提交订单
                PurchaseOrderService.AddPurchaseOrder(purchaseOrder, purchaseOrderDetails);

                MessageBox.Show("订单提交成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);

                // 清空列表和界面
                purchaseOrderDetailDTOList.Clear();
                PurchaseOrderDetailGrid.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"订单提交失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

            private bool insertToTable(PurchaseOrderDetail purchaseOrderDetail)
        {
            var purchaseOrderDetailDTOToInsert = PurchaseOrderService.purchaseOrderToDTO(purchaseOrderDetail);
            if (purchaseOrderDetailDTOToInsert == null)
            {
                MessageBox.Show("这本书不存在，要添加这本书吗？");
                return false;
            }
            if (purchaseOrderDetailDTOToInsert.amount <= 0)
            {
                MessageBox.Show("数量必须大于0");
                return false;
            }
            if (purchaseOrderDetailDTOToInsert.price <= 0)
            {
                MessageBox.Show("价格必须大于0");
                return false;
            }
            foreach (var purchaseOrderDetailDTO in purchaseOrderDetailDTOList)
            {
                if (purchaseOrderDetailDTO.isbn == purchaseOrderDetailDTOToInsert.isbn && purchaseOrderDetailDTO.price == purchaseOrderDetailDTOToInsert.price)
                {
                    purchaseOrderDetailDTO.amount += purchaseOrderDetailDTOToInsert.amount;
                    return true;
                }
            }
            purchaseOrderDetailDTOList.Add(purchaseOrderDetailDTOToInsert);
            return true;
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // 获取点击按钮对应的行数据
            var button = sender as Button;
            if (button?.Tag is PurchaseOrderService.PurchaseOrderDetailDTO dtoToRemove)
            {
                // 从列表中移除选中的对象
                purchaseOrderDetailDTOList.Remove(dtoToRemove);

                // 刷新 DataGrid 显示
                refreshData();
            }
        }
    }
}
