using BookManagement.entity;
using BookManagement.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.mapper
{
    class UserMapper
    {
        public static User getUserById(string userId)
        {
            return DatabaseService.Instance.Db.Queryable<BookManagement.entity.User>()
                                           .First(u => u.userId == userId);
        }
        public static List<User> getUsers() {
            return DatabaseService.Instance.Db.Queryable<BookManagement.entity.User>().ToList();
        }
        public static void updateUser(User user)
        {
            DatabaseService.Instance.Db.Updateable(user).ExecuteCommand();
        }
        public static void deleteUser(string userId) {
            DatabaseService.Instance.Db.Deleteable<BookManagement.entity.User>().Where(it => it.userId == userId).ExecuteCommand();
        }
        public static List<User> GetPagedUsers(int pageIndex, int pageSize)
        {
            return DatabaseService.Instance.Db.Queryable<BookManagement.entity.User>()
                                              .Skip((pageIndex - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();
        }
        public static  List<User> GetFilteredUsers(String userId,string username,string role)
        {
            var query = UserMapper.getUsers().AsQueryable();
            if (!string.IsNullOrEmpty(userId))
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    query = query.Where(it => it.userId == userId);
                }
                else
                {
                    return new List<User>(); // 返回空列表
                }
            }
            if (!string.IsNullOrEmpty(role))
            {
                if (!string.IsNullOrEmpty(role))
                {
                    query = query.Where(it => it.role == role);
                }
                else
                {

                    return new List<User>(); // 返回空列表
                }
            }
            if (!string.IsNullOrEmpty(username))
            {
                if (!string.IsNullOrEmpty(username))
                {
                    query = query.Where(it => it.username == username);
                }
                else
                {

                    return new List<User>(); // 返回空列表
                }
            }




            var result = query.ToList();

            if (!result.Any())
            {

                return new List<User>();
            }

            return result;
        }
        }

}
