using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Models
{
    public class EditableSupplier
    {
        public int SupplierID { get; set; }
        [Display(Name = "Nom de la compagnie")]
        public string CompanyName { get; set; }
        [Display(Name = "Nom du contact")]
        public string ContactName { get; set; }
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
        [Display(Name = "Site web")]
        public string HomePage { get; set; }


        public List<SelectListItem> Contacts
        {
            get
            {
                using (var dao = new Entities())
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    foreach (Contact contact in dao.Contacts)
                    {
                        if (contact.ContactName != null)
                        {
                            items.Add(new SelectListItem
                            {
                                Text = contact.ContactName,
                                Value = contact.ContactName
                            });
                        }
                        
                    }
                    return items;
                }
            }
        }

        public EditableSupplier()
        {
        }

        public EditableSupplier(Supplier supplier)
        {
            this.SupplierID = supplier.SupplierID;
            this.CompanyName = supplier.CompanyName;
            this.ContactName = supplier.ContactName;
            this.ContactTitle = supplier.ContactTitle;
            this.Address = supplier.Address;
            this.City = supplier.City;
            this.Region = supplier.Region;
            this.PostalCode = supplier.PostalCode;
            this.Country = supplier.Country;
            this.Phone = supplier.Phone;
            this.Fax = supplier.Fax;
            this.HomePage = supplier.HomePage;
        }
    }
}