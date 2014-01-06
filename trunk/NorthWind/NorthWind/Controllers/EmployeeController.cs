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


                    /* var fileName = Path.GetFileName(file.FileName);
                    byte[] buf = new byte[file.ContentLength];*/
                    
                    /* var file = Request.Files["photoPath"];
                   
                    byte[] buf = null;
                   
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        buf = ms.GetBuffer();
                    }*/


                    var file = Request.Files["photoPath"];

                    byte[] buf = null;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        buf = ms.GetBuffer();
                        
                    }


                    /* byte[] buf = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        buf = binaryReader.ReadBytes(file.ContentLength);
                    }


                    string myHex = String.Format("0x{0:X}", buf);
                    //buf = System.Text.Encoding.ASCII.GetBytes(myHex);


                    buf = System.Text.UTF8Encoding.ASCII.GetBytes(myHex);*/


                    /* using (MemoryStream ms = new MemoryStream())
                    {
                        FileStream files = new FileStream("C:\\Users\\Folabi\\Documents\\Bluetooth Folder\\IMG_20140105_170756.jpg", FileMode.Open, FileAccess.Read);
                        buf = new byte[files.Length];
                        files.Read(buf, 0, (int)files.Length);
                        ms.Write(buf, 0, (int)files.Length);
                    }*/


                    /* byte[] buf = new byte[] { };
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        buf = binaryReader.ReadBytes(file.ContentLength);
                    }*/
                    
                    /* FileStream buf = new FileStream("C:\\Users\\Folabi\\Documents\\Bluetooth Folder\\IMG_20140105_170756.jpg", FileMode.Open, FileAccess.Read); //create a file stream object associate to user selected file 
                    byte[] img = new byte[buf.Length]; //create a byte array with size of user select file stream length
                    buf.Read(img, 0, Convert.ToInt32(buf.Length));//read user selected file stream in to byte array*/


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
                    employee.Photo = buf;
                    employee.Notes = editableEmployee.Notes;
                    employee.ReportsTo = editableEmployee.ReportsTo;
                    
                    employee.PhotoPath = "test";


                    /* System.Diagnostics.Debug.WriteLine("employee.LastName " + employee.LastName);
                    System.Diagnostics.Debug.WriteLine("employee.FirstName " + employee.FirstName);
                    System.Diagnostics.Debug.WriteLine("employee.Title " + employee.Title);
                    System.Diagnostics.Debug.WriteLine("employee.TitleOfCourtesy " + employee.TitleOfCourtesy);
                    System.Diagnostics.Debug.WriteLine("employee.BirthDate " + employee.BirthDate);
                    System.Diagnostics.Debug.WriteLine("employee.HireDate " + employee.HireDate);
                    System.Diagnostics.Debug.WriteLine("employee.Address " + employee.Address);

                    System.Diagnostics.Debug.WriteLine("employee.City " + employee.City);
                    System.Diagnostics.Debug.WriteLine("employee.Region " + employee.Region);
                    System.Diagnostics.Debug.WriteLine("employee.PostalCode " + employee.PostalCode);

                    System.Diagnostics.Debug.WriteLine("employee.Country " + employee.Country);
                    System.Diagnostics.Debug.WriteLine("employee.HomePhone " + employee.HomePhone);
                    System.Diagnostics.Debug.WriteLine("employee.Extension " + employee.Extension);
                    System.Diagnostics.Debug.WriteLine("employee.Notes " + employee.Notes);

                    System.Diagnostics.Debug.WriteLine("employee.Photo " + employee.Photo);
                    System.Diagnostics.Debug.WriteLine("employee.ReportsTo " + employee.ReportsTo);
                    System.Diagnostics.Debug.WriteLine("employee.PhotoPath " + employee.PhotoPath);*/
                    
                    employeeRepository.Add(employee);
                    employeeRepository.Save();


                    //System.Diagnostics.Debug.WriteLine("photo " + String.Format("{0,10:X}", buf));

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
                    var image = employee.Photo;

                    // 78 is the size of the OLE header for Northwind images
                    ms.Write(image, 78, image.Length - 78);
                    return File(ms.ToArray(), "image/jpeg");


                    //ms.Write(image, 0, image.Length - 0);

                    //System.Diagnostics.Debug.WriteLine("image : " + id + " --> " + ms.Length);
                    //return File(ms.ToArray(), "image/jpeg", string.Format("{0}.jpeg", id));
                }
            }
        }
    }
}
