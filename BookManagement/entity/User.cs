﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.entity
{
    [SugarTable("users")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int id { get; set; }

        public string userId { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string role { get; set; }

    }
}
