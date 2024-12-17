using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.entity
{
    [SugarTable("purchase_order_detail")]
    internal class PurchaseOrderDetail
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "purchase_order_detail_id")]
        public int PurchaseOrderDetailId { get; set; }
        [SugarColumn(ColumnName = "purchase_order_id")]
        public int PurchaseOrderId { get; set; }
        [SugarColumn(ColumnName = "isbn")]
        public int Isbn { get; set; }
        [SugarColumn(ColumnName = "amount")]
        public int OrderStatus { get; set; }
        [SugarColumn(ColumnName = "price")]
        public decimal Price { get; set; }
    }

}
