using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ZMCMSSys.ViewModels
{
    public class ViewModel_Hospitals
    {
        public string HospRowid { get; set; }

        //[UIHint("HospID")]
        [Display(Name = "醫事機構代碼")]
        //[Required(ErrorMessage = "{0}不可以空白 !")]
        public string HospID { get; set; }

        [Display(Name = "醫事機構名稱")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        [MaxLength(255)]
        public string HospName { get; set; }

        [Display(Name = "醫院地址")]
        [MaxLength(255)]
        public string HospAddress { get; set; }

        [Display(Name = "地區代碼")]
        [MaxLength(255)]
        public string HospAreaCode { get; set; }

        [Display(Name = "醫院電話")]
        [MaxLength(100)]
        public string HospPhone { get; set; }

        [Display(Name = "傳真號碼")]
        //[Required(ErrorMessage = "Mobile no. is required")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression("[0-9]{2}?[-.]?[0-9]{15}", ErrorMessage = "請輸入正確的傳真號碼，例如：03-1234567")]
        [MaxLength(20)]
        public string HospFaxno{ get; set; }

        [Display(Name = "電子信箱")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "電子郵件信箱格式不正確 !")]
        [MaxLength(255)]
        public string HospEmail { get; set; }

        [Display(Name = "醫事機構級別")]
        public string HospLevel { get; set; }

        [Display(Name = "負責人")]        
        public string HospPrincipal { get; set; }

        [Display(Name = "院長")]        
        public string HospDean { get; set; }
        
        [Display(Name = "醫療系統廠商")]
        [MaxLength(100)]
        public string HospHIS { get; set; }

        [Display(Name = "啟用")]
        //[MaxLength(100)]
        public bool HospActive { get; set; }

        [Display(Name = "外觀圖片")]
        public string HospImage { get; set; }

        [Display(Name = "掛號系統開啟狀態")]
        public string HospRegSys { get; set; }
    }
    
    public class ViewModel_SysHospitalGrafting
    {   
        public string HGRowid { get; set; }

        [Display(Name = "醫事機構資料序號")]
        public string HospRowid { get; set; }

        [Display(Name = "嫁接項目資料序號")]
        public string CBDRowid { get; set; }

        [Display(Name = "嫁接項目資料名稱")]
        public string CBDDescription { get; set; }

        [Display(Name = "嫁接資料電腦主機名稱")]
        public string HGMachine { get; set; }

        [Display(Name = "嫁接資料電腦主機埠號")]
        public string HGMachinePort { get; set; }

        [Display(Name = "嫁接登入帳號一")]
        public string HGLoginUser1 { get; set; }

        [Display(Name = "嫁接登入帳號二")]
        public string HGLoginUser2 { get; set; }

        [Display(Name = "嫁接登入帳號三")]
        public string HGLoginUser3 { get; set; }

        [Display(Name = "登入密碼")]
        public string HGLoginPassword { get; set; }

        [Display(Name = "資料取得位置")]
        public string HGGetDataPath { get; set; }

        [Display(Name = "系統紀錄存放位置")]
        public string HGSaveLogPath { get; set; }

        [Display(Name = "連結位置")]
        public string HGUrl { get; set; }
    }

    public class ViewModel_SysHospitalClinic
    {
        public string HCRowid { get; set; }

        //public string MGRowid { get; set; }

        //public string HospRowid { get; set; }

        //[Display(Name = "應用程式名稱")]
        //[StringLength(128, ErrorMessage = "使用的應用程式不能超過 128 個字元.")]
        public string HCClinicDept { get; set; }

        public string HCDeptName { get; set; }

        //[Display(Name = "科別名稱")]
        //public string HCDeptName { get; set; }

        [Display(Name = "顯示排列順序")]
        //[Required(ErrorMessage = "{0}不可以空白 !")]
        public int HCDisplaySeq { get; set; }
    }

    public class ViewModel_SysHospitalDept
    {
        public string HDRowid { get; set; }

        //public string MGRowid { get; set; }

        //public string HospRowid { get; set; }

        //[Display(Name = "應用程式名稱")]
        //[StringLength(128, ErrorMessage = "使用的應用程式不能超過 128 個字元.")]
        public string HDClinicDept { get; set; }

        public string HDDeptName { get; set; }

        public string HDUserRowid { get; set; }

        public string HDUserName { get; set; }

        //[Display(Name = "科別名稱")]
        //public string HCDeptName { get; set; }

        [Display(Name = "顯示排列順序")]
        //[Required(ErrorMessage = "{0}不可以空白 !")]
        public int HDDisplaySeq { get; set; }
    }

    public class ViewModel_SysHospitalClinicRoom
    {
        public string HCRRowid { get; set; }
        
        public string HCRClinicRoom { get; set; }

        public string HCRClinicRoomName { get; set; }
      
        [Display(Name = "顯示排列順序")]
        //[Required(ErrorMessage = "{0}不可以空白 !")]
        public int HCRDisplaySeq { get; set; }
    }

    public class ViewModel_SysHospitalClinicDoctor
    {
        public string HCDRowid { get; set; }       

        //public string MGRowid { get; set; }

        //public string HospRowid { get; set; }

        //public string HCRowid { get; set; }

        public string UserRowid { get; set; }

        public string UserName { get; set; }
                
        public bool HCDBelongMedicalGroup { get; set; }

        [Display(Name = "第一階家醫照護病人分配額度")]
        public int HCDQuota { get; set; }

        [Display(Name = "第二階家醫照護病人分配額度")]
        public int HCDQuota2 { get; set; }

        [Display(Name = "第三階家醫照護病人分配額度")]
        public int HCDQuota3 { get; set; }

        [Display(Name = "顯示排列順序")]
        //[Required(ErrorMessage = "{0}不可以空白 !")]
        public int HCDDisplaySeq { get; set; }
    }

    public class ViewModel_InfoPanel
    {
        [Display(Name = "資料序號")]
        public string IPRowid { get; set; }

        [Display(Name = "醫事機構資料序號")]
        public string IPHospRowid { get; set; }

        [Display(Name = "醫事機構名稱")]
        public string IPHospName { get; set; }

        [Display(Name = "資訊面板名稱")]
        public string IPName { get; set; }

        [Display(Name = "顯示順序")]
        public int IPDisplaySeq { get; set; }

        [Display(Name = "資訊面板類別")]
        public string IPCBDCode { get; set; }

        public string IPCipher { get; set; }
    }
}