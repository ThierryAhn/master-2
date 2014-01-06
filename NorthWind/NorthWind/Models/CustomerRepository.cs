using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class CustomerRepository
    {
        private Entities db;

        public CustomerRepository(Entities entities) { this.db = entities; }

        public Customer GetCustomer(string id)
        {
            return db.Customers.SingleOrDefault(customer => customer.CustomerID == id);
        }

        public IQueryable<Customer> FindAllCustomers()
        {
            return db.Customers.Include("Orders").OrderBy(customer => customer.CompanyName);
        }

        public List<Supplier> GetSupplierOfCustomer(string id)
        {

            List<Supplier> SuppliersWithCustomer = (from customer in db.Customers 
                                         where customer.CustomerID.Equals(id)
                                        join order in db.Orders on customer.CustomerID equals order.CustomerID
                                        join order_detail in db.Order_Details on order.OrderID equals order_detail.OrderID
                                        join product in db.Products on order_detail.ProductID equals product.ProductID
                                        join supplier in db.Suppliers on product.SupplierID equals supplier.SupplierID
                                        select supplier).ToList();

            return SuppliersWithCustomer;
        }

        public void Add(Customer customer)
        {
            db.Customers.Add(customer);
        }

        public void Delete(Customer customer)
        {
            db.Customers.Remove(customer);
        }

        public void Save() { db.SaveChanges(); }
    }
}