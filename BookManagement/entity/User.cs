using SqlSugar;
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
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "user_id")]
        public int userId { get; set; }
        [SugarColumn(ColumnName = "username")]
        public string username { get; set; }
        [SugarColumn(ColumnName = "password")]
        public string password { get; set; }
        [SugarColumn(ColumnName = "role")]
        public string role { get; set; }

        [SugarColumn(ColumnName = "created_at")]
        public DateTime CreatedAt { get; set; }


    }

}
