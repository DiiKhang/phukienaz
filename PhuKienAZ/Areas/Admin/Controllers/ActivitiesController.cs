using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Security;

namespace PhuKienAZ.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin", Roles = "Manager")]
    public class ActivitiesController : Controller
    {
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Admin/Activities
        public ActionResult Index()
        {
            ViewBag.lastCheckedActivityId = System.Web.HttpContext.Current.Application["lastCheckedActivityId"];

            var activities = db.Activities.Include(a => a.Controller).Include(a => a.User).OrderByDescending(x => x.Id).ToList();

            System.Web.HttpContext.Current.Application["lastCheckedActivityId"] = activities.First().Id;

            return View(activities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
