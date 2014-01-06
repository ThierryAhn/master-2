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

        public ActionResult Index(int? page)
        {
            using (var dao = new Entities())
            {

                ProductRepository productRepository = new ProductRepository(dao);
                
                const int pageSize = 10;
                var upcomingProducts = productRepository.FindAllProducts();
                
                var paginatedProducts = new PaginatedList<Product>(upcomingProducts, page ?? 0, pageSize);

                ViewBag.HasPreviousPage = paginatedProducts.HasPreviousPage;
                ViewBag.HasNextPage = paginatedProducts.HasNextPage;
                ViewBag.PageIndex = (page ?? 0);
                
                return View(paginatedProducts);
            }
        }

        // GET Create Product
        [Authorize]
        public ActionResult Create()
        {
            return View(new EditableProduct());
        }

        // POST Create Product
        [AcceptVerbs(HttpVerbs.Post), Authorize]
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

        // GET Edit Product
        [Authorize]
        public ActionResult Edit(int id)
        {
            using (var dao = new Entities())
            {
                ProductRepository productRepository = new ProductRepository(dao);
                Product product = productRepository.GetProduct(id);

                EditableProduct editableProduct = new EditableProduct(product);

                return View(editableProduct);
            }
        }

        // POST Edit Product
        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Edit(EditableProduct editableProduct)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    ProductRepository productRepository = new ProductRepository(dao);

                    Product product = productRepository.GetProduct(editableProduct.ProductID);

                    product.ProductName = editableProduct.ProductName;
                    product.SupplierID = editableProduct.SupplierID;
                    product.CategoryID = editableProduct.CategoryID;
                    product.QuantityPerUnit = editableProduct.QuantityPerUnit;
                    product.UnitPrice = editableProduct.UnitPrice;
                    product.UnitsInStock = editableProduct.UnitsInStock;
                    product.UnitsOnOrder = editableProduct.UnitsOnOrder;
                    product.ReorderLevel = editableProduct.ReorderLevel;
                    product.Discontinued = editableProduct.Discontinued;

                    UpdateModel(product);
                    productRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editableProduct);
                }
            }
        }

        // GET Delete Product
        [Authorize]
        public ActionResult Delete(int id)
        {
            using (var dao = new Entities())
            {
                ProductRepository productRepository = new ProductRepository(dao);
                Product product = productRepository.GetProduct(id);

                if (product == null)
                {
                    return HttpNotFound();
                }

                return View(product);
            }
        }

        // POST Delete Product
        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Delete(int id, String action)
        {
            using (var dao = new Entities())
            {
                ProductRepository productRepository = new ProductRepository(dao);
                Product product = productRepository.GetProduct(id);

                if (product == null)
                {
                    return HttpNotFound();
                }

                productRepository.Delete(product);
                productRepository.Save();
                return RedirectToAction("Index");
            }
        }

    }
}
