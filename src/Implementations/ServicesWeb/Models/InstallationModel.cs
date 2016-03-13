namespace ServicesWeb.Models
{
    using Common;
    using Common.Web;

    public class InstallationModel
    {
        public bool DatabaseUpgradeAvailable => Database.UpgradeRequired;
    }
}