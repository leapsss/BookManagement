using BookManagement.entity;
using BookManagement.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.mapper
{
    public class SalesOrderDetailMapper
    {
        public static void add(SalesOrderDetail salesOrderDetail)
        {
            DatabaseService.Instance.Db.Insertable(salesOrderDetail).ExecuteCommand();
        }

        public static void deleteBySalesOrderDetailId(int salesOrderDetailId)
        {
            DatabaseService.Instance.Db.Deleteable<SalesOrderDetail>(salesOrderDetailId).ExecuteCommand();
        }

        public static List<SalesOrderDetail> getAllBySalesOrderId(int salesOrderId)
        {
            return DatabaseService.Instance.Db.Queryable<SalesOrderDetail>().Where(it => it.SalesOrderId == salesOrderId).ToList();
        }
    }
}
