using SqlSugar;

namespace BookManagement.entity
{
    [SugarTable("inventory", TableDescription = "库存信息")]
    public class Inventory
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDescription = "库存ID，自增主键")] // 主键
        public int InventoryId { get; set; }
        public string Isbn { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string BookName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public decimal Price { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string ClcName { get; set; }
    }
}
