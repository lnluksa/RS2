using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace RS2.Models
{
    /*
     * Sadrzi osnovne podatke o zgradi
     * i dodatne podatke o zgradi koji stoje u city_buildings modelu
     */

    public class BuildingDetailsModel
    {
        public building BuildingModel;
        public city_buildings CityBuildingsModel;

        
        // Puni model detaljima o zgradi koja se nalazi u gradu sa cityID, u redu row i koloni column 
         
        public void fillBuildingsDetailsModel(int cityID, int row, int column, RS2Entities entities)
        {
            try
            {
                var result = (from BUILDING in entities.buildings
                              join CITY_BUILDINGS in entities.city_buildings on BUILDING.id equals CITY_BUILDINGS.building_id
                              where (CITY_BUILDINGS.city_id == cityID
                                    && CITY_BUILDINGS.building_positionX == row
                                    && CITY_BUILDINGS.building_positionY == column)
                              select new
                              {
                                  resultBuilding = BUILDING,
                                  resultCityBuildings = CITY_BUILDINGS
                              }
                                         ).First();

                BuildingModel = result.resultBuilding;
                CityBuildingsModel = result.resultCityBuildings;
            }
            catch
            {
                this.BuildingModel = null;
                this.CityBuildingsModel = null;
            }
        }

    }
}