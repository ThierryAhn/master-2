using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class ShipperController : Controller
    {
        //
        // GET: /Shipper/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            using (var dao = new Entities())
            {
                ShipperRepository shipperRepository = new ShipperRepository(dao);
                Shipper shipper = shipperRepository.GetShipper(id);

                //liste order a ajouter
                //ViewData["customerName"] = order.Employee.FirstName + " " + order.Employee.LastName;
                
                return View(shipper);
            }
        }

    }
}
