using BookManagement.entity;
using BookManagement.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.mapper
{
    internal class PurchaseOrderDetailMapper
    {
        public static List<PurchaseOrderDetail> GetPurchaseOrderDetails()
        {
            return DatabaseService.Instance.Db.Queryable<PurchaseOrderDetail>().ToList();
        }
        public static PurchaseOrderDetail GetPurchaseOrderDetailById(int id)
        {
            return DatabaseService.Instance.Db.Queryable<PurchaseOrderDetail>().Where(it => it.PurchaseOrderDetailId == id).First();
        }
        public static List<PurchaseOrderDetail> GetPurchaseOrderDetails(string orderId, string supplierId, decimal? minPrice, decimal? maxPrice)
        {
            return GetPurchaseOrderDetails()
                .Where(po =>
                    (string.IsNullOrEmpty(orderId) || po.PurchaseOrderId.ToString().Contains(orderId)) &&
                    (!minPrice.HasValue || po.Price >= minPrice) &&
                    (!maxPrice.HasValue || po.Price <= maxPrice))
                .ToList();
        }
    }
}
