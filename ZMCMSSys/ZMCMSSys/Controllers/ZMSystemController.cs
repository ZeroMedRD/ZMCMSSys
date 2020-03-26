using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ZMCMSSys.Models;
using ZMCMSSys.ViewModels;

namespace ZMCMSSys.Controllers
{
    public class ZMSystemController : Controller
    {
        private SysEntities db = new SysEntities();

        [SessionExpireFilter]
        public ActionResult SysCombo()
        {
            return View();
        }

        //
        // 片語資料維護區
        //
        [SessionExpireFilter]
        public ActionResult SysComboH()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SysComboH(FormCollection post)
        {
            ViewBag.HospRowid = post["HospRowid"];

            return View();
        }

        [SessionExpireFilter]
        public ActionResult SysRole()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult SysProgram()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult SysUser()
        {
            return View();
        }

        public ActionResult SysUserCopy()
        {
            return View();
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

        public ActionResult GetRole()
        {
            var sysprograms = new SysEntities().SysRole.Select(readData => new ViewModel_GetRole
            {
                SRRowid = readData.SRRowid,
                SRName = readData.SRName.Trim()
            });

            return Content(JsonConvert.SerializeObject(sysprograms), "application/json");
        }

        #region ComboMaster
        public ActionResult ComboMaster_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<ComboMaster> combomaster = db.ComboMaster;
            DataSourceResult result = combomaster.ToDataSourceResult(request, d => new {
                CBMRowid = d.CBMRowid,
                CBMClass = d.CBMClass,
                CBMDescription = d.CBMDescription,
                CBMRemark = d.CBMRemark,
                CBMGroup = d.CBMGroup,
                CBMLastModifiedDateTime = d.CBMLastModifiedDateTime,
                CBMModifiedUserRowid = d.CBMModifiedUserRowid
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboMaster_Create(ViewModel_ComboMasters combomaster)
        {
            if (combomaster != null && ModelState.IsValid)
            {
                var target = new ComboMaster();
                target.CBMRowid = Guid.NewGuid().ToString();
                target.CBMClass = combomaster.CBMClass;
                target.CBMDescription = combomaster.CBMDescription;
                target.CBMRemark = combomaster.CBMRemark;
                target.CBMGroup = combomaster.CBMGroup;
                target.CBMLastModifiedDateTime = combomaster.CBMLastModifiedDateTime;
                target.CBMModifiedUserRowid = combomaster.CBMModifiedUserRowid;

                db.ComboMaster.Add(target);
                db.SaveChanges();

                combomaster.CBMRowid = target.CBMRowid;
            }

            return Json(new[] { combomaster }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboMaster_Update([DataSourceRequest]DataSourceRequest request, ComboMaster combomaster)
        {
            if (ModelState.IsValid)
            {
                var entity = new ComboMaster
                {
                    CBMRowid = combomaster.CBMRowid,
                    CBMClass = combomaster.CBMClass,
                    CBMDescription = combomaster.CBMDescription,
                    CBMRemark = combomaster.CBMRemark,
                    CBMGroup = combomaster.CBMGroup,
                    CBMLastModifiedDateTime = combomaster.CBMLastModifiedDateTime,
                    CBMModifiedUserRowid = combomaster.CBMModifiedUserRowid
                };

                db.ComboMaster.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { combomaster }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboMaster_Destroy([DataSourceRequest]DataSourceRequest request, ComboMaster combomaster)
        {
            if (ModelState.IsValid)
            {
                var entity = new ComboMaster
                {
                    CBMRowid = combomaster.CBMRowid,
                    CBMClass = combomaster.CBMClass,
                    CBMDescription = combomaster.CBMDescription,
                    CBMRemark = combomaster.CBMRemark,
                    CBMGroup = combomaster.CBMGroup,
                    CBMLastModifiedDateTime = combomaster.CBMLastModifiedDateTime,
                    CBMModifiedUserRowid = combomaster.CBMModifiedUserRowid
                };

                db.ComboMaster.Attach(entity);
                db.ComboMaster.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { combomaster }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region ComboDetail
        public ActionResult ComboDetail_Read(string CBMRowid, [DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<ComboDetail> combodetail = db.ComboDetail;

            DataSourceResult result = combodetail
                .Where(s => s.CBMRowid == CBMRowid)
                .ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboDetail_Create(string CBMRowid, ViewModel_ComboDetails combodetail)
        {
            if (combodetail != null && ModelState.IsValid)
            {
                var target = new ComboDetail();
                target.CBDRowid = Guid.NewGuid().ToString();
                target.CBMRowid = CBMRowid;
                target.CBDCode = combodetail.CBDCode;
                target.CBDDescription = combodetail.CBDDescription;
                target.CBDDisplayFlag = combodetail.CBDDisplayFlag;
                target.CBDDisplayOrder = combodetail.CBDDisplayOrder;
                target.CBDLastModifiedDateTime = combodetail.CBDLastModifiedDateTime;
                target.CBDModifiedUserRowid = combodetail.CBDModifiedUserRowid;

                db.ComboDetail.Add(target);
                db.SaveChanges();
            }

            return Json(new[] { combodetail }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboDetail_Update([DataSourceRequest]DataSourceRequest request, ComboDetail combodetail)
        {
            if (ModelState.IsValid)
            {
                var entity = new ComboDetail
                {
                    CBDRowid = combodetail.CBDRowid,
                    CBMRowid = combodetail.CBMRowid,
                    CBDCode = combodetail.CBDCode,
                    CBDDescription = combodetail.CBDDescription,
                    CBDDisplayFlag = combodetail.CBDDisplayFlag,
                    CBDDisplayOrder = combodetail.CBDDisplayOrder,
                    CBDLastModifiedDateTime = combodetail.CBDLastModifiedDateTime,
                    CBDModifiedUserRowid = combodetail.CBDModifiedUserRowid
                };

                db.ComboDetail.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { combodetail }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboDetail_Destroy([DataSourceRequest]DataSourceRequest request, ComboDetail combodetail)
        {
            if (ModelState.IsValid)
            {
                var entity = new ComboDetail
                {
                    CBDRowid = combodetail.CBDRowid,
                    CBMRowid = combodetail.CBMRowid,
                    CBDCode = combodetail.CBDCode,
                    CBDDescription = combodetail.CBDDescription,
                    CBDDisplayFlag = combodetail.CBDDisplayFlag,
                    CBDDisplayOrder = combodetail.CBDDisplayOrder,
                    CBDLastModifiedDateTime = combodetail.CBDLastModifiedDateTime,
                    CBDModifiedUserRowid = combodetail.CBDModifiedUserRowid
                };

                db.ComboDetail.Attach(entity);
                db.ComboDetail.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { combodetail }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region ComboHMaster
        public ActionResult ComboHMaster_Read([DataSourceRequest]DataSourceRequest request, string param)
        {
            IQueryable<ComboHMaster> combomaster = db.ComboHMaster;
            DataSourceResult result = combomaster.Where(s => s.CBMHospRowid == param).
                ToDataSourceResult(request, d => new {
                    CBMRowid = d.CBMRowid,
                    CBMClass = d.CBMClass,
                    CBMDescription = d.CBMDescription,
                    CBMRemark = d.CBMRemark,
                    CBMGroup = d.CBMGroup,
                    CBMLastModifiedDateTime = d.CBMLastModifiedDateTime,
                    CBMModifiedUserRowid = d.CBMModifiedUserRowid
                });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboHMaster_Create(ViewModel_ComboHMaster combohmaster, string param)
        {
            if (combohmaster != null && ModelState.IsValid)
            {
                var target = new ComboHMaster();
                target.CBMRowid = Guid.NewGuid().ToString();
                target.CBMHospRowid = param;
                target.CBMClass = combohmaster.CBMClass;
                target.CBMDescription = combohmaster.CBMDescription;
                target.CBMRemark = combohmaster.CBMRemark;
                target.CBMGroup = combohmaster.CBMGroup;
                target.CBMLastModifiedDateTime = combohmaster.CBMLastModifiedDateTime;
                target.CBMModifiedUserRowid = combohmaster.CBMModifiedUserRowid;

                db.ComboHMaster.Add(target);
                db.SaveChanges();

                combohmaster.CBMRowid = target.CBMRowid;
            }

            return Json(new[] { combohmaster }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboHMaster_Update([DataSourceRequest]DataSourceRequest request, ComboHMaster combohmaster, string param)
        {
            if (ModelState.IsValid)
            {
                var entity = new ComboHMaster
                {
                    CBMRowid = combohmaster.CBMRowid,
                    CBMHospRowid = param,
                    CBMClass = combohmaster.CBMClass,
                    CBMDescription = combohmaster.CBMDescription,
                    CBMRemark = combohmaster.CBMRemark,
                    CBMGroup = combohmaster.CBMGroup,
                    CBMLastModifiedDateTime = combohmaster.CBMLastModifiedDateTime,
                    CBMModifiedUserRowid = combohmaster.CBMModifiedUserRowid
                };

                db.ComboHMaster.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { combohmaster }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboHMaster_Destroy([DataSourceRequest]DataSourceRequest request, ComboHMaster combohmaster)
        {
            if (ModelState.IsValid)
            {
                var entity = new ComboHMaster
                {
                    CBMRowid = combohmaster.CBMRowid
                    //CBMClass = combohmaster.CBMClass,
                    //CBMDescription = combohmaster.CBMDescription,
                    //CBMRemark = combohmaster.CBMRemark,
                    //CBMGroup = combohmaster.CBMGroup,
                    //CBMLastModifiedDateTime = combohmaster.CBMLastModifiedDateTime,
                    //CBMModifiedUserRowid = combohmaster.CBMModifiedUserRowid
                };

                db.ComboHMaster.Attach(entity);
                db.ComboHMaster.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { combohmaster }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region ComboHDetail
        public ActionResult ComboHDetail_Read(string CBMRowid, [DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<ComboHDetail> combohdetail = db.ComboHDetail;

            DataSourceResult result = combohdetail
                .Where(s => s.CBMRowid == CBMRowid)
                .ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboHDetail_Create(string CBMRowid, ViewModel_ComboHDetail combohdetail)
        {
            if (combohdetail != null && ModelState.IsValid)
            {
                var target = new ComboHDetail();
                target.CBDRowid = Guid.NewGuid().ToString();
                target.CBMRowid = CBMRowid;
                target.CBDCode = combohdetail.CBDCode;
                target.CBDDescription = combohdetail.CBDDescription;
                target.CBDDisplayFlag = combohdetail.CBDDisplayFlag;
                target.CBDDisplayOrder = combohdetail.CBDDisplayOrder;
                target.CBDLastModifiedDateTime = combohdetail.CBDLastModifiedDateTime;
                target.CBDModifiedUserRowid = combohdetail.CBDModifiedUserRowid;

                db.ComboHDetail.Add(target);
                db.SaveChanges();
            }

            return Json(new[] { combohdetail }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboHDetail_Update([DataSourceRequest]DataSourceRequest request, ComboHDetail combohdetail)
        {
            if (ModelState.IsValid)
            {
                var entity = new ComboHDetail
                {
                    CBDRowid = combohdetail.CBDRowid,
                    CBMRowid = combohdetail.CBMRowid,
                    CBDCode = combohdetail.CBDCode,
                    CBDDescription = combohdetail.CBDDescription,
                    CBDDisplayFlag = combohdetail.CBDDisplayFlag,
                    CBDDisplayOrder = combohdetail.CBDDisplayOrder,
                    CBDLastModifiedDateTime = combohdetail.CBDLastModifiedDateTime,
                    CBDModifiedUserRowid = combohdetail.CBDModifiedUserRowid
                };

                db.ComboHDetail.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { combohdetail }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ComboHDetail_Destroy([DataSourceRequest]DataSourceRequest request, ComboHDetail combohdetail)
        {
            if (ModelState.IsValid)
            {
                var entity = new ComboHDetail
                {
                    CBDRowid = combohdetail.CBDRowid
                    //CBMRowid = combohdetail.CBMRowid,
                    //CBDCode = combohdetail.CBDCode,
                    //CBDDescription = combohdetail.CBDDescription,
                    //CBDDisplayFlag = combohdetail.CBDDisplayFlag,
                    //CBDDisplayOrder = combohdetail.CBDDisplayOrder,
                    //CBDLastModifiedDateTime = combohdetail.CBDLastModifiedDateTime,
                    //CBDModifiedUserRowid = combohdetail.CBDModifiedUserRowid
                };

                db.ComboHDetail.Attach(entity);
                db.ComboHDetail.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { combohdetail }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region SysRole
        public ActionResult SysRole_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SysRole> sysrole = db.SysRole;
            DataSourceResult result = sysrole.ToDataSourceResult(request, sysRole => new {
                SRRowid = sysRole.SRRowid,
                SRName = sysRole.SRName,
                SRDisplayName = sysRole.SRDisplayName,
                SRDescript = sysRole.SRDescript,
                SRFont = sysRole.SRFont,
                SRUrl = sysRole.SRUrl
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysRole_Create(ViewModel_Roles sysRole)
        {
            if (sysRole != null && ModelState.IsValid)
            {
                var target = new SysRole();
                target.SRRowid = Guid.NewGuid().ToString();
                target.SRName = sysRole.SRName;
                target.SRDisplayName = sysRole.SRDisplayName;
                target.SRDescript = sysRole.SRDescript;
                target.SRFont = sysRole.SRFont;
                target.SRUrl = sysRole.SRUrl;

                db.SysRole.Add(target);
                db.SaveChanges();

                sysRole.SRRowid = target.SRRowid;
            }

            return Json(new[] { sysRole }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysRole_Update([DataSourceRequest]DataSourceRequest request, SysRole sysRole)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysRole
                {
                    SRRowid = sysRole.SRRowid,
                    SRName = sysRole.SRName,
                    SRDisplayName = sysRole.SRDisplayName,
                    SRDescript = sysRole.SRDescript,
                    SRFont = sysRole.SRFont,
                    SRUrl = sysRole.SRUrl
                };

                db.SysRole.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sysRole }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysRole_Destroy([DataSourceRequest]DataSourceRequest request, SysRole sysRole)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysRole
                {
                    SRRowid = sysRole.SRRowid,
                    SRName = sysRole.SRName,
                    SRDisplayName = sysRole.SRDisplayName,
                    SRDescript = sysRole.SRDescript,
                    SRFont = sysRole.SRFont,
                    SRUrl = sysRole.SRUrl
                };

                db.SysRole.Attach(entity);
                db.SysRole.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sysRole }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region SysRoleProgram
        public ActionResult GetProgram()
        {
            var sysprograms = new SysEntities().SysProgram.Select(readSysProgram => new ViewModel_GetProgram
            {
                SPRowid = readSysProgram.SPRowid,
                SPName = readSysProgram.SPName.Trim()
            });

            return Content(JsonConvert.SerializeObject(sysprograms), "application/json");
        }

        // 下面是 SysRoleProgram 的 CRUD
        public ActionResult SysRoleProgram_Read(string SRRowid, [DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SysRoleProgram> sysroleprogram = db.SysRoleProgram;

            DataSourceResult result = sysroleprogram
                .Where(s => s.SRRowid == SRRowid)
                .Join(db.SysProgram, a => a.SPRowid, b => b.SPRowid, (a, b) => new {
                    SRPRowid = a.SRPRowid,
                    SRPActive = a.SRPActive,
                    SRPDisplaySeq = a.SRPDisplaySeq,
                    SRRowid = a.SRRowid,
                    SPRowid = a.SPRowid,
                    SPName = b.SPName
                })
                .ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysRoleProgram_Create(string SRRowid, ViewModel_RolePrograms roleprogram)
        {
            if (roleprogram != null && ModelState.IsValid)
            {
                var target = new SysRoleProgram();
                target.SRPRowid = Guid.NewGuid().ToString();
                target.SRRowid = SRRowid;
                target.SPRowid = roleprogram.SPRowid;
                target.SRPActive = roleprogram.SRPActive;
                target.SRPDisplaySeq = roleprogram.SRPDisplaySeq;

                db.SysRoleProgram.Add(target);
                db.SaveChanges();

                //roleprogram.SRPRowid = target.SRPRowid;
            }

            return Json(new[] { roleprogram }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysRoleProgram_Update([DataSourceRequest]DataSourceRequest request, SysRoleProgram roleprogram)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysRoleProgram
                {
                    SRPRowid = roleprogram.SRPRowid,
                    SRPActive = roleprogram.SRPActive,
                    SRPDisplaySeq = roleprogram.SRPDisplaySeq,
                    SRRowid = roleprogram.SRRowid,
                    SPRowid = roleprogram.SPRowid
                };

                db.SysRoleProgram.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { roleprogram }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysRoleProgram_Destroy([DataSourceRequest]DataSourceRequest request, SysRoleProgram roleprogram)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysRoleProgram
                {
                    SRPRowid = roleprogram.SRPRowid,
                    SRPActive = roleprogram.SRPActive,
                    SRPDisplaySeq = roleprogram.SRPDisplaySeq,
                    SRRowid = roleprogram.SRRowid,
                    SPRowid = roleprogram.SPRowid
                };

                db.SysRoleProgram.Attach(entity);
                db.SysRoleProgram.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { roleprogram }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region SysProgram
        public ActionResult SysProgram_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SysProgram> sysprogram = db.SysProgram;
            DataSourceResult result = sysprogram.ToDataSourceResult(request, sysProgram => new {
                SPRowid = sysProgram.SPRowid,
                SPName = sysProgram.SPName,
                SPUrl = sysProgram.SPUrl,
                SPFont = sysProgram.SPFont,
                SPSysNo = sysProgram.SPSysNo
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysProgram_Create([DataSourceRequest]DataSourceRequest request, SysProgram sysProgram)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysProgram
                {
                    SPRowid = Guid.NewGuid().ToString(),
                    SPName = sysProgram.SPName,
                    SPUrl = sysProgram.SPUrl,
                    SPFont = sysProgram.SPFont,
                    SPSysNo = sysProgram.SPSysNo
                };

                db.SysProgram.Add(entity);
                db.SaveChanges();
                sysProgram.SPRowid = entity.SPRowid;
            }

            return Json(new[] { sysProgram }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysProgram_Update([DataSourceRequest]DataSourceRequest request, SysProgram sysProgram)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysProgram
                {
                    SPRowid = sysProgram.SPRowid,
                    SPName = sysProgram.SPName,
                    SPUrl = sysProgram.SPUrl,
                    SPFont = sysProgram.SPFont,
                    SPSysNo = sysProgram.SPSysNo
                };

                db.SysProgram.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { sysProgram }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysProgram_Destroy([DataSourceRequest]DataSourceRequest request, SysProgram sysProgram)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysProgram
                {
                    SPRowid = sysProgram.SPRowid,
                    SPName = sysProgram.SPName,
                    SPUrl = sysProgram.SPUrl,
                    SPFont = sysProgram.SPFont,
                    SPSysNo = sysProgram.SPSysNo
                };

                db.SysProgram.Attach(entity);
                db.SysProgram.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { sysProgram }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region SysUser
        public ActionResult SysUser_Read([DataSourceRequest]DataSourceRequest request, string MGType)
        {
            string sUserRowid = Session["UserRowid"].ToString();
            string[] hosp = (from umg in db.UserMedicalGroup
                             join ud in db.UserData on umg.UserRowid equals ud.UserRowid
                             join sub in (from smg in db.SysMedicalGroup
                                          join smgh in db.SysMedicalGroupHospital on smg.MGRowid equals smgh.MGRowid
                                          where smg.MGType == MGType
                                          select new { smg.MGRowid, smg.MGName, smgh.HospRowid, smgh.MGHDisplaySeq }) on umg.MGRowid equals sub.MGRowid
                             join sh in (from sh in db.SysHospital select new { sh.HospRowid, sh.HospName }) on sub.HospRowid equals sh.HospRowid
                             //join sh1 in (from sh in db.SysHospital select new { sh.HospRowid, sh.HospName }) on ud.UserBelongCompany equals sh1.HospRowid
                             //join sh2 in (from sh in db.SysHospital select new { sh.HospRowid, sh.HospName }) on ud.UserBelongCompany equals sh2.HospRowid
                             where ud.UserRowid == sUserRowid
                             orderby sub.MGHDisplaySeq
                             select sh.HospRowid).ToArray();


            //IQueryable<AspNetUsers> aspsysusers = db.AspNetUsers;

            DataSourceResult
                result = (from anu in db.AspNetUsers
                          join ud in db.UserData on anu.UserRowid equals ud.UserRowid
                          where hosp.Contains(ud.UserBelongCompany)
                          join sh in db.SysHospital on ud.UserBelongCompany equals sh.HospRowid
                          join sub1 in (from cm in db.ComboMaster join cd in db.ComboDetail on cm.CBMRowid equals cd.CBMRowid where cm.CBMClass == "PROIDENTITY" select new { cd.CBDRowid, cd.CBDCode, cd.CBDDescription }) on ud.UserProIdentity equals sub1.CBDCode into ps
                          from sub1 in ps.DefaultIfEmpty()
                          select new
                          {
                              Id = anu.Id,
                              UserRowid = ud.UserRowid,
                              UserNo = ud.UserNo,
                              UserIdno = ud.UserIdno,
                              Email = anu.Email,
                              PasswordHash = anu.PasswordHash,
                              SecurityStamp = anu.SecurityStamp,
                              PhoneNumber = anu.PhoneNumber,
                              UserName = anu.UserName,
                              UserRealName = ud.UserName,
                              UserPhoto = ud.UserPhoto,
                              UserProIdentityDescription = sub1.CBDDescription,
                              UserBelongCompany = sh.HospRowid,
                              UserBelongCompanyDescription = sh.HospName,
                              LockoutEndDateUtc = anu.LockoutEndDateUtc
                          }).ToDataSourceResult(request);            

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> SysUser_Create(ViewModel_AspNetUsers model)
        {
            if (model != null && ModelState.IsValid)
            {
                var userdata = new SysEntities().UserData.Select(readUserData => new ViewModel_GetUserData
                {
                    UserNo = readUserData.UserNo,
                    UserRowid = readUserData.UserRowid,
                    UserName = readUserData.UserName.Trim(),
                    UserIdno = readUserData.UserIdno
                }).Where(f => f.UserRowid == model.UserRowid).FirstOrDefault();

                var user = new ApplicationUser
                {
                    UserName = userdata.UserNo,         // 帳號
                    Email = model.Email,                // Email
                    UserRowid = model.UserRowid,        // 使用者編號(UserData系統產生)
                    LockoutEnabled = true,
                    CreateDate = DateTime.Now
                };

                IdentityResult result = await UserManager.CreateAsync(user, userdata.UserIdno);                
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // 如需如何進行帳戶確認及密碼重設的詳細資訊，請前往 https://go.microsoft.com/fwlink/?LinkID=320771
                    // 傳送包含此連結的電子郵件
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "確認您的帳戶", "請按一下此連結確認您的帳戶 <a href=\"" + callbackUrl + "\">這裏</a>");

                    //return View(); //RedirectToAction("Index", "Home");
                }
                AddErrors(result);

            }

            return Json(new[] { model }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysUser_Update([DataSourceRequest]DataSourceRequest request, AspNetUsers model)
        {
            if (ModelState.IsValid)
            {
                var entity = new AspNetUsers
                {
                    Id = model.Id,
                    Email = model.Email,
                    PasswordHash = model.PasswordHash,
                    SecurityStamp = model.SecurityStamp,
                    PhoneNumber = model.PhoneNumber,
                    UserRowid = model.UserRowid,
                    UserName = model.UserName,
                    LockoutEnabled = true,
                    LockoutEndDateUtc = model.LockoutEndDateUtc,
                    CreateDate = model.CreateDate
                };

                db.AspNetUsers.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysUser_Destroy([DataSourceRequest]DataSourceRequest request, AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                var entity = new AspNetUsers
                {
                    Id = aspNetUsers.Id
                };

                db.AspNetUsers.Attach(entity);
                db.AspNetUsers.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { aspNetUsers }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region SysUserRole
        public ActionResult SysUserRole_Read(string URRowid, [DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SysUserRole> readData = db.SysUserRole;

            DataSourceResult result = readData
                .Where(s => s.URRowid == URRowid)
                .Join(db.SysRole, c1 => c1.SRRowid, c2 => c2.SRRowid, (c1, c2) => new { c1, c2 })
                .Select(q => new
                {
                    SURRowid = q.c1.SURRowid,
                    SURDisplaySeq = q.c1.SURDisplaySeq,
                    URRowid = q.c1.URRowid,
                    SRRowid = q.c1.SRRowid,
                    SRName = q.c2.SRName
                })
                .ToDataSourceResult(request);

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysUserRole_Create(string URRowid, ViewModel_SysUserRoleTemplate insertData)
        {
            if (insertData != null && ModelState.IsValid)
            {
                var target = new SysUserRole();
                target.SURRowid = Guid.NewGuid().ToString();
                target.URRowid = URRowid;
                target.SURDisplaySeq = insertData.SURDisplaySeq;
                target.SRRowid = insertData.SRRowid;

                db.SysUserRole.Add(target);
                db.SaveChanges();

                //roleprogram.SRPRowid = target.SRPRowid;
            }

            return Json(new[] { insertData }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysUserRole_Update([DataSourceRequest]DataSourceRequest request, SysUserRole updateData)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysUserRole
                {
                    SURRowid = updateData.SURRowid,
                    URRowid = updateData.URRowid,
                    SRRowid = updateData.SRRowid,
                    SURDisplaySeq = updateData.SURDisplaySeq
                };

                db.SysUserRole.Attach(entity);
                db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { updateData }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SysUserRole_Destroy([DataSourceRequest]DataSourceRequest request, SysUserRole destoryData)
        {
            if (ModelState.IsValid)
            {
                var entity = new SysUserRole
                {
                    SURRowid = destoryData.SURRowid
                };

                db.SysUserRole.Attach(entity);
                db.SysUserRole.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { destoryData }.ToDataSourceResult(request, ModelState));
        }
        #endregion        
    }
}