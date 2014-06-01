using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RS2.App_Start;
using System.Configuration;

namespace RS2.Controllers
{
    public class MapController : Controller
    {
        //
        // GET: /Map/

        public ActionResult Index()
        {
            //string row = MapConfigurator.ReadSetting("mapRow");
            //MapConfigurator.WriteSetting("mapColumn", "35");
            //string column = MapConfigurator.ReadSetting("mapColumn");

            String Version = ConfigurationManager.AppSettings["mapRow"];


            // ---- Writing a key


            ExeConfigurationFileMap FileMap = new ExeConfigurationFileMap();
            FileMap.ExeConfigFilename = Server.MapPath(@"~\Web.config");


            Configuration Config = ConfigurationManager.OpenMappedExeConfiguration(FileMap, ConfigurationUserLevel.None);


            Config.AppSettings.Settings.Remove("mapColumn");
            Config.AppSettings.Settings.Add("mapColumn", "77");
            Config.Save(ConfigurationSaveMode.Modified);
            String column = ConfigurationManager.AppSettings["mapColumn"];
            return View();
        }

        //
        // GET: /Map/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Map/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Map/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Map/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Map/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Map/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Map/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
