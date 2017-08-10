using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Farm2.Data;
using Farm2.Models;

namespace Farm2.Controllers
{
    public class ParcelController : Controller
    {
        // GET: Parcel
        public ActionResult Index()
        {
            return View();
        }// GET: Parcel
        public ActionResult Create()
        {
            return View();
        }// GET: Parcel
        public ActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllParcelByFarmId(int? farmId)
        {
            try
            {
                if (farmId != null)
                {
                    return Json(ParcelData.ParcelList.Where(p => p.IdFarm == farmId), JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AddParcel([Bind(Include = "Size,Description,IdFarm,Observations,ConditionIds")] Parcel parcel)
        {
            try
            {
                if (parcel.Size > 0 && !string.IsNullOrEmpty(parcel.Description) && parcel.Observations.Count > 0)
                {
                    parcel.PlantIds = new List<int>();
                    if (ParcelData.ParcelList.LastOrDefault() == null)
                    {
                        parcel.Id = 1;
                    }
                    else
                    {
                        parcel.Id = (ParcelData.ParcelList.LastOrDefault()).Id + 1;
                    }
                    ParcelData.ParcelList.Add(parcel);
                    return Json(parcel, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult GetParcel(int? parcelId)
        {
            try
            {
                if (parcelId != null)
                {
                    var parcel = ParcelData.ParcelList.Where(s => s.Id == parcelId).FirstOrDefault();
                    if (parcel != null)
                    {
                        return Json(parcel, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPut]
        public JsonResult UpdateParcel([Bind(Include = "Id,Size,Description,IdFarm,Observations,ConditionIds")] Parcel parcel)
        {
            try
            {
                if (parcel.Size > 0 && !string.IsNullOrEmpty(parcel.Description) && parcel.Observations.Count > 0)
                {
                    var foundParcel = ParcelData.ParcelList.Where(s => s.Id == parcel.Id).FirstOrDefault();
                    if (foundParcel != null)
                    {
                        foundParcel.Size = parcel.Size;
                        foundParcel.Description = parcel.Description;
                        foundParcel.Observations = parcel.Observations;
                        foundParcel.ConditionIds = parcel.ConditionIds;
                        return Json(foundParcel, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpDelete]
        public JsonResult RemoveParcel(int? parcelId)
        {
            try
            {
                if (parcelId != null)
                {
                    var parcel = ParcelData.ParcelList.Where(f => f.Id == parcelId).FirstOrDefault();
                    if (parcel != null)
                    {
                        ParcelData.ParcelList.Remove(parcel);
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

    }
}