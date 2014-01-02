using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class ProductRepository
    {
        private Entities db;

        public ProductRepository(Entities entities) { this.db = entities; }

        public IQueryable<Product> FindAllProducts() { return db.Products.Include("Category").Include("Supplier").OrderBy(product => product.ProductName); }

        public Product GetProduct(int id)
        {
            return db.Products.SingleOrDefault(product => product.ProductID == id);
        }

        public void Add(Product product) { db.Products.Add(product); }

        public void Delete(Product product)
        {
            db.Products.Remove(product);
        }

        public void Save() { db.SaveChanges(); }
    }
}