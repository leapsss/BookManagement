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
    internal class PurchaseOrderDetailService
    {
        private readonly PurchaseOrderDetailMapper purchaseOrderDetailMapper;
        public PurchaseOrderDetailService() {
            purchaseOrderDetailMapper = new PurchaseOrderDetailMapper();
        }
        public List<PurchaseOrderDetail> GetPurchaseOrderDetails()
        {
            return purchaseOrderDetailMapper.GetPurchaseOrderDetails();
        }
        public  PurchaseOrderDetail GetPurchaseOrderDetailById(int id)
        {
            return purchaseOrderDetailMapper.GetPurchaseOrderDetailById(id);
        }
        public List<PurchaseOrderDetail> GetPurchaseOrderDetails(string orderId, string supplierId, decimal? minPrice, decimal? maxPrice)
        {
            return purchaseOrderDetailMapper.GetPurchaseOrderDetails()
                .Where(po =>
                    (string.IsNullOrEmpty(orderId) || po.PurchaseOrderId.ToString().Contains(orderId)) &&
                    (!minPrice.HasValue || po.Price >= minPrice) &&
                    (!maxPrice.HasValue || po.Price <= maxPrice))
                .ToList();
        }
    }
}
