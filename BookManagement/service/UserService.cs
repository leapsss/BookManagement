using BookManagement.entity;
using BookManagement.mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.service
{
    class UserService
    {
        public static User GetUserByUserIdAndPassword(string userId,string password)
        {
            return UserMapper.GetUserByUserIdAndPassword(userId, password);
        }

        public static User GetUserById(int Id)
        {
            return UserMapper.GetUserById(Id);
        }
        
        public  List<User> GetUsers()
        {
            return UserMapper.GetUsers();
        }
        public  void DeleteUser(string userId) {
             UserMapper.DeleteUser(userId);
        }
        public  void UpdateUser(User user)
        {
            UserMapper.UpdateUser(user);
        }
        public List<User> GetFilteredUsers(String userId, string username, string role)
        {
            return UserMapper.GetFilteredUsers(userId, username, role);
        }
        public List<User> GetPagedUsers(int pageIndex, int pageSize)
        {
            return UserMapper.GetPagedUsers(pageIndex, pageSize);
        }
    }
}
