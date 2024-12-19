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
        static readonly InventoryService inventoryService = new InventoryService();

        public static void addSalesOrder(SalesOrder salesOrder, List<SalesOrderDetail> salesOrderDetails)
        {
            int id = SalesOrderMapper.add(salesOrder);
            salesOrderDetails.ForEach(salesOrderDetail =>
            {
                salesOrderDetail.SalesOrderId = id;
                SalesOrderDetailMapper.add(salesOrderDetail);
                inventoryService.RemoveInventory(salesOrderDetail.isbn, salesOrderDetail.Amount);
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
            if (inventoryService.GetInventoryByIsbn(salesOrderDetail.isbn) != null)
            {
                salesOrderDetailDTO.inventory = inventoryService.GetInventoryByIsbn(salesOrderDetail.isbn).Quantity;
            } else
            {
                salesOrderDetailDTO.inventory = 0;
            }
            return salesOrderDetailDTO;
        }

        public static List<CompleteSalesOrder> getAllCompleteSalesOrder()
        {
            return SalesOrderDetailMapper.getAllCompleteSalesOrder();
        }

        public static int GetgetAllCompleteSalesOrderCount()
        {
            return SalesOrderDetailMapper.getAllCompleteSalesOrderCount();
        }

        public static List<CompleteSalesOrder> GetCompleteSalesOrdersByPage(int currentPage, int pageSize)
        {
            return SalesOrderDetailMapper.GetCompleteSalesOrdersByPage(currentPage, pageSize);
        }

        public static List<CompleteSalesOrder> GetCompleteSalesOrdersByMultipleConditions(int _currentPage, int _pageSize, int? salesOrderId,string? isbn,string? userId, DateOnly? startOrderDate,DateOnly? endOrderDate)
        {
            return SalesOrderDetailMapper.GetCompleteSalesOrdersByMultipleConditions(_currentPage, _pageSize, salesOrderId, isbn, userId, startOrderDate, endOrderDate);
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

            public int inventory { get; set; }

            public decimal totalPrice
            {
                get
                {
                    return amount * price;
                }
            }
        }

        public class CompleteSalesOrder
        {
            public int salesOrderId { get; set; }
            public string bookName { get; set; }
            public string isbn { get; set; }
            public DateOnly orderDate { get; set; }
            public int amount { get; set; }
            public decimal price { get; set; }
            public string userId { get; set; }
            public string username { get; set; }
            public decimal totalPrice
            {
                set { }
                get
                {
                    return amount * price;
                }
               
            }
        }


    }
}
