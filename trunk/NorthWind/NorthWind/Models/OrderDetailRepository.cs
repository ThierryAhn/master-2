using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class OrderDetailRepository
    {
        private Entities db;

        public OrderDetailRepository(Entities entities) { this.db = entities; }

        public IQueryable<Order_Detail> FindAllOrder_Details() { return db.Order_Details.Include("Product").OrderBy(m => m.OrderID); }

        public void Save() { db.SaveChanges(); }

        public void Add(Order_Detail od) { db.Order_Details.Add(od); }

        public void Delete(Order_Detail od)
        {
            db.Order_Details.Remove(od);
        }

        public Order_Detail GetOrderDetail(int order, int product){
            return db.Order_Details.SingleOrDefault(ord => ord.OrderID == order && ord.ProductID == product);
        }

    }
}