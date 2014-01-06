using NorthWind.Models;
using System;
using System.Collections;
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

        public ActionResult Index(int? page)
        {
            using (var dao = new Entities())
            {

                CustomerRepository customerRepository = new CustomerRepository(dao);

                const int pageSize = 4;
                var upcomingCustomers = customerRepository.FindAllCustomers();

                var paginatedCustomers = new PaginatedList<Customer>(upcomingCustomers, page ?? 0, pageSize);

                ViewBag.HasPreviousPage = paginatedCustomers.HasPreviousPage;
                ViewBag.HasNextPage = paginatedCustomers.HasNextPage;
                ViewBag.PageIndex = (page ?? 0);

                // on ne récupère que le nom et le id des suppliers
                Dictionary<string, Dictionary<Dictionary<string, int>, int>> supplierOfCustomer = 
                        new Dictionary<string, Dictionary<Dictionary<string, int>, int>>();
                
                foreach (Customer customer in customerRepository.FindAllCustomers())
                {
                    
                    Dictionary<Dictionary<string, int>, int> suppliersWithNumberOfProducts = new Dictionary<Dictionary<string, int>, int>();
                    foreach(Supplier supplier in customerRepository.GetSupplierOfCustomer(customer.CustomerID))
                    {


                        Dictionary<string, int> supplierNameWithId = new Dictionary<string, int>();
                        supplierNameWithId.Add(supplier.CompanyName, supplier.SupplierID);

                        if (suppliersWithNumberOfProducts.ContainsKey(supplierNameWithId))
                        {
                            suppliersWithNumberOfProducts[supplierNameWithId] = suppliersWithNumberOfProducts[supplierNameWithId] + 1;
                        }
                        else
                        {
                            suppliersWithNumberOfProducts.Add(supplierNameWithId, supplier.Products.Count());
                        }
                        
                    }
                    
                    supplierOfCustomer.Add(customer.CustomerID, suppliersWithNumberOfProducts);
                }
                
                ViewBag.supplierOfCustomer = supplierOfCustomer;

                return View(paginatedCustomers);
            }
        }

        // Details
        public ActionResult Details(String id)
        {
            using (var dao = new Entities())
            {
                CustomerRepository customerRepository = new CustomerRepository(dao);
                Customer customer = customerRepository.GetCustomer(id);
                return View(customer);
            }
        }


        // GET Create Customer
        public ActionResult Create()
        {
            return View(new EditableCustomer());
        }

        // POST Create Customer
        [HttpPost]
        public ActionResult Create(EditableCustomer editableCustomer)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    CustomerRepository customerRepository = new CustomerRepository(dao);
                    Customer customer = new Customer();


                    // génération aléatoire de l'identifiant
                    string id = customerRepository.FindAllCustomers().ToList().First().CustomerID;
                    do
                    {
                        Random random = new Random();
                        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        char[] buffer = new char[5];
                        for (int i = 0; i < 5; i++)
                        {
                            buffer[i] = chars[random.Next(chars.Length)];
                        }
                        id = new string(buffer);
                        System.Diagnostics.Debug.WriteLine("id int: " + id);
                    } while (customerRepository.GetCustomer(id) != null);



                    customer.CustomerID = id;
                    customer.CompanyName = editableCustomer.CompanyName;
                    customer.ContactName = editableCustomer.ContactName;
                    customer.ContactTitle = editableCustomer.ContactTitle;
                    customer.Address = editableCustomer.Address;
                    customer.City = editableCustomer.City;
                    customer.Region = editableCustomer.Region;
                    customer.PostalCode = editableCustomer.PostalCode;
                    customer.Country = editableCustomer.Country;
                    customer.Phone = editableCustomer.Phone;
                    customer.Fax = editableCustomer.Fax;

                    customerRepository.Add(customer);
                    customerRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editableCustomer);
                }
            }
        }

        // GET Edit Customer
        public ActionResult Edit(string id)
        {
            using (var dao = new Entities())
            {
                CustomerRepository customerRepository = new CustomerRepository(dao);
                Customer customer = customerRepository.GetCustomer(id);

                EditableCustomer editableCustomer = new EditableCustomer(customer);

                return View(editableCustomer);
            }
        }

        // POST Edit Customer
        [HttpPost]
        public ActionResult Edit(EditableCustomer editableCustomer)
        {
            using (var dao = new Entities())
            {
                if (ModelState.IsValid)
                {
                    CustomerRepository customerRepository = new CustomerRepository(dao);
                    Customer customer = customerRepository.GetCustomer(editableCustomer.CustomerID);

                    customer.CompanyName = editableCustomer.CompanyName;
                    customer.ContactName = editableCustomer.ContactName;
                    customer.ContactTitle = editableCustomer.ContactTitle;
                    customer.Address = editableCustomer.Address;
                    customer.City = editableCustomer.City;
                    customer.Region = editableCustomer.Region;
                    customer.PostalCode = editableCustomer.PostalCode;
                    customer.Country = editableCustomer.Country;
                    customer.Phone = editableCustomer.Phone;
                    customer.Fax = editableCustomer.Fax;

                    UpdateModel(customer);

                    customerRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(editableCustomer);
                }
            }
        }


        // GET Delete Customer
        public ActionResult Delete(String id)
        {
            using (var dao = new Entities())
            {
                CustomerRepository customerRepository = new CustomerRepository(dao);
                Customer customer = customerRepository.GetCustomer(id);

                if (customer == null)
                {
                    return HttpNotFound();
                }

                return View(customer);
            }
        }

        // POST Delete Customer
        [HttpPost]
        public ActionResult Delete(String id, String action)
        {
            using (var dao = new Entities())
            {
                CustomerRepository customerRepository = new CustomerRepository(dao);
                Customer customer = customerRepository.GetCustomer(id);

                if (customer == null)
                {
                    return HttpNotFound();
                }

                customerRepository.Delete(customer);
                customerRepository.Save();

                return RedirectToAction("Index");
            }
        }

    }
}
