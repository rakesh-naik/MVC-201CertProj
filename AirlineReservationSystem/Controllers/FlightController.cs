using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirlineReservationSystem.Controllers
{
    
    public class FlightController : Controller
    {
        // GET: Booking
        public ActionResult Search()
        {
            return View();
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
    }
}