using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PassionProjUditesh.Models;
using PassionProjUditesh.Models.ViewModels;

namespace PassionProjUditesh.Controllers
{
    public class OrderController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static OrderController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44394/api/");
        }
        // GET: Order/List
        public ActionResult List()
        {
            //objective: Communicate with our Order data api to retrieve a list of Orders
            //curl https://localhost:44394/api/orderdata/listorders
            string url = "orderdata/listorders";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The response code is : ");
            //Debug.WriteLine(response.StatusCode);
            IEnumerable<OrderDto> orders = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;
            return View(orders);
        }

        // GET: Order/Details/2
        public ActionResult Details(int id)
        {
            // Objective: To communicate with the Customer data API to retrieve one Customer
            //curl https://localhost:44394/api/orderdata/findorder/2

            string url = "orderdata/findorder/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            OrderDto order = response.Content.ReadAsAsync<OrderDto>().Result;
            return View(order);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Order/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            // Objective : Add a new Order into our system using the API
            //curl -d @Order.json -H "Content-type:application/json" https://localhost:44394/api/orderdata/addorder

            string jsonpayload = jss.Serialize(order);
            string url = "orderdata/addorder";
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

        // GET: Order/Update/5
        public ActionResult Update(int id)
        {
            UpdateOrder ViewModel = new UpdateOrder();
            string url = "orderdata/findorder/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            OrderDto order = response.Content.ReadAsAsync<OrderDto>().Result;
            ViewModel.SelectedOrder = order;

            url = "customerdata/listcustomers/";
            response = client.GetAsync(url).Result;
            IEnumerable<CustomerDto> CustomerOptions = response.Content.ReadAsAsync<IEnumerable<CustomerDto>>().Result;
            ViewModel.CustomerOptions = CustomerOptions;

            url = "sectordata/listsectors/";
            response = client.GetAsync(url).Result;
            IEnumerable<SectorDto> SectorOptions = response.Content.ReadAsAsync<IEnumerable<SectorDto>>().Result;
            ViewModel.SectorOptions = SectorOptions;

            return View(ViewModel);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Order order)
        {
            //Objective : Add a new order into our system using the API
            //curl -d @Order.json -H "Content-type:application/json" https://localhost:44394/api/orderdata/updateorder/5
            string url = "orderdata/updateorder/" + id;

            string jsonpayload = jss.Serialize(order);

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

        // GET: Order/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "orderdata/findorder/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            Order order = response.Content.ReadAsAsync<Order>().Result;

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Order order)
        {
            string url = "orderdata/deleteorder/" + id;

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
