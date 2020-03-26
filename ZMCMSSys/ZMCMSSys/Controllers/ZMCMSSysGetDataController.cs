using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
using System.Collections.Generic;
using ZMCMSSys.Models;
using ZMCMSSys.ViewModels;

namespace ZMCMSSys.Controllers
{
    public class ZMCMSSysGetDataController : Controller
    {
        private SysEntities db = new SysEntities();

        // GET: ZMCMSSysGetData
        public JsonResult GetCombos(string stext)
        {
            var result =
                db.ComboMaster
                .Where(s => s.CBMClass == stext)
                .Join(db.ComboDetail, a => a.CBMRowid, b => b.CBMRowid, (a, b) => new
                {
                    CBDRowid = b.CBDRowid,
                    CBDCode = b.CBDCode,
                    CBDDescription = b.CBDDescription,
                    CBDDisplayFlag = b.CBDDisplayFlag,
                    CBDDisplayOrder = b.CBDDisplayOrder
                }).Where(r => r.CBDDisplayFlag == true).OrderBy(b => b.CBDDisplayOrder);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBelongCompanyUserData()
        {
            string sUserRowid = Session["UserRowid"].ToString();

            string[] hosp = (from umg in db.UserMedicalGroup
                             join ud in db.UserData on umg.UserRowid equals ud.UserRowid
                             join sub in (from smg in db.SysMedicalGroup
                                          join smgh in db.SysMedicalGroupHospital on smg.MGRowid equals smgh.MGRowid
                                          where smg.MGType == "S"
                                          select new { smg.MGRowid, smg.MGName, smgh.HospRowid, smgh.MGHDisplaySeq }) on umg.MGRowid equals sub.MGRowid
                             join sh in (from sh in db.SysHospital select new { sh.HospRowid }) on sub.HospRowid equals sh.HospRowid
                             where ud.UserRowid == sUserRowid
                             orderby sub.MGHDisplaySeq
                             select sh.HospRowid).ToArray();

            var result = from ud in db.UserData
                         where ud.UserResignationDay == null && hosp.Contains(ud.UserBelongCompany)
                         orderby ud.UserNo, ud.UserName
                         select new
                         {
                             ud.UserRowid,
                             ud.UserNo,
                             ud.UserName
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserData()
        {
            string sUserRowid = Session["UserRowid"].ToString();

            string[] hosp = (from umg in db.UserMedicalGroup
                             join ud in db.UserData on umg.UserRowid equals ud.UserRowid
                             join sub in (from smg in db.SysMedicalGroup
                                          join smgh in db.SysMedicalGroupHospital on smg.MGRowid equals smgh.MGRowid
                                          where smg.MGType == "S"
                                          select new { smg.MGRowid, smg.MGName, smgh.HospRowid, smgh.MGHDisplaySeq }) on umg.MGRowid equals sub.MGRowid
                             join sh in (from sh in db.SysHospital select new { sh.HospRowid }) on sub.HospRowid equals sh.HospRowid
                             where ud.UserRowid == sUserRowid
                             orderby sub.MGHDisplaySeq
                             select sh.HospRowid).ToArray();

            var result = from ud in db.UserData
                         join anu in db.AspNetUsers on ud.UserRowid equals anu.UserRowid into cp
                         from anu in cp.DefaultIfEmpty()
                         where ud.UserResignationDay == null && !db.AspNetUsers.Any(anu => anu.UserRowid == ud.UserRowid) && hosp.Contains(ud.UserBelongCompany)
                         select new
                         {
                             ud.UserRowid,
                             ud.UserNo,
                             ud.UserName
                         };
            //.Where(p => p.HospActive == "true");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 取得有Email人員
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAspNetUsers()
        {
            var result = (from anu in db.AspNetUsers
                          join ud in db.UserData on anu.UserRowid equals ud.UserRowid
                          where anu.Email != null
                          select new
                          {
                              UserRowid = anu.UserRowid,
                              UserNo = ud.UserNo,
                              UserName = ud.UserName,
                              UserEmail = anu.Email,
                              UserPhoto = ud.UserRowid + "_" + ud.UserPhoto
                          });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDoctorByProIdentity()
        {
            // 取得所屬醫療群或體系的醫師名單
            var doctorList = (from ud in db.UserData
                          where ud.UserProIdentity == "01"
                          orderby ud.UserNo
                          select new { ud.UserRowid, ud.UserName });
            //var result = from umg in db.UserMedicalGroup
            //             where umg.UserRowid == Session["UserRowid"].ToString()
            //             join ud in db.UserData on umg.UserRowid equals ud.UserRowid
            //             join sub in (from smg in db.SysMedicalGroup
            //                          join smgh in db.SysMedicalGroupHospital on smg.MGRowid equals smgh.MGRowid
            //                          select new { smg.MGRowid, smg.MGName, smgh.HospRowid, smgh.MGHDisplaySeq }) on umg.MGRowid equals sub.MGRowid
            //             join sh in (from sh in db.SysHospital select new { sh.HospRowid, sh.HospName }) on sub.HospRowid equals sh.HospRowid
            //             orderby sh.HospName, sub.MGHDisplaySeq
            //             select new { umg.UserRowid, umg.MGRowid, ud.UserName, sub.MGName, sh.HospRowid, sh.HospName, sub.MGHDisplaySeq };

            //var UserBelobgHospitalByGroup = from umg in result
            //                                orderby umg.HospName
            //                                group umg by new { umg.UserRowid, umg.UserName, umg.HospRowid, umg.HospName } into g
            //                                select new { UserRowid = g.Key.UserRowid, UserName = g.Key.UserName, HospRowid = g.Key.HospRowid, HospName = g.Key.HospName };

            //var doctorList = from sub in (from ud in db.UserData where ud.UserProIdentity == "01" select new { ud.UserRowid, ud.UserName, ud.UserBelongCompany })
            //                 join ubhbg in UserBelobgHospitalByGroup on sub.UserBelongCompany equals ubhbg.HospRowid
            //                 orderby ubhbg.HospRowid
            //                 select new { UserRowid = sub.UserRowid, UserName = sub.UserName };

            return Json(doctorList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHospital_MedialGroup(string param)
        {
            var result =
            (from umg in db.UserMedicalGroup
             where umg.UserRowid == param
             join ud in db.UserData on umg.UserRowid equals ud.UserRowid
             join sub in (from smg in db.SysMedicalGroup
                          join smgh in db.SysMedicalGroupHospital on smg.MGRowid equals smgh.MGRowid
                          where smg.MGType == "S"
                          select new { smg.MGRowid, smg.MGName, smgh.HospRowid, smgh.MGHDisplaySeq }) on umg.MGRowid equals sub.MGRowid
             join sh in (from sh in db.SysHospital select new { sh.HospRowid, sh.HospID, sh.HospName }) on sub.HospRowid equals sh.HospRowid
             orderby sub.MGHDisplaySeq, sh.HospID
             select new { umg.UserRowid, umg.MGRowid, ud.UserName, sub.MGName, sh.HospRowid, sh.HospID, sh.HospName, sub.MGHDisplaySeq });

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}