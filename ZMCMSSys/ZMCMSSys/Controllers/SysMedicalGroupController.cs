﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ZMCMSSys.Models;
using ZMCMSSys.ViewModels;

namespace ZMCMSSys.Controllers
{
    public class SysMedicalGroupController : Controller
    {
        private SysEntities db = new SysEntities();

        [SessionExpireFilter]
        public ActionResult SysMedicalGroup()
        {
            //ViewData["sysMedicalGroups"] = db.SysMedicalGroup.Select(s => new
            //{
            //    MGRowid = s.MGRowid,
            //    MGName = s.MGName
            //});

            //ViewData["sysHospital"] = db.SysHospital.Select(s => new
            //{
            //    HospRowid = s.HospRowid,
            //    HospID = s.HospID,
            //    HospName = s.HospName
            //});

            return View();
        }

        public ActionResult SysMedicalGroup_Read([DataSourceRequest]DataSourceRequest request, string MGType)
        {
            IQueryable<SysMedicalGroup> sysmedicalgroup = db.SysMedicalGroup;
            DataSourceResult result = sysmedicalgroup
                .Where(o => o.MGType == MGType)
                .ToDataSourceResult(request, sysMedicalGroup => new {
                MGRowid = sysMedicalGroup.MGRowid,
                MGName = sysMedicalGroup.MGName,
                MGDescription = sysMedicalGroup.MGDescript,
                MGFont = sysMedicalGroup.MGFont
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysMedicalGroup_Create(ViewModel_MedicalGroups sysMedicalGroups, string MGType)
        {
            if (sysMedicalGroups != null && ModelState.IsValid)
            {
                var target = new SysMedicalGroup();
                target.MGRowid = Guid.NewGuid().ToString();
                target.MGName = sysMedicalGroups.MGName;
                target.MGDescript = sysMedicalGroups.MGDescript;
                target.MGFont = sysMedicalGroups.MGFont;
                target.MGType = MGType;

                db.SysMedicalGroup.Add(target);
                db.SaveChanges();

                sysMedicalGroups.MGRowid = target.MGRowid;
            }

            return Json(new[] { sysMedicalGroups }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysMedicalGroup_Update([DataSourceRequest]DataSourceRequest request, SysMedicalGroup sysMedicalGroup)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysMedicalGroup
                {
                    MGRowid = sysMedicalGroup.MGRowid,
                    MGName = sysMedicalGroup.MGName,
                    MGDescript = sysMedicalGroup.MGDescript,
                    MGFont = sysMedicalGroup.MGFont
                };

                db.SysMedicalGroup.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sysMedicalGroup }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysMedicalGroup_Destroy([DataSourceRequest]DataSourceRequest request, SysMedicalGroup sysMedicalGroup)
        {
            string sMGRowid = String.Empty;
            if (ModelState.IsValid)
            {
                var entity = new SysMedicalGroup
                {
                    MGRowid = sysMedicalGroup.MGRowid                    
                    //MGName = sysMedicalGroup.MGName,
                    //MGDescript = sysMedicalGroup.MGDescript,
                    //MGFont = sysMedicalGroup.MGFont
                };

                sMGRowid = sysMedicalGroup.MGRowid;
                                
                var entitySysMedicalGroupHospital = db.SysMedicalGroupHospital.Where(x => x.MGRowid == sMGRowid);
                db.SysMedicalGroupHospital.RemoveRange(entitySysMedicalGroupHospital);
                db.SaveChanges();

                db.SysMedicalGroup.Attach(entity);
                db.SysMedicalGroup.Remove(entity);
                db.SaveChanges();                
            }

            return Json(new[] { sysMedicalGroup }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
               
        
        //private SysMedicalGroupHospital GetSysMedicalGroupHospitalByID(string id)
        //{
        //    return db.SysMedicalGroupHospital.FirstOrDefault(o => o.MGHRowid == id);
        //}
       
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult GetHospital()
        {
            var syshospitals = new SysEntities().SysHospital.Select(readSysHospital => new ViewModel_GetHospital
            {
                HospRowid = readSysHospital.HospRowid,
                HospName = readSysHospital.HospName.Trim(),
                HospActive = readSysHospital.HospActive.Value ? "true" : "false"
            }).Where(p => p.HospActive == "true");

            return Content(JsonConvert.SerializeObject(syshospitals), "application/json");
        }

        // 下面是 SysMedicalGroupHosptial 的 CRUD
        public ActionResult SysMedicalGroupHospital_Read(string MGRowid, [DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SysMedicalGroupHospital> sysMGH = db.SysMedicalGroupHospital;

            DataSourceResult result = sysMGH
                .Where(s => s.MGRowid == MGRowid)                
                .Join(db.SysHospital, a => a.HospRowid, b => b.HospRowid, (a, b) => new
                {
                    MGHRowid = a.MGHRowid,
                    HospRowid = a.HospRowid,
                    MGRowid = a.MGRowid,
                    MGHDisplaySeq = a.MGHDisplaySeq,
                    HospName = b.HospName
                })                
                .ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysMedicalGroupHospital_Create(string MGRowid,ViewModel_MedicalGroupHospitals sysMedicalGroupHospitals)
        {
            if (sysMedicalGroupHospitals != null && ModelState.IsValid)
            {
                var target = new SysMedicalGroupHospital();
                target.MGHRowid = Guid.NewGuid().ToString();
                target.MGRowid = MGRowid;                
                target.HospRowid = sysMedicalGroupHospitals.HospRowid;
                target.MGHDisplaySeq = sysMedicalGroupHospitals.MGHDisplaySeq;                

                db.SysMedicalGroupHospital.Add(target);
                db.SaveChanges();
                
                sysMedicalGroupHospitals.MGHRowid = target.MGHRowid;
            }

            return Json(new[] { sysMedicalGroupHospitals }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]        
        public ActionResult SysMedicalGroupHospital_Update([DataSourceRequest]DataSourceRequest request, SysMedicalGroupHospital sysMedicalGroupHospital)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysMedicalGroupHospital
                {
                    MGHRowid = sysMedicalGroupHospital.MGHRowid,
                    MGRowid = sysMedicalGroupHospital.MGRowid,
                    HospRowid = sysMedicalGroupHospital.HospRowid,
                    MGHDisplaySeq = sysMedicalGroupHospital.MGHDisplaySeq
                };

                db.SysMedicalGroupHospital.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sysMedicalGroupHospital }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysMedicalGroupHospital_Destroy([DataSourceRequest]DataSourceRequest request, SysMedicalGroupHospital sysMedicalGroupHospital)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysMedicalGroupHospital
                {
                    MGHRowid = sysMedicalGroupHospital.MGHRowid
                    //MGRowid = sysMedicalGroupHospital.MGRowid,
                    //HospRowid = sysMedicalGroupHospital.HospRowid,
                    //MGHDisplaySeq = sysMedicalGroupHospital.MGHDisplaySeq
                };

                db.SysMedicalGroupHospital.Attach(entity);
                db.SysMedicalGroupHospital.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sysMedicalGroupHospital }.ToDataSourceResult(request, ModelState));
        }
    }
}
