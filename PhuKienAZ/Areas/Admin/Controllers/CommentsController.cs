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
    public class CommentsController : Controller
    {
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Admin/Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Customer).Include(c => c.Product);
            return View(comments.Where(x=>!x.Deleted).OrderByDescending(x => x.Id).ToList());
        }
             
        public ActionResult Delete(int? id)
        {
            Comment comment = db.Comments.Find(id);
            comment.Deleted = true;
            db.SaveChanges();
            SystemLog.Add("06", "Xóa", id.ToString());
            return RedirectToAction("Index");
        }

        // POST: Admin/Comments/Delete/5
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
