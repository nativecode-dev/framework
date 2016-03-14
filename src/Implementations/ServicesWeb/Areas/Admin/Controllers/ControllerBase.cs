namespace ServicesWeb.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using Common.Web.Filters;

    [Authorize(Roles = "NATIVECODE\\Domain Admins")]
    [RouteArea("admin")]
    [IgnoreSiteMaintenance]
    public abstract class ControllerBase : Controller
    {
    }
}