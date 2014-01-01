﻿using NorthWind.Models;
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
            using (var dao = new Entities())
            {
                List<SelectListItem> listCustomers = new List<SelectListItem>();
                CustomerRepository customerRepository = new CustomerRepository(dao);
                List<Customer> customers = customerRepository.FindAllCustomers().ToList();
                foreach (Customer customer in customers)
                {
                    listCustomers.Add(new SelectListItem() { Text = customer.CompanyName, Value = customer.CustomerID });
                }

                List<SelectListItem> listEmployees = new List<SelectListItem>();
                EmployeeRepository employeeRepository = new EmployeeRepository(dao);
                List<Employee> employees = employeeRepository.FindAllEmployees().ToList();
                foreach (Employee employee in employees)
                {
                    listEmployees.Add(new SelectListItem() { Text = (employee.FirstName + " " + employee.LastName) , Value = employee.EmployeeID.ToString() });
                }

                List<SelectListItem> listShippers = new List<SelectListItem>();
                ShipperRepository shipperRepository = new ShipperRepository(dao);
                List<Shipper> shippers = shipperRepository.FindAllShippers().ToList();
                foreach (Shipper shipper in shippers)
                {
                    listShippers.Add(new SelectListItem() { Text = shipper.CompanyName , Value = shipper.ShipperID.ToString() });
                }

                ViewData["Customers"] = listCustomers;
                ViewData["Employees"] = listEmployees;
                ViewData["Shippers"] = listShippers;
            }
            return View();
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
                    //order.OrderID = edtOrder.OrderID;
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
    }
}
