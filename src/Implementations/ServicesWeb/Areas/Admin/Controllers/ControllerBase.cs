namespace ServicesWeb.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using ServicesWeb.Filters;

    [Authorize(Roles = "Application Admins")]
    [RouteArea("admin")]
    [IgnoreSiteMaintenance]
    public abstract class ControllerBase : Controller
    {
    }
}