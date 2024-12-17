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
        public static User getUserById(string userId) 
        {
            return DatabaseService.Instance.Db.Queryable<BookManagement.entity.User>()
                                           .First(u => u.userId == userId);
        }
         



    }
}
