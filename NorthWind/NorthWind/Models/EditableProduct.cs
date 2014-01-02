using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Models
{
    public class EditableProduct
    {

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public List<SelectListItem> Suppliers
        {
            get
            {
                using (var dao = new Entities())
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    foreach (Supplier supplier in dao.Suppliers)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = supplier.CompanyName,
                            Value = supplier.SupplierID.ToString()
                        });
                    }
                    return items;
                }
            }
        }

        public List<SelectListItem> Categories
        {
            get
            {
                using (var dao = new Entities())
                {
                    List<SelectListItem> items = new List<SelectListItem>();
                    foreach (Category category in dao.Categories)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = category.CategoryName,
                            Value = category.CategoryID.ToString()
                        });
                    }
                    return items;
                }
            }
        }

        public EditableProduct()
        {
        }

        public EditableProduct(Product product)
        {
            this.ProductID = product.ProductID;
            this.ProductName = product.ProductName;
            this.SupplierID = product.SupplierID;
            this.CategoryID = product.CategoryID;
            this.QuantityPerUnit = product.QuantityPerUnit;
            this.UnitPrice = product.UnitPrice;
            this.UnitsInStock = product.UnitsInStock;
            this.UnitsOnOrder = product.UnitsOnOrder;
            this.ReorderLevel = product.ReorderLevel;
            this.Discontinued = product.Discontinued;
        }
        
    }
}