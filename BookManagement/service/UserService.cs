using BookManagement.entity;
using BookManagement.mapper;
using System;
using System.Collections.Generic;
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
    }
}
