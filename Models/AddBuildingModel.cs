using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RS2.Models
{
    public class AddBuildingModel
    {
        public UserDetailsModel UserDetailsModel { get; set; }
        public List<building> AvailableBuildings { get; set; }

        public int cityID { get; set; }
        public int row { get; set; }
        public int column { get; set; }

        public AddBuildingModel()
        {
            AvailableBuildings = new List<building>();
        }

        public void fillAddBuildingModel(UserDetailsModel U, int cityID, int row, int column, RS2Entities entities)
        {
            AvailableBuildings.Clear();

            this.UserDetailsModel = U;
            this.cityID = cityID;
            this.row = row;
            this.column = column;
            this.AvailableBuildings = (from BUILDING in entities.buildings
                                  select BUILDING).ToList();
        }
    }
}