using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm2.Data;
using Farm2.Models;

namespace Farm2.Controllers
{
    public class PlantController : Controller
    {
        // GET: Plant
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddPlant([Bind(Include = "Name,Description,PlantedDate,IdParcel,PlantType")] Plant plant)
        {
            try
            {
                if (!string.IsNullOrEmpty(plant.Name) && !string.IsNullOrEmpty(plant.Description))
                {
                    var last = PlantData.PlantList.Last();
                    plant.Id = last.Id + 1;
                    PlantData.PlantList.Add(plant);
                    return Json(plant, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpDelete]
        public JsonResult RemovePlant(int? plantId)
        {
            try
            {
                if (plantId != null)
                {
                    var plant = PlantData.PlantList.Where(f => f.Id == plantId).FirstOrDefault();
                    PlantData.PlantList.Remove(plant);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getPlant(int? plantId)
        {
            try
            {
                if (plantId != null)
                {
                    var plant = PlantData.PlantList.Where(s => s.Id == plantId).FirstOrDefault();
                    return Json(plant, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPut]
        public JsonResult UpdatePlant([Bind(Include = "Id,Name,Description,PlantedDate,IdParcel,PlantType")] Plant plant)
        {
            try
            {
                var foundPlant = PlantData.PlantList.Where(s => s.Id == plant.Id).FirstOrDefault();
                foundPlant.Name = plant.Name;
                foundPlant.Description = plant.Description;
                foundPlant.PlantedDate = plant.PlantedDate;
                foundPlant.IdParcel = plant.IdParcel;
                foundPlant.PlantType = plant.PlantType;
                return Json(foundPlant, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetAllPlantsByParcelId(int? parcelId)
        {
            try
            {
                if (parcelId != null)
                {
                    return Json(PlantData.PlantList.Where(p => p.IdParcel == parcelId), JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }


        [HttpGet]
        public JsonResult GetParcelByTypeAndCount(int? parcelId)
        {
            try
            {
                if (parcelId!=null) {
                    var a = from p in PlantData.PlantList
                            where p.IdParcel == parcelId
                            group p by p.PlantType
                into g
                            select g.ToList();
                    List<Object> result = new List<object>();
                    foreach (var b in a)
                    {
                        result.Add(new
                        {
                            count = b.Count,
                            type = GetTypeName(b.FirstOrDefault().PlantType)
                        });
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        private string GetTypeName(int? plantId)
        {
            try
            {
                if (plantId!=null) {
                    return PlantData.PlantTypeList.Where(p => p.Id == plantId).FirstOrDefault().Name;
                }
                return null;
            }
            catch
            {
                return null;
            }
            
        }
    }
}