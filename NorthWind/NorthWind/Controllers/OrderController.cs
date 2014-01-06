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
                    List<Order_Detail> listOrderDetails = new List<Order_Detail>();

                    foreach (EditableOrderDetail editOrdD in tuple.EditableOrderDetList)
                    {
                        Order_Detail ordD = new Order_Detail()
                        {
                            OrderID = ord.OrderID,
                            ProductID = editOrdD.ProductID,
                            UnitPrice = editOrdD.UnitPrice,
                            Quantity = editOrdD.Quantity,
                            Discount = editOrdD.Discount
                        };
                        listOrderDetails.Add(ordD);
                    }

                    foreach (Order_Detail od in listOrderDetails)
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

        public ActionResult Edit(int id)
        {
            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                Order order = orderRepository.GetOrder(id);

                EditableOrder eo = new EditableOrder(order);
                List<EditableOrderDetail> eodList = new List<EditableOrderDetail>();
                foreach(Order_Detail orD in order.Order_Details){
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

        [HttpPost]
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

                    OrderDetailRepository orderDetailRepository = new OrderDetailRepository(dao);
                    List<Order_Detail> odList = new List<Order_Detail>();

                    foreach (EditableOrderDetail edtOd in tuple.EditableOrderDetList)
                    {
                        Order_Detail odTmp = orderDetailRepository.GetOrderDetail(tuple.EditableOrder.OrderID, edtOd.ProductID);
                        if (odTmp != null)
                        {
                            odTmp.ProductID = edtOd.ProductID;
                            odTmp.UnitPrice = edtOd.UnitPrice;
                            odTmp.Quantity = edtOd.Quantity;
                            odTmp.Discount = edtOd.Discount;
                            odList.Add(odTmp);
                            orderDetailRepository.Save();
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
                            odList.Add(odTmpBis);
                            orderDetailRepository.Add(odTmpBis);
                            orderDetailRepository.Save();
                        }
                    }
                    foreach (Order_Detail o in orderRepository.GetOrder(tuple.EditableOrder.OrderID).Order_Details)
                    {
                        if (!odList.Contains(o))
                        {
                            orderDetailRepository.Delete(o);
                            orderDetailRepository.Save();
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(tuple);
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


        public ActionResult ExportToExcel()
        {
            System.Diagnostics.Debug.WriteLine("export ");


            var orders = new System.Data.DataTable("orders");
            orders.Columns.Add("OrderID", typeof(int));
            orders.Columns.Add("CustomerID", typeof(string));
            orders.Columns.Add("EmployeeID", typeof(int));

            orders.Columns.Add("OrderDate", typeof(DateTime));
            orders.Columns.Add("RequiredDate", typeof(DateTime));
            orders.Columns.Add("ShippedDate", typeof(DateTime));

            orders.Columns.Add("ShipVia", typeof(int));
            orders.Columns.Add("Freight", typeof(decimal));
            
            orders.Columns.Add("ShipName", typeof(string));
            orders.Columns.Add("ShipAddress", typeof(string));
            orders.Columns.Add("ShipCity", typeof(string));
            orders.Columns.Add("ShipRegion", typeof(string));
            orders.Columns.Add("ShipPostalCode", typeof(string));
            orders.Columns.Add("ShipCountry", typeof(string));

            using (var dao = new Entities())
            {
                OrderRepository orderRepository = new OrderRepository(dao);
                int i = 1;
                foreach (Order order in orderRepository.FindAllOrders().Where(order => order.OrderID > 11074))
                {

                    try
                    {
                        orders.Rows.Add(i, order.OrderID);
                        orders.Columns.Add(order.CustomerID);
                        orders.Columns.Add("" + order.EmployeeID);
                        orders.Columns.Add("" + order.OrderDate);
                        orders.Columns.Add("" + order.RequiredDate);
                        orders.Columns.Add("" + order.ShippedDate);
                        orders.Columns.Add("" + order.ShipVia);
                        orders.Columns.Add("" + order.Freight);
                        orders.Columns.Add(order.ShipName);
                        orders.Columns.Add(order.ShipAddress);
                        orders.Columns.Add(order.ShipCity);
                        orders.Columns.Add(order.ShipRegion);
                        orders.Columns.Add(order.ShipPostalCode);
                        orders.Columns.Add(order.ShipCountry);
                        i++;
                    }
                    catch (System.Data.DuplicateNameException)
                    {
                        
                    }


                    
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


            /* var products = new System.Data.DataTable("teste");
            products.Columns.Add("col1", typeof(int));
            products.Columns.Add("col2", typeof(string));

            products.Rows.Add(1, "product 1");
            products.Rows.Add(2, "product 2");
            products.Rows.Add(3, "product 3");
            products.Rows.Add(4, "product 4");
            products.Rows.Add(5, "product 5");
            products.Rows.Add(6, "product 6");
            products.Rows.Add(7, "product 7");


            var grid = new GridView();
            grid.DataSource = products;
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
            Response.End();*/

            return RedirectToAction("Index");
        }

    }
}
