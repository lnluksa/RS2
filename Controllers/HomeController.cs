using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

using RS2.Models;

namespace RS2.Controllers
{
    public class HomeController : Controller
    {
        private RS2Entities entites = new RS2Entities();

        public ActionResult Index()
        {
            int currentUserID;
           
            if (!Request.Cookies.AllKeys.Contains("userID"))
            {
                return RedirectToAction("LogIn","Account");
            }
               
            currentUserID = Int32.Parse(Request.Cookies["userID"].Value);

            user currentUser = (from USER in entites.users
                                where USER.id == currentUserID
                                select USER).First();

            UserDetailsModel userDetailsModel = new UserDetailsModel();
            userDetailsModel.fillUserDetailsModel(currentUser, entites);

            return View(userDetailsModel);
        }
        public ActionResult ShowCity(int id)
        {
            // ako nismo ulogovani, prebacuje nas na login

            if (!Request.Cookies.AllKeys.Contains("username"))
            {
                return RedirectToAction("LogIn", "Account");
            }

            // napunimo model za grad
            CityDetailsModel cityDetails = new CityDetailsModel();
            var currentCity = (from C in entites.cities
                               where C.id==id
                               select C).First();

            cityDetails.fillCityDetailsModel(currentCity, entites);

            // napunimo model za korisnika
            int currentUserID = Int32.Parse(Request.Cookies["userID"].Value);
            user currentUser = (from e in entites.users
                                where e.id == currentUserID
                                select e).First();

            UserDetailsModel userDetails = new UserDetailsModel();
            userDetails.fillUserDetailsModel(currentUser, entites);

            // showcity model napunimo ovim modelima
            ShowCityModel showCityModel = new ShowCityModel();
            showCityModel.fillShowCityModel(userDetails, cityDetails);
            return View(showCityModel);
        }
    }
}
