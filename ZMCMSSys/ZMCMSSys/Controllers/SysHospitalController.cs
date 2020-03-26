using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ZMCMSSys.Models;
using ZMCMSSys.ViewModels;

namespace ZMCMSSys.Controllers
{
    public class SysHospitalController : Controller
    {
        private SysEntities db = new SysEntities();

        [SessionExpireFilter]
        public ActionResult SysHospital()
        {            
            return View();
        }

        [SessionExpireFilter]
        public ActionResult SysHospitalClinic()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult SysHospitalDept()
        {
            return View();
        }

        //[SessionExpireFilter]
        //public ActionResult SysHospitalClinicRoom()
        //{
        //    return View();
        //}

        [SessionExpireFilter]
        public ActionResult SysHCDept()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult SysHospotalOpinionEmp()
        {
            return View();
        }

        #region SysHospital
        [SessionExpireFilter]
        public ActionResult SysHospital_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SysHospital> rdata = db.SysHospital;
            DataSourceResult result = (from sh in db.SysHospital
                                       select new
                                       {
                                           sh.HospRowid,
                                           sh.HospID,
                                           sh.HospName,
                                           sh.HospPrincipal,
                                           sh.HospDean,
                                           sh.HospAddress,
                                           sh.HospAreaCode,
                                           sh.HospPhone,
                                           sh.HospFaxno,
                                           sh.HospEmail,
                                           sh.HospLevel,
                                           sh.HospHIS,
                                           sh.HospActive,
                                           sh.HospImage,
                                           sh.HospRegSys
                                       }).ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospital_Create([DataSourceRequest]DataSourceRequest request, SysHospital sysHospital)
        {
            string sRowid = Guid.NewGuid().ToString();
            //string sPath = "/PhotoCloud/HospitalImages/";
            //string sMoveFile = (Session["SourcePhotoFileName"] == null) ? sRowid + "_no-photo-available.png" : sRowid + "_" + Session["SourcePhotoFileName"].ToString();

            if (ModelState.IsValid)
            {
                var entity = new SysHospital
                {
                    HospRowid = sRowid,
                    HospID = sysHospital.HospID,
                    HospName = sysHospital.HospName,
                    HospPrincipal = sysHospital.HospPrincipal,
                    HospDean = sysHospital.HospDean,
                    HospAddress = sysHospital.HospAddress,
                    HospAreaCode = sysHospital.HospAreaCode,
                    HospPhone = sysHospital.HospPhone,
                    HospFaxno = sysHospital.HospFaxno,
                    HospEmail = sysHospital.HospEmail,
                    HospLevel = sysHospital.HospLevel,
                    HospHIS = sysHospital.HospHIS,
                    HospActive = sysHospital.HospActive,
                    HospImage = (Session["SourcePhotoFileName"] == null) ? "no-photo-available.png" : Session["SourcePhotoFileName"].ToString(),
                    //HospImage = (sysHospital.HospImage == null) ? "no-photo-available.png" : sysHospital.HospImage,
                    HospRegSys = "0"
                };

                db.SysHospital.Add(entity);
                db.SaveChanges();
                sysHospital.HospRowid = entity.HospRowid;
                sysHospital.HospImage = entity.HospImage;
                //if (Session["SourcePhotoFileName"] == null)
                //{
                //    System.IO.File.Copy(Request.MapPath(sPath + "no-photo-available.png"), Request.MapPath(sPath + sMoveFile));
                //    sysHospital.HospImage = sRowid + "_no-photo-available.png";
                //}
                //else
                //{
                //    sysHospital.HospImage = entity.HospImage;
                //}
            }
            
            //Session.Remove("SourcePhotoFileName");

            return Json(new[] { sysHospital }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospital_Update([DataSourceRequest]DataSourceRequest request, SysHospital sysHospital)
        {
            if (ModelState.IsValid)
            {
                // 處理HospImage變作空白後要產生一張空白圖片給該機構
                // 已有影像資料者不得做上述動作                
                //if (String.IsNullOrEmpty(sysHospital.HospImage) == true)
                //{
                //    string sPath = "/PhotoCloud/HospitalImages/";
                //    string targetPathAndFile = sysHospital.HospRowid + "_" + "no-photo-available.png";

                //    if (System.IO.File.Exists(Request.MapPath(sPath + targetPathAndFile)))
                //    {
                //        System.IO.File.Delete(Request.MapPath(sPath + targetPathAndFile));
                //    }
                        
                //    System.IO.File.Copy(Request.MapPath(sPath + "no-photo-available.png"), Request.MapPath(sPath + targetPathAndFile));
                //}

                // 開始更新資料
                var entity = new SysHospital
                {
                    HospRowid = sysHospital.HospRowid,
                    HospID = sysHospital.HospID,
                    HospName = sysHospital.HospName,
                    HospPrincipal = sysHospital.HospPrincipal,
                    HospDean = sysHospital.HospDean,
                    HospAddress = sysHospital.HospAddress,
                    HospAreaCode = sysHospital.HospAreaCode,
                    HospPhone = sysHospital.HospPhone,
                    HospFaxno = sysHospital.HospFaxno,
                    HospEmail = sysHospital.HospEmail,
                    HospLevel = sysHospital.HospLevel,
                    HospHIS = sysHospital.HospHIS,
                    HospActive = sysHospital.HospActive,
                    //HospImage = (Session["SourcePhotoFileName"] == null) ? sysHospital.HospRowid + "_no-photo-available.png" : sysHospital.HospImage,
                    HospImage = sysHospital.HospImage,
                    HospRegSys = sysHospital.HospRegSys
                };

                db.SysHospital.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                //sysHospital.HospImage = (Session["SourcePhotoFileName"] == null) ? sysHospital.HospRowid + "_no-photo-available.png" : sysHospital.HospImage;
                
                //Session.Remove("SourcePhotoFileName");
            }

            return Json(new[] { sysHospital }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospital_Destroy([DataSourceRequest]DataSourceRequest request, SysHospital sysHospital)
        {
            string imgFile = String.Empty;

            if (ModelState.IsValid)
            {
                var entity = new SysHospital
                {
                    HospRowid = sysHospital.HospRowid,
                    HospImage = sysHospital.HospImage
                };

                // Delete Image file
                if (sysHospital.HospImage.Contains("no-photo-available") == false)
                {
                    string fullPath = Request.MapPath("/PhotoCloud/HospitalImages/" + sysHospital.HospImage);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                db.SysHospital.Attach(entity);
                db.SysHospital.Remove(entity);
                db.SaveChanges();
            }
                        
            //Session.Remove("SourcePhotoFileName");

            return Json(new[] { sysHospital }.ToDataSourceResult(request, ModelState));
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

        public ActionResult SysHospitalActive_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SysHospital> syshospital = db.SysHospital;
            DataSourceResult result = syshospital
                .Where(o => o.HospActive == true)
                .ToDataSourceResult(request, sysHospital => new
                {
                    HospRowid = sysHospital.HospRowid,
                    HospID = sysHospital.HospID,
                    HospName = sysHospital.HospName,
                    HospAddress = sysHospital.HospAddress,
                    HospAreaCode = sysHospital.HospAreaCode,
                    HospPhone = sysHospital.HospPhone
                    //HospHIS = sysHospital.HospHIS,
                    //HospActive = sysHospital.HospActive
                });

            return Json(result);
        }

        public ActionResult Async_SaveHospital(HttpPostedFileBase files, string id )
        {
            string fileName = String.Empty;

            // The Name of the Upload component is "files"
            if (files != null)
            {
                // Remove all files by HospID
                var dir = new System.IO.DirectoryInfo(Server.MapPath("~/PhotoCloud/HospitalImages/"));
                string sDir = Server.MapPath("~/PhotoCloud") + @"/HospitalImages/";
                string physicalPath = String.Empty;

                foreach (var file in dir.EnumerateFiles(id + "_*.*"))
                {
                    physicalPath = System.IO.Path.Combine(sDir + "/", file.ToString());
                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }

                fileName = id + "_" + Path.GetFileName(files.FileName);
                physicalPath = Path.Combine(sDir + "/", fileName);

                files.SaveAs(physicalPath);

                Session["SourcePhotoFileName"] = fileName;

                // update SysHospital set HospImage = fileName where HospRowid = id
                using (var db = new SysEntities())
                {
                    var result = db.SysHospital.SingleOrDefault(b => b.HospRowid == id);
                    if (result != null)
                    {
                        result.HospImage = fileName;
                        db.SaveChanges();
                    }
                }
            }

            // Return an empty string to signify success
            return Json(new { HospImage = fileName }, "text/plain");
        }

        public ActionResult Async_RemoveHospital(string fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"
            string fileName = String.Empty;
            string sFileName = String.Empty;
            if (fileNames != null && Session["SourcePhotoFileName"] != null &&
                Session["SourcePhotoFileName"].ToString().Contains("no-photo-available") == false)
            //if (fileNames != null)
            {
                fileName = Path.GetFileName(fileNames);
                string sDir = Server.MapPath("~/PhotoCloud") + @"/HospitalImages/";
                sFileName = Session["SourcePhotoFileName"].ToString();
                var physicalPath = Path.Combine(sDir + "/", sFileName);

                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }

            Session.Remove("SourcePhotoFileName");

            //return Json(new { HospImage = ""sFileName }, "text /plain");
            return Json(new { HospImage = "no-photo-available.png" }, "text /plain");
        }
        #endregion

        #region SysHospitalClinic
        public ActionResult SysHospitalClinic_Read(string HospRowid, [DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SysHospitalClinic> crud = db.SysHospitalClinic;

            DataSourceResult result = crud
                .Where(s => s.HospRowid == HospRowid)
                .Join(db.SysHospital, a => a.HospRowid, b => b.HospRowid, (a, b) => new { a, b })
                .GroupJoin(
                    db.ComboMaster.Where(c => c.CBMClass == "CLINICDEPT")
                        .Join(db.ComboDetail, cm => cm.CBMRowid, cd => cd.CBMRowid, (cm, cd) => new { CBDCode = cd.CBDCode, CBDDescription = cd.CBDDescription }),
                    userdata1 => userdata1.a.HCClinicDept, b1 => b1.CBDCode, (userdata1, b1) => b1.Select(x => new { clinicdept = userdata1, code1 = x })).SelectMany(g1 => g1)
                .Select(q => new
                {
                    HCRowid = q.clinicdept.a.HCRowid,
                    HospRowid = q.clinicdept.a.HospRowid,
                    HCClinicDept = q.clinicdept.a.HCClinicDept,
                    HCDeptName = q.code1.CBDDescription,
                    HCDisplaySeq = q.clinicdept.a.HCDisplaySeq
                })
                .ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalClinic_Create(string HospRowid, ViewModel_SysHospitalClinic sysHospitalClinic)
        {
            if (sysHospitalClinic != null && ModelState.IsValid)
            {
                var target = new SysHospitalClinic();
                target.HCRowid = Guid.NewGuid().ToString();
                target.HospRowid = HospRowid;
                target.HCClinicDept = sysHospitalClinic.HCClinicDept;
                target.HCDisplaySeq = sysHospitalClinic.HCDisplaySeq;

                db.SysHospitalClinic.Add(target);
                db.SaveChanges();

                sysHospitalClinic.HCRowid = target.HCRowid;
            }

            return Json(new[] { sysHospitalClinic }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalClinic_Update([DataSourceRequest]DataSourceRequest request, SysHospitalClinic sysHospitalClinic)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalClinic
                {
                    HCRowid = sysHospitalClinic.HCRowid,
                    HospRowid = sysHospitalClinic.HospRowid,
                    HCClinicDept = sysHospitalClinic.HCClinicDept,
                    HCDisplaySeq = sysHospitalClinic.HCDisplaySeq
                };

                db.SysHospitalClinic.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sysHospitalClinic }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalClinic_Destroy([DataSourceRequest]DataSourceRequest request, SysHospitalClinic sysHospitalClinic)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalClinic
                {
                    HCRowid = sysHospitalClinic.HCRowid
                    //MGRowid = sysMedicalGroupHospital.MGRowid,
                    //HospRowid = sysMedicalGroupHospital.HospRowid,
                    //MGHDisplaySeq = sysMedicalGroupHospital.MGHDisplaySeq
                };

                db.SysHospitalClinic.Attach(entity);
                db.SysHospitalClinic.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sysHospitalClinic }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region SysHospitalDept
        public ActionResult SysHospitalDept_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            //IQueryable<SysHospitalDept> crud = db.SysHospitalDept;

            DataSourceResult result = (from shd in db.SysHospitalDept
                                       where shd.HospRowid == id
                                       join sh in db.SysHospital on shd.HospRowid equals sh.HospRowid
                                       join ud in db.UserData on shd.HDUserRowid equals ud.UserRowid into ps1
                                       from ud in ps1.DefaultIfEmpty()
                                       join sub in (
                                           from cm in db.ComboMaster
                                           join cd in db.ComboDetail on cm.CBMRowid equals cd.CBMRowid
                                           where cm.CBMClass == "DEPT"
                                           select new { cd.CBDRowid, cd.CBDCode, cd.CBDDescription }) on shd.HDClinicDept equals sub.CBDCode into ps2
                                       from sub in ps2.DefaultIfEmpty()
                                       select new
                                       {
                                           HDRowid = shd.HDRowid,
                                           HospRowid = shd.HospRowid,
                                           HospName = sh.HospName,
                                           HDClinicDept = shd.HDClinicDept,
                                           HDDeptName = sub.CBDDescription,
                                           HDUserRowid = shd.HDUserRowid,
                                           HDUserName = ud.UserName,
                                           HDDisplaySeq = shd.HDDisplaySeq
                                       }).ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalDept_Create(ViewModel_SysHospitalDept sysHospitalDept, string id)
        {
            if (sysHospitalDept != null && ModelState.IsValid)
            {
                var target = new SysHospitalDept();
                target.HDRowid = Guid.NewGuid().ToString();
                target.HospRowid = id;
                target.HDClinicDept = sysHospitalDept.HDClinicDept;
                target.HDUserRowid = sysHospitalDept.HDUserRowid;
                target.HDDisplaySeq = sysHospitalDept.HDDisplaySeq;

                db.SysHospitalDept.Add(target);
                db.SaveChanges();

                sysHospitalDept.HDRowid = target.HDRowid;
            }

            return Json(new[] { sysHospitalDept }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalDept_Update([DataSourceRequest]DataSourceRequest request, SysHospitalDept sysHospitalDept)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalDept
                {
                    HDRowid = sysHospitalDept.HDRowid,
                    HospRowid = sysHospitalDept.HospRowid,
                    HDClinicDept = sysHospitalDept.HDClinicDept,
                    HDUserRowid = sysHospitalDept.HDUserRowid,
                    HDDisplaySeq = sysHospitalDept.HDDisplaySeq
                };

                db.SysHospitalDept.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sysHospitalDept }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalDept_Destroy([DataSourceRequest]DataSourceRequest request, SysHospitalDept sysHospitalDept)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalDept
                {
                    HDRowid = sysHospitalDept.HDRowid
                };

                db.SysHospitalDept.Attach(entity);
                db.SysHospitalDept.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sysHospitalDept }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region SysHospitalClinicRoom
        public ActionResult SysHospitalClinicRoom_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            IQueryable<SysHospitalClinicRoom> crud = db.SysHospitalClinicRoom;

            DataSourceResult result = crud
                .Where(s => s.HospRowid == id)
                .Join(db.SysHospital, a => a.HospRowid, b => b.HospRowid, (a, b) => new { a, b })
                .GroupJoin(
                    db.ComboMaster.Where(c => c.CBMClass == "CLINICROOM")
                        .Join(db.ComboDetail, cm => cm.CBMRowid, cd => cd.CBMRowid, (cm, cd) => new { CBDCode = cd.CBDCode, CBDDescription = cd.CBDDescription }),
                    userdata1 => userdata1.a.HCRClinicRoom, b1 => b1.CBDCode, (userdata1, b1) => b1.Select(x => new { clinicroom = userdata1, code1 = x })).SelectMany(g1 => g1)
                .Select(q => new
                {
                    HCRRowid = q.clinicroom.a.HCRRowid,
                    HospRowid = q.clinicroom.a.HospRowid,
                    HCRClinicRoom = q.clinicroom.a.HCRClinicRoom,
                    HCRClinicRoomName = q.code1.CBDDescription,
                    HCRDisplaySeq = q.clinicroom.a.HCRDisplaySeq
                })
                .ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalClinicRoom_Create(ViewModel_SysHospitalClinicRoom crud, string id)
        {
            if (crud != null && ModelState.IsValid)
            {
                var target = new SysHospitalClinicRoom();
                target.HCRRowid = Guid.NewGuid().ToString();
                target.HospRowid = id;
                target.HCRClinicRoom = crud.HCRClinicRoom;
                target.HCRDisplaySeq = crud.HCRDisplaySeq;

                db.SysHospitalClinicRoom.Add(target);
                db.SaveChanges();

                crud.HCRRowid = target.HCRRowid;
            }

            return Json(new[] { crud }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalClinicRoom_Update([DataSourceRequest]DataSourceRequest request, SysHospitalClinicRoom crud)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalClinicRoom
                {
                    HCRRowid = crud.HCRRowid,
                    HospRowid = crud.HospRowid,
                    HCRClinicRoom = crud.HCRClinicRoom,
                    HCRDisplaySeq = crud.HCRDisplaySeq
                };

                db.SysHospitalClinicRoom.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { crud }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalClinicRoom_Destroy([DataSourceRequest]DataSourceRequest request, SysHospitalClinicRoom crud)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalClinicRoom
                {
                    HCRRowid = crud.HCRRowid
                    //MGRowid = sysMedicalGroupHospital.MGRowid,
                    //HospRowid = sysMedicalGroupHospital.HospRowid,
                    //MGHDisplaySeq = sysMedicalGroupHospital.MGHDisplaySeq
                };

                db.SysHospitalClinicRoom.Attach(entity);
                db.SysHospitalClinicRoom.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { crud }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region SysHospitalClinicDoctor
        public ActionResult SysHospitalClinicDoctor_Read(string HospRowid, string HCRowid, [DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SysHospitalClinicDoctor> crud = db.SysHospitalClinicDoctor;

            DataSourceResult result = crud
                .Where(s => s.HospRowid == HospRowid && s.HCRowid == HCRowid)
                .Join(db.UserData, a => a.UserRowid, b => b.UserRowid, (a, b) => new { a, b })
                .Select(q => new
                {
                    HCDRowid = q.a.HCDRowid,
                    HospRowid = q.a.HospRowid,
                    HCRowid = q.a.HCRowid,
                    UserRowid = q.a.UserRowid,
                    UserName = q.b.UserName,
                    HCDBelongMedicalGroup = q.a.HCDBelongMedicalGroup,
                    HCDQuota = q.a.HCDQuota,
                    HCDQuota2 = q.a.HCDQuota2,
                    HCDQuota3 = q.a.HCDQuota3,
                    HCDDisplaySeq = q.a.HCDDisplaySeq
                })
                .ToDataSourceResult(request);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalClinicDoctor_Create(string HospRowid, string HCRowid, ViewModel_SysHospitalClinicDoctor sysHospitalClinicDoctor)
        {
            if (sysHospitalClinicDoctor != null && ModelState.IsValid)
            {
                var target = new SysHospitalClinicDoctor();
                target.HCDRowid = Guid.NewGuid().ToString();
                target.HospRowid = HospRowid;
                target.HCRowid = HCRowid;
                target.UserRowid = sysHospitalClinicDoctor.UserRowid;
                target.HCDBelongMedicalGroup = sysHospitalClinicDoctor.HCDBelongMedicalGroup;
                target.HCDQuota = sysHospitalClinicDoctor.HCDQuota;
                target.HCDQuota2 = sysHospitalClinicDoctor.HCDQuota2;
                target.HCDQuota3 = sysHospitalClinicDoctor.HCDQuota3;
                target.HCDDisplaySeq = sysHospitalClinicDoctor.HCDDisplaySeq;

                db.SysHospitalClinicDoctor.Add(target);
                db.SaveChanges();

                sysHospitalClinicDoctor.HCDRowid = target.HCDRowid;
            }

            return Json(new[] { sysHospitalClinicDoctor }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalClinicDoctor_Update([DataSourceRequest]DataSourceRequest request, SysHospitalClinicDoctor sysHospitalClinicDoctor)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalClinicDoctor
                {
                    HCDRowid = sysHospitalClinicDoctor.HCDRowid,
                    HospRowid = sysHospitalClinicDoctor.HospRowid,
                    HCRowid = sysHospitalClinicDoctor.HCRowid,
                    UserRowid = sysHospitalClinicDoctor.UserRowid,
                    HCDBelongMedicalGroup = sysHospitalClinicDoctor.HCDBelongMedicalGroup,
                    HCDQuota = sysHospitalClinicDoctor.HCDQuota,
                    HCDQuota2 = sysHospitalClinicDoctor.HCDQuota2,
                    HCDQuota3 = sysHospitalClinicDoctor.HCDQuota3,
                    HCDDisplaySeq = sysHospitalClinicDoctor.HCDDisplaySeq
                };

                db.SysHospitalClinicDoctor.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sysHospitalClinicDoctor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysHospitalClinicDoctor_Destroy([DataSourceRequest]DataSourceRequest request, SysHospitalClinicDoctor sysHospitalClinicDoctor)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHospitalClinicDoctor
                {
                    HCDRowid = sysHospitalClinicDoctor.HCDRowid
                };

                db.SysHospitalClinicDoctor.Attach(entity);
                db.SysHospitalClinicDoctor.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sysHospitalClinicDoctor }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region SysHCDept
        [SessionExpireFilter]
        public ActionResult SHCDCombo_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = (from cm in db.ComboMaster
                                       where cm.CBMClass == "HOSPITALDEPT"
                                       join cd in db.ComboDetail on cm.CBMRowid equals cd.CBMRowid
                                       select new
                                       {
                                           CBMRowid = cm.CBMRowid,
                                           //CBMDescription = cm.CBMDescription,
                                           //CBMRemark = cm.CBMRemark,
                                           //CBMGroup = cm.CBMGroup,
                                           CBDRowid = cd.CBDRowid,
                                           CBDCode = cd.CBDCode,
                                           CBDDescription = cd.CBDDescription,
                                           //CBDDisplayFlag = cd.CBDDisplayFlag,
                                           CBDDisplayOrder = cd.CBDDisplayOrder
                                       }).OrderBy(c => c.CBDDisplayOrder).ToDataSourceResult(request);

            return Json(result);
        }

        [SessionExpireFilter]
        public ActionResult SHCDept_Read(string sCBDRowid, [DataSourceRequest]DataSourceRequest request)
        {
            //IQueryable<SysHCDept> rdata = db.SysHCDept;

            DataSourceResult result = (from shcd in db.SysHCDept
                                       where shcd.CBDRowid == sCBDRowid
                                       join cd in db.ComboDetail on shcd.SHDCode equals cd.CBDRowid
                                       select new
                                       {
                                           SHDRowid = shcd.SHDRowid,
                                           CBDRowid = shcd.CBDRowid,
                                           SHDCode = shcd.SHDCode,
                                           SHDDisplayOrder = shcd.SHDDisplayOrder,
                                           CBDDescription = cd.CBDDescription
                                       }).ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SHCDept_Create(string sCBDRowid, ViewModel_SysHCDept cdata)
        {
            if (cdata != null && ModelState.IsValid)
            {
                var target = new SysHCDept();
                target.SHDRowid = Guid.NewGuid().ToString();
                target.CBDRowid = sCBDRowid;
                target.SHDCode = cdata.SHDCode;
                target.SHDDisplayOrder = cdata.SHDDisplayOrder;

                db.SysHCDept.Add(target);
                db.SaveChanges();
            }

            return Json(new[] { cdata }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SHCDept_Update([DataSourceRequest]DataSourceRequest request, SysHCDept udata)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHCDept
                {
                    SHDRowid = udata.SHDRowid,
                    CBDRowid = udata.CBDRowid,
                    SHDCode = udata.SHDCode,
                    SHDDisplayOrder = udata.SHDDisplayOrder
                };

                db.SysHCDept.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { udata }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SHCDept_Destroy([DataSourceRequest]DataSourceRequest request, SysHCDept ddata)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysHCDept
                {
                    SHDRowid = ddata.SHDRowid
                };

                db.SysHCDept.Attach(entity);
                db.SysHCDept.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { ddata }.ToDataSourceResult(request, ModelState));
        }
        #endregion
    }
}