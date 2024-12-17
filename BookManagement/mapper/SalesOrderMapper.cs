using BookManagement.entity;
using BookManagement.util;

namespace BookManagement.mapper
{
    public class SalesOrderMapper
    {
        public static int add(SalesOrder salesOrder)
        {
            return DatabaseService.Instance.Db.Insertable(salesOrder).ExecuteReturnIdentity(); // return auto increase id
        }

        public static void update(SalesOrder salesOrder)
        {
            DatabaseService.Instance.Db.Updateable(salesOrder).ExecuteCommand();
        }

        public static void deleteBySalesOrderId(int salesOrderId)
        {
            DatabaseService.Instance.Db.Deleteable<SalesOrder>().Where(it => it.SalesOrderId == salesOrderId).ExecuteCommand();
        }

        public static SalesOrder getBySalesOrderId(int salesOrderId)
        {
            return DatabaseService.Instance.Db.Queryable<SalesOrder>().Where(it => it.SalesOrderId == salesOrderId).First();
        }

        public static List<SalesOrder> getAll()
        {
            return DatabaseService.Instance.Db.Queryable<SalesOrder>().ToList();
        }
    }
}
