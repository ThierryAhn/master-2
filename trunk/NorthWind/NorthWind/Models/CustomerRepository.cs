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
            return db.Customers;
        }

        public void Add(Customer customer)
        {
        }

        public void Delete(Customer customer)
        {
        }

        public void Save() { db.SaveChanges(); }
    }
}