using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class SupplierRepository
    {

        private Entities db;

        public SupplierRepository(Entities entities) { this.db = entities; }

        public IQueryable<Supplier> FindAllSuppliers() { return db.Suppliers.Include("Products").OrderBy(supplier => supplier.CompanyName); }

        public Supplier GetSupplier(int id)
        {
            return db.Suppliers.SingleOrDefault(supplier => supplier.SupplierID == id);
        }

        public void Add(Supplier supplier) { db.Suppliers.Add(supplier); }

        public void Delete(Supplier supplier)
        {
            db.Suppliers.Remove(supplier);
        }

        public void Save() { db.SaveChanges(); }
    }
}