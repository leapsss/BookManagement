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
        public static List<User> GetUsers()
        {
            return DatabaseService.Instance.Db.Queryable<User>().ToList();
        }
        public static User GetUserById(string id)
        {
            return DatabaseService.Instance.Db.Queryable<User>().Where(it => it.userId == id).First();
        }
        public static void UpdateUser(User user)
        {
            DatabaseService.Instance.Db.Updateable(user).ExecuteCommand();
        }
        public static void DeleteUser(string id)
        {
            DatabaseService.Instance.Db.Deleteable<User>().Where(it => it.userId == id).ExecuteCommand();
        }
        public static void AddUser(User user)
        {
            DatabaseService.Instance.Db.Insertable(user).ExecuteCommand();
        }
    }
}
