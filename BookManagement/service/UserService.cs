using BookManagement.entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.service
{
    public class UserService
    {
        private SqlSugarScope _db;

        public UserService()
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
        public List<User> GetUsers()
        {
            return _db.Queryable<User>().ToList();
        }
        public User GetUserById(int id)
        {
            return _db.Queryable<User>().Where(it => it.userId == id).First();
        }
        public void UpdateUser(User user)
        {
            _db.Updateable(user).ExecuteCommand();
        }
        public void DeleteUser(int id)
        {
            _db.Deleteable<User>().Where(it => it.userId == id).ExecuteCommand();
        }
        public void AddUser(User user)
        {
            _db.Insertable(user).ExecuteCommand();
        }


    }


}
