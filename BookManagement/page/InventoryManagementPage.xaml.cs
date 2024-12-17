using BookManagement.entity;
using BookManagement.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookManagement.page
{
    public partial class InventoryManagementPage : Page
    {
        private InventoryService inventoryService;
        private BookService bookService;

        public InventoryManagementPage()
        {
            InitializeComponent();
            inventoryService = new InventoryService();
            bookService = new BookService();
            showInventoryInfo(); // 初始化时加载所有库存信息
        }

        // 显示库存列表
        public void showInventoryInfo(string bookName = null, string category = "所有分类", decimal? minPrice = null, decimal? maxPrice = null)
        {
            // 获取所有库存信息
            List<Inventory> inventoryList = inventoryService.getAllInventory();

            // 通过条件筛选
            if (!string.IsNullOrEmpty(bookName))
            {
                inventoryList = inventoryList.Where(i => bookService.getBookByISBN(i.Isbn).bookName.Contains(bookName)).ToList();
            }

            if (category != "所有分类")
            {
                inventoryList = inventoryList.Where(i => bookService.getBookByISBN(i.Isbn).clcName == category).ToList();
            }

            if (minPrice.HasValue)
            {
                inventoryList = inventoryList.Where(i => bookService.getBookByISBN(i.Isbn).price >= minPrice.Value).ToList();
            }

            if (maxPrice.HasValue)
            {
                inventoryList = inventoryList.Where(i => bookService.getBookByISBN(i.Isbn).price <= maxPrice.Value).ToList();
            }

            // 为每个库存项附加书籍信息
            foreach (var inventory in inventoryList)
            {
                var book = bookService.getBookByISBN(inventory.Isbn);
                inventory.BookName = book.bookName;
                inventory.ClcName = book.clcName;
                inventory.Price = book.price;
            }

            InventoryListView.ItemsSource = inventoryList;
        }

        // 查询按钮点击事件
        public void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            string bookName = BookNameTextBox.Text;
            string category = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "所有分类";
            decimal? minPrice = string.IsNullOrEmpty(MinPriceTextBox.Text) ? (decimal?)null : decimal.Parse(MinPriceTextBox.Text);
            decimal? maxPrice = string.IsNullOrEmpty(MaxPriceTextBox.Text) ? (decimal?)null : decimal.Parse(MaxPriceTextBox.Text);

            showInventoryInfo(bookName, category, minPrice, maxPrice); // 使用查询条件更新库存列表
        }
    }
}
