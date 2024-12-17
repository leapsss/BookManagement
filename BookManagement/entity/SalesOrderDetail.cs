using SqlSugar;

namespace BookManagement.entity
{
    [SugarTable("sales_order_detail")]
    public class SalesOrderDetail
    {
        [SugarColumn(ColumnName = "sales_order_id", IsPrimaryKey = true)]
        public int SalesOrderId { get; set; } // 销售订单ID，复合主键

        [SugarColumn(ColumnName = "isbn", IsPrimaryKey = true)]
        public string ISBN { get; set; } // ISBN号，复合主键

        [SugarColumn(ColumnName = "amount")]
        public int Amount { get; set; } // 数量

        [SugarColumn(ColumnName = "price")]
        public decimal Price { get; set; } // 售价
    }
}
