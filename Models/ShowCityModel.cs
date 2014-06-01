using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using RS2.Models;

namespace RS2.Models
{
    /*
     * ShowCityModel se koristi za prikaz gradova.
     * Sadrzi UserDetailsModel koji se u View-u renderuje na levoj strani ekrana
     * i CityDetailsModel koji se renderuje na desnoj strani ekrana
     */

    public class ShowCityModel
    {
        private RS2Entities entities = new RS2Entities();

        public CityDetailsModel CityDetailsModel { get; set; }
        public UserDetailsModel UserDetailsModel { get; set; }

        public void fillShowCityModel(UserDetailsModel U, CityDetailsModel C)
        {
            this.UserDetailsModel = U;
            this.CityDetailsModel = C;
        }
    }
}