using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {

        private Model1 db = new Model1();
        //[Authorize]
        public ActionResult Index()
        {
            var model = new ModelForView();
            model.AirportsDeparture = db.AirportsDeparture.ToList();
            model.AirportsArrival = db.AirportsArrival.ToList();
            model.Passengers = db.Passengers.ToList();
            ViewBag.SearchKey = "";

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(string nameToFind)
        {
            var model = new ModelForView();
            model.AirportsDeparture = db.AirportsDeparture.ToList();
            model.AirportsArrival = db.AirportsArrival.ToList();
            ViewBag.SearchKey = nameToFind;

            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("Action", actionName);
            dict.Add("Пользователь", HttpContext.User.Identity.Name);
            dict.Add("Аутентифицирован?", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Тип аутентификации", HttpContext.User.Identity.AuthenticationType);
            dict.Add("В роли Users?", HttpContext.User.IsInRole("Users"));

            return dict;
        }
    }
}