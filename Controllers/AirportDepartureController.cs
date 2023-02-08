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
    public class AirportDepartureController : Controller
    {
        private Model1 db = new Model1();

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("AirportsDepartures").Result;
            var responseContent = await response.Content.ReadAsStringAsync();
            IEnumerable<AirportsDeparture> airport = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GlobalVariables.WebApiClient.BaseAddress.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var Employees = JsonConvert.DeserializeObject<IEnumerable<AirportsDeparture>>(responseContent);
                    return View(Employees);
                }
                else
                {
                    airport = Enumerable.Empty<AirportsDeparture>();
                    ModelState.AddModelError(string.Empty, "Ошибка сервера");
                }

                return View(airport);
            }
        }

        public ActionResult Create()
        {
            return View(new AirportsDeparture());
        }
        [HttpPost]
        public ActionResult Create(AirportsDeparture airportsDeparture)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVariables.WebApiClient.BaseAddress.ToString());

                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("AirportsDepartures", airportsDeparture).Result;
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
                return View(new AirportsDeparture());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync($"AirportsDepartures/{id}").Result;
                var p = response.Content.ReadAsAsync<AirportsDeparture>().Result;
                return View(p);
            }
        }

        [HttpPost]
        public ActionResult Edit(AirportsDeparture airportsDeparture)
        {
            
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync($"AirportsDepartures/{airportsDeparture.FlightNumber}", airportsDeparture).Result;
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
                AirportsDeparture airportsDeparture = db.AirportsDeparture.Find(id);
                //HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync($"AirportsDepartures/{id}").Result;
                db.Entry(airportsDeparture)
                .Collection(c => c.Passengers)
                .Load();

                db.AirportsDeparture.Remove(airportsDeparture);
                db.SaveChanges();

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
