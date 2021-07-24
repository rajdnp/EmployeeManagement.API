using EmployeeManagement.API.DatabaseContext;
using EmployeeManagement.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase // employee controller to handle employee operations.
    {

        // injecting db context for the controller.
        private readonly EmployeeDbContext dbContext;

        public EmployeeController(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        ///   To create an employee
        /// </summary>
        /// <param name="model"></param>
        /// <returns> if success will return an employee who got created.</returns>
        [Route("add")]
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            dbContext.Employees.Add(model);
            dbContext.SaveChanges();
            return Ok(model);
        }

        /// <summary>
        ///  To get all the employees
        /// </summary>
        /// <returns>list of employees</returns>
        [Route("all")]
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            var employees = dbContext.Employees.ToList();
            return Ok(employees);
        }

        /// <summary>
        ///  To update employee.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>message</returns>
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateEmployee(int id, Employee model)
        {
            var employee = dbContext.Employees.Find(id);
            employee.Name = model.Name;
            dbContext.SaveChanges();
            return Ok(new { Message = "Employee updated successfully"});
        }


        /// <summary>
        ///  To delete an employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Message</returns>
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = dbContext.Employees.Find(id);
            dbContext.Remove(employee);
            dbContext.SaveChanges();
            return Ok(new { message = "Delete successfully." });
        }
    }
}
