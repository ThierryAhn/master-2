using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class CalendarController : Controller
    {
        //
        // GET: /Calendar/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Calendar/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Calendar/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Calendar/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Calendar/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Calendar/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Calendar/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Calendar/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public JsonResult GetEvents(double start, double end)
        {
            using (var dao = new Entities())
            {
                var events = new List<CalendarEvent>();
                var dtstart = ConvertFromUnixTimestamp(start);
                var dtend = ConvertFromUnixTimestamp(end);

                OrderRepository orderRepository = new OrderRepository(dao);
                List<Order> ol = orderRepository.FindAllOrders().ToList();
                var onCalls = from p in ol
                              select new { p.OrderID, p.ShippedDate, p.Customer.CompanyName };

                DateTime currStart;
                DateTime currEnd;
                foreach (var p in onCalls)
                {
                    currStart = Convert.ToDateTime(p.ShippedDate);
                    currEnd = Convert.ToDateTime(p.ShippedDate);
                    events.Add(new CalendarEvent()
                    {
                        id = p.OrderID.ToString(),
                        title = p.CompanyName,
                        start = p.ShippedDate.ToString(),
                        end = p.ShippedDate.ToString(),
                        url = "/Order/Details/" + p.OrderID
                    });
                }
                var rows = events.ToArray();
                return Json(rows, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
