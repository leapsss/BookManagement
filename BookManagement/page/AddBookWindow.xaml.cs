using BookManagement.entity;
using BookManagement.service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace BookManagement.page
{
    /// <summary>
    /// AddBookWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddBookWindow : Window
    {
        private Book _book = new Book();
        private BookService _bookService = new BookService();

        public AddBookWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 自动将焦点设置到 bookNameTextBox 输入框
            ISBNTextBox.Focus();
        }

        private void ISBNTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // 当按下回车时，调用查询方法
                QueryButton_Click(sender, e);
            }
        }

        public async void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            string isbn = ISBNTextBox.Text;
            if (string.IsNullOrWhiteSpace(isbn))
            {
                MessageBox.Show("请输入有效的 ISBN 号");
                return;
            }
            try
            {
               
                string apiUrl = $"https://data.isbn.work/openApi/getInfoByIsbn?isbn={isbn}&appKey=ae1718d4587744b0b79f940fbef69e77";
   
                using (HttpClient client = new HttpClient())
                {                    
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode(); // 确保请求成功

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(jsonResponse);
                    JObject data = json["data"] as JObject;
                    
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    if (data != null) {
                        _book = data.ToObject<Book>(JsonSerializer.Create(settings));
                                                                                        
                        bookNameTextBox.Text = _book.bookName;
                        authorTextBox.Text = _book.author;
                        pressTextBox.Text = _book.press;
                        pressDateTextBox.Text = _book.pressDate;
                        clcNameTextBox.Text = _book.clcName;
                        priceTextBox.Text = (_book.price/100m).ToString("0.00");
                        bookDescTextBox.Text = _book.bookDesc;                      
                    }
                    else
                    {
                        MessageBox.Show("未找到该 ISBN 对应的图书信息,请手动输入");
                    }
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show($"查询失败: {ex.Message}");
            }
        }        

        public void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            _book.isbn = ISBNTextBox.Text;
            _book.bookName = bookNameTextBox.Text;
            _book.author = authorTextBox.Text;
            _book.press = pressTextBox.Text;
            _book.pressDate = pressDateTextBox.Text;
            _book.clcName = clcNameTextBox.Text;
            _book.price = (int)(Convert.ToDecimal(priceTextBox.Text) * 100);
            _book.bookDesc = bookDescTextBox.Text;

            
            _bookService.addBook(_book);
            this.DialogResult = true;
            this.Close();
        }
    }
}
