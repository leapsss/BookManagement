using BookManagement.entity;
using BookManagement.mapper;
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
        private readonly UserMapper userMapper;
        public UserService()
        {
            userMapper = new UserMapper();
        }
        public List<User> GetUsers()
        {
            return userMapper.GetUsers();
        }
        public User GetUserById(int id)
        {
            return userMapper.GetUserById(id);
        }
        public void UpdateUser(User user)
        {
            userMapper.UpdateUser(user);
        }
        public void DeleteUser(int id)
        {
            userMapper.DeleteUser(id);
        }
        public void AddUser(User user)
        {
            userMapper.AddUser(user);
        }


    }


}
