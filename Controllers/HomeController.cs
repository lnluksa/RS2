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
           
            if (!Request.Cookies.AllKeys.Contains("currentUser"))
            {
                return RedirectToAction("LogIn","Account");
            }
            //var id = Request.Cookies["userID"].Value;
            currentUserID = Int32.Parse(Request.Cookies["currentUser"]["id"]);

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

            if (!Request.Cookies.AllKeys.Contains("currentUser"))
            {
                return RedirectToAction("LogIn", "Account");
            }

            //validacija user-a
            int currentUserID = Int32.Parse(Request.Cookies["currentUser"]["id"]);
            var userOwnsCity = (from C in entites.user_cities
                               where C.city_id == id && C.user_id == currentUserID
                               select C).Count();
            if (userOwnsCity == 0) { 
            //ERROR PAGE
                return RedirectToAction("Error", "Account");
            }
            

            // napunimo model za grad
            CityDetailsModel cityDetails = new CityDetailsModel();
            var currentCity = (from C in entites.cities
                               where C.id==id
                               select C).First();

            cityDetails.fillCityDetailsModel(currentCity, entites);

            // napunimo model za korisnika
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
