using PhuKienAZ.Areas.Admin.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace PhuKienAZ.Areas.Customer.Controllers
{
    public class ProductController : Controller
    {
        PhuKienAZEntities db = new PhuKienAZEntities();
        // GET: Customer/Product
        public ActionResult Index(string id)
        {
            var product = db.Products.Find(id);
            if (product == null)
                return Redirect("/Customer/Home");
            ViewBag.RelatedProducts = db.Products.Where(x => x.CategoryId == product.CategoryId).Take(4);
            return View(product);
        }

        public ActionResult Liked(int? page)
        {
            var customer = (Admin.Models.DataModel.Customer)Session["customer"];
            if (customer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var products = from p in db.Products
                           join l in db.Likes.Where(x => x.CustomerId == customer.Id) on p.Id equals l.ProductId
                           select p;

            return View(products.ToList().ToPagedList(page ?? 1, 8));
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
                    }
                    else
                    {
                        result = "Đã thích sản phẩm này !";
                    }
                }
                else
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


        [HttpPost]
        public ActionResult Comment(string productId, string content)
        {
            if (Session["customer"] == null)
            {
                return Content("Bạn chưa đăng nhập !");
            }
            var product = db.Products.Find(productId);
            if (product == null)
            {
                return Content("Sản phẩm không tồn tại !");
            }
            else
            {
                string[] badWords = new[] { "địt", "cặc", "lồn", "cứt" };
                foreach (var item in badWords)
                {
                    if (content.ToLower().Contains(item))
                        return Content("Ăn nói mất dạy !");
                }
            }
            var comment = new Comment()
            {
                ProductId = productId,
                CustomerId = ((Admin.Models.DataModel.Customer)Session["customer"]).Id,
                Content = content,
                Datetime = DateTime.Now
            };
            db.Comments.Add(comment);
            db.SaveChanges();
            return GetComment(productId, 1);
        }

        class CommentViewModel
        {
            public string CustomerName { get; set; }
            public string Content { get; set; }
            public string Datetime { get; set; }
        }

        [HttpPost]
        public ActionResult GetComment(string productId, int page)
        {
            List<CommentViewModel> comments = new List<CommentViewModel>();
            var productComments = db.Comments.Include("Customer").Where(x => x.ProductId == productId && !x.Deleted).ToList();
            int totalComment = productComments.Count;
            var newComments = productComments.OrderByDescending(x => x.Id).ToList().Skip((page - 1) * 3).Take(3);
            foreach (var item in newComments)
            {
                comments.Add(new CommentViewModel
                {
                    CustomerName = item.Customer.Name,
                    Content = item.Content,
                    Datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
                });
            }
            return Content(totalComment + "|" + Newtonsoft.Json.JsonConvert.SerializeObject(comments));
        }
    }
}