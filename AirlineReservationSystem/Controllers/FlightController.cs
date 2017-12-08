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
            if (ModelState.IsValid)
            {
                SearchManager srchMgr = new SearchManager();
                searchParam.SearchResults = srchMgr.SearchFlights(Convert.ToInt16(searchParam.SourceCity), Convert.ToInt16(searchParam.DestinationCity));
            }
            return View(searchParam);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Book(int id, int fareId)
        {
            FlightInfo flgtDetails = new FlightInfo();
            try
            {
                SearchManager srchMgr = new SearchManager();
                flgtDetails = srchMgr.GetFlightInfo(id, fareId);
                return View(flgtDetails);
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult Book(FormCollection postedForm)
        {
            if (Session["user"] == null)
            { RedirectToAction("Login", "Account"); }

            if (postedForm != null && postedForm.Count > 0)
            {
                int selectdSch = int.Parse(postedForm["ScheduleId"]);
                int fare = int.Parse(postedForm["SelectedFare.FareId"]);
                BookingManager book = new BookingManager();
                if (book.BookTickets(selectdSch, fare, ((UserDetailDO)Session["user"]).CustomerId))
                    return RedirectToAction("MyBooking");
                else
                {
                    TempData.Add("Error", "Failed to book ticket. Please try later");
                    return RedirectToAction("Book", new { id = selectdSch, fareId = fare });
                }

            }
            return View("Search", new FlightSearch());
        }

        [Authorize]
        public ActionResult MyBooking()
        {
            List<FlightInfo> myBookings = new List<FlightInfo>();
            try
            {
                UserManager mgr = new UserManager();
                myBookings = mgr.GetBookingOfUser(((UserDetailDO)Session["user"]).CustomerId);
            }
            catch (Exception exc)
            {

                throw;
            }
            return View(myBookings);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CancelTkt(FormCollection formData)
        {
            try
            {
                string tktId = formData[0];
                if (string.IsNullOrEmpty(tktId))
                { TempData.Add("Error", "Could not cancel tickets"); }
                else
                {
                    BookingManager bookMgr = new BookingManager();
                    if (!bookMgr.CancelTickets(tktId))
                    { TempData.Add("Error", "Could not cancel tickets"); }
                }
                return RedirectToAction("MyBooking");
            }
            catch (Exception e)
            { throw e; }
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

        public JsonResult GetFare(int fareId)
        {
            try
            {
                SearchManager srch = new SearchManager();
                Fare fareObj = srch.GetFarebyId(fareId);
                if (fareObj != null && fareObj.Cost > 0)
                {
                    return Json(fareObj.Cost.ToString("#0.00"), JsonRequestBehavior.AllowGet);
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