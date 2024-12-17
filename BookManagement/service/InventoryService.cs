using BookManagement.entity;
using BookManagement.mapper;

namespace BookManagement.service
{
    public class InventoryService
    {
        public InventoryService() { }

        public void addInventory(Inventory inventory)
        {
            InventoryMapper.addInventory(inventory);
        }

        public void updateInventory(Inventory inventory)
        {
            InventoryMapper.updateInventory(inventory);
        }

        public void deleteInventoryById(int inventoryId)
        {
            InventoryMapper.deleteInventoryById(inventoryId);
        }

        public Inventory getInventoryById(int inventoryId)
        {
            return InventoryMapper.getInventoryById(inventoryId);
        }

        public List<Inventory> getAllInventory()
        {
            return InventoryMapper.getAllInventory();
        }

        public Inventory getInventoryByIsbn(string isbn)
        {
            return InventoryMapper.getInventoryByIsbn(isbn);
        }

        public List<Inventory> getInventoryWithBookInfo(string bookName = "", string clcName = "", decimal? minPrice = null, decimal? maxPrice = null)
        {
            return InventoryMapper.getInventoryWithBookInfo(bookName, clcName, minPrice, maxPrice);
        }
    }
}
