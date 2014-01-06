using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Models
{
    public class EditableOrderDetail
    {
        [Display(Name = "Numéro de la commande")]
        public int OrderID { get; set; }

        [Display(Name = "Employé")]
        public int ProductID { get; set; }

        [Display(Name = "Prix Unitaire")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Quantité")]
        public short Quantity { get; set; }

        [Display(Name = "Remise")]
        public float Discount { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public List<SelectListItem> Products
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                using (var dao = new Entities())
                {
                    ProductRepository productRepository = new ProductRepository(dao);
                    List<Product> p = productRepository.FindAllProducts().ToList();
                    foreach (Product prod in p)
                    {
                        itemList.Add(new SelectListItem { Text = prod.ProductName, Value = prod.ProductID.ToString() });
                    }
                    return itemList;
                }
            }
        }

        public EditableOrderDetail() { }

        public EditableOrderDetail(Order_Detail orD) {
            this.OrderID = orD.OrderID;
            this.ProductID = orD.ProductID;
            this.UnitPrice = orD.UnitPrice;
            this.Quantity = orD.Quantity;
            this.Discount = orD.Discount;
        }
    }
}