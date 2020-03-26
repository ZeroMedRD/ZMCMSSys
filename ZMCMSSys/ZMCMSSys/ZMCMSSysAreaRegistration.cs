using System.Web.Mvc;

namespace ZMCMS.Areas.ZMCMSSys
{
    public class ZMCMSSysAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ZMCMSSys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ZMCMSSys_default",
                "ZMCMSSys/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "ZMCMSSys.Controllers" }
            );
        }
    }
}