using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            using (var dao = new Entities())
            {

                ProductRepository productRepository = new ProductRepository(dao);
                List<Product> products = productRepository.FindAllProducts().ToList();

                return View(products);
            }
        }

        public ActionResult Create()
        {
            return View(new EditableProduct());
        }

        [HttpPost]
        public ActionResult Create(EditableProduct editableProduct)
        {
            /* System.Diagnostics.Debug.WriteLine("Create "); */
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    ProductRepository productRepository = new ProductRepository(dao);
                    Product product = new Product();

                    product.ProductName = editableProduct.ProductName;
                    product.SupplierID = editableProduct.SupplierID;
                    product.CategoryID = editableProduct.CategoryID;
                    product.QuantityPerUnit = editableProduct.QuantityPerUnit;
                    product.UnitPrice = editableProduct.UnitPrice;
                    product.UnitsInStock = editableProduct.UnitsInStock;
                    product.UnitsOnOrder = editableProduct.UnitsOnOrder;
                    product.ReorderLevel = editableProduct.ReorderLevel;
                    product.Discontinued = editableProduct.Discontinued;

                    productRepository.Add(product);
                    productRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editableProduct);
                }
            }

            
        }

    }
}
