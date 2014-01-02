using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class CategoryRepository
    {
        private Entities db;

        public CategoryRepository(Entities entities) { this.db = entities; }

        public IQueryable<Category> FindAllCategories() { return db.Categories; }

        public Category GetCategory(int id)
        {
            return db.Categories.SingleOrDefault(category => category.CategoryID == id);
        }

        public void Add(Category category) { db.Categories.Add(category); }

        public void Delete(Category category)
        {
            db.Categories.Remove(category);
        }

        public void Save() { db.SaveChanges(); }
    }
}