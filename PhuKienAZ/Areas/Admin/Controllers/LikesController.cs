using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Areas.Admin.Models.ViewModel;
using PhuKienAZ.Security;

namespace PhuKienAZ.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class LikesController : Controller
    {
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Admin/Likes
        public ActionResult Index()
        {
            var data = from l in db.Likes
                       join p in db.Products on l.ProductId equals p.Id
                       group l by new { l.ProductId, p.Name } into g
                       select new LikeViewModel
                       {
                           ProductId =  g.FirstOrDefault().ProductId,
                           CategoryName = g.FirstOrDefault().Product.Category.Name,
                           ProductName = g.FirstOrDefault().Product.Name,
                           Likes = g.Count()
                       };
            return View(data.OrderBy(x => x.ProductId).ToList());
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
