using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Models
{
    public class EditableOrder
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public System.DateTime? OrderDate { get; set; }
        public System.DateTime? RequiredDate { get; set; }
        public System.DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public List<SelectListItem> Customers
        {
            get
            {
                List<SelectListItem> listCustomer = new List<SelectListItem>();
                using (var dao = new Entities())
                {

                    CustomerRepository customerRepository = new CustomerRepository(dao);
                    List<Customer> customers = customerRepository.FindAllCustomers().ToList();
                    foreach(Customer customer in customers)
                    {
                        listCustomer.Add(new SelectListItem() { Text = customer.CustomerID, Value = customer.CompanyName });
                    }
                          
                };
                return listCustomer;
            }
        }


        public EditableOrder(Order order)
        {
            this.OrderID = order.OrderID;
            this.CustomerID = order.CustomerID;
            this.EmployeeID = order.EmployeeID;
            this.OrderDate = order.OrderDate;
            this.RequiredDate = order.RequiredDate;
            this.RequiredDate = order.RequiredDate;
            this.ShippedDate = order.RequiredDate;
            this.ShipVia = order.ShipVia;
            this.Freight = order.Freight;
            this.ShipName = order.ShipName;
            this.ShipAddress = order.ShipAddress;
            this.ShipCity = order.ShipCity;
            this.ShipRegion = order.ShipRegion;
            this.ShipPostalCode = order.ShipPostalCode;
            this.ShipCountry = order.ShipCountry;
        }
    }
}