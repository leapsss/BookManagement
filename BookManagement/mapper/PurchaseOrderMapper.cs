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
    }
}
