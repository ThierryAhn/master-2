using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class SupplierController : Controller
    {
        //
        // GET: /Supplier/

        public ActionResult Index(int? page)
        {
            using (var dao = new Entities())
            {

                SupplierRepository supplierRepository = new SupplierRepository(dao);
                
                const int pageSize = 10;
                var upcomingSuppliers = supplierRepository.FindAllSuppliers();

                var paginatedSuppliers = new PaginatedList<Supplier>(upcomingSuppliers, page ?? 0, pageSize);

                ViewBag.HasPreviousPage = paginatedSuppliers.HasPreviousPage;
                ViewBag.HasNextPage = paginatedSuppliers.HasNextPage;
                ViewBag.PageIndex = (page ?? 0);

                return View(paginatedSuppliers);
            }
        }

        public ActionResult Details(int id)
        {
            using (var dao = new Entities())
            {
                SupplierRepository supplierRepository = new SupplierRepository(dao);
                Supplier supplier = supplierRepository.GetSupplier(id);
                return View(supplier);
            }
        }

        // GET Create Supplier
        public ActionResult Create()
        {
            return View(new EditableSupplier());
        }

        // GET Create Supplier
        [HttpPost]
        public ActionResult Create(EditableSupplier editableSupplier)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    SupplierRepository supplierRepository = new SupplierRepository(dao);
                    Supplier supplier = new Supplier();

                    supplier.CompanyName = editableSupplier.CompanyName;
                    supplier.ContactName = editableSupplier.ContactName;
                    supplier.ContactTitle = editableSupplier.ContactTitle;
                    supplier.Address = editableSupplier.Address;
                    supplier.City = editableSupplier.City;
                    supplier.Region = editableSupplier.Region;
                    supplier.PostalCode = editableSupplier.PostalCode;
                    supplier.Country = editableSupplier.Country;
                    supplier.Phone = editableSupplier.Phone;
                    supplier.Fax = editableSupplier.Fax;
                    supplier.HomePage = editableSupplier.HomePage;

                    supplierRepository.Add(supplier);
                    supplierRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editableSupplier);
                }
            }
        }


        // GET Edit Supplier
        public ActionResult Edit(int id)
        {
            using (var dao = new Entities())
            {
                SupplierRepository supplierRepository = new SupplierRepository(dao);
                Supplier supplier = supplierRepository.GetSupplier(id);

                EditableSupplier editableSupplier = new EditableSupplier(supplier);

                return View(editableSupplier);
            }
        }

        // POST Edit Supplier
        [HttpPost]
        public ActionResult Edit(EditableSupplier editableSupplier)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    SupplierRepository supplierRepository = new SupplierRepository(dao);

                    Supplier supplier = supplierRepository.GetSupplier(editableSupplier.SupplierID);

                    supplier.CompanyName = editableSupplier.CompanyName;
                    supplier.ContactName = editableSupplier.ContactName;
                    supplier.ContactTitle = editableSupplier.ContactTitle;
                    supplier.Address = editableSupplier.Address;
                    supplier.City = editableSupplier.City;
                    supplier.Region = editableSupplier.Region;
                    supplier.PostalCode = editableSupplier.PostalCode;
                    supplier.Country = editableSupplier.Country;
                    supplier.Phone = editableSupplier.Phone;
                    supplier.Fax = editableSupplier.Fax;
                    supplier.HomePage = editableSupplier.HomePage;

                    UpdateModel(supplier);

                    supplierRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editableSupplier);
                }
            }
        }


        // GET Delete Product
        public ActionResult Delete(int id)
        {
            using (var dao = new Entities())
            {
                SupplierRepository supplierRepository = new SupplierRepository(dao);
                Supplier supplier = supplierRepository.GetSupplier(id);

                if (supplier == null)
                {
                    return HttpNotFound();
                }

                return View(supplier);
            }
        }

        // POST Delete Product
        [HttpPost]
        public ActionResult Delete(int id, String action)
        {
            using (var dao = new Entities())
            {
                SupplierRepository supplierRepository = new SupplierRepository(dao);
                Supplier supplier = supplierRepository.GetSupplier(id);

                if (supplier == null)
                {
                    return HttpNotFound();
                }

                supplierRepository.Delete(supplier);
                supplierRepository.Save();
                
                return RedirectToAction("Index");
            }
        }
    }
}
