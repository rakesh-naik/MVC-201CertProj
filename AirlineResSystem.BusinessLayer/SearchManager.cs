using AirlineResSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineResSystem.BusinessLayer
{
    public class SearchManager
    {
        public Dictionary<string, string> GetCountryList()
        {
            var dbctx = new AirlineReservationEntities();
            var countryList = new Dictionary<string, string>();

            var tblData = (from cou in dbctx.Countries
                           orderby cou.country_name ascending
                           select cou);
            foreach (var country in tblData)
            {
                if (!countryList.ContainsKey(country.country_code))
                    countryList.Add(country.country_code, country.country_name);
            }

            return countryList;
        }

        public Dictionary<string, string> GetCountryList(bool AddSelectItem)
        {
            var dbctx = new AirlineReservationEntities();
            var countryList = new Dictionary<string, string>();

            var tblData = (from cou in dbctx.Countries
                           orderby cou.country_name ascending
                           select cou);
            if (tblData.Count() > 0)
            {
                countryList.Add("", "-- Select --");

            }
            foreach (var country in tblData)
            {
                if (!countryList.ContainsKey(country.country_code))
                    countryList.Add(country.country_code, country.country_name);
            }

            return countryList;
        }

        public Dictionary<string, string> GetCitiesOfCountry(string countryCode)
        {
            var dbctx = new AirlineReservationEntities();
            var cityList = new Dictionary<string, string>();

            var tblData = (from city in dbctx.Cities
                           where city.country_code == countryCode
                           orderby city.city_name ascending
                           select city);
            foreach (var city in tblData)
            {
                if (!cityList.ContainsKey(city.city_id.ToString()))
                {
                    string displayName = string.Format("{0} - {1}", city.city_name, city.airport_code);
                    cityList.Add(city.city_id.ToString(), displayName);
                }
            }

            return cityList;
        }
    }
}
