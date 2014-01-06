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

        [Authorize]
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
        [Authorize]
        public ActionResult Create()
        {
            return View(new EditableEmployee());
        }

        // GET Create Employee
        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Create(EditableEmployee editableEmployee)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    
                    EmployeeRepository employeeRepository = new EmployeeRepository(dao);
                    Employee employee = new Employee();

                    employee.LastName = editableEmployee.LastName;
                    employee.FirstName = editableEmployee.FirstName;
                    employee.Title = editableEmployee.Title;
                    employee.TitleOfCourtesy = editableEmployee.TitleOfCourtesy;
                    employee.BirthDate = editableEmployee.BirthDate;
                    employee.HireDate = editableEmployee.HireDate;
                    employee.Address = editableEmployee.Address;
                    employee.City = editableEmployee.City;
                    employee.Region = editableEmployee.Region;
                    employee.PostalCode = editableEmployee.PostalCode;
                    employee.Country = editableEmployee.Country;
                    employee.HomePhone = editableEmployee.HomePhone;
                    employee.Extension = editableEmployee.Extension;
                    employee.Photo = null;
                    employee.Notes = editableEmployee.Notes;
                    employee.ReportsTo = editableEmployee.ReportsTo;
                    
                    employee.PhotoPath = null;

                    employeeRepository.Add(employee);
                    employeeRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editableEmployee);
                }
            }
        }


        // GET Edit Employee
        [Authorize]
        public ActionResult Edit(int id)
        {
            using (var dao = new Entities())
            {
                EmployeeRepository employeeRepository = new EmployeeRepository(dao);
                Employee employee = employeeRepository.GetEmployee(id);

                EditableEmployee editableEmployee = new EditableEmployee(employee);

                return View(editableEmployee);
            }
        }

        // POST Edit Employee
        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Edit(EditableEmployee editableEmployee)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    EmployeeRepository employeeRepository = new EmployeeRepository(dao);
                    Employee employee = employeeRepository.GetEmployee(editableEmployee.EmployeeID);

                    employee.LastName = editableEmployee.LastName;
                    employee.FirstName = editableEmployee.FirstName;
                    employee.Title = editableEmployee.Title;
                    employee.TitleOfCourtesy = editableEmployee.TitleOfCourtesy;
                    employee.BirthDate = editableEmployee.BirthDate;
                    employee.HireDate = editableEmployee.HireDate;
                    employee.Address = editableEmployee.Address;
                    employee.City = editableEmployee.City;
                    employee.Region = editableEmployee.Region;
                    employee.PostalCode = editableEmployee.PostalCode;
                    employee.Country = editableEmployee.Country;
                    employee.HomePhone = editableEmployee.HomePhone;
                    employee.Extension = editableEmployee.Extension;
                    employee.Photo = null;
                    employee.Notes = editableEmployee.Notes;
                    employee.ReportsTo = editableEmployee.ReportsTo;
                    employee.PhotoPath = null;

                    UpdateModel(employee);

                    employeeRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editableEmployee);
                }
            }
        }



        // GET Delete Product
        public ActionResult Delete(int id)
        {
            using (var dao = new Entities())
            {
                EmployeeRepository employeeRepository = new EmployeeRepository(dao);
                Employee employee = employeeRepository.GetEmployee(id);

                if (employee == null)
                {
                    return HttpNotFound();
                }

                return View(employee);
            }
        }

        // POST Delete Product
        [HttpPost]
        public ActionResult Delete(int id, String action)
        {
            using (var dao = new Entities())
            {
                EmployeeRepository employeeRepository = new EmployeeRepository(dao);
                Employee employee = employeeRepository.GetEmployee(id);

                if (employee == null)
                {
                    return HttpNotFound();
                }

                employeeRepository.Delete(employee);
                employeeRepository.Save();

                return RedirectToAction("Index");
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

                    if (employee.Photo != null)
                    {
                        var image = employee.Photo;

                        // 78 is the size of the OLE header for Northwind images
                        ms.Write(image, 78, image.Length - 78);
                        return File(ms.ToArray(), "image/jpeg");
                    }
                    return null;
                }
            }
        }
    }
}
