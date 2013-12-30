using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Details(String id)
        {
            using (var dao = new Entities())
            {
                CustomerRepository customerRepository = new CustomerRepository(dao);
                Customer customer = customerRepository.GetCustomer(id);
                return View(customer);
            }
        }


    }
}
