using PhuKienAZ.Areas.Admin.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace PhuKienAZ.Areas.Customer.Controllers
{
    public class SearchController : Controller
    {
        PhuKienAZEntities db = new PhuKienAZEntities();
        // GET: Customer/Search
        public ActionResult Index()
        {
            Session["searchProductName"] = Request["searchProductName"] ?? "";
            var searchProductName = Session["searchProductName"].ToString();

            var products = db.Products.Where(x => !x.Deleted).ToList();

            if (searchProductName != "")
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchProductName)).ToList();
            }

            ViewBag.Categories = products.GroupBy(x => x.CategoryId).Select(g => g.First().Category).ToList();
            ViewBag.Manufacturers = products.GroupBy(x => x.ManufacturerId).Select(g => g.First().Manufacturer).ToList();

            return View();

        }

        [HttpPost]
        public ActionResult UpdateSearchResult(string categoryNames, string manufacturerNames, int page, string order, int minPrice, int maxPrice)
        {
            var products = db.Products.Where(x => !x.Deleted).OrderBy("Name asc").ToList();

            products = products.Where(x => categoryNames.Contains(x.Category.Name)).ToList();
            products = products.Where(x => manufacturerNames.Contains(x.Manufacturer.Name)).ToList();
            products = products.Where(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
            products = products.OrderBy(order).ToList();

            TempData["TotalProduct"] = products.Count;

            return PartialView(products.Skip((page - 1) * 9).Take(9).ToList());
        }
    }
}