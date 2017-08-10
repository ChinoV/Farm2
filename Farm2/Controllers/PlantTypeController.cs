using Farm2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farm2.Controllers
{
    public class PlantTypeController : Controller
    {
        // GET: PlantType
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllPlantTypes()
        {
            try
            {
                return Json(PlantData.PlantTypeList, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public JsonResult GetTypePlantById(int plantTypeId)
        {
            try
            {
                return Json(PlantData.PlantTypeList.Where(p => p.Id == plantTypeId).FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}