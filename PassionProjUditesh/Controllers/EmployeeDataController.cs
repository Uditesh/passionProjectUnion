using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProjUditesh.Models;

namespace PassionProjUditesh.Controllers
{
    public class EmployeeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EmployeeData/ListEmployees
        [HttpGet]
        [ResponseType(typeof(EmployeeDto))]
        public IEnumerable<EmployeeDto> ListEmployees()
        {
            List<Employee> Employees = db.Employees.ToList();
            List<EmployeeDto> EmployeeDtos = new List<EmployeeDto>();

            Employees.ForEach(e => EmployeeDtos.Add(new EmployeeDto()
            {
                EmployeeID = e.EmployeeID,
                EmployeeName = e.EmployeeName,
                EmployeeMobNum = e.EmployeeMobNum,
                EmployeeEmail = e.EmployeeEmail,
                EmployeeAdrress = e.EmployeeAdrress,
                SectorName = e.Sector.SectorName
            }));

            return EmployeeDtos;
        }

        // GET: api/EmployeeData/FindEmployee/5
        [ResponseType(typeof(EmployeeDto))]
        [HttpGet]
        public IHttpActionResult FindEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            EmployeeDto EmplDto = new EmployeeDto()
            {
                EmployeeID = employee.EmployeeID,
                EmployeeName = employee.EmployeeName,
                EmployeeMobNum = employee.EmployeeMobNum,
                EmployeeEmail = employee.EmployeeEmail,
                EmployeeAdrress = employee.EmployeeAdrress,
                SectorName = employee.Sector.SectorName
            };
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(EmplDto);
        }

        // POST: api/EmployeeData/UpdateEmployee/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeeData/AddEmployee
        [ResponseType(typeof(Employee))]
        [HttpPost]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
        }

        // POST: api/EmployeeData/DeleteEmployee/5
        [ResponseType(typeof(Employee))]
        [HttpPost]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}