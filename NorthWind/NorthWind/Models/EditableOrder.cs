using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Models
{
    public class EditableOrder
    {
        public int OrderID { get; set; }
        [Display(Name = "Customer")]
        public string CustomerID { get; set; }
        [Display(Name="Employe")]
        public int? EmployeeID { get; set; }
        [Display(Name = "Order Date")]
        public System.DateTime? OrderDate { get; set; }
        [Display(Name = "Required Date")]
        public System.DateTime? RequiredDate { get; set; }
        [Display(Name = "Shipped Date")]
        public System.DateTime? ShippedDate { get; set; }
        [Display(Name = "Ship Via")]
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        [Display(Name = "Ship Name")]
        public string ShipName { get; set; }
        [Display(Name = "Ship Address")]
        public string ShipAddress { get; set; }
        [Display(Name = "Ship City")]
        public string ShipCity { get; set; }
        [Display(Name = "Ship Region")]
        public string ShipRegion { get; set; }
        [Display(Name = "Ship Postal Code")]
        public string ShipPostalCode { get; set; }
        [Display(Name = "Ship Country")]
        public string ShipCountry { get; set; }

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

        public EditableOrder(){ }

        public List<SelectListItem> Customers
        {
            get
            {
                using (var dao = new Entities())
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    foreach (Customer e in dao.Customers)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = e.CompanyName,
                            Value = e.CustomerID.ToString(),
                        });
                    }
                    return items;
                }
            }
        }

        public List<SelectListItem> Employees
        {
            get
            {
                using (var dao = new Entities())
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    foreach (Employee e in dao.Employees)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = e.FirstName + " " + e.LastName,
                            Value = e.EmployeeID.ToString(),
                        });
                    }
                    return items;
                }
            }
        }

        public List<SelectListItem> Shippers
        {
            get
            {
                using (var dao = new Entities())
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    foreach (Shipper e in dao.Shippers)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = e.CompanyName,
                            Value = e.ShipperID.ToString(),
                        });
                    }
                    return items;
                }
            }
        }
    }
}