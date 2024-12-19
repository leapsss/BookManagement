using BookManagement.entity;
using BookManagement.util;
using System.Data;

namespace BookManagement.mapper
{
    class UserMapper
    {

        public static User? GetUserByUserIdAndPassword(string userId,string password)
        {
            return DatabaseService.Instance.Db.Queryable<User>()
                                           .Where(u => u.userId == userId && u.password == password)
                                           .First();
        }
        public static User GetUserById(int Id)
        {
            return DatabaseService.Instance.Db.Queryable<User>()
                                           .First(u => u.id == Id);
        }
        public static User GetUserByUserId(string userId)
        {
            return DatabaseService.Instance.Db.Queryable<User>()
                                           .First(u => u.userId == userId);
        }
        public static List<User> GetUsers() {
            return DatabaseService.Instance.Db.Queryable<User>().ToList();
        }
        public static void UpdateUser(User user)
        {
            DatabaseService.Instance.Db.Updateable(user).ExecuteCommand();
        }
        public static void DeleteUser(string userId) {

            DatabaseService.Instance.Db.Deleteable<User>().Where(it => it.userId == userId).ExecuteCommand();
        }
        public static List<User> GetPagedUsers(int pageIndex, int pageSize)
        {
            return DatabaseService.Instance.Db.Queryable<User>()
                                              .Skip((pageIndex - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToList();
        }
        public static  List<User> GetFilteredUsers(string userId,string username,string role)
        {
            var query = UserMapper.GetUsers().AsQueryable();
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
