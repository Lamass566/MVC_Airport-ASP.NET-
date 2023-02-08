using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class PassengersController : Controller
    {
        private Model1 db = new Model1();
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Passenger").Result;
            var responseContent = await response.Content.ReadAsStringAsync();
            IEnumerable<Passengers> airport = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GlobalVariables.WebApiClient.BaseAddress.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var Employees = JsonConvert.DeserializeObject<IEnumerable<Passengers>>(responseContent);
                    Employees = db.Passengers.Include(d => d.AirportsDeparture);
                    return View(Employees);
                }
                else
                {
                    airport = Enumerable.Empty<Passengers>();
                    ModelState.AddModelError(string.Empty, "Ошибка сервера");
                }
                
                return View(airport);
            }
        }

        public ActionResult Create()
        {
            ViewBag.FlightN = new SelectList(db.AirportsDeparture, "FlightNumber", "City_Departure");
            return View(new Passengers());
        }
        [HttpPost]
        public ActionResult Create(Passengers passengers)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVariables.WebApiClient.BaseAddress.ToString());

                    HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Passenger", passengers).Result;
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

        public ActionResult Edit(int? id)
        {
            Passengers passengers = db.Passengers.Find(id);
            if (id == 0)
            {
                return View(new Passengers());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync($"Passenger/{id}").Result;
                ViewBag.FlightN = new SelectList(db.AirportsDeparture, "FlightNumber", "City_Departure", passengers.FlightN);
                var p = response.Content.ReadAsAsync<Passengers>().Result;
                return View(p);
            }
        }

        [HttpPost]
        public ActionResult Edit(Passengers passengers)
        {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync($"Passenger/{passengers.Id_pass}", passengers).Result;
                ViewBag.FlightN = new SelectList(db.AirportsDeparture, "FlightNumber", "City_Departure", passengers.FlightN);
           
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync($"Passenger/{id}").Result;
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
