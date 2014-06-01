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
            /* sa Bind-om smo rekli da je LoginData validan model ako se
             * ne racunaju id i nickname */

            if (!ModelState.IsValid)
            {
               // this.Flash("Invalid data!", FlashLevel.Warning);
                return View();
            }
            
            // Proverimo da li je korisnik u bazi

            try
            {
                var user = (from USER in entites.users
                              where USER.username == LoginData.username && USER.password == LoginData.password
                              select USER).First();


                //Debug.WriteLine("count = " + count);

                // ako smo nasli tog korisnika u bazi, pravimo cookie sa njegovim id-em

                var currentUser = new HttpCookie("currentUser");
                currentUser.Values["id"] = user.id.ToString();
                currentUser.Values["username"] = user.username.ToString();
                currentUser.Values["nickname"] = user.nickname.ToString();
                Response.AppendCookie(currentUser);

                return RedirectToAction("Index", "Home");
            }
            catch {
                // ako nema tog korisnika u bazi, vracamo prikaz login forme
                //this.Flash("No registrated user with those credentials!", FlashLevel.Error);
                return View();
            }
        }
        public ActionResult LogOut()
        {
            /* Ako postoji cookie, brisemo ga */

            if (Request.Cookies["currentUser"]["id"] != null)
            {
                var c = new HttpCookie("currentUser");
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

                var cookie = new HttpCookie("currentUser");
                cookie.Values["id"] = RegisterData.id.ToString();
                cookie.Values["username"] = RegisterData.username.ToString();
                cookie.Values["nickname"] = RegisterData.nickname.ToString();
                Response.AppendCookie(cookie);
                return RedirectToAction("Index", "Home");
            }
                

        }
    }
}
