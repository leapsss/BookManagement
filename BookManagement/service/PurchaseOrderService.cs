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
        private readonly PurchaseOrderMapper purchaseOrderMapper;
        public PurchaseOrderService() {
            purchaseOrderMapper = new PurchaseOrderMapper();
        }
        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return purchaseOrderMapper.GetPurchaseOrders();
        }
        public PurchaseOrder GetPurchaseOrderById(int id)
        {
            return purchaseOrderMapper.GetPurchaseOrderById(id);
        }
        public List<PurchaseOrder> GetPagedPurchaseOrders(int pageIndex, int pageSize)
        {
            return purchaseOrderMapper.GetPagedPurchaseOrders(pageIndex, pageSize);
        }
        public List<PurchaseOrder> GetFilteredPurchaseOrders(string orderId, string supplierId, string purchaserId, DateTime? startDate, DateTime? endDate)
        {
            var query = purchaseOrderMapper.GetPurchaseOrders().AsQueryable();

            if (!string.IsNullOrEmpty(orderId))
                query = query.Where(po => po.PurchaseOrderId.ToString().Contains(orderId)); // 使用ToString()转换为字符串后进行匹配

            if (!string.IsNullOrEmpty(supplierId))
                query = query.Where(po => po.SupplierId.ToString().Contains(supplierId));

            if (!string.IsNullOrEmpty(purchaserId))
                query = query.Where(po => po.PurchaserId.ToString().Contains(purchaserId));

            if (startDate.HasValue)
                query = query.Where(po => po.OrderDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(po => po.OrderDate <= endDate.Value);

            return query.ToList();
        }

    }
}
