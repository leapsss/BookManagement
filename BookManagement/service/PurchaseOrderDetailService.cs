using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagement.entity;
namespace BookManagement.service
{
    internal class PurchaseOrderDetailService
    {
        private SqlSugarScope _db;
        public PurchaseOrderDetailService()
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
        public List<PurchaseOrderDetail> GetPurchaseOrderDetails()
        {
            return _db.Queryable<PurchaseOrderDetail>().ToList();
        }
        public PurchaseOrderDetail GetPurchaseOrderDetailById(int id)
        {
            return _db.Queryable<PurchaseOrderDetail>().Where(it => it.PurchaseOrderDetailId == id).First();
        }
    }
}
