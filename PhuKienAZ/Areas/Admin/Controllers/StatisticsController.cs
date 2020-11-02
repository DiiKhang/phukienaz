using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhuKienAZ.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin", Roles = "Manager")]
    public class StatisticsController : Controller
    {
        PhuKienAZEntities db = new PhuKienAZEntities();
        // GET: Admin/Statistics
        public ActionResult SoldproductByMonth(int? year)
        {
            // số lượng sản phẩm bán ra theo tháng
            int sYear = year ?? DateTime.Now.Year;

            ViewBag.year = sYear;

            ViewBag.soldProductByMonths = db.Orders.Where(x => x.Date.Value.Year == sYear && !x.Deleted && x.Completed).ToList().Select(o => new { o.Date.Value.Month, o.OrderDetails }).GroupBy(x => new { x.Month }, (key, group) => new
            {
                Month = key.Month,
                totalQuantity = group.Sum(x => x.OrderDetails.Sum(y => y.Quantity))
            }).ToList().ToDictionary(g => g.Month, g => g.totalQuantity);
            //ViewBag.soldProductByMonths = db.Orders.Where(x => x.Date.Value.Year == sYear && !x.Deleted && x.Completed).ToList().GroupBy(x => x.Date.Value.Month).ToDictionary(g => g.Key, g => g.First().OrderDetails.Sum(x => x.Quantity));
            return View();
        }

        public ActionResult MoneyByMonth(int? year)
        {
            // số tiền bán hàng theo tháng
            int sYear = year ?? DateTime.Now.Year;

            ViewBag.year = sYear;

            ViewBag.moneyByMonth = db.Orders.Where(x => x.Date.Value.Year == sYear && !x.Deleted && x.Completed).ToList().Select(o => new { o.Date.Value.Month, o.OrderDetails }).GroupBy(x => new { x.Month }, (key, group) => new
            {
                Month = key.Month,
                totalMoney = group.Sum(x => x.OrderDetails.Sum(y => y.Quantity*y.Price))
            }).ToList().ToDictionary(g => g.Month, g => g.totalMoney);

            return View();
        }

        public ActionResult ProByCat()
        {
            ViewBag.ProByCat = db.Products.Where(x => !x.Deleted).ToList().GroupBy(x => x.CategoryId).ToDictionary(x => x.First().Category.Name, x => x.Count());

            return View();
        }
    }
}