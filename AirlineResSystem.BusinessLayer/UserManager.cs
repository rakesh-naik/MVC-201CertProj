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

        public Dictionary<string, string> GetCountryList()
        {
            SearchManager search = new SearchManager();
            return search.GetCountryList();
        }

        public bool AuthenticateUser(string UserName, string pswrd)
        {
            var dbctx = new AirlineReservationEntities();
            Cryptography crypt = new Cryptography();
            pswrd = crypt.Encrypt(pswrd);
            var userSearch = (from u in dbctx.UserInfoes
                              where u.user_name.ToLower() == UserName.ToLower() && pswrd == u.password
                              select u).FirstOrDefault();
            if (userSearch != null && userSearch.cust_id > 0)
                return true;

            return false;
        }
    }
}
