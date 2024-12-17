using BookManagement.entity;
using BookManagement.service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BookManagement.page
{
    public partial class InventoryManagementPage : Page
    {
        private InventoryService inventoryService;

        public InventoryManagementPage()
        {
            InitializeComponent();
            inventoryService = new InventoryService();
            LoadInventory();
        }

        // 加载库存列表
        private void LoadInventory()
        {
            var inventoryList = inventoryService.GetAllInventory();
            InventoryListView.ItemsSource = inventoryList;
        }

        // 查询库存
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string isbn = SearchIsbnTextBox.Text;
            var inventory = inventoryService.GetInventoryByIsbn(isbn);
            InventoryListView.ItemsSource = inventory != null ? new[] { inventory } : null;
        }

        // 入库
        private void AddInventoryButton_Click(object sender, RoutedEventArgs e)
        {
            string isbn = IsbnTextBox.Text;
            if (int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                if (quantity <= 0)
                {
                    MessageBox.Show("数量必须大于零");
                    return;
                }

                try
                {
                    inventoryService.AddInventory(isbn, quantity);
                    MessageBox.Show("入库成功！");
                    LoadInventory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"入库失败: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("请输入有效的数量");
            }
        }

        // 出库
        private void RemoveInventoryButton_Click(object sender, RoutedEventArgs e)
        {
            string isbn = IsbnTextBox.Text;
            if (int.TryParse(QuantityTextBox.Text, out int quantity))
            {
                if (quantity <= 0)
                {
                    MessageBox.Show("数量必须大于零");
                    return;
                }

                try
                {
                    inventoryService.RemoveInventory(isbn, quantity);
                    MessageBox.Show("出库成功！");
                    LoadInventory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"出库失败: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("请输入有效的数量");
            }
        }
    }
}