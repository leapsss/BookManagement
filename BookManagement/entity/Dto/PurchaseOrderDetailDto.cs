using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.entity.Dto
{
    public class PurchaseOrderDetailDto
    {

        public int PurchaseOrderDetailId { get; set; }

        public int PurchaseOrderId { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string PurchaserId { get; set; }
        public string Username { get; set; }
        public string Isbn { get; set; }
        public DateTime OrderDate { get; set; }
        public string BookName { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Price { get; set; }
    }
}
