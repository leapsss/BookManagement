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
    }
}