using SqlSugar;

namespace BookManagement.entity
{
    [SugarTable("book")] // 表名与数据库中的表名对应
    public class Book
    {
        [SugarColumn(IsPrimaryKey = true)] // 主键，自动增长
        public string isbn { get; set; }
        public string author { get; set; }
        public string bookName { get; set; }
        public string press { get; set; }
        public string pressDate { get; set; }
        public string picture { get; set; }
        public string clcName { get; set; }
        public int price { get; set; }
        public string bookDesc { get; set; }
        public string language { get; set; }
    }
}
