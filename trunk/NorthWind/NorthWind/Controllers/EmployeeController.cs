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

        public ActionResult Index(int? page)
        {
            using (var dao = new Entities())
            {

                EmployeeRepository employeeRepository = new EmployeeRepository(dao);

                const int pageSize = 10;
                var upcomingEmployees = employeeRepository.FindAllEmployees();

                var paginatedEmployees = new PaginatedList<Employee>(upcomingEmployees, page ?? 0, pageSize);

                ViewBag.HasPreviousPage = paginatedEmployees.HasPreviousPage;
                ViewBag.HasNextPage = paginatedEmployees.HasNextPage;
                ViewBag.PageIndex = (page ?? 0);

                return View(paginatedEmployees);
            }
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


        // GET Create Employee
        public ActionResult Create()
        {
            return View(new EditableEmployee());
        }

        // GET Create Supplier
        [HttpPost]
        public ActionResult Create(EditableEmployee editableEmployee)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    var file = Request.Files["photoPath"];
                    //byte[] buf = new byte[file.ContentLength];

                    var fileName = Path.GetFileName(file.FileName);


                    /* using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }*/



                    System.Diagnostics.Debug.WriteLine("photo " + fileName);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editableEmployee);
                }
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
