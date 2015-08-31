namespace FindHomeMadeFoodNearMe.Web.Filters
{
    using System.Configuration;
    using System.Web.Mvc;

    public class BaseAddressFilterAttribute : ActionFilterAttribute
    {

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.ServiceBaseAddress = GetValueFromConfiguration("ServiceBaseAddress");
            filterContext.Controller.ViewBag.WebSiteBaseAddress = GetValueFromConfiguration("WebsiteBaseAddress");
        }

        private string GetValueFromConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}