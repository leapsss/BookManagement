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
    }
}
