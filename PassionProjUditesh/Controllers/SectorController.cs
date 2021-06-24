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
    public class SectorController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static SectorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44394/api/orderdata/");
        }
        // GET: Sector/List
        public ActionResult List()
        {
            //objective: Communicate with our Sector data api to retrieve a list of Sectors
            //curl https://localhost:44324/api/orderdata/listsectors
            string url = "listsectors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine("The response code is : ");
            //Debug.WriteLine(response.StatusCode);
            IEnumerable<SectorDto> sectors = response.Content.ReadAsAsync<IEnumerable<SectorDto>>().Result;
            return View(sectors);
        }

        // GET: Sector/Details/5
        public ActionResult Details(int id)
        {
            // Objective: To communicate with the Sector data API to retrieve one Sector
            //curl https://localhost:44343/api/orderdata/findsector/2

            string url = "findsector/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            SectorDto sectorDto = response.Content.ReadAsAsync<SectorDto>().Result;
            return View(sectorDto);
        }

        // GET: Sector/Create
        /*public ActionResult Create()
        {
            return View();
        }

        // POST: Sector/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sector/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sector/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sector/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sector/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/
    }
}
