using BookManagement.window; // 引入弹出窗口
using BookManagement.service; // SupplierService
using System.Windows;
using System.Windows.Controls;
using BookManagement.entity;

namespace BookManagement.page
{
    public partial class SupplierManagementPage : Page
    {
        public SupplierManagementPage()
        {
            InitializeComponent();
            LoadSupplierData();
        }

        // 加载所有供货商数据
        private void LoadSupplierData()
        {
            List<Supplier> suppliers = SupplierService.getAll();
            dataGridSuppliers.ItemsSource = suppliers;
        }

        // 添加供货商按钮
        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new SupplierEditWindow();
            if (editWindow.ShowDialog() == true) // 如果点击保存按钮
            {
                SupplierService.add(editWindow.SupplierData);
                MessageBox.Show("供货商已添加！");
                LoadSupplierData();
            }
        }

        // 更新选中供货商按钮
        private void UpdateSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSuppliers.SelectedItem is Supplier selectedSupplier)
            {
                // 传入选中的数据到弹出窗口
                var editWindow = new SupplierEditWindow(selectedSupplier);
                if (editWindow.ShowDialog() == true) // 如果点击保存按钮
                {
                    SupplierService.update(editWindow.SupplierData);
                    MessageBox.Show("供货商信息已更新！");
                    LoadSupplierData();
                }
            }
            else
            {
                MessageBox.Show("请选择要更新的供货商！");
            }
        }

        // 删除选中供货商
        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSuppliers.SelectedItem is Supplier selectedSupplier)
            {
                SupplierService.deleteBySupplierId(selectedSupplier.SupplierId);
                MessageBox.Show("供货商已删除！");
                LoadSupplierData();
            }
            else
            {
                MessageBox.Show("请选择要删除的供货商！");
            }
        }

        // 刷新列表
        private void RefreshList_Click(object sender, RoutedEventArgs e)
        {
            LoadSupplierData();
        }
    }
}
