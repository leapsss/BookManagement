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
        public static User getUserById(string userId)
        {
            return UserMapper.getUserById(userId);
        }
        public  List<User> GetUsers()
        {
            return UserMapper.getUsers();
        }
        public  void DeleteUser(string userId) {
             UserMapper.deleteUser(userId);
        }
        public  void UpdateUser(User user)
        {
            UserMapper.updateUser(user);
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
