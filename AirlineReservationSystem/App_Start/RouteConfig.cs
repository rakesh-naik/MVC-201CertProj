using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AirlineReservationSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "MyBookings",
                url: "My/Bookings",
                defaults: new { controller = "Flight", action = "MyBooking" }
            );
            routes.MapRoute(
                name: "BookConfirmation",
                url: "Flight/Book/{id}/{fareId}",
                defaults: new { controller = "Flight", action = "Book" }
                );


            routes.MapRoute(
                name: "CancelTicket",
                url: "Ticket/Cancel",
                defaults: new { controller = "Flight", action = "CancelTicket" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Flight", action = "Search", id = UrlParameter.Optional }
            );
        }
    }
}
