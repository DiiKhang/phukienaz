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
    public class CustomersController : Controller
    {
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Admin/Customers
        public ActionResult Index()
        {
            return View(db.Customers.OrderByDescending(x => x.Id).ToList());
        }

        // GET: Admin/Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }


        // GET: Admin/Customers/Delete/5
        public ActionResult Disable(string id)
        {
            var customer = db.Customers.Find(id);
            customer.Disabled = true;
            db.SaveChanges();
            SystemLog.Add("05", "Vô hiệu hóa", id.ToString());
            return RedirectToAction("Index");
        }

        public ActionResult Enable(string id)
        {
            var customer = db.Customers.Find(id);
            customer.Disabled = false;
            db.SaveChanges();
            SystemLog.Add("05", "Kích hoạt", id.ToString());
            return RedirectToAction("Index");
        }

        // POST: Admin/Customers/Delete/5

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
