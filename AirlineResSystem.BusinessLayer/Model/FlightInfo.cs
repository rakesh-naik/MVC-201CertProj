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
        public string SourceCity { get; set; }
        public string DestCity { get; set; }
        public string Route { get; set; }
        public string Class { get; set; }
        public string Fare { get; set; }
        public DateTime DepDateTime { get; set; }
        public string ArrivalDateTime { get; set; }
        public string Status { get; set; }
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

    }
}
