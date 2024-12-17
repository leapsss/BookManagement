using BookManagement.entity;
using BookManagement.util;

namespace BookManagement.mapper
{
    public class InventoryMapper
    {
        public static void addInventory(Inventory inventory)
        {
            DatabaseService.Instance.Db.Insertable(inventory).ExecuteCommand();
        }

        public static void updateInventory(Inventory inventory)
        {
            DatabaseService.Instance.Db.Updateable(inventory).ExecuteCommand();
        }

        public static void deleteInventoryById(int inventoryId)
        {
            DatabaseService.Instance.Db.Deleteable<Inventory>().Where(it => it.InventoryId == inventoryId).ExecuteCommand();
        }

        public static Inventory getInventoryById(int inventoryId)
        {
            return DatabaseService.Instance.Db.Queryable<Inventory>().Where(it => it.InventoryId == inventoryId).First();
        }

        public static List<Inventory> getAllInventory()
        {
            return DatabaseService.Instance.Db.Queryable<Inventory>().ToList();
        }

        public static Inventory getInventoryByIsbn(string isbn)
        {
            return DatabaseService.Instance.Db.Queryable<Inventory>().Where(it => it.Isbn == isbn).First();
        }

        public static List<Inventory> getInventoryWithBookInfo(string bookName = "", string clcName = "", decimal? minPrice = null, decimal? maxPrice = null)
        {
            var query = DatabaseService.Instance.Db.Queryable<Inventory>()
                .LeftJoin<Book>((i, b) => i.Isbn == b.isbn)
                .WhereIF(!string.IsNullOrEmpty(bookName), (i, b) => b.bookName.Contains(bookName))
                .WhereIF(!string.IsNullOrEmpty(clcName), (i, b) => b.clcName == clcName)
                .WhereIF(minPrice.HasValue, (i, b) => b.price >= minPrice.Value)
                .WhereIF(maxPrice.HasValue, (i, b) => b.price <= maxPrice.Value)
                .Select((i, b) => new Inventory
                {
                    InventoryId = i.InventoryId,
                    Isbn = i.Isbn,
                    Quantity = i.Quantity,
                    LastUpdated = i.LastUpdated,
                    BookName = b.bookName,
                    Price = b.price,
                    ClcName = b.clcName
                })
                .ToList();

            return query;
        }
    }
}