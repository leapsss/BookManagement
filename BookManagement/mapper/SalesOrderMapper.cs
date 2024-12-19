using BookManagement.entity;
using BookManagement.service;
using BookManagement.util;
using SqlSugar;

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

        public static List<SalesOrder> GetSalesOrdersBySalespersonId(int salespersonId) {
            return DatabaseService.Instance.Db.Queryable<SalesOrder>().Where(it => it.SalespersonId == salespersonId).ToList();
        }

        //根据用户id实现多条件查询
        public static List<SalesOrderService.CompleteSalesOrder> GetCompleteSalesOrdersByMultipleConditionsAndUserId(
        int currentPage,
        int pageSize,
        int? salesOrderId,
        string? isbn,
        string? bookName,
        DateOnly? startOrderDate,
        DateOnly? endOrderDate,
        int userId)
        {
            var query = DatabaseService.Instance.Db.Queryable<SalesOrder, SalesOrderDetail, Book>((so, sod, b) =>
                new JoinQueryInfos(
                    JoinType.Inner, so.SalesOrderId == sod.SalesOrderId,
                    JoinType.Inner, sod.isbn == b.isbn
                ))
                .Where(so => so.SalespersonId == userId);

            if (salesOrderId.HasValue)
            {
                query = query.Where((so, sod, b) => so.SalesOrderId == salesOrderId.Value);
            }
            if (!string.IsNullOrEmpty(isbn))
            {
                query = query.Where((so, sod, b) => sod.isbn.Contains(isbn));
            }
            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where((so, sod, b) => b.bookName.Contains(bookName));
            }
            if (startOrderDate.HasValue)
            {
                var startDateTime = startOrderDate.Value.ToDateTime(TimeOnly.MinValue);
                query = query.Where((so, sod, b) => so.OrderDate >= startDateTime);
            }
            if (endOrderDate.HasValue)
            {
                var endDateTime = endOrderDate.Value.ToDateTime(TimeOnly.MaxValue);
                query = query.Where((so, sod, b) => so.OrderDate <= endDateTime);
            }

            var result = query
                .OrderBy(so => so.SalesOrderId)
                .Select((so, sod, b) => new
            {
                salesOrderId = so.SalesOrderId,
                bookName = b.bookName,
                isbn = sod.isbn,
                orderDate = so.OrderDate,
                amount = sod.Amount,
                price = sod.Price,
                userId = so.SalespersonId.ToString()
            })
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            return result.Select(item => new SalesOrderService.CompleteSalesOrder
            {
                salesOrderId = item.salesOrderId,
                bookName = item.bookName,
                isbn = item.isbn,
                orderDate = DateOnly.FromDateTime(item.orderDate),
                amount = item.amount,
                price = item.price,
                userId = item.userId,
                username = "" // 如果需要用户名，可能需要额外查询
            }).ToList();
        }

        public static int GetCompleteSalesOrderCountByUserId(
            int? salesOrderId,
            string? isbn,
            string? bookName,
            DateOnly? startOrderDate,
            DateOnly? endOrderDate,
            int userId)
        {
            var query = DatabaseService.Instance.Db.Queryable<SalesOrder, SalesOrderDetail, Book>((so, sod, b) =>
                new JoinQueryInfos(
                    JoinType.Inner, so.SalesOrderId == sod.SalesOrderId,
                    JoinType.Inner, sod.isbn == b.isbn
                ))
                .Where(so => so.SalespersonId == userId);

            if (salesOrderId.HasValue)
            {
                query = query.Where((so, sod, b) => so.SalesOrderId == salesOrderId.Value);
            }
            if (!string.IsNullOrEmpty(isbn))
            {
                query = query.Where((so, sod, b) => sod.isbn.Contains(isbn));
            }
            if (!string.IsNullOrEmpty(bookName))
            {
                query = query.Where((so, sod, b) => b.bookName.Contains(bookName));
            }
            if (startOrderDate.HasValue)
            {
                var startDateTime = startOrderDate.Value.ToDateTime(TimeOnly.MinValue);
                query = query.Where((so, sod, b) => so.OrderDate >= startDateTime);
            }
            if (endOrderDate.HasValue)
            {
                var endDateTime = endOrderDate.Value.ToDateTime(TimeOnly.MaxValue);
                query = query.Where((so, sod, b) => so.OrderDate <= endDateTime);
            }

            return query.Count();
        }

    }
}
