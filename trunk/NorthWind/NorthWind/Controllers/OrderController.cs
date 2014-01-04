using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class OrderController : Controller
    {

        public ActionResult Index(int? page)
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);

                const int pageSize = 10;
                var upcomingOrders = orderRepository.FindAllOrders().OrderBy(ord => ord.OrderID);
                var paginatedOrders = new PaginatedList<Order>(upcomingOrders, page ?? 0, pageSize);
                ViewBag.HasPreviousPage = paginatedOrders.HasPreviousPage;
                ViewBag.HasNextPage = paginatedOrders.HasNextPage;
                ViewBag.PageIndex = (page ?? 0);

                return View(paginatedOrders);
            }

        }

        public ActionResult Details(int id)
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                Order order = orderRepository.GetOrder(id);

                ViewData["customerName"] = order.Employee.FirstName + " " + order.Employee.LastName;
                ViewData["clientName"] = order.Customer.CompanyName;
                ViewData["phone"] = order.Shipper.Phone;
                ViewData["shipVia"] = order.Shipper.CompanyName;

                return View(order);
            }
        }

        public ActionResult Create()
        {
            return View(new EditableOrder());
        }

        [HttpPost]
        public ActionResult Create(EditableOrder edtOrder)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    OrderRepository orderRepository = new OrderRepository(dao);
                    Order order = new Order();

                    order.CustomerID = edtOrder.CustomerID;
                    order.EmployeeID = edtOrder.EmployeeID;
                    order.OrderDate = edtOrder.OrderDate;
                    order.RequiredDate = edtOrder.RequiredDate;
                    order.ShippedDate = edtOrder.ShippedDate;
                    order.ShipVia = edtOrder.ShipVia;
                    order.Freight = edtOrder.Freight;
                    order.ShipName = edtOrder.ShipName;
                    order.ShipAddress = edtOrder.ShipAddress;
                    order.ShipCity = edtOrder.ShipCity;
                    order.ShipRegion = edtOrder.ShipRegion;
                    order.ShipPostalCode = edtOrder.ShipPostalCode;
                    order.ShipCountry = edtOrder.ShipCountry;

                    System.Diagnostics.Debug.Write("CustomerID: " + order.CustomerID + "\n"
                                + "EmployeeID: " + order.EmployeeID + "\n"
                                + "OrderDate: " + order.OrderDate + "\n"
                                + "RequiredDate: " + order.RequiredDate + "\n"
                                + "ShippedDate: " + order.ShippedDate + "\n"
                                + "ShipVia: " + order.ShipVia + "\n"
                                + "Freight: " + order.Freight + "\n"
                                + "ShipName: " + order.ShipName + "\n"
                                + "ShipAddress: " + order.ShipAddress + "\n"
                                + "ShipCity: " + order.ShipCity + "\n"
                                + "ShipRegion: " + order.ShipRegion + "\n"
                                + "ShipPostalCode: " + order.ShipPostalCode + "\n"
                                + "ShipCountry: " + order.ShipCountry + "\n");

                    orderRepository.Add(order);
                    orderRepository.Save();
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(edtOrder);
                }
            }
        }

        public ActionResult Edit(int id)
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                Order order = orderRepository.GetOrder(id);
                EditableOrder editableorder = new EditableOrder(order);
                return View(editableorder);
            }
        }

        public ActionResult AllOrdersSortedByDate(int? page)
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);

                const int pageSize = 10;
                var upcomingOrders = orderRepository.FindAllOrders().OrderByDescending(ord => ord.OrderDate);
                var paginatedOrders = new PaginatedList<Order>(upcomingOrders, page ?? 0, pageSize);
                ViewBag.HasPreviousPage = paginatedOrders.HasPreviousPage;
                ViewBag.HasNextPage = paginatedOrders.HasNextPage;
                ViewBag.PageIndex = (page ?? 0);

                return View(paginatedOrders);
            }
        }

        public ActionResult NextOrders(int? page)
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);

                const int pageSize = 10;
                var upcomingOrders = orderRepository.FindAllOrders().Where(order => order.OrderDate >= DateTime.Now)
                                                                    .Where(order => order.OrderDate <= EntityFunctions.AddDays(DateTime.Now, 7))
                                                                    .OrderBy(ord => ord.OrderDate);

                var paginatedOrders = new PaginatedList<Order>(upcomingOrders, page ?? 0, pageSize);
                ViewBag.HasPreviousPage = paginatedOrders.HasPreviousPage;
                ViewBag.HasNextPage = paginatedOrders.HasNextPage;
                ViewBag.PageIndex = (page ?? 0);
                return View(paginatedOrders);
            }
        }

        public ActionResult ViewByClient()
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                List<Order> orders = orderRepository.FindAllOrders().ToList();

                var ords = (from order in orders orderby order.CustomerID ascending select order).ToList();

                return View(ords);
            }
        }

        public ActionResult ViewByEmployee()
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                List<Order> orders = orderRepository.FindAllOrders().ToList();

                var ords = (from order in orders orderby order.EmployeeID ascending select order).ToList();

                return View(ords);
            }
        }
        
        //public ActionResult CalendarView()
        //{
        //    return View();
        //}

        //public JsonResult GetEvents(double start, double end)
        //{
        //    using (var dao = new Entities())
        //    {
        //        var events = new List<CalendarEvent>();
        //        var dtstart = ConvertFromUnixTimestamp(start);
        //        var dtend = ConvertFromUnixTimestamp(end);

        //        OrderRepository orderRepository = new OrderRepository(dao);
        //        List<Order> ol = orderRepository.FindAllOrders().ToList();
        //        var onCalls = from p in ol
        //                      select new { p.OrderID, p.ShippedDate, p.Customer.CompanyName };

        //        DateTime currStart;
        //        DateTime currEnd;
        //        foreach (var p in onCalls)
        //        {
        //            currStart = Convert.ToDateTime(p.ShippedDate);
        //            currEnd = Convert.ToDateTime(p.ShippedDate);
        //            events.Add(new CalendarEvent()
        //            {
        //                id = p.OrderID.ToString(),
        //                title = p.CompanyName,
        //                start = p.ShippedDate.ToString(),
        //                end = p.ShippedDate.ToString(),
        //                url = "/Order/Details/" + p.OrderID
        //            });
        //        }
        //        var rows = events.ToArray();
        //        return Json(rows, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //private static DateTime ConvertFromUnixTimestamp(double timetamp)
        //{
        //    var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //    return origin.AddSeconds(timetamp);
        //}
    }
}
