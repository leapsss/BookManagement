using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.entity.Dto
{
    public class PurchaseOrderDto
    {

        public int PurchaseOrderId { get; set; }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public DateTime OrderDate { get; set; }

        public int PurchaserId { get; set; }
        public string PurchaserName { get; set; }
        public decimal price { get; set; }
       
    }
}
