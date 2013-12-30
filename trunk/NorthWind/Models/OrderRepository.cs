using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class OrderRepository
    {
        private Entities db;

        public OrderRepository(Entities entities) { this.db = entities; }

        public IQueryable<Order> FindAllOrders() { return db.Orders.Include("Customer").Include("Employee").Include("Shipper").Include("Order_Details"); }

        public IQueryable<Order> FindUpcomingOrders()
        {
            return from order in db.Orders
                   where order.OrderDate > DateTime.Now
                   orderby order.OrderDate
                   select order;
        }


        public Order GetOrder(int id) {
            return db.Orders.SingleOrDefault(order => order.OrderID == id);
        }

        public void Add(Order order) { db.Orders.Add(order); }

        public void Delete(Order order)
        {
            //db.Orders.de
        }
        public void Save() { db.SaveChanges(); }
    }
}