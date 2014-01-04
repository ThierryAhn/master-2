using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class EditableCustomer
    {
        [Display(Name = "Identifiant")]
        public string CustomerID { get; set; }
        [Display(Name = "Nom de la compagnie")]
        public string CompanyName { get; set; }
        [Display(Name = "Nom du contact")]
        public string ContactName { get; set; }
        [Display(Name = "Poste")]
        public string ContactTitle { get; set; }
        [Display(Name = "Adresse")]
        public string Address { get; set; }
        [Display(Name = "Ville")]
        public string City { get; set; }
        [Display(Name = "Region")]
        public string Region { get; set; }
        [Display(Name = "Code postal")]
        public string PostalCode { get; set; }
        [Display(Name = "Pays")]
        public string Country { get; set; }
        [Display(Name = "Téléphone")]
        public string Phone { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        public EditableCustomer()
        {
        }

        public EditableCustomer(Customer customer)
        {
            this.CustomerID = customer.CustomerID;
            this.CompanyName = customer.CompanyName;
            this.ContactName = customer.ContactName;
            this.ContactTitle = customer.ContactTitle;
            this.Address = customer.Address;
            this.City = customer.City;
            this.Region = customer.Region;
            this.PostalCode = customer.PostalCode;
            this.Country = customer.Country;
            this.Phone = customer.Phone;
            this.Fax = customer.Fax;
        }
    }
}