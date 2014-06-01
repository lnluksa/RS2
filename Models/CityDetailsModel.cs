using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RS2.Models
{
    /*
     * Sadrzi osnovne podatke o gradu,
     * listu detatljnih podataka o zgrada u gradu
     * i podatke o tipu grada
     */

    public class CityDetailsModel
    {
        public city City { get; set; }
        public List<BuildingDetailsModel> BuildingDetailsList { get; set; }
        public city_types CityType { get; set; }

        public CityDetailsModel()
        {
            BuildingDetailsList = new List<BuildingDetailsModel>();
        }

        public void fillCityDetailsModel(city City, RS2Entities entities)
        {
            BuildingDetailsList.Clear();

            this.City = City;

            this.CityType = (from CITY_TYPES in entities.city_types
                             where CITY_TYPES.id == City.city_type
                             select CITY_TYPES).First();

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    BuildingDetailsModel current = new BuildingDetailsModel();
                    current.fillBuildingsDetailsModel(City.id, i, j, entities);
                    BuildingDetailsList.Add(current);
                }
        }



    }
}