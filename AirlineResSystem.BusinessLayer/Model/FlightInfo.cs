using AirlineResSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AirlineResSystem.BusinessLayer.Model
{
    public class FlightInfo
    {
        [Display(Name = "From City")]
        public string SourceCity { get; set; }
        [Display(Name = "To City")]
        public string DestCity { get; set; }
        public string Route { get; set; }
        public string GetRouteInfo
        {
            get
            {
                if (string.IsNullOrEmpty(this.Route))
                    return "Direct";

                return Route;
            }
        }
        public Fare SelectedFare { get; set; }

        public List<Fare> AvailableFares { get; set; }

        [Display(Name = "Departure")]
        public DateTime DepDateTime { get; set; }
        [Display(Name = "Arrival")]
        public DateTime ArrivalDateTime { get; set; }
        public string Status { get; set; }

        public int ScheduleId { get; set; }
        public int JourneyId { get; set; }
        [Display(Name = "Ticket Number")]
        public string TicketId { get; set; }

        public string DisplayDepDate
        {
            get
            { return DepDateTime.ToString("dd-MMM-yy HH:mm"); }
        }

        public Dictionary<string, string> Farelist { get; set; }

        public string DisplayArrvlDate
        {
            get
            { return ArrivalDateTime.ToString("dd-MMM-yy HH:mm"); }
        }
    }

    public class FlightSearch
    {
        [Required(ErrorMessage = " *")]
        [Display(Name = "Country")]
        public string SourceCountry { get; set; }
        [Required(ErrorMessage = " *")]
        [Display(Name = "Country")]
        public string DestCountry { get; set; }
        [Required(ErrorMessage = " *")]
        [Display(Name = "City")]
        public int? SourceCity { get; set; }
        [Required(ErrorMessage = " *")]
        [Display(Name = "City")]
        public int? DestinationCity { get; set; }

        public List<FlightInfo> SearchResults { get; set; }

        public FlightInfo SelectedFlight { get; set; }

    }

    public class Fare
    {
        public int FareId { get; set; }
        [Display(Name = "Fare")]
        public decimal Cost { get; set; }

        public string ClassCode { get; set; }
        [Display(Name = "Selected Class")]
        public string ClassDesc { get; set; }

        public void DeriveFareDesc()
        {

            switch (ClassCode.ToUpper())
            {
                case "EK":
                    ClassDesc = "Economy";
                    break;
                case "BS":
                    ClassDesc = "Business";
                    break;
                case "FR":
                    ClassDesc = "First Class";
                    break;
                default:
                    break;
            }


        }
    }
}
