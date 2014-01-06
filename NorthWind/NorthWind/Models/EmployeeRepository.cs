using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class EmployeeRepository
    {
        private Entities db;

        public EmployeeRepository(Entities entities) { this.db = entities; }

        public Employee GetEmployee(int id)
        {
            return db.Employees.SingleOrDefault(employee => employee.EmployeeID == id);
        }

        public IQueryable<Employee> FindAllEmployees()
        {
            return db.Employees.Include("Orders").OrderBy(employee => employee.LastName);
        }

        public void Add(Employee employee)
        {
            db.Employees.Add(employee);
        }

        public void Delete(Employee employee)
        {
            db.Employees.Remove(employee);
        }

        public void Save() { db.SaveChanges(); }
    }
}