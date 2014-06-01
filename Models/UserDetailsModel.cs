using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RS2.Models
{
    /* 
     * Sadrzi osnovne podatke o korisniku
     * i osnovne podatke o gradovima koje
     * korisnik poseduje
     */

    public class UserDetailsModel
    {
        public user User { get; set; }
        public List<city> Cities { get; set; }

        public UserDetailsModel()
        {
            Cities = new List<city>();
        }

        public void fillUserDetailsModel(user User, RS2Entities entities)
        {
            this.User = User;
            this.Cities = (from USER in entities.users
                            join USERCITY in entities.user_cities on USER.id equals USERCITY.user_id
                            join CITY in entities.cities on USERCITY.city_id equals CITY.id
                            where USER.id == User.id
                            select CITY
                           ).ToList();
        }
    }
}