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
        public static List<PurchaseOrder> GetFilteredPurchaseOrders(
     string orderId,
     string supplierId,
     string purchaserId,
     DateTime? startDate,
     DateTime? endDate,
     string purchaserName,
     string supplierName,
     int pageIndex,
     int pageSize)
        {
            // 定义转换后的变量
            int? orderIdInt = null;
            int? supplierIdInt = null;
            int? purchaserIdInt = null;

            // 尝试将传入的字符串转换为整数
            if (int.TryParse(orderId, out int tempOrderId))
            {
                orderIdInt = tempOrderId;
            }

            if (int.TryParse(supplierId, out int tempSupplierId))
            {
                supplierIdInt = tempSupplierId;
            }

            if (int.TryParse(purchaserId, out int tempPurchaserId))
            {
                purchaserIdInt = tempPurchaserId;
            }

            // 开始构建 SQL 查询
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT po.purchase_order_id, po.supplier_id, po.order_date, po.purchaser_id ");
            sql.Append("FROM purchase_order po ");
            sql.Append("JOIN users us ON us.id = po.purchaser_id ");
            sql.Append("JOIN supplier s ON s.supplier_id = po.supplier_id ");

            // 构建 WHERE 子句
            List<string> conditions = new List<string>();

            if (orderIdInt.HasValue)
            {
                conditions.Add("po.purchase_order_id = @OrderId");
            }

            if (supplierIdInt.HasValue)
            {
                conditions.Add("po.supplier_id = @SupplierId");
            }

            if (purchaserIdInt.HasValue)
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

            // 如果有任何过滤条件，将它们使用 AND 连接
            if (conditions.Count > 0)
            {
                sql.Append("WHERE " + string.Join(" AND ", conditions));
            }

            // 添加分页
            sql.Append(" ORDER BY po.purchase_order_id ");
            sql.Append("OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            // 执行查询
            var result = DatabaseService.Instance.Db.SqlQueryable<PurchaseOrder>(sql.ToString())
                .AddParameters(new
                {
                    OrderId = orderIdInt,
                    SupplierId = supplierIdInt,
                    PurchaserId = purchaserIdInt,
                    StartDate = startDate,
                    EndDate = endDate,
                    PurchaserName = "%" + purchaserName + "%",
                    SupplierName = "%" + supplierName + "%",
                    Offset = (pageIndex - 1) * pageSize, // 计算偏移量
                    PageSize = pageSize
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
