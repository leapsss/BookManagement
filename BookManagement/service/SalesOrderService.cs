using BookManagement.entity;
using BookManagement.mapper;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookManagement.service
{
    public class SalesOrderService
    {
        public static void addSalesOrder(SalesOrder salesOrder, List<SalesOrderDetail> salesOrderDetails)
        {
            int id = SalesOrderMapper.add(salesOrder);
            salesOrderDetails.ForEach(salesOrderDetail =>
            {
                salesOrderDetail.SalesOrderId = id;
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

        public static SalesOrderDetailDTO? salesOrderDetailToDTO(SalesOrderDetail salesOrderDetail)
        {
            SalesOrderDetailDTO salesOrderDetailDTO = new SalesOrderDetailDTO();
            Book book = BookMapper.getBookByISBN(salesOrderDetail.isbn);
            if (book == null)
            {
                return null;
            }
            salesOrderDetailDTO.isbn = salesOrderDetail.isbn;
            salesOrderDetailDTO.bookName = book.bookName;
            salesOrderDetailDTO.press = book.press;
            salesOrderDetailDTO.author = book.author;
            salesOrderDetailDTO.amount = salesOrderDetail.Amount;
            salesOrderDetailDTO.price = salesOrderDetail.Price;
            salesOrderDetailDTO.originalPrice = book.price / 100;
            return salesOrderDetailDTO;
        }

        public class SalesOrderDetailDTO
        {
            public string isbn {  get; set; }
            public string bookName { get; set; }
            public string press { get; set; }
            public string author { get; set; }
            public int amount { get; set; }
            public decimal price { get; set; }

            public decimal originalPrice { get; set; }

            public decimal totalPrice
            {
                get
                {
                    return amount * price;
                }
            }
        }
    }
}
