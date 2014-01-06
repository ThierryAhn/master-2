using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Models
{
    public class EditableEmployee
    {

        public int EmployeeID { get; set; }
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [MinLength(2, ErrorMessage = "Prenom au minimum 2 caractères")]
        [MaxLength(10, ErrorMessage = "Prenom au maximum 10 caractères")]
        [Display(Name = "Prenom")]
        public string FirstName { get; set; }
        [Display(Name = "Poste")]
        public string Title { get; set; }
        [Display(Name = "Civilité")]
        public string TitleOfCourtesy { get; set; }
        [Display(Name = "Date de naissance")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Date d'embauche")]
        public DateTime? HireDate { get; set; }
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
        [Display(Name = "Téléphone domicile")]
        public string HomePhone { get; set; }
        [MaxLength(4, ErrorMessage = "Extension au maximum 4 caractères")]
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }
        [Display(Name = "URL de la photo")]
        public string PhotoPath { get; set; }

        public EditableEmployee()
        {
        }

        public EditableEmployee(Employee employee)
        {
            this.EmployeeID = employee.EmployeeID;
            this.LastName = employee.LastName;
            this.FirstName = employee.FirstName;
            this.Title = employee.Title;
            this.TitleOfCourtesy = employee.TitleOfCourtesy;
            this.BirthDate = employee.BirthDate;
            this.HireDate = employee.HireDate;
            this.Address = employee.Address;
            this.City = employee.City;
            this.Region = employee.Region;
            this.PostalCode = employee.PostalCode;
            this.Country = employee.Country;
            this.HomePhone = employee.HomePhone;
            this.Extension = employee.Extension;
            this.Photo = employee.Photo;
            this.Notes = employee.Notes;
            this.ReportsTo = employee.ReportsTo;
            this.PhotoPath = employee.PhotoPath;
        }

    }
}