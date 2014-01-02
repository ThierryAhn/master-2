using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Commande/

        public ActionResult Index()
        {
            using (var dao = new Entities())
            {

                OrderRepository orderRepository = new OrderRepository(dao);
                List<Order> orders = orderRepository.FindAllOrders().ToList();

                return View(orders);
            }

        }


        // Order details

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
                    order.ShipCountry = order.ShipCountry;

                    orderRepository.Add(order);
                    orderRepository.Save();

                    List<Order> orders = orderRepository.FindAllOrders().ToList();
                    return RedirectToAction("Index", orders);
                }
                else
                {
                    return View(edtOrder);
                }
            }
        }

        public ActionResult AllOrdersSortedByDate() {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                List<Order> orders = orderRepository.FindAllOrders().ToList();

                var ords = (from order in orders orderby order.OrderDate descending select order).ToList();

                return View(ords);
            }
        }

        public ActionResult NextOrders()
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                List<Order> orders = orderRepository.FindAllOrders().ToList();

                var ords = (from order in orders where order.OrderDate >= DateTime.Now && order.OrderDate <= DateTime.Now.AddDays(7) orderby order.OrderDate ascending select order).ToList();

                return View(ords);
            }
        }

        public ActionResult ViewByClient()
            {
            using (var dao = new Entities())
            {
                 OrderRepository orderRepository = new OrderRepository(dao);
                List<Order> orders = orderRepository.FindAllOrders().ToList();

                var ords = (from order in orders  orderby order.CustomerID ascending select order).ToList();

                return View(ords);
            }
        }
    }
}
