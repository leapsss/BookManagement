using SqlSugar;

namespace BookManagement.entity
{
    [SugarTable("sales_order")]
    public class SalesOrder
    {
        [SugarColumn(ColumnName = "sales_order_id", IsPrimaryKey = true, IsIdentity = true)]
        public int SalesOrderId { get; set; } // 销售订单ID，自增主键

        [SugarColumn(ColumnName = "salesperson_id")]
        public int SalespersonId { get; set; } // 销售员ID

        [SugarColumn(ColumnName = "order_date")]
        public DateTime OrderDate { get; set; } // 订单日期
    }
}
