namespace ServicesWeb.Areas.Admin
{
    using System.Web.Mvc;

    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // context.MapRoute("Admin_default", "admin/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}