using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineResSystem.DataAccess
{
    public class UserDAL
    {
        public bool AddNewUser(UserInfo userObj)
        {
            try
            {
                var dbCtx = new AirlineReservationEntities();
                UserInfo newUser = dbCtx.UserInfoes.Add(userObj);
                int saved = dbCtx.SaveChanges();
                if (newUser.cust_id > 0)
                    return true;

                return false;
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
