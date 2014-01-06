using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        [Authorize]
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

        [Authorize]
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

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Create(TupleOrder tuple)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    OrderRepository orderRepository = new OrderRepository(dao);
                    Order ord = new Order()
                    {
                        OrderID = tuple.EditableOrder.OrderID,
                        CustomerID = tuple.EditableOrder.CustomerID,
                        EmployeeID = tuple.EditableOrder.EmployeeID,
                        OrderDate = tuple.EditableOrder.OrderDate,
                        RequiredDate = tuple.EditableOrder.RequiredDate,
                        ShippedDate = tuple.EditableOrder.ShippedDate,
                        ShipVia = tuple.EditableOrder.ShipVia,
                        Freight = tuple.EditableOrder.Freight,
                        ShipName = tuple.EditableOrder.ShipName,
                        ShipAddress = tuple.EditableOrder.ShipAddress,
                        ShipCity = tuple.EditableOrder.ShipCity,
                        ShipRegion = tuple.EditableOrder.ShipRegion,
                        ShipPostalCode = tuple.EditableOrder.ShipPostalCode,
                        ShipCountry = tuple.EditableOrder.ShipCountry,
                    };

                    orderRepository.Add(ord);
                    orderRepository.Save();
                    OrderDetailRepository orderDetailRepository = new OrderDetailRepository(dao);
                    List<Order_Detail> odLst = new List<Order_Detail>();

                    foreach (EditableOrderDetail edtOd in tuple.EditableOrderDetList)
                    {
                        Order_Detail odTmp = new Order_Detail()
                        {
                            OrderID = ord.OrderID,
                            ProductID = edtOd.ProductID,
                            UnitPrice = edtOd.UnitPrice,
                            Quantity = edtOd.Quantity,
                            Discount = edtOd.Discount
                        };
                        odLst.Add(odTmp);
                    }

                    foreach (Order_Detail od in odLst)
                    {
                        orderDetailRepository.Add(od);

                        orderDetailRepository.Save();
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tuple);
                }
            }
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                Order order = orderRepository.GetOrder(id);

                EditableOrder eo = new EditableOrder(order);
                List<EditableOrderDetail> eodList = new List<EditableOrderDetail>();
                foreach (Order_Detail orD in order.Order_Details)
                {
                    eodList.Add(new EditableOrderDetail(orD));
                }

                TupleOrder tuple = new TupleOrder()
                {
                    EditableOrder = eo,
                    EditableOrderDetList = eodList,
                };
                return View(tuple);
            }
        }

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Edit(TupleOrder tuple)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    OrderRepository orderRepository = new OrderRepository(dao);
                    Order ord = orderRepository.GetOrder(tuple.EditableOrder.OrderID);
                    ord.CustomerID = tuple.EditableOrder.CustomerID;
                    ord.EmployeeID = tuple.EditableOrder.EmployeeID;
                    ord.OrderDate = tuple.EditableOrder.OrderDate;
                    ord.RequiredDate = tuple.EditableOrder.RequiredDate;
                    ord.ShippedDate = tuple.EditableOrder.ShippedDate;
                    ord.ShipVia = tuple.EditableOrder.ShipVia;
                    ord.Freight = tuple.EditableOrder.Freight;
                    ord.ShipName = tuple.EditableOrder.ShipName;
                    ord.ShipAddress = tuple.EditableOrder.ShipAddress;
                    ord.ShipCity = tuple.EditableOrder.ShipCity;
                    ord.ShipRegion = tuple.EditableOrder.ShipRegion;
                    ord.ShipPostalCode = tuple.EditableOrder.ShipPostalCode;
                    ord.ShipCountry = tuple.EditableOrder.ShipCountry;

                    orderRepository.Save();
                    OrderDetailRepository order_detailRepository = new OrderDetailRepository(dao);
                    List<Order_Detail> odlist = new List<Order_Detail>();

                    foreach (EditableOrderDetail edtOd in tuple.EditableOrderDetList)
                    {
                        Order_Detail odTmp = order_detailRepository.GetOrderDetail(tuple.EditableOrder.OrderID, edtOd.ProductID);
                        if (odTmp != null)
                        {
                            odTmp.ProductID = edtOd.ProductID;
                            odTmp.UnitPrice = edtOd.UnitPrice;
                            odTmp.Quantity = edtOd.Quantity;
                            odTmp.Discount = edtOd.Discount;
                            odlist.Add(odTmp);
                            order_detailRepository.Save();
                        }
                        else
                        {
                            Order_Detail odTmpBis = new Order_Detail()
                            {
                                OrderID = ord.OrderID,
                                ProductID = edtOd.ProductID,
                                UnitPrice = edtOd.UnitPrice,
                                Quantity = edtOd.Quantity,
                                Discount = edtOd.Discount
                            };
                            odlist.Add(odTmpBis);
                            order_detailRepository.Add(odTmpBis);
                            order_detailRepository.Save();
                        }
                    }

                    foreach (Order_Detail o in orderRepository.GetOrder(tuple.EditableOrder.OrderID).Order_Details)
                    {
                        if (!odlist.Contains(o))
                        {
                            order_detailRepository.Delete(o);
                            order_detailRepository.Save();
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TupleOrder newTuple = new TupleOrder()
                    {
                        EditableOrder = tuple.EditableOrder,
                        EditableOrderDetList = tuple.EditableOrderDetList,
                    };
                    return View(newTuple);
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
        [Authorize]
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
        [AcceptVerbs(HttpVerbs.Post), Authorize]
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

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public void ExportToExcel(int[] checkBox)
        {
            List<Order> orders = new List<Order>();
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                Order order;

                foreach (int id in checkBox)
                {
                    order = orderRepository.GetOrder(id);
                    orders.Add(order);
                }
            }

            var grid = new GridView();
            grid.DataSource = orders;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }

    }
}
