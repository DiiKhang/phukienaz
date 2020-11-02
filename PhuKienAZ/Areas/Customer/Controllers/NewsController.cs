using PagedList;
using PhuKienAZ.Areas.Admin.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhuKienAZ.Areas.Customer.Controllers
{
    public class NewsController : Controller
    {
        PhuKienAZEntities db = new PhuKienAZEntities();
        // GET: Customer/News
        public ActionResult Index(int? page)
        {
            var news = db.News.Where(x => !x.Deleted).OrderByDescending(x=>x.Date).ToList();
            return View(news.ToPagedList(page ?? 1, 5));
        }

        public ActionResult Details(int id)
        {
            var news = db.News.Find(id);
            if (news == null)
            {
                return RedirectToAction("Index");
            }
            else if (news.Deleted)
            {
                return RedirectToAction("Index");
            }
            return View(news);
        }

    }
}