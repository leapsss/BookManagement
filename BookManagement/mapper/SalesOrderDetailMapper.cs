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

        public static int getAllCompleteSalesOrderCount()
        {
            string sql = "select count(*) " +
                         "from sales_order so join sales_order_detail sod on so.sales_order_id = sod.sales_order_id " +
                                             "join users on users.id = so.salesperson_id " +
                                             "join book on book.isbn = sod.isbn ";

            return DatabaseService.Instance.Db.Ado.GetInt(sql);
        }
        public static List<CompleteSalesOrder> GetCompleteSalesOrdersByPage(int currentPage, int pageSize)
        {
            int offset = (currentPage - 1) * pageSize;
            var sql = "SELECT so.sales_order_id, so.order_date, sod.isbn, sod.amount, sod.price, book.book_name, users.user_id, users.username " +
                      "FROM sales_order so JOIN sales_order_detail sod ON so.sales_order_id = sod.sales_order_id " +
                                          "JOIN users ON users.id = so.salesperson_id " +
                                          "JOIN book ON book.isbn = sod.isbn " +
                    "ORDER BY so.sales_order_id " +
                    "LIMIT @pageSize OFFSET @offset";

            // 使用参数化查询，避免 SQL 注入
            return DatabaseService.Instance.Db.SqlQueryable<CompleteSalesOrder>(sql)
                     .AddParameters(new { pageSize, offset })
                     .ToList();
        }

        public static List<CompleteSalesOrder> GetCompleteSalesOrdersByMultipleConditions(int currentPage, int pageSize, int? salesOrderId, string? isbn, string? userId, DateOnly? startOrderDate, DateOnly? endOrderDate)
        {
            int offset = (currentPage - 1) * pageSize;
            string sql = "SELECT so.sales_order_id, so.order_date, sod.isbn, sod.amount, sod.price, book.book_name, users.user_id, users.username " +
                 "FROM sales_order so JOIN sales_order_detail sod ON so.sales_order_id = sod.sales_order_id " +
                                     "JOIN users ON users.id = so.salesperson_id " +
                                     "JOIN book ON book.isbn = sod.isbn WHERE 1=1 ";

            if (salesOrderId.HasValue)
            {
                sql += "AND so.sales_order_id = @salesOrderId ";
            }
            if (!string.IsNullOrEmpty(isbn))
            {
                sql += "AND sod.isbn = @isbn ";
            }
            if (!string.IsNullOrEmpty(userId))
            {
                sql += "AND users.user_id = @userId ";
            }
            if (startOrderDate.HasValue)
            {
                sql += "AND so.order_date >= @startOrderDate ";
            }
            if (endOrderDate.HasValue)
            {
                sql += "AND so.order_date <= @endOrderDate ";
            }
            sql += "ORDER BY so.sales_order_id ASC " +
                   "LIMIT @pageSize OFFSET @offset;";

            return DatabaseService.Instance.Db.SqlQueryable<CompleteSalesOrder>(sql)
                    .AddParameters( new { salesOrderId, isbn, userId, startOrderDate = startOrderDate?.ToDateTime(TimeOnly.MinValue), endOrderDate = endOrderDate?.ToDateTime(TimeOnly.MinValue), pageSize, offset })
                    .ToList();
        }
    }
}
