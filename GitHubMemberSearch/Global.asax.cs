using GitHubMemberSearch.App_Start;
using log4net;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GitHubMemberSearch
{
    [ExcludeFromCodeCoverage]
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.CreateMappings();
            log4net.Config.XmlConfigurator.Configure();
        }

        private static readonly ILog Log = LogManager.GetLogger(typeof(MvcApplication));

        private void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            Log.Error("App_Error", ex);
        }
    }
}