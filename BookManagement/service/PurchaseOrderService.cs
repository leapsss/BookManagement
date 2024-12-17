using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagement.entity;
namespace BookManagement.service
{
    internal class PurchaseOrderService
    {
        private SqlSugarScope _db;
        public PurchaseOrderService()
        {
            // 初始化 SqlSugar 数据库连接（修改连接字符串为你的数据库信息）
            _db = new SqlSugarScope(new ConnectionConfig()
            {
                ConnectionString = "Server=52.194.237.192;Port=32750;Database=book;Uid=postgres;Pwd=9#327.5",
                DbType = DbType.PostgreSQL,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        }
        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return _db.Queryable<PurchaseOrder>().ToList();
        }
        public PurchaseOrder GetPurchaseOrderById(int id)
        {
            return _db.Queryable<PurchaseOrder>().Where(it => it.PuchaseOrderId == id).First();
        }
    }
}
