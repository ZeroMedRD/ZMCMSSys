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
using Newtonsoft.Json;
using ZMCMSSys.Models;
using ZMCMSSys.ViewModels;
using ZMLib;

namespace ZMCMSSys.Controllers
{
    public class InfoPanelController : Controller
    {
        private OPDBoardEntities db_opdboard = new OPDBoardEntities();
        private SysEntities db_sys = new SysEntities();

        #region InfoPanel
        public ActionResult InfoPanel_Read([DataSourceRequest]DataSourceRequest request, string id)
        {
            DataSourceResult result = (from ip in db_opdboard.InfoPanel
                where ip.IPHospRowid == id
                select new
                {
                    IPRowid = ip.IPRowid,
                    IPHospRowid = ip.IPHospRowid,
                    IPName = ip.IPName,
                    IPDisplaySeq = ip.IPDisplaySeq,
                    IPCBDCode = ip.IPCBDCode,
                    IPKey = ip.IPKey,
                    IPCipher = ip.IPCipher
                }).ToDataSourceResult(request);
            
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InfoPanel_Create(ViewModel_InfoPanel infoPanel, string id)
        {
            if (infoPanel != null && ModelState.IsValid)
            {
                // Random Encrpty Key
                ZMClass zmc = new ZMClass();

                string sIPRowid = Guid.NewGuid().ToString();
                string sKey = sIPRowid.Substring(0, 16);

                // 用自key產生加密key
                byte[] bKey = zmc.AESEncrypt(sKey, sKey);
                sKey = Convert.ToBase64String(bKey).Substring(0,16);
                byte[] bCipher = zmc.AESEncrypt(sIPRowid, sKey);
                string sCipher = Convert.ToBase64String(bCipher);

                var target = new InfoPanel();
                target.IPRowid = sIPRowid;
                target.IPHospRowid = id;
                target.IPName = infoPanel.IPName;
                target.IPDisplaySeq = infoPanel.IPDisplaySeq;
                target.IPCBDCode = infoPanel.IPCBDCode;
                target.IPKey = sKey;
                target.IPCipher = "obid=" + sCipher;

                db_opdboard.InfoPanel.Add(target);
                db_opdboard.SaveChanges();

                infoPanel.IPRowid = target.IPRowid;
            }

            return Json(new[] { infoPanel }.ToDataSourceResult(new DataSourceRequest(), ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InfoPanel_Update([DataSourceRequest]DataSourceRequest request, InfoPanel infoPanel)
        {
            if (ModelState.IsValid)
            {
                var entity = new InfoPanel
                {
                    IPRowid = infoPanel.IPRowid,
                    IPHospRowid = infoPanel.IPHospRowid,
                    IPName = infoPanel.IPName,
                    IPDisplaySeq = infoPanel.IPDisplaySeq,
                    IPCBDCode = infoPanel.IPCBDCode                
                };

                db_opdboard.InfoPanel.Attach(entity);
                db_opdboard.Entry(entity).State = EntityState.Modified;
                db_opdboard.SaveChanges();
            }

            return Json(new[] { infoPanel }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InfoPanel_Destroy([DataSourceRequest]DataSourceRequest request, InfoPanel infoPanel)
        {
            if (ModelState.IsValid)
            {
                var entity = new InfoPanel
                {
                    IPRowid = infoPanel.IPRowid
                };

                db_opdboard.InfoPanel.Attach(entity);
                db_opdboard.InfoPanel.Remove(entity);
                db_opdboard.SaveChanges();
            }

            return Json(new[] { infoPanel }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db_opdboard.Dispose();
            base.Dispose(disposing);
        }
    }
}