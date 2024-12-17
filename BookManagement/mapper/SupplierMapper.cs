

using BookManagement.entity;
using BookManagement.util;

namespace BookManagement.repository
{
    public class SupplierMapper
    {
        public static void add(Supplier supplier)
        {
            DatabaseService.Instance.Db.Insertable(supplier).ExecuteCommand();
        }

        public static void deleteBySupplierId(int supplierId)
        {
            DatabaseService.Instance.Db.Deleteable<Supplier>(supplierId).ExecuteCommand();
        }

        public static void update(Supplier supplier)
        {
            DatabaseService.Instance.Db.Updateable(supplier).ExecuteCommand();
        }

        public static Supplier getBySupplierId(int supplierId)
        {
            return DatabaseService.Instance.Db.Queryable<Supplier>().Where(it => it.SupplierId == supplierId).First();
        }

        public static List<Supplier> getAll()
        {
            return DatabaseService.Instance.Db.Queryable<Supplier>().ToList();
        }
    }
}