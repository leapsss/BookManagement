using BookManagement.entity;
using BookManagement.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static List<PurchaseOrder> GetFilteredPurchaseOrders(string orderId, string supplierId, string purchaserId, DateTime? startDate, DateTime? endDate)
        {
            var query = PurchaseOrderMapper.GetPurchaseOrders().AsQueryable();

            if (!string.IsNullOrEmpty(orderId))
            {
                if (int.TryParse(orderId, out int parsedOrderId))
                {
                    query = query.Where(po => po.PurchaseOrderId == parsedOrderId);
                }
                else
                {
                    return new List<PurchaseOrder>(); // 返回空列表
                }
            }
            if (!string.IsNullOrEmpty(supplierId))
            {
                if (int.TryParse(supplierId, out int parsedSupplierId))
                {
                    query = query.Where(po => po.SupplierId == parsedSupplierId);
                }
                else
                {

                    return new List<PurchaseOrder>(); // 返回空列表
                }
            }

            if (!string.IsNullOrEmpty(purchaserId))
            {
                if (int.TryParse(purchaserId, out int parsedPurchaserId))
                {
                    query = query.Where(po => po.PurchaserId == parsedPurchaserId);
                }
                else
                {

                    return new List<PurchaseOrder>(); // 返回空列表
                }
            }

            if (startDate.HasValue)
                query = query.Where(po => po.OrderDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(po => po.OrderDate <= endDate.Value);


            var result = query.ToList();

            if (!result.Any())
            {

                return new List<PurchaseOrder>();
            }

            return result;
        }
    }
}
