using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ZMCMSSys.Models;
using ZMCMSSys.ViewModels;

namespace ZMCMSSys.Controllers
{
    public class SysHospitalGraftingController : Controller
    {
        private SysEntities db = new SysEntities();

        [SessionExpireFilter]
        public ActionResult SysHospitalGrafting()
        {
            //List<string> sysHosptialData = (from sh in db.SysHospital where sh.HospActive == true select sh.HospName).ToList();

            //ViewBag.Attendees = (from sh in db.SysHospital where sh.HospActive == true select sh.HospName).ToList();

            return View();
        }

        // GET: SysHospitalGrafting        
        public ActionResult SysHospitalGrafting_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            DataSourceResult result = 
                (from shg in db.SysHospitalGrafting
                 where shg.HospRowid == id
                 join sub1 in (from cm in db.ComboMaster where cm.CBMClass == "HOSPGRAFTING" join cd in db.ComboDetail on cm.CBMRowid equals cd.CBMRowid select new { cd.CBDRowid, cd.CBDDescription })
                 on shg.CBDRowid equals sub1.CBDRowid into ps1
                 from sub1 in ps1.DefaultIfEmpty()
                 select new {
                     shg.HGRowid,
                     shg.HospRowid,
                     shg.CBDRowid,
                     sub1.CBDDescription,
                     shg.HGMachine,
                     shg.HGMachinePort,
                     shg.HGLoginUser1,
                     shg.HGLoginUser2,
                     shg.HGLoginUser3,
                     shg.HGLoginPassword,
                     shg.HGGetDataPath,
                     shg.HGSaveLogPath,
                     shg.HGUrl
                 }).ToDataSourceResult(request);

            return Json(result);
        }

        public ActionResult GetHospital_Read()
        {
            var result =
                (from sh in db.SysHospital where sh.HospActive == true select new { sh.HospRowid, sh.HospID, sh.HospName });

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalGrafting_Create([DataSourceRequest]DataSourceRequest request, SysHospitalGrafting insert_SHG, string id)
        {
            string sRowid = Guid.NewGuid().ToString();

            if (ModelState.IsValid)
            {
                var entity = new SysHospitalGrafting
                {
                    HGRowid = Guid.NewGuid().ToString(),
                    HospRowid = id,
                    CBDRowid = insert_SHG.CBDRowid,
                    HGMachine = insert_SHG.HGMachine,
                    HGMachinePort = insert_SHG.HGMachinePort,
                    HGLoginUser1 = insert_SHG.HGLoginUser1,
                    HGLoginUser2 = insert_SHG.HGLoginUser2,
                    HGLoginUser3 = insert_SHG.HGLoginUser3,
                    HGLoginPassword = insert_SHG.HGLoginPassword,
                    HGGetDataPath = insert_SHG.HGGetDataPath,
                    HGSaveLogPath = insert_SHG.HGSaveLogPath,
                    HGUrl = insert_SHG.HGUrl
                };

                db.SysHospitalGrafting.Add(entity);
                db.SaveChanges();

                insert_SHG.HGRowid = entity.HGRowid;
            }

            return Json(new[] { insert_SHG }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalGrafting_Update([DataSourceRequest]DataSourceRequest request, SysHospitalGrafting update_SHG)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalGrafting
                {
                    HGRowid = update_SHG.HGRowid,
                    HospRowid = update_SHG.HospRowid,
                    CBDRowid = update_SHG.CBDRowid,
                    HGMachine = update_SHG.HGMachine,
                    HGMachinePort = update_SHG.HGMachinePort,
                    HGLoginUser1 = update_SHG.HGLoginUser1,
                    HGLoginUser2 = update_SHG.HGLoginUser2,
                    HGLoginUser3 = update_SHG.HGLoginUser3,
                    HGLoginPassword = update_SHG.HGLoginPassword,
                    HGGetDataPath = update_SHG.HGGetDataPath,
                    HGSaveLogPath = update_SHG.HGSaveLogPath,
                    HGUrl = update_SHG.HGUrl
                };

                db.SysHospitalGrafting.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { update_SHG }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalGrafting_Destroy([DataSourceRequest]DataSourceRequest request, SysHospitalGrafting delete_SHG)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalGrafting
                {
                    HGRowid = delete_SHG.HGRowid
                };

                db.SysHospitalGrafting.Attach(entity);
                db.SysHospitalGrafting.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { delete_SHG }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}