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
using PhuKienAZ.Areas.Admin.Utilities;

namespace PhuKienAZ.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class CategoriesController : Controller
    {
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Admin/Categories
        public ActionResult Index()
        {
            return View(db.Categories.Where(x => !x.Deleted).OrderByDescending(x => x.Id).ToList());
        }

        // GET: Admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category.Deleted && !((User)Session["user"]).Manager)
            {
                return RedirectToAction("Index");
            }
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                SystemLog.Add("01", "Tạo mới", db.Categories.ToList().Last().Id.ToString());
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Deleted")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                SystemLog.Add("01", "Sửa", category.Id.ToString());

                return RedirectToAction("Index");
            }
            return View(category);
        }


        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (db.Products.FirstOrDefault(x => x.CategoryId == category.Id) != null)
            {
                TempData["erorMessageAlert"] = "<script>alert('Không thể xóa mục này vì thông tin này đang được dùng ở một nơi khác')</script>";
                return RedirectToAction("Index");
            }
            category.Deleted = true;
            db.SaveChanges();
            SystemLog.Add("01", "Xóa", id.ToString());
            return RedirectToAction("Index");
        }

        public ActionResult Restore(int id)
        {
            Category category = db.Categories.Find(id);
            if (!category.Deleted)
            {
                TempData["erorMessageAlert"] = "<script>alert('Mục này chưa bị xóa')</script>";
                return RedirectToAction("Details", new { id = id });
            } else
            {
                category.Deleted = false;
                db.SaveChanges();
                SystemLog.Add("01", "Khôi phục", id.ToString());
                TempData["erorMessageAlert"] = "<script>alert('Khôi phục dữ liệu thành công')</script>";
                return RedirectToAction("Details", new { id = id});
            }
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
