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


    }
}
