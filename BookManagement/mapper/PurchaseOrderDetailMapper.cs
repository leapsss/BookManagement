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
    internal class PurchaseOrderDetailMapper
    {
        public static List<PurchaseOrderDetail> GetPurchaseOrderDetails()
        {
            return DatabaseService.Instance.Db.Queryable<PurchaseOrderDetail>().ToList();
        }
        public static List<PurchaseOrderDetailDto> GetPurchaseOrderDetailDtos()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT pod.purchase_order_detail_id, pod.purchase_order_id, po.supplier_id, sp.supplier_name, ");
            sql.Append("po.purchaser_id, us.username, pod.isbn, bo.book_name, pod.amount, ");
            sql.Append("pod.price, (pod.price * pod.amount) AS total_price, po.purchaser_id ");
            sql.Append("FROM purchase_order_detail pod ");
            sql.Append("JOIN purchase_order po ON po.purchase_order_id = pod.purchase_order_id ");
            sql.Append("JOIN supplier sp ON sp.supplier_id = po.supplier_id ");
            sql.Append("JOIN book bo ON bo.isbn = pod.isbn ");
            sql.Append("JOIN users us ON us.id = po.purchaser_id ");
            var result = DatabaseService.Instance.Db.SqlQueryable<PurchaseOrderDetailDto>(sql.ToString())
                          .ToList();

            return result;

        }
        public static PurchaseOrderDetail GetPurchaseOrderDetailById(int id)
        {
            return DatabaseService.Instance.Db.Queryable<PurchaseOrderDetail>().Where(it => it.PurchaseOrderDetailId == id).First();
        }
        public static List<PurchaseOrderDetail> GetPurchaseOrderDetailByPurchaseOrderId(int purchaseOrderId)
        {
            return DatabaseService.Instance.Db.Queryable<PurchaseOrderDetail>().Where(it => it.PurchaseOrderId == purchaseOrderId).ToList();
        }
        public static List<PurchaseOrderDetailDto> QueryPurchaseOrderDetailDtos(string orderId, string isbn, string supplierName, string supplierId, string purchaserId, string username, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT pod.purchase_order_detail_id, pod.purchase_order_id, po.supplier_id, sp.supplier_name, ");
            sql.Append("po.purchaser_id, us.username, pod.isbn, bo.book_name, pod.amount, ");
            sql.Append("pod.price, (pod.price * pod.amount) AS total_price, po.purchaser_id ");
            sql.Append("FROM purchase_order_detail pod ");
            sql.Append("JOIN purchase_order po ON po.purchase_order_id = pod.purchase_order_id ");
            sql.Append("JOIN supplier sp ON sp.supplier_id = po.supplier_id ");
            sql.Append("JOIN book bo ON bo.isbn = pod.isbn ");
            sql.Append("JOIN users us ON us.id = po.purchaser_id ");

            // 构建 WHERE 条件
            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(orderId))
            {
                conditions.Add("pod.purchase_order_id = @OrderId");
            }

            if (!string.IsNullOrEmpty(isbn))
            {
                conditions.Add("pod.isbn = @Isbn");
            }

            if (!string.IsNullOrEmpty(supplierName))
            {
                conditions.Add("sp.supplier_name LIKE @SupplierName");
            }

            if (!string.IsNullOrEmpty(supplierId))
            {
                conditions.Add("po.supplier_id = @SupplierId");
            }

            if (!string.IsNullOrEmpty(purchaserId))
            {
                conditions.Add("po.purchaser_id = @PurchaserId");
            }

            if (!string.IsNullOrEmpty(username))
            {
                conditions.Add("us.username LIKE @Username");
            }

            if (minPrice.HasValue)
            {
                conditions.Add("pod.price >= @MinPrice");
            }

            if (maxPrice.HasValue)
            {
                conditions.Add("pod.price <= @MaxPrice");
            }

            if (startDate.HasValue)
            {
                conditions.Add("po.order_date >= @StartDate");
            }

            if (endDate.HasValue)
            {
                conditions.Add("po.order_date <= @EndDate");
            }

            // 如果有过滤条件，拼接 WHERE 子句
            if (conditions.Count > 0)
            {
                sql.Append("WHERE " + string.Join(" AND ", conditions));
            }

            // 执行查询

            var result = DatabaseService.Instance.Db.SqlQueryable<PurchaseOrderDetailDto>(sql.ToString())
                           .AddParameters(new
                           {
                               OrderId = orderId,
                               Isbn = isbn,
                               SupplierName = "%" + supplierName + "%",
                               SupplierId = supplierId,
                               PurchaserId = purchaserId,
                               Username = "%" + username + "%",
                               MinPrice = minPrice,
                               MaxPrice = maxPrice,
                               StartDate = startDate,
                               EndDate = endDate
                           })
                           .ToList();

            return result;
        }
        public static void Add(PurchaseOrderDetail purchaseOrderDetail)
        {
            DatabaseService.Instance.Db.Insertable(purchaseOrderDetail).ExecuteCommand();
        }
    }
}
