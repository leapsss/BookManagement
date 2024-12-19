using BookManagement.entity;
using BookManagement.util;
using SqlSugar;
using System;
using System.Linq;

namespace BookManagement.mapper
{
    public class InventoryMapper
    {
        // 获取所有库存
        public static List<Inventory> GetAllInventory()
        {
            return DatabaseService.Instance.Db.Queryable<Inventory>().ToList();
        }

        // 通过ISBN获取库存
        public static Inventory GetInventoryByIsbn(string isbn)
        {
            return DatabaseService.Instance.Db.Queryable<Inventory>()
                                                .Where(it => it.Isbn == isbn)
                                                .First();
        }

        // 增加库存
        public static void AddInventory(string isbn, int quantity)
        {
            var inventory = GetInventoryByIsbn(isbn);
            if (inventory != null)
            {
                // 如果库存存在，更新库存数量
                inventory.Quantity += quantity;
                inventory.LastUpdated = DateTime.Now;
                DatabaseService.Instance.Db.Updateable(inventory).ExecuteCommand();
            }
            else
            {
                // 库存不存在，创建新的库存记录
                var newInventory = new Inventory { Isbn = isbn, Quantity = quantity, LastUpdated = DateTime.Now };
                DatabaseService.Instance.Db.Insertable(newInventory).ExecuteCommand();
            }
        }

        // 减少库存
        public static void RemoveInventory(string isbn, int quantity)
        {
            var inventory = GetInventoryByIsbn(isbn);
            if (inventory == null)
            {
                throw new Exception("库存中不存在该ISBN的书籍");
            }

            if (inventory.Quantity < quantity)
            {
                throw new Exception("库存不足，无法出库");
            }

            // 如果库存存在且数量足够，减少库存
            inventory.Quantity -= quantity;
            inventory.LastUpdated = DateTime.Now;
            DatabaseService.Instance.Db.Updateable(inventory).ExecuteCommand();
        }
    }
}