using AirlineResSystem.BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirlineResSystem.BusinessLayer;
using AirlineReservationSystem.Models;
using System.Threading.Tasks;
using System.Web.Security;

namespace AirlineReservationSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            UserManager usr = new UserManager();
            return View(new UserDetailDO() { DateOfBirth = DateTime.Now });
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserDetailDO postedForm)
        {
            if (!ModelState.IsValid)
            {
                TempData.Add("Message", "Registration Failed"); return View(new UserDetailDO());
            }
            UserManager usrMngr = new UserManager();
            if (usrMngr.RegisterUser(postedForm))
            { TempData.Add("Message", "Registration Success, Login again to proceed."); return RedirectToAction("Login"); }
            else
            { TempData.Add("Message", "Registration Failed"); return View(); }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserManager usrBL = new UserManager();
            UserDetailDO userDetail = usrBL.AuthenticateUser(model.UserName, model.Password);
            if (userDetail != null)
            { Session["user"] = userDetail; FormsAuthentication.SetAuthCookie(userDetail.UserName.ToString(), true); return RedirectToLocal(returnUrl); }
            TempData.Add("Error", "Incorrect UserName or Password");
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["user"] = null;
            return RedirectToAction("Search", "Flight");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Search", "Flight");
        }
    }
}