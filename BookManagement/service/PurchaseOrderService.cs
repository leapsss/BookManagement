using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagement.entity;
using BookManagement.mapper;
namespace BookManagement.service
{
    internal class PurchaseOrderService
    {
        private static readonly InventoryService inventoryService = new InventoryService();
        public PurchaseOrderService() { }
        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return PurchaseOrderMapper.GetPurchaseOrders();
        }
        public PurchaseOrder GetPurchaseOrderById(int id)
        {
            return PurchaseOrderMapper.GetPurchaseOrderById(id);
        }
        public List<PurchaseOrder> GetPagedPurchaseOrders(int pageIndex, int pageSize)
        {
            return PurchaseOrderMapper.GetPagedPurchaseOrders(pageIndex, pageSize);
        }
        public List<PurchaseOrder> GetFilteredPurchaseOrders(string orderId, string supplierId, string purchaserId, DateTime? startDate, DateTime? endDate)
        {
            return PurchaseOrderMapper.GetFilteredPurchaseOrders(orderId, supplierId, purchaserId, startDate, endDate);
        }

        public static void AddPurchaseOrder(PurchaseOrder purchaseOrder, List<PurchaseOrderDetail> purchaseOrderDetails)
        {
            int id = PurchaseOrderMapper.Add(purchaseOrder);
            purchaseOrderDetails.ForEach(purchaseOrderDetail =>
            {
                purchaseOrderDetail.PurchaseOrderId = id;
                PurchaseOrderDetailMapper.Add(purchaseOrderDetail);
                inventoryService.AddInventory(purchaseOrderDetail.Isbn, purchaseOrderDetail.Amount);
            });
        }

        public static PurchaseOrderDetailDTO? purchaseOrderToDTO(PurchaseOrderDetail purchaseOrderDetail)
        {
            PurchaseOrderDetailDTO purchaseOrderDetailDTO = new PurchaseOrderDetailDTO();
            Book book = BookMapper.getBookByISBN(purchaseOrderDetail.Isbn);
            if (book == null)
            {
                return null;
            }
            purchaseOrderDetailDTO.isbn = purchaseOrderDetail.Isbn;
            purchaseOrderDetailDTO.bookName = book.bookName;
            purchaseOrderDetailDTO.press = book.press;
            purchaseOrderDetailDTO.author = book.author;
            purchaseOrderDetailDTO.amount = purchaseOrderDetail.Amount;
            purchaseOrderDetailDTO.price = purchaseOrderDetail.Price;
            purchaseOrderDetailDTO.suggestedRetailPrice = book.price / 100;
            if (inventoryService.GetInventoryByIsbn(purchaseOrderDetail.Isbn) != null)
            {
                purchaseOrderDetailDTO.inventory = inventoryService.GetInventoryByIsbn(purchaseOrderDetail.Isbn).Quantity;
            }
            else
            {
                purchaseOrderDetailDTO.inventory = 0;
            }
            return purchaseOrderDetailDTO;
        }

        public class PurchaseOrderDetailDTO
        {
            public string isbn { get; set; }
            public string bookName { get; set; }
            public string press { get; set; }
            public string author { get; set; }
            public int amount { get; set; }
            public decimal price { get; set; }
            public decimal suggestedRetailPrice { get; set; }
            public int inventory { get; set; }

            public decimal totalPrice
            {
                get
                {
                    return price * amount;
                }
            }
        }




    }
}
