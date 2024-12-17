using BookManagement.entity;
using System.Windows;

namespace BookManagement.window
{
    public partial class SupplierEditWindow : Window
    {
        public Supplier SupplierData { get; set; }

        public SupplierEditWindow(Supplier supplier = null)
        {
            InitializeComponent();
            SupplierData = supplier ?? new Supplier(); // 如果为 null，表示是添加操作
            DataContext = SupplierData; // 数据绑定
            LoadData();
        }

        // 加载数据到输入框
        private void LoadData()
        {
            txtSupplierName.Text = SupplierData.SupplierName;
            txtContactNumber.Text = SupplierData.ContactNumber;
            txtEmail.Text = SupplierData.Email;
            txtAddress.Text = SupplierData.Address;
        }

        // 保存按钮
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // 表单赋值
            SupplierData.SupplierName = txtSupplierName.Text;
            SupplierData.ContactNumber = txtContactNumber.Text;
            SupplierData.Email = txtEmail.Text;
            SupplierData.Address = txtAddress.Text;

            // 输入校验
            if (string.IsNullOrWhiteSpace(SupplierData.SupplierName))
            {
                MessageBox.Show("供货商名称不能为空！");
                return;
            }

            DialogResult = true; // 关闭窗口并返回结果
            this.Close();
        }

        // 取消按钮
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // 关闭窗口
            this.Close();
        }
    }
}
