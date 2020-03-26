using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ZMCMSSys.ViewModels
{
    public class ViewModel_ComboMasters
    {
        public string CBMRowid { get; set; }

        [Display(Name = "組合選單代碼")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        [MaxLength(30)]
        public string CBMClass { get; set; }

        [Display(Name = "組合選單說明")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        [MaxLength(50)]
        public string CBMDescription { get; set; }

        [Display(Name = "組合選單註解")]
        [MaxLength(100)]
        public string CBMRemark { get; set; }

        [Display(Name = "組合選單分組註解")]
        [MaxLength(2)]
        public string CBMGroup { get; set; }

        public DateTime CBMLastModifiedDateTime { get; set; }
                
        public string CBMModifiedUserRowid { get; set; }
    }

    public class ViewModel_ComboDetails
    {
        public string CBDRowid { get; set; }

        //public string CBMRowid { get; set; }

        public string CBDCode { get; set; }
               
        [Display(Name = "組合選單說明")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        [MaxLength(50)]
        public string CBDDescription { get; set; }

        public bool CBDDisplayFlag { get; set; }

        public int CBDDisplayOrder { get; set; }
        
        public DateTime CBDLastModifiedDateTime { get; set; }

        public string CBDModifiedUserRowid { get; set; }
    }

    public class ViewModel_ComboHMaster
    {
        public string CBMRowid { get; set; }

        [Display(Name = "片語所屬醫療機構代碼")]
        public string CBMHospRowid { get; set; }

        [Display(Name = "組合選單代碼")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        [MaxLength(30)]
        public string CBMClass { get; set; }

        [Display(Name = "組合選單說明")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        [MaxLength(50)]
        public string CBMDescription { get; set; }

        [Display(Name = "組合選單註解")]
        [MaxLength(100)]
        public string CBMRemark { get; set; }

        [Display(Name = "組合選單分組註解")]
        [MaxLength(2)]
        public string CBMGroup { get; set; }

        public DateTime CBMLastModifiedDateTime { get; set; }

        public string CBMModifiedUserRowid { get; set; }
    }

    public class ViewModel_ComboHDetail
    {
        public string CBDRowid { get; set; }

        //public string CBMRowid { get; set; }

        public string CBDCode { get; set; }

        [Display(Name = "組合選單說明")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        [MaxLength(50)]
        public string CBDDescription { get; set; }

        public bool CBDDisplayFlag { get; set; }

        public int CBDDisplayOrder { get; set; }

        public DateTime CBDLastModifiedDateTime { get; set; }

        public string CBDModifiedUserRowid { get; set; }
    }

    public class ViewModel_Roles
    {
        public string SRRowid { get; set; }

        [Display(Name = "角色名稱")]
        [Required(ErrorMessage = "{0}不可以空白 !")]        
        public string SRName { get; set; }

        [Display(Name = "選單顯示名稱")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        [MaxLength(255)]
        public string SRDisplayName { get; set; }

        [Display(Name = "角色詳細說明")]
        [MaxLength(255)]
        public string SRDescript { get; set; }

        [Display(Name = "角色選單網址超連結")]
        [MaxLength(255)]
        public string SRUrl { get; set; }

        [Display(Name = "選單代表圖示")]        
        [MaxLength(100)]
        public string SRFont { get; set; }        
    }

    public class ViewModel_AspNetUsers
    {
        public string Id { get; set; }

        public string UserRowid { get; set; }

        [Display(Name = "使用者證號")]
        public string UserNo { get; set; }

        [Display(Name = "使用者帳號")]        
        public string UserName { get; set; }
                
        [Display(Name = "姓名")]
        public string UserRealName { get; set; }

        public string UserPhoto { get; set; }

        [Display(Name = "電子郵件")]        
        public string Email { get; set; }
        
        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        [Display(Name = "職稱角色名稱")]        
        public string UserProIdentityDescription { get; set; }

        [Display(Name = "所屬機構名稱")]
        public string UserBelongCompanyDescription { get; set; }       
       
        public string UserIdno { get; set; }

        [Display(Name = "停用日期及時間")]
        public DateTime ? LockoutEndDateUtc { get; set; }
    }

    public class ViewModel_Programs
    {
        public string SPRowid { get; set; }

        [Display(Name = "應用程式名稱")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        [MaxLength(255)]
        public string SPName { get; set; }

        [Display(Name = "應用程式路徑")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        public string SPUrl { get; set; }
        
        [Display(Name = "應用程式代表圖示")]
        [StringLength(100, ErrorMessage = "應用程式簡碼不能超過 100 個字元.")]        
        public string SPFont { get; set; }

        [Display(Name = "應用程式簡碼")]
        [StringLength(2, ErrorMessage = "應用程式簡碼不能超過 2 個字元.")]        
        public string SPSysNo { get; set; }
    }

    public class ViewModel_RolePrograms
    {
        public string SRPRowid { get; set; }

        //public string SRRowid { get; set; }

        public string SPRowid { get; set; }

        //[Display(Name = "應用程式名稱")]
        //[StringLength(128, ErrorMessage = "使用的應用程式不能超過 128 個字元.")]
        public string SPName { get; set; }

        //[Display(Name = "選單焦點")]
        public bool SRPActive { get; set; }

        //[Display(Name = "顯示排列順序")]
        //[Required(ErrorMessage = "{0}不可以空白 !")]
        public int SRPDisplaySeq { get; set; }        
    }
    
    public class ViewModel_MedicalGroups
    {
        public string MGRowid { get; set; }

        [Display(Name = "醫療群名稱")]
        [Required(ErrorMessage = "{0}不可以空白 !")]
        public string MGName { get; set; }

        [Display(Name = "醫療群詳細說明")]
        [MaxLength(255)]
        public string MGDescript { get; set; }
               
        [Display(Name = "醫療群代表圖示")]
        [MaxLength(100)]
        public string MGFont { get; set; }
    }

    public class ViewModel_MedicalGroupHospitals
    {
        public string MGHRowid { get; set; }

        //public string MGRowid { get; set; }

        public string HospRowid { get; set; }

        //[Display(Name = "應用程式名稱")]
        //[StringLength(128, ErrorMessage = "使用的應用程式不能超過 128 個字元.")]
        public string HospName { get; set; }

        //[Display(Name = "顯示排列順序")]
        //[Required(ErrorMessage = "{0}不可以空白 !")]
        public int MGHDisplaySeq { get; set; }
    }

    public class ViewModel_GetProgram
    {
        public string SPRowid { get; set; }

        public string SPName { get; set; }
    }

    public class ViewModel_GetHospital
    {
        public string HospRowid { get; set; }
        
        public string HospName { get; set; }   
        
        public string HospActive { get; set; }
    }

    public class ViewModel_SysUser
    {
        public string Id { get; set; }

        public string UserRowid { get; set; }

        [Required]
        //[EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "姓名")]
        public string UserName { get; set; }        
    }

    public class ViewModel_GetUserData
    {
        public string UserNo { get; set; }

        public string UserRowid { get; set; }

        public string UserName { get; set; }

        public string UserIdno { get; set; }

        //public string HospActive { get; set; }
    }

    public class ViewModel_GetRole
    {
        public string SRRowid { get; set; }

        public string SRName { get; set; }
    }

    public class ViewModel_GetMedicalGroup
    {
        public string MGRowid { get; set; }

        public string MGName { get; set; }
    }

    public class ViewModel_SysUserRoleTemplate
    {
        public string SURRowid { get; set; }
        
        //[Display(Name = "顯示排列順序")]
        //[Required(ErrorMessage = "{0}不可以空白 !")]
        public int SURDisplaySeq { get; set; }

        //public string URRowid { get; set; }

        //[Display(Name = "應用程式名稱")]
        //[StringLength(128, ErrorMessage = "使用的應用程式不能超過 128 個字元.")]
        public string SRRowid { get; set; }

        public string SRName { get; set; }
    }


    public class ViewModel_SHCDCombo
    {
        [Display(Name = "資料編號")]
        public string CBDRowid { get; set; }

        [Display(Name = "資料值")]
        public string CBDCode { get; set; }

        [Display(Name = "名稱")]
        public string CBDDescription { get; set; }

        [Display(Name = "顯示排列順序")]
        public int CBDDisplayOrder { get; set; }
    }


    public class ViewModel_SysHCDept
    {
        [Display(Name = "資料編號")]
        public string SHDRowid { get; set; }

        [Display(Name = "連結值")]
        public string CBDRowid { get; set; }
                
        [Display(Name = "名稱代碼")]
        public string SHDCode { get; set; }

        [Display(Name = "名稱")]
        public string CBDDescription { get; set; }

        [Display(Name = "顯示排列順序")]
        public int SHDDisplayOrder { get; set; }
    }

    public class ViewModel_OpinionReactionEmp
    {
        [Display(Name = "資料編號")]
        public string ORERowid { get; set; }

        [Display(Name = "醫事機構資料編號")]
        public string HospRowid { get; set; }

        [Display(Name = "醫事機構名稱")]
        public string HospName { get; set; }

        [Display(Name = "使用者資料編號")]
        public string OREUserRowid { get; set; }

        [Display(Name = "姓名")]
        public string UserName { get; set; }

        [Display(Name = "電子郵件")]
        public string UserEmail { get; set; }

        public DateTime OREDateTime { get; set; }
    }
}