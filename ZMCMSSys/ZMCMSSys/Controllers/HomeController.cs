using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZMCMSSys.Models;

namespace ZMCMSSys.Controllers
{
    public class HomeController : Controller
    {
        private SysEntities db = new SysEntities();

        // GET: Home
        public ActionResult Index()
        {
            getUser("C1003");
            ViewBag.Name = Session["UserName"].ToString();

            return View();
        }

        private void getUser(string sUser)
        {
            var aspnetuser = from anu in db.AspNetUsers
                             where anu.UserName == sUser
                             join ud in db.UserData on anu.UserRowid equals ud.UserRowid
                             join sh in db.SysHospital on ud.UserBelongCompany equals sh.HospRowid
                             join position in (from cm in db.ComboMaster join cd in db.ComboDetail on cm.CBMRowid equals cd.CBMRowid where cm.CBMClass == "POSITION" select new { cd.CBDRowid, cd.CBDCode, cd.CBDDescription }) on ud.UserPosition equals position.CBDCode
                             join proidentity in (from cm in db.ComboMaster join cd in db.ComboDetail on cm.CBMRowid equals cd.CBMRowid where cm.CBMClass == "PROIDENTITY" select new { cd.CBDRowid, cd.CBDCode, cd.CBDDescription }) on ud.UserProIdentity equals proidentity.CBDCode
                             join dept in (from cm in db.ComboMaster join cd in db.ComboDetail on cm.CBMRowid equals cd.CBMRowid where cm.CBMClass == "DEPT" select new { cd.CBDRowid, cd.CBDCode, cd.CBDDescription }) on ud.UserDept equals dept.CBDCode
                             join belongdept in (from cm in db.ComboMaster join cd in db.ComboDetail on cm.CBMRowid equals cd.CBMRowid where cm.CBMClass == "DEPT" select new { cd.CBDRowid, cd.CBDCode, cd.CBDDescription }) on ud.UserBelongDept equals belongdept.CBDCode
                             select new
                             {
                                 Id = anu.Id,
                                 Email = anu.Email,
                                 UserRowid = ud.UserRowid,
                                 UserNo = ud.UserNo,
                                 UserIdno = ud.UserIdno,
                                 UserMobilePhone = ud.UserMobilePhone,
                                 CreateDate = anu.CreateDate,
                                 UserName = ud.UserName,
                                 UserRealName = ud.UserName,
                                 UserPhoto = ud.UserPhoto,
                                 UserPosition = ud.UserPosition,
                                 UserPositionDescription = position.CBDDescription,
                                 UserProIdentity = ud.UserProIdentity,
                                 UserProIdentityDescription = proidentity.CBDDescription,
                                 UserBelongCompany = ud.UserBelongCompany,
                                 UserBelongCompanyDescription = sh.HospName,
                                 UserDept = ud.UserDept,
                                 UserDeptDescription = dept.CBDDescription,
                                 UserBelongDept = ud.UserBelongDept,
                                 UserBelongDeptDescription = belongdept.CBDDescription
                             };

            //將使用者頁面相關資訊存入 Session 裡面
            Session["Email"] = aspnetuser.FirstOrDefault().Email == null ? String.Empty : aspnetuser.FirstOrDefault().Email;
            Session["UserRowid"] = aspnetuser.FirstOrDefault().UserRowid;
            Session["UserNo"] = aspnetuser.FirstOrDefault().UserNo;
            Session["UserName"] = aspnetuser.FirstOrDefault().UserName;
            Session["UserRealName"] = aspnetuser.FirstOrDefault().UserRealName;
            Session["UserMobilePhone"] = aspnetuser.FirstOrDefault().UserMobilePhone == null ? String.Empty : aspnetuser.FirstOrDefault().UserMobilePhone;
            Session["ImageUrl"] = "/Assets/Images/Users/" + aspnetuser.FirstOrDefault().UserRowid + "_" + aspnetuser.FirstOrDefault().UserPhoto;
            Session["CreateDate"] = aspnetuser.FirstOrDefault().CreateDate.ToString();
            Session["UserPosition"] = aspnetuser.FirstOrDefault().UserPosition;
            Session["UserPositionDescription"] = aspnetuser.FirstOrDefault().UserPositionDescription;
            Session["UserProIdentity"] = aspnetuser.FirstOrDefault().UserProIdentity;
            Session["UserProIdentityDescription"] = aspnetuser.FirstOrDefault().UserProIdentityDescription;
            Session["UserBelongCompany"] = aspnetuser.FirstOrDefault().UserBelongCompany;
            Session["UserBelongCompanyDescription"] = aspnetuser.FirstOrDefault().UserBelongCompanyDescription;
            Session["HospName"] = aspnetuser.FirstOrDefault().UserBelongCompanyDescription;
            Session["UserDept"] = aspnetuser.FirstOrDefault().UserDept;
            Session["UserDeptDescription"] = aspnetuser.FirstOrDefault().UserDeptDescription;
            Session["UserBelongDept"] = aspnetuser.FirstOrDefault().UserBelongDept;
            Session["UserBelongDeptDescription"] = aspnetuser.FirstOrDefault().UserBelongDeptDescription;
            Session["LoginLevel"] = 1;
        }
    }
}