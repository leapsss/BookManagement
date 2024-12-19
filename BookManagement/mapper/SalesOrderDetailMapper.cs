using BookManagement.entity;
using BookManagement.util;
using static BookManagement.service.SalesOrderService;

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
        
        public static List<CompleteSalesOrder> getAllCompleteSalesOrder()
        {
            string sql = "select so.sales_order_id,so.order_date,sod.isbn,sod.amount,sod.price,book.book_name,users.user_id,users.username " +
                         "from sales_order so join sales_order_detail sod on so.sales_order_id = sod.sales_order_id " +
                                             "join users on users.id = so.salesperson_id " +
                                             "join book on book.isbn = sod.isbn " +
                         "ORDER BY so.sales_order_id ASC;";

            return DatabaseService.Instance.Db.SqlQueryable<CompleteSalesOrder>(sql).ToList();
        }
    }
}
