using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class AirportArrivalController : Controller
    {
        private Model1 db = new Model1();

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("AirportsArrivals").Result;
            var responseContent = await response.Content.ReadAsStringAsync();
            IEnumerable<AirportsArrival> airport = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GlobalVariables.WebApiClient.BaseAddress.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var Employees = JsonConvert.DeserializeObject<IEnumerable<AirportsArrival>>(responseContent);
                    return View(Employees);
                }
                else
                {
                    airport = Enumerable.Empty<AirportsArrival>();
                    ModelState.AddModelError(string.Empty, "Ошибка сервера");
                }

                return View(airport);
            }
        }

        public ActionResult Create()
        {
            return View(new AirportsArrival());
        }
        [HttpPost]
        public ActionResult Create(AirportsArrival airportsArrival)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVariables.WebApiClient.BaseAddress.ToString());

                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("AirportsArrivals", airportsArrival).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ошибка сервера");
                    }
                }
            }
            return View("Index");
        }

        public ActionResult Edit(string id)
        {
            
            if (id == null)
            {
                return View(new AirportsArrival());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync($"AirportsArrivals/{id}").Result;
                var p = response.Content.ReadAsAsync<AirportsArrival>().Result;
                return View(p);
            }
        }

        [HttpPost]
        public ActionResult Edit(AirportsArrival airportsArrival)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync($"AirportsArrivals/{airportsArrival.FlightNumber}", airportsArrival).Result;

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync($"AirportsArrivals/{id}").Result;
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
