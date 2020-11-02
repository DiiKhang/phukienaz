using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PhuKienAZ.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class HomeController : Controller
    {
        PhuKienAZEntities db = new PhuKienAZEntities();
        // GET: Admin/Home
        [AreaAuthorize("Admin", Roles = "Manager")]
        public ActionResult Index()
        {
            var now = DateTime.Now;
            var orderByMonths = db.Orders.Where(x => x.Date.Value.Year == DateTime.Now.Year && !x.Deleted).GroupBy(x => x.Date.Value.Month).ToDictionary(g => g.Key, g => g.Count());
            ViewBag.OrderByMonths = orderByMonths;
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult GetNotifications()
        {
            var user = (User)Session["user"];
            int lastCheckedOrderId = 0;
            if (user.Manager)
            {
                lastCheckedOrderId = (int)System.Web.HttpContext.Current.Application["lastCheckedOrderIdM"];
            } else
            {
                lastCheckedOrderId = (int)System.Web.HttpContext.Current.Application["lastCheckedOrderIdS"];
            }
            int lastCheckedActivityId = (int)System.Web.HttpContext.Current.Application["lastCheckedActivityId"];
            int totalNewOrders = db.Orders.Count(x => x.Id > lastCheckedOrderId);
            int totalNewActivities = db.Activities.Count(x => x.Id > lastCheckedActivityId);
            return Content(totalNewOrders + "|" + totalNewActivities);
        }
    }
}