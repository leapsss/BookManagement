using SqlSugar;
using System;

namespace BookManagement.util
{
    class DatabaseService
    {
        private static DatabaseService _instance;
        private static readonly object _lock = new object();
        private SqlSugarClient _db;

        private DatabaseService()
        {
            _db = new SqlSugarClient(new ConnectionConfig()
            {
                //ConnectionString = "Server=52.194.237.192;Port=32750;Database=book;Uid=postgres;Pwd=9#327.5",
                //todo remove test database
                ConnectionString = "Server=localhost;Port=5432;Database=book;Uid=postgres;Pwd=1234",
                DbType = DbType.PostgreSQL,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            _db.CurrentConnectionConfig.ConfigureExternalServices = new ConfigureExternalServices
            {
                EntityService = (property, column) =>
                {
                    // 属性名驼峰 -> 列名下划线
                    column.DbColumnName = string.Concat(property.Name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
                }
            };


            // 打印SQL日志（可选）
            _db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql); // 输出SQL语句到控制台
            };
        }

        public static DatabaseService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseService();
                    }
                    return _instance;
                }
            }
        }

        public SqlSugarClient Db => _db;
    }
}
