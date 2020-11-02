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
    public class ManufacturersController : Controller
    {
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Admin/Manufacturers
        public ActionResult Index()
        {
            return View(db.Manufacturers.Where(x => !x.Deleted).OrderByDescending(x => x.Id).ToList());
        }

        // GET: Admin/Manufacturers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer.Deleted && !((User)Session["user"]).Manager)
            {
                return RedirectToAction("Index");
            }
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // GET: Admin/Manufacturers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Manufacturers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Deleted")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                SystemLog.Add("02", "Tạo mới",db.Manufacturers.ToList().Last().Id.ToString());
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        // GET: Admin/Manufacturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: Admin/Manufacturers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Deleted")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                SystemLog.Add("02", "Sửa", manufacturer.Id.ToString());
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }


        public ActionResult Delete(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (db.Products.FirstOrDefault(x => x.ManufacturerId == manufacturer.Id) != null)
            {
                TempData["erorMessageAlert"] = "<script>alert('Không thể xóa mục này vì thông tin này đang được dùng ở một nơi khác')</script>";
                return RedirectToAction("Index");
            }
            manufacturer.Deleted = true;
            db.SaveChanges();
            SystemLog.Add("02", "Xóa", id.ToString());
            return RedirectToAction("Index");
        }
        public ActionResult Restore(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (!manufacturer.Deleted)
            {
                TempData["erorMessageAlert"] = "<script>alert('Mục này chưa bị xóa')</script>";
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                manufacturer.Deleted = false;
                db.SaveChanges();
                SystemLog.Add("02", "Khôi phục", id.ToString());
                TempData["erorMessageAlert"] = "<script>alert('Khôi phục dữ liệu thành công')</script>";
                return RedirectToAction("Details", new { id = id });
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
