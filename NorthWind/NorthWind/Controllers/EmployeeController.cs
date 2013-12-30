using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            using (var dao = new Entities())
            {
                EmployeeRepository employeeRepository = new EmployeeRepository(dao);
                Employee employee = employeeRepository.GetEmployee(id);
                return View(employee);
            }
        }


        public ActionResult GetImage(int id)
        {
            using (var dao = new Entities())
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    EmployeeRepository employeeRepository = new EmployeeRepository(dao);
                    Employee employee = employeeRepository.GetEmployee(id);
                    var image = employee.Photo;
                    // 78 is the size of the OLE header for Northwind images
                    ms.Write(image, 78, image.Length - 78);

                    return File(ms.ToArray(), "image/jpeg");
                }
            }
        }
    }
}
