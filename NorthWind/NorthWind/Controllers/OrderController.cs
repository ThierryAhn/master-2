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
            DateTime now = DateTime.Now;
            int today = (int)now.DayOfWeek;
            int diffNowMonday = (7 - (today - 1)) % 7;
            DateTime nextMonday = now.AddDays(diffNowMonday);
            EditableOrder eo = new EditableOrder();
            List<EditableOrderDetail> eodList = new List<EditableOrderDetail>();
            TupleOrder tuple = new TupleOrder()
            {
                EditableOrder = eo,
                EditableOrderDetList = eodList,
            };
            ViewData["nextMonday"] = nextMonday.ToShortDateString();
            return View(tuple);
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

        [HttpPost]
        public ActionResult Edit(EditableOrder edtOrder)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    OrderRepository orderRepository = new OrderRepository(dao);
                    Order order = orderRepository.GetOrder(edtOrder.OrderID);
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
                    UpdateModel(order);
                    orderRepository.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(edtOrder);
                }
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

        // GET Delete Product
        public ActionResult Delete(int id)
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                Order order = orderRepository.GetOrder(id);

                if (order == null)
                {
                    return HttpNotFound();
                }

                return View(order);
            }
        }

        // POST Delete Product
        [HttpPost]
        public ActionResult Delete(int id, String action)
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                Order order = orderRepository.GetOrder(id);

                if (order == null)
                {
                    return HttpNotFound();
                }

                orderRepository.Delete(order);
                orderRepository.Save();

                return RedirectToAction("Index");
            }
        }

        public PartialViewResult AddOrderDetailLine()
        {
            EditableOrderDetail eod = new EditableOrderDetail();
            return PartialView("OrderDetailLine", eod);
        }

    }
}
