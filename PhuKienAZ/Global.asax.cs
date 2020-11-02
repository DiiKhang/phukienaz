using PhuKienAZ.Areas.Admin.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PhuKienAZ
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["lastCheckedOrderIdM"] = 0;
            Application["lastCheckedOrderIdS"] = 0;
            Application["lastCheckedActivityId"] = 0;
        }

        protected void Session_Start()
        {
            Session["user"] = null;
            Session["customer"] = null;
            Session["cart"] = null;
            Session["totalCartQuantity"] = 0;
            using (PhuKienAZEntities db = new PhuKienAZEntities())
            {
                if (HttpContext.Current.User.Identity.Name.Length > 0)
                    Session["user"] = db.Users.SingleOrDefault(x => x.Username == HttpContext.Current.User.Identity.Name);
            }
        }

    }
}
