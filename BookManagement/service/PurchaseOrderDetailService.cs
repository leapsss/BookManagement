using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagement.entity;
using BookManagement.mapper;
using BookManagement.entity.Dto;
namespace BookManagement.service
{
    internal class PurchaseOrderDetailService
    {
        public PurchaseOrderDetailService() { }
        public List<PurchaseOrderDetail> GetPurchaseOrderDetails()
        {
            return PurchaseOrderDetailMapper.GetPurchaseOrderDetails();
        }
        public List<PurchaseOrderDetailDto> GetPurchaseOrderDetailDtos()
        {
            return PurchaseOrderDetailMapper.GetPurchaseOrderDetailDtos();
        }
        public  PurchaseOrderDetail GetPurchaseOrderDetailById(int id)
        {
            return PurchaseOrderDetailMapper.GetPurchaseOrderDetailById(id);
        }
        public List<PurchaseOrderDetailDto> QueryPurchaseOrderDetailDtos(string orderId, string isbn,string supplierName,string supplierId,string purchaserId,string purchaserName, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate)
        {
            return PurchaseOrderDetailMapper.QueryPurchaseOrderDetailDtos(orderId, isbn, supplierName, supplierId, purchaserId, purchaserName, minPrice, maxPrice, startDate, endDate);
        }
    }
}
