using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Areas.Admin.Utilities;
using PhuKienAZ.Security;

namespace PhuKienAZ.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class NewsController : Controller
    {
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Admin/News
        public ActionResult Index()
        {
            var news = db.News.Include(n => n.User);
            return View(news.Where(x=> !x.Deleted).OrderByDescending(x => x.Id).ToList());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news.Deleted && !((User)Session["user"]).Manager)
            {
                return RedirectToAction("Index");
            }
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( News news, HttpPostedFileBase pictureFile)
        {

            if (pictureFile == null)
            {
                ViewBag.eror += pictureFile.FileName;
                return View();
            }
            if (ModelState.IsValid)
            {
                news.Date = DateTime.Now;
                news.UserId = ((User)(Session["user"])).Id;

                var newId = db.News.ToList().Last().Id + 1;
                pictureFile.SaveAs(Server.MapPath("~/Areas/Admin/Content/images/news/news-" + newId + ".jpg"));
                news.Picture = "/Areas/Admin/Content/images/news/news-" + newId + ".jpg";

                db.News.Add(news);
                db.SaveChanges();

                

                SystemLog.Add("04", "Tạo mới",db.News.ToList().Last().Id.ToString());
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", news.UserId);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", news.UserId);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase pictureFile, News news)
        {
            if (pictureFile != null)
            {
                pictureFile.SaveAs(Server.MapPath("~/Areas/Admin/Content/images/news/news-" + news.Id + ".jpg"));
            }
            if (ModelState.IsValid)
            {
                var uNews = db.News.Find(news.Id);
                uNews.Title = news.Title;
                uNews.Content = news.Content;
                uNews.Brief = news.Brief;
                db.SaveChanges();
                SystemLog.Add("04", "Sửa", news.Id.ToString());
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", news.UserId);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            News news = db.News.Find(id);
            news.Deleted = true;
            db.SaveChanges();
            SystemLog.Add("04", "Xóa", id.ToString());
            return RedirectToAction("Index");
        }
        public ActionResult Restore(int id)
        {
            News news = db.News.Find(id);
            if (!news.Deleted)
            {
                TempData["erorMessageAlert"] = "<script>alert('Mục này chưa bị xóa')</script>";
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                news.Deleted = false;
                db.SaveChanges();
                SystemLog.Add("04", "Khôi phục", id.ToString());
                TempData["erorMessageAlert"] = "<script>alert('Khôi phục dữ liệu thành công')</script>";
                return RedirectToAction("Details", new { id = id });
            }
        }
        // POST: Admin/News/Delete/5

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
