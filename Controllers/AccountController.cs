using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using RS2.Models;

namespace RS2.Controllers
{
    public class AccountController : Controller
    {
        private RS2Entities entites = new RS2Entities();

        public ActionResult Index()
        {
            return View(entites.users);
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn([Bind(Exclude = "id,nickname")] user LoginData)
        {
            //int count;

            /* sa Bind-om smo rekli da je LoginData validan model ako se
             * ne racunaju id i nickname */

            if (!ModelState.IsValid)
            {
                return View();
            }
            
            // Proverimo da li je korisnik u bazi

            //count = (from USER in entites.users
            //         where USER.username == LoginData.username && USER.password == LoginData.password
            //         select USER).Count();
            var userID = from USER in entites.users
                         where USER.username == LoginData.username && USER.password == LoginData.password
                         select USER.id;
            

            //Debug.WriteLine("count = " + count);

            // ako nema tog korisnika u bazi, vracamo prikaz login forme

            if (userID == null)
            {
                return View();
            }
            else
            {

                // ako smo nasli tog korisnika u bazi, pravimo cookie sa njegovim id-em

                var userIDCookie = new HttpCookie("userID", userID.ToString());
                Response.AppendCookie(userIDCookie);

                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LogOut()
        {
            /* Ako postoji cookie, brisemo ga */

            if (Request.Cookies["userID"] != null)
            {
                var c = new HttpCookie("userID");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToAction("Index","Home");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register([Bind] user RegisterData)
        {
            int maxID;

            if (!ModelState.IsValid)
            {
                return View();
            }

            try{
            var user = (from USER in entites.users
                                where USER.username == RegisterData.username
                                select USER).First();

                return View();
            }
            catch {
                // dopunimo RegisterData sa ID-jem koji je veci od najveceg u tabeli

                try
                {
                    maxID = (entites.users.Max(u => u.id)) + 1;
                }
                catch
                {
                    maxID = 1;
                }

                RegisterData.id = maxID;

                // ubacimo objekat u bazu

                entites.AddTousers(RegisterData);
                entites.SaveChanges();

                // nakon toga odmah i ulogujemo korisnika

                var cookie = new HttpCookie("userID", maxID.ToString());
                Response.AppendCookie(cookie);
                return RedirectToAction("Index", "Home");
            }
                

        }
    }
}
