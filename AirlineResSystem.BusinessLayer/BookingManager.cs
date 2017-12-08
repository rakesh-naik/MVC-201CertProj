using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirlineResSystem.BusinessLayer.Model;
using AirlineResSystem.DataAccess;

namespace AirlineResSystem.BusinessLayer
{
    public class BookingManager
    {
        public bool BookTickets(int schId, int fareId, int custId)
        {
            string tktId = DateTime.Now.ToString("ddMMMyyhhmmss");
            try
            {
                var dbCtx = new AirlineReservationEntities();
                Ticketing_Info tktInfo = new Ticketing_Info() { ticket_id = tktId, Fare_Id = fareId, cust_id = custId, schedule_id = schId, status = "C" };

                dbCtx.Ticketing_Info.Add(tktInfo);
                int saved = dbCtx.SaveChanges();
                return saved > 0;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public bool CancelTickets(string tktId)
        {

            if (string.IsNullOrEmpty(tktId))
                return false;

            var dbCtx = new AirlineReservationEntities();
            Ticketing_Info cnclTkt = dbCtx.Ticketing_Info.Find(tktId);
            if (cnclTkt == null)
                return false;

            cnclTkt.status = "X";
            dbCtx.SaveChanges();
            return true;
        }
    }
}
