using AirlineResSystem.BusinessLayer;
using AirlineResSystem.BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservationSystem.Controllers
{

    public class FlightController : Controller
    {
        #region ActionMethods
        // GET: Booking
        public ActionResult Search()
        {
            return View(new FlightSearch());
        }

        [HttpPost]
        public ActionResult Search(FlightSearch searchParam)
        {
            return View(searchParam);
        }

        [Authorize]
        public ActionResult Book()
        {
            return View();
        }

        [Authorize]
        public ActionResult CancelTicket()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CancelTicket(FormCollection formData)
        {
            return View();
        }

        public JsonResult GetCityList(string countryId)
        {
            try
            {
                SearchManager srch = new SearchManager();
                var cities = srch.GetCitiesOfCountry(countryId);
                if (cities != null && cities.Count > 0)
                {
                    return Json(new SelectList(cities, "Key", "Value"), JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion

        #region HelperMethods
        [NonAction]
        public Dictionary<string, string> GetCountryList()
        {
            SearchManager srch = new SearchManager();
            var countries = srch.GetCountryList();
            if (countries != null && countries.Count > 0)
                return countries;
            return new Dictionary<string, string>();
        }

        #endregion
    }
}