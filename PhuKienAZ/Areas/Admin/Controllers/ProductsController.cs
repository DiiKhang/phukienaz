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
    public class ProductsController : Controller
    {   
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Where(x => !x.Deleted).Include(p => p.Category).Include(p => p.Manufacturer);
            return View(products.OrderByDescending(x => x.Id).ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product.Deleted && !((User)Session["user"]).Manager)
            {
                return RedirectToAction("Index");
            }
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.Where(x => !x.Deleted), "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.Where(x => !x.Deleted), "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase pictureFile)
        {
            if (db.Products.ToList().Count == 0)
            {
                product.Id = "SP001";
            }
            else
            {
                string lastProductId = db.Products.ToList().Last().Id;
                product.Id = "SP" + (int.Parse(lastProductId.Substring(2)) + 1).ToString().PadLeft(3, '0');
            }
            if (pictureFile == null)
            {
                ViewBag.eror += pictureFile.FileName;
                return View();
            }
            else
            {
                pictureFile.SaveAs(Server.MapPath("~/Areas/Admin/Content/images/products/" + product.Id + ".jpg"));
                product.Picture = "/Areas/Admin/Content/images/products/" + product.Id + ".jpg";
            }
            if (ModelState.IsValid)
            {
                //Kiem tra file upload
                db.Products.Add(product);
                db.SaveChanges();
                SystemLog.Add("03", "Tạo mới",db.Products.ToList().Last().Id.ToString());
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories.Where(x => !x.Deleted), "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.Where(x => !x.Deleted), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories.Where(x=>!x.Deleted), "Id", "Name", product.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.Where(x=>!x.Deleted), "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase pictureFile)
        {
            if (pictureFile != null)
            {
                pictureFile.SaveAs(Server.MapPath("~/Areas/Admin/Content/images/products/" + product.Id + ".jpg"));
            }
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                SystemLog.Add("03", "Sửa", product.Id.ToString());
                return RedirectToAction("Index");
            }
            
            ViewBag.CategoryId = new SelectList(db.Categories.Where(x => !x.Deleted), "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers.Where(x => !x.Deleted), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(string id)
        {
            Product product = db.Products.Find(id);
            //string fileName = product.Picture.Split('/')[6];
            //string filePath = Server.MapPath("~/Areas/Admin/Content/images/products/" + fileName);
            //if (System.IO.File.Exists(filePath))
            //    System.IO.File.Delete(filePath);
            product.Deleted = true;
            db.SaveChanges();
            SystemLog.Add("03", "Xóa", id.ToString());
            return RedirectToAction("Index");
        }
        public ActionResult Restore(string id)
        {
            Product product = db.Products.Find(id);
            if (!product.Deleted)
            {
                TempData["erorMessageAlert"] = "<script>alert('Mục này chưa bị xóa')</script>";
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                product.Deleted = false;
                db.SaveChanges();
                SystemLog.Add("03", "Khôi phục", id.ToString());
                TempData["erorMessageAlert"] = "<script>alert('Khôi phục dữ liệu thành công')</script>";
                return RedirectToAction("Details", new { id = id });
            }
        }

        // POST: Admin/Products/Delete/5

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
