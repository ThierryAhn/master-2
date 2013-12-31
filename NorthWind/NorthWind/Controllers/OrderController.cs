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

    }
}
