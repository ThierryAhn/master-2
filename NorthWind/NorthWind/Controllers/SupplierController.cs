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

        public ActionResult Index()
        {
            return View();
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
