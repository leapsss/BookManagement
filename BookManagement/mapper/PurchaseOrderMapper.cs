using BookManagement.entity;
using BookManagement.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagement.entity.Dto;
using SqlSugar;
namespace BookManagement.mapper
{
    internal class PurchaseOrderMapper
    {
        public static List<PurchaseOrder> GetPurchaseOrders()
        {
            return DatabaseService.Instance.Db.Queryable<PurchaseOrder>().ToList();
        }
        public static PurchaseOrder GetPurchaseOrderById(int id)
        {
            return DatabaseService.Instance.Db.Queryable<PurchaseOrder>().Where(it => it.PurchaseOrderId == id).First();
        }
        public static List<PurchaseOrder> GetPagedPurchaseOrders(int pageIndex, int pageSize)
        {
            return DatabaseService.Instance.Db.Queryable<PurchaseOrder>()
                                              .Skip((pageIndex - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();
        }
        public static List<PurchaseOrder> GetPurchaseOrdersByUserId(int id)
        {
            return DatabaseService.Instance.Db.Queryable<PurchaseOrder>().Where(it => it.PurchaserId == id).ToList();
        }
        public static List<PurchaseOrder> GetFilteredPurchaseOrders(string orderId, string supplierId, string purchaserId, DateTime? startDate, DateTime? endDate, string purchaserName, string supplierName)
        {
            // 开始构建 SQL 查询
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT po.purchase_order_id, po.supplier_id, po.order_date, po.purchaser_id ");
            sql.Append("FROM purchase_order po ");
            sql.Append("JOIN users us ON us.id = po.purchaser_id ");
            sql.Append("JOIN supplier s ON s.supplier_id = po.supplier_id ");

            // 根据传入的参数构建 WHERE 子句
            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(orderId))
            {
                conditions.Add("po.purchase_order_id = @OrderId");
            }

            if (!string.IsNullOrEmpty(supplierId))
            {
                conditions.Add("po.supplier_id = @SupplierId");
            }

            if (!string.IsNullOrEmpty(purchaserId))
            {
                conditions.Add("po.purchaser_id = @PurchaserId");
            }

            if (startDate.HasValue)
            {
                conditions.Add("po.order_date >= @StartDate");
            }

            if (endDate.HasValue)
            {
                conditions.Add("po.order_date <= @EndDate");
            }

            if (!string.IsNullOrEmpty(purchaserName))
            {
                conditions.Add("us.username LIKE @PurchaserName");
            }

            if (!string.IsNullOrEmpty(supplierName))
            {
                conditions.Add("s.supplier_name LIKE @SupplierName");
            }

            // 如果有过滤条件，使用 AND 连接它们
            if (conditions.Count > 0)
            {
                sql.Append("WHERE " + string.Join(" AND ", conditions));
            }

            // 使用 SqlSugar 执行查询
            var result = DatabaseService.Instance.Db.SqlQueryable<PurchaseOrder>(sql.ToString())
                           .AddParameters(new
                           {
                               OrderId = orderId,
                               SupplierId = supplierId,
                               PurchaserId = purchaserId,
                               StartDate = startDate,
                               EndDate = endDate,
                               PurchaserName = "%" + purchaserName + "%",
                               SupplierName = "%" + supplierName + "%"
                           })
                           .ToList();

            return result;
        }


        public static int Add(PurchaseOrder purchaseOrder)
        {
            return DatabaseService.Instance.Db.Insertable(purchaseOrder).ExecuteReturnIdentity();
        }
    }
}
