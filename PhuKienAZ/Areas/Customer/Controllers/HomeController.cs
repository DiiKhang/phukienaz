using PhuKienAZ.Areas.Admin.Models.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhuKienAZ.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        PhuKienAZEntities db = new PhuKienAZEntities();
        // GET: Customer/Home
        public ActionResult Index()
        {
            ViewBag.NewProducts = db.Products.Where(x => !x.Deleted).OrderByDescending(x => x.Id).Take(5).ToList();
            var orderDetails = (from od in db.OrderDetails
                                join o in db.Orders.Where(x => !x.Deleted) on od.OrderId equals o.Id
                                group od by new { od.ProductId } into g
                                select new
                                {
                                    g.Key.ProductId,
                                    TotalQuantity = g.Sum(od => od.Quantity)
                                }).OrderByDescending(x => x.TotalQuantity).Take(5).ToList();
            ViewBag.TopSelling = (from p in db.Products.Where(x=> !x.Deleted).ToList()
                                  join od in orderDetails on p.Id equals od.ProductId
                                  select p).ToList();
            ViewBag.News = db.News.Where(x=> !x.Deleted).OrderByDescending(x => x.Date).Take(3).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Like(string productId, bool like)
        {
            string result = "";
            var customer = (Admin.Models.DataModel.Customer)Session["customer"];
            var productLike = db.Likes.SingleOrDefault(x => x.CustomerId == customer.Id && x.ProductId == productId);
            int newLikeCount = 0;
            if (db.Products.Find(productId) == null)
            {
                result = "Sản phẩm không tồn tại !";
            }
            else
            {
                if (like)
                {
                    if (productLike == null)
                    {
                        db.Likes.Add(new Like() { CustomerId = customer.Id, ProductId = productId });
                        db.SaveChanges();
                        newLikeCount = db.Likes.Count(x => x.ProductId == productId);
                        result = newLikeCount + "." + like;
                    } else
                    {
                        result = "Đã thích sản phẩm này !";
                    }
                } else
                {
                    if (productLike != null)
                    {
                        db.Likes.Remove(productLike);
                        db.SaveChanges();
                        newLikeCount = db.Likes.Count(x => x.ProductId == productId);
                        result = newLikeCount + "." + like;
                    }
                    else
                    {
                        result = "Chưa thích sản phẩm này !";
                    }
                }
            }
            return Content(result);
        }
    }
}