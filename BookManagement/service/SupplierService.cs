using BookManagement.entity;
using BookManagement.repository;

namespace BookManagement.service
{
    public class SupplierService
    {
        public static void add(Supplier supplier)
        {
            SupplierMapper.add(supplier);
        }

        public static void deleteBySupplierId(int supplierId)
        {
            SupplierMapper.deleteBySupplierId(supplierId);
        }

        public static void update(Supplier supplier)
        {
            SupplierMapper.update(supplier);
        }

        public static Supplier getBySupplierId(int supplierId)
        {
            return SupplierMapper.getBySupplierId(supplierId);
        }

        public static List<Supplier> getAll()
        {
            return SupplierMapper.getAll();
        }
    }
}
