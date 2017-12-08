using AirlineResSystem.BusinessLayer.Model;
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
            try
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
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public Dictionary<string, string> GetCountryList(bool AddSelectItem)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Dictionary<string, string> GetCitiesOfCountry(string countryCode)
        {
            try
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
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public string GetCityName(int cityId)
        {
            try
            {
                var dbctx = new AirlineReservationEntities();
                var city = (from c in dbctx.Cities
                            where c.city_id == cityId
                            select c.city_name).FirstOrDefault();
                if (!string.IsNullOrEmpty(city))
                    return city;

                return string.Empty;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public List<FlightInfo> SearchFlights(int srcCityId, int destCityId)
        {
            try
            {
                var dbctx = new AirlineReservationEntities();
                var tblData = (from city in dbctx.Cities
                               where city.city_id == srcCityId || city.city_id == destCityId
                               orderby city.city_name ascending
                               select city);
                string srcCityName, destCityName;
                srcCityName = destCityName = string.Empty;
                foreach (var city in tblData)
                {
                    if (city.city_id == srcCityId)
                        srcCityName = city.city_name;
                    else if (city.city_id == destCityId)
                        destCityName = city.city_name;
                }

                var searchResults = (from jou in dbctx.Journeys
                                     join sc in dbctx.Schedules on jou.journey_id equals sc.journey_id
                                     where jou.source == srcCityId && jou.dest == destCityId
                                     orderby sc.dep_date_time ascending
                                     select new FlightInfo
                                     {
                                         SourceCity = srcCityName,
                                         DestCity = destCityName,
                                         Route = jou.route,
                                         ArrivalDateTime = sc.arr_date_time,
                                         DepDateTime = sc.dep_date_time,
                                         ScheduleId = sc.schedule_id,
                                         JourneyId = jou.journey_id
                                     });
                if (searchResults != null && searchResults.Count() > 0)
                {
                    List<FlightInfo> lstSrchResult = new List<FlightInfo>();
                    foreach (var item in searchResults)
                    {
                        List<Fare> lstFares = GetFarelistForJourney(item.JourneyId);
                        //Add flight info to list only if fares are available
                        if (lstFares != null && lstFares.Count > 0)
                        {
                            item.AvailableFares = lstFares;
                            item.SelectedFare = new Fare();
                            item.SelectedFare.Cost = item.AvailableFares[0].Cost;
                            lstSrchResult.Add(item);
                        }
                    }
                    return lstSrchResult;
                }
                return new List<FlightInfo>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public FlightInfo GetFlightInfo(int scheduleId, int selFareId)
        {
            FlightInfo flght = new FlightInfo();
            try
            {
                var dbcntxt = new AirlineReservationEntities();
                var flghtDetail = (from sch in dbcntxt.Schedules
                                   join jry in dbcntxt.Journeys on sch.journey_id equals jry.journey_id
                                   where sch.schedule_id == scheduleId
                                   select sch).FirstOrDefault();
                                   //select new FlightInfo()
                                   //{
                                   //    ScheduleId = sch.schedule_id,
                                   //    ArrivalDateTime = sch.arr_date_time,
                                   //    DepDateTime = sch.dep_date_time,
                                   //    Route = jry.route,
                                   //    SourceCity = GetCityName(jry.source),
                                   //    DestCity = GetCityName(jry.dest)
                                   //}).FirstOrDefault();

                var fareObj = (from f in dbcntxt.FareMappings
                               where f.Fare_id == selFareId
                               select f).FirstOrDefault();

                if (flghtDetail != null && fareObj != null)
                {
                    flght = new FlightInfo()
                    {
                        ScheduleId = flghtDetail.schedule_id,
                        ArrivalDateTime = flghtDetail.arr_date_time,
                        DepDateTime = flghtDetail.dep_date_time,
                        Route = flghtDetail.Journey.route,
                        SourceCity = flghtDetail.Journey.SourceCity.city_name,
                        DestCity = flghtDetail.Journey.DestCity.city_name
                    };
                    flght.SelectedFare = new Fare() { FareId = selFareId, ClassCode = fareObj.@class, Cost = fareObj.cost };
                    flght.SelectedFare.DeriveFareDesc();
                }
                return flght;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private List<Fare> GetFarelistForJourney(int journyId)
        {
            var dbctx = new AirlineReservationEntities();

            var fares = (from f in dbctx.FareMappings
                         where f.journey_id == journyId && f.IsActive == true
                         select f);

            if (fares != null && fares.Count() > 0)
            {
                List<Fare> lstAvailFares = new List<Fare>();
                foreach (var fare in fares)
                {
                    Fare fareObj = new Fare() { FareId = fare.Fare_id, Cost = fare.cost, ClassCode = fare.@class };
                    fareObj.DeriveFareDesc();
                    lstAvailFares.Add(fareObj);
                }
                return lstAvailFares;
            }
            return new List<Fare>();
        }

        public Fare GetFarebyId(int fareId)
        {
            try
            {
                Fare fare = new Fare();
                var dbctx = new AirlineReservationEntities();

                var dbFareObj = (from f in dbctx.FareMappings
                                 where f.Fare_id == fareId && f.IsActive == true
                                 select f).FirstOrDefault();
                if (dbFareObj != null)
                {
                    fare = new Fare() { FareId = dbFareObj.Fare_id, Cost = dbFareObj.cost, ClassCode = dbFareObj.@class };
                    fare.DeriveFareDesc();
                }
                return fare;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
