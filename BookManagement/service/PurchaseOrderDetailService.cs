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
        public PurchaseOrderDetailService() { }
        public List<PurchaseOrderDetail> GetPurchaseOrderDetails()
        {
            return PurchaseOrderDetailMapper.GetPurchaseOrderDetails();
        }
        public  PurchaseOrderDetail GetPurchaseOrderDetailById(int id)
        {
            return PurchaseOrderDetailMapper.GetPurchaseOrderDetailById(id);
        }
        public List<PurchaseOrderDetail> GetPurchaseOrderDetails(string orderId, string supplierId, decimal? minPrice, decimal? maxPrice)
        {
            return PurchaseOrderDetailMapper.GetPurchaseOrderDetails(orderId, supplierId, minPrice, maxPrice);
        }
    }
}
