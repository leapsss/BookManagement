using SqlSugar;


namespace BookManagement.entity
{
    [SugarTable("supplier", TableDescription = "存储供货商信息")]
    public class Supplier
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "供货商ID，自增主键")]
        public int SupplierId { get; set; }

        [SugarColumn(ColumnDescription = "供货商名称", Length = 255, IsNullable = false)]
        public string SupplierName { get; set; }

        [SugarColumn(ColumnDescription = "联系电话", Length = 50, IsNullable = true)]
        public string ContactNumber { get; set; }

        [SugarColumn(ColumnDescription = "电子邮件", Length = 255, IsNullable = true)]
        public string Email { get; set; }

        [SugarColumn(ColumnDescription = "地址", IsNullable = true, ColumnDataType = "TEXT")]
        public string Address { get; set; }

        [SugarColumn(ColumnDescription = "记录创建时间", IsNullable = true, DefaultValue = "CURRENT_TIMESTAMP")]
        public DateTime? CreatedAt { get; set; }
    }
}
