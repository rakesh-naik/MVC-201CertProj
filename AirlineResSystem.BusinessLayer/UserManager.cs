using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineResSystem.DataAccess;
using AirlineResSystem.BusinessLayer.Model;
using System.Data.Linq;
using System.Data.Entity;

namespace AirlineResSystem.BusinessLayer
{
    public class UserManager
    {

        public bool RegisterUser(UserDetailDO userDetails)
        {
            try
            {
                var dbUserObj = new UserInfo();
                dbUserObj.user_name = userDetails.UserName;
                Cryptography cryptObj = new Cryptography();
                dbUserObj.password = cryptObj.Encrypt(userDetails.Password);

                dbUserObj.first_name = userDetails.FirstName;
                dbUserObj.last_name = userDetails.LastName;
                dbUserObj.country = userDetails.Nationality;
                dbUserObj.mobile = userDetails.Mobile;
                dbUserObj.email_addr = userDetails.EmailId;
                dbUserObj.passport_no = userDetails.PassportNo;
                dbUserObj.misc_info = userDetails.MiscInfo;
                UserDAL dataAcs = new UserDAL();
                return dataAcs.AddNewUser(dbUserObj);
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public UserDetailDO AuthenticateUser(string UserName, string pswrd)
        {
            var dbctx = new AirlineReservationEntities();
            Cryptography crypt = new Cryptography();
            pswrd = crypt.Encrypt(pswrd);
            var userSearch = (from u in dbctx.UserInfoes
                              where u.user_name.ToLower() == UserName.ToLower() && pswrd == u.password
                              select new UserDetailDO()
                              {
                                  UserName = u.user_name,
                                  CustomerId = u.cust_id,
                                  FirstName = u.first_name,
                                  LastName = u.last_name,
                                  EmailId = u.email_addr,
                                  Mobile = u.mobile
                              }).FirstOrDefault();
            if (userSearch != null && !string.IsNullOrEmpty(userSearch.FirstName))
                return userSearch;

            return null;
        }

        public int GetCustIdFromUserName(String userName)
        {
            int custId = 0;
            var dbCtxt = new AirlineReservationEntities();

            var userInfo = (from usr in dbCtxt.UserInfoes
                            where usr.user_name == userName
                            select usr).FirstOrDefault();

            if (userInfo != null && userInfo.cust_id > 0)
                custId = userInfo.cust_id;

            return custId;
        }

        public List<FlightInfo> GetBookingOfUser(int custId)
        {
            try
            {
                var dbCtxt = new AirlineReservationEntities();

                //var custId = GetCustIdFromUserName(userName);

                if (custId > 0)
                {
                    var myBookings = (from ti in dbCtxt.Ticketing_Info
                                      where ti.cust_id == custId
                                      select ti).ToList();

                    if (myBookings != null && myBookings.Count > 0)
                    {
                        List<FlightInfo> lstMyBookings = new List<FlightInfo>();
                        foreach (var booking in myBookings)
                        {
                            FlightInfo flight = new FlightInfo()
                            {
                                TicketId = booking.ticket_id,
                                DepDateTime = booking.Schedule.dep_date_time,
                                ArrivalDateTime = booking.Schedule.arr_date_time,
                                SourceCity = booking.Schedule.Journey.SourceCity.city_name,
                                DestCity = booking.Schedule.Journey.DestCity.city_name,
                                Route = booking.Schedule.Journey.route,
                                Status = booking.status,
                                SelectedFare = new Fare() { ClassCode = booking.FareMapping.@class, Cost = booking.FareMapping.cost }                            
                            };
                            flight.SelectedFare.DeriveFareDesc();
                            lstMyBookings.Add(flight);
                            
                        }
                        return lstMyBookings;
                    }
                }
                return new List<FlightInfo>();
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }
    }
}
