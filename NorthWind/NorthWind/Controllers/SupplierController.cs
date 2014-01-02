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

    }
}
