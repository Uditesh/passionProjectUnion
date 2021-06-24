using PassionProjUditesh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProjUditesh.Controllers
{
    public class CustomerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static CustomerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44394/api/customerdata/");
        }

        // GET: Customer/List
        public ActionResult List()
        {
            //objective: Communicate with our Customer data api to retrieve a list of Customers
            //curl https://localhost:44324/api/customerdata/listcustomers
            string url = "listcustomers";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The response code is : ");
            //Debug.WriteLine(response.StatusCode);
            IEnumerable<Customer> customers = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;
            return View(customers);
        }

        // GET: Customer/Details/2
        public ActionResult Details(int id)
        {
            // Objective: To communicate with the Customer data API to retrieve one Customer
            //curl https://localhost:44343/api/customerdata/findcustomer/2

            string url = "findcustomer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Customer customer = response.Content.ReadAsAsync<Customer>().Result;
            return View(customer);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Customer/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Customer/New
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            // Objective : Add a new customer into our system using the API
            //curl -d @Customer.json -H "Content-type:application/json" https://localhost:44343/api/customerdata/addcustomer

            string jsonpayload = jss.Serialize(customer);
            string url = "addcustomer";
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

        // GET: Customer/Update/5
        public ActionResult Update(int id)
        {
            string url = "findcustomer/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Customer customer = response.Content.ReadAsAsync<Customer>().Result;

            return View(customer);
        }

        // POST: Customer/Update/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            //Objective : Add a new customer into our system using the API
            //curl -H "Content-type:application/json" -d @Customer.json https://localhost:44343/api/customerdata/updatecustomer/5
            string url = "updatecustomer/" + id;

            string jsonpayload = jss.Serialize(customer);

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

        // GET: Customer/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findcustomer/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Customer customer = response.Content.ReadAsAsync<Customer>().Result;

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "deletecustomer/" + id;

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
