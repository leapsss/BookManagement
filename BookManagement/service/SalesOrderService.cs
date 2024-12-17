using BookManagement.entity;
using BookManagement.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.service
{
    public class SalesOrderService
    {
        public static void addSalesOrder(SalesOrder salesOrder, List<SalesOrderDetail> salesOrderDetails)
        {
            SalesOrderMapper.add(salesOrder);
            salesOrderDetails.ForEach(salesOrderDetail =>
            {
                salesOrderDetail.SalesOrderId = salesOrder.SalesOrderId;
                SalesOrderDetailMapper.add(salesOrderDetail);
                //todo when inventory ready, update inventory data.
            });
        }

        public static List<SalesOrder> getAll()
        {
            return SalesOrderMapper.getAll();
        }

        public static List<SalesOrderDetail> getSalesOrderDetailBySalesOrderId(SalesOrder salesOrder)
        {
            return SalesOrderDetailMapper.getAllBySalesOrderId(salesOrder.SalesOrderId);
        }

        public static SalesOrderDetailDTO salesOrderDetailToDTO(SalesOrderDetail salesOrderDetail)
        {
            SalesOrderDetailDTO salesOrderDetailDTO = new SalesOrderDetailDTO();
            Book book = BookMapper.getBookByISBN(salesOrderDetail.ISBN);
            salesOrderDetailDTO.isbn = salesOrderDetail.ISBN;
            salesOrderDetailDTO.bookName = book.bookName;
            salesOrderDetailDTO.press = book.press;
            salesOrderDetailDTO.author = book.author;
            salesOrderDetailDTO.amount = salesOrderDetail.Amount;
            salesOrderDetailDTO.price = salesOrderDetail.Price;
            return salesOrderDetailDTO;
        }

        public class SalesOrderDetailDTO
        {
            public string isbn;
            public string bookName;
            public string press;
            public string author;
            public int amount;
            public decimal price;
        }
    }
}
