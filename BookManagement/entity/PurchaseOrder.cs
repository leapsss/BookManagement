using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.entity
{
    [SugarTable("purchase_order")]
    internal class PurchaseOrder
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "purchase_order_id")]
        public int PuchaseOrderId { get; set; }
        [SugarColumn(ColumnName = "supplier_id")]
        public int SupplierId { get; set; }
        [SugarColumn(ColumnName = "order_date")]
        public DateTime OrderDate { get; set; }
        [SugarColumn(ColumnName = "PurchaserId")]
        public int PurchaserId { get; set; }

    }
}
