using PassionProjUditesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Script.Serialization;
using PassionProjUditesh.Models.ViewModels;

namespace PassionProjUditesh.Controllers
{
    public class EmployeeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static EmployeeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44394/api/");
        }
        // GET: Employee/List
        public ActionResult List()
        {
            //objective: Communicate with our Employee data api to retrieve a list of Employees
            //curl https://localhost:44394/api/employeedata/listemployees
            string url = "employeedata/listemployees";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The response code is : ");
            //Debug.WriteLine(response.StatusCode);
            IEnumerable<EmployeeDto> employees = response.Content.ReadAsAsync<IEnumerable<EmployeeDto>>().Result;
            return View(employees);
        }

        // GET: Employee/Details/2
        public ActionResult Details(int id)
        {
            // Objective: To communicate with the Employee data API to retrieve one Employee
            //curl https://localhost:44343/api/employeedata/findemployee/2

            string url = "employeedata/findemployee/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            EmployeeDto employee = response.Content.ReadAsAsync<EmployeeDto>().Result;
            return View(employee);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Employee/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            // Objective : Add a new Employee into our system using the API
            //curl -d @Employee.json -H "Content-type:application/json" https://localhost:44343/api/employeedata/addemployee

            string jsonpayload = jss.Serialize(employee);
            string url = "employeedata/addemployee";
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Employee/Update/5
        public ActionResult Update(int id)
        {
            UpdateEmployee ViewModel = new UpdateEmployee();
            string url = "employeedata/findemployee/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            EmployeeDto employee = response.Content.ReadAsAsync<EmployeeDto>().Result;
            ViewModel.SelectedEmployee = employee;

            url = "sectordata/listsectors/";
            response = client.GetAsync(url).Result;
            IEnumerable<SectorDto> SectorOptions = response.Content.ReadAsAsync<IEnumerable<SectorDto>>().Result;
            ViewModel.SectorOptions = SectorOptions;

            return View(ViewModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee employee)
        {
            
            //Objective : Add a new employee into our system using the API
            //curl -d @Employee.json -H "Content-type:application/json" https://localhost:44343/api/employeedata/updateemployee/5
            string url = "employeedata/updateemployee/" + id;

            string jsonpayload = jss.Serialize(employee);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Employee/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "employeedata/findemployee/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Employee employee = response.Content.ReadAsAsync<Employee>().Result;

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            string url = "employeedata/deleteemployee/" + id;

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
