using BookManagement.entity;
using BookManagement.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.mapper
{
    class UserMapper
    {
        public  List<User> GetUsers()
        {
            return DatabaseService.Instance.Db.Queryable<User>().ToList();
        }
        public  User GetUserById(string id)
        {
            return DatabaseService.Instance.Db.Queryable<User>().Where(it => it.userId == id).First();
        }
        public  void UpdateUser(User user)
        {
            DatabaseService.Instance.Db.Updateable(user).ExecuteCommand();
        }
        public void DeleteUser(string id)
        {
            DatabaseService.Instance.Db.Deleteable<User>().Where(it => it.userId == id).ExecuteCommand();
        }
        public  void AddUser(User user)
        {
            DatabaseService.Instance.Db.Insertable(user).ExecuteCommand();
        }
    }
}
