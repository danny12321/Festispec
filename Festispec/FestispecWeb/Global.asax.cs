using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FestispecWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
           
            HttpContext context = HttpContext.Current;
            // CheckSession() inlined
            if (Context.Request.Url.LocalPath != "/")
            {
                if (context.Session["username"] == null )
                {
                    Response.Redirect("~/");
                }
            }
        }
    }
}
