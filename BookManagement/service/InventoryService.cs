using BookManagement.mapper;
using BookManagement.entity;
using System;
using System.Collections.Generic;

namespace BookManagement.service
{
    public class InventoryService
    {
        public List<Inventory> GetAllInventory()
        {
            return InventoryMapper.GetAllInventory();
        }

        public List<InventoryDisplay> GetAllInventoryWithBookName()
        {
            var inventories = InventoryMapper.GetAllInventory();
            var result = new List<InventoryDisplay>();

            foreach (var inventory in inventories)
            {
                var book = BookMapper.getBookByISBN(inventory.Isbn);
                result.Add(new InventoryDisplay
                {
                    InventoryId = inventory.InventoryId,
                    Isbn = inventory.Isbn,
                    BookName = book?.bookName ?? "未知书名",
                    Quantity = inventory.Quantity,
                    LastUpdated = inventory.LastUpdated
                });
            }

            return result;
        }

        public string GetBookNameByIsbn(string isbn)
        {
            var book = BookMapper.getBookByISBN(isbn);
            return book?.bookName ?? "未知书名";
        }

        public Inventory GetInventoryByIsbn(string isbn)
        {
            return InventoryMapper.GetInventoryByIsbn(isbn);
        }

        public void AddInventory(string isbn, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("入库数量必须大于零");
            }

            InventoryMapper.AddInventory(isbn, quantity);
        }

        public void RemoveInventory(string isbn, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("出库数量必须大于零");
            }

            InventoryMapper.RemoveInventory(isbn, quantity);
        }

        public class InventoryDisplay
        {
            public int InventoryId { get; set; }
            public string Isbn { get; set; }
            public string BookName { get; set; }
            public int Quantity { get; set; }
            public DateTime LastUpdated { get; set; }
        }
    }
}