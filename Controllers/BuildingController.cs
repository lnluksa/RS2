using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

using RS2.Models;

namespace RS2.Controllers
{
    public class BuildingController : Controller
    {
        private RS2Entities entities = new RS2Entities();

        public ActionResult Index()
        {
            return View();
        }     
        [HttpGet]
        [ActionName("AddBuilding")]
        public ActionResult AddBuilding(int id, int row, int column)
        {
            // napunimo model za korisnika

            string currentUsername = Request.Cookies["username"].Value;
            user currentUser = (from e in entities.users
                                where e.username == currentUsername
                                select e).First();

            UserDetailsModel userDetails = new UserDetailsModel();
            userDetails.fillUserDetailsModel(currentUser, entities);

            /* napunimo addBuilding model, koji sadrzi detalje korisnika,
             * id grada u kojem pravimo zgradu, 
             * i sve zgrade koje mogu da se prave*/

            AddBuildingModel addBuildingModel = new AddBuildingModel();
            addBuildingModel.fillAddBuildingModel(userDetails, id, row, column, entities);

            return View(addBuildingModel);
        }    
        [ActionName("AddBuilding1")]
        public ActionResult AddBuilding(int id, int row, int column, int buildingID, long buildDate, int buildTime)
        {
            city_buildings CB = new city_buildings();
            int nextID;

            try
            {
                nextID = (entities.city_buildings.Max(u => u.id)) + 1;
            }
            catch
            {
                nextID = 0;
            }
            
            CB.id = nextID;
            CB.lvl = 1;
            CB.building_positionX = row;
            CB.building_positionY = column;
            CB.building_id = buildingID;
            CB.city_id = id;
            CB.isPending = 1;
            CB.buildStarted = Constants.convertSecondsToDateTime(buildDate);
            Debug.WriteLine("U bazu idem datum:" + buildDate);
            CB.buildTime = buildTime;

            Debug.Write("ID: " + CB.id + "\n" +
                        "CityID: " + CB.city_id + "\n" +
                        "BuildingID: " + CB.building_id + "\n" +
                        "Lvl: " + CB.lvl + "\n" +
                        "X: " + CB.building_positionX + "\n" +
                        "Y: " + CB.building_positionY + "\n" +
                        "isPending: " + CB.isPending + "\n" +
                        "buildStarted: " + CB.buildStarted + "\n" +
                        "buildTime: " + CB.buildTime + "\n");

            entities.AddTocity_buildings(CB);
            entities.SaveChanges();
            return RedirectToAction("ShowCity", "Home", new { id = id });
            
        }
        public ActionResult Delete(int id, int row, int column)
        {
      
            var result = (from CITY_BUILDINGS in entities.city_buildings
                                        where (CITY_BUILDINGS.city_id == id 
                                        && CITY_BUILDINGS.building_positionX == row
                                        && CITY_BUILDINGS.building_positionY == column)
                                        select CITY_BUILDINGS).First();

            entities.city_buildings.DeleteObject(result);
            entities.SaveChanges();
            

            return RedirectToAction("ShowCity", "Home", new { id = result.city_id });
        }
        //[HttpPost]
        public ActionResult Create(int id, int row, int column)
        {
            var result = (from CITY_BUILDING in entities.city_buildings
                          where CITY_BUILDING.city_id == id
                          && CITY_BUILDING.building_positionX == row
                          && CITY_BUILDING.building_positionY == column
                          select CITY_BUILDING);

            if (result.Count() == 0)
                return RedirectToAction("ShowCity", "Home", new { id = id });
            
            result.First().isPending = 0;
            result.First().lvl++;

            entities.SaveChanges();
            return RedirectToAction("ShowCity", "Home", new { id = result.First().city_id });
        }
    }
}
