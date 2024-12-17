using BookManagement.entity;
using BookManagement.mapper;
using BookManagement.util;
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


    }
}
