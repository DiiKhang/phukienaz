using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Areas.Admin.Models.ViewModel;
using System;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhuKienAZ.Security;

namespace PhuKienAZ.Areas.Admin.Controllers
{
    [AreaAuthorize("Admin")]
    public class OrdersController : Controller
    {
        
        private PhuKienAZEntities db = new PhuKienAZEntities();
        // GET: Admin/Orders
        public ActionResult Index()
        {
            var user = (User)Session["user"];

            if (user.Manager)
            {
                ViewBag.lastCheckedOrderId = System.Web.HttpContext.Current.Application["lastCheckedOrderIdM"];
                System.Web.HttpContext.Current.Application["lastCheckedOrderIdM"] = db.Orders.ToList().Last().Id;
            }
            else
            {
                ViewBag.lastCheckedOrderId = System.Web.HttpContext.Current.Application["lastCheckedOrderIdS"];
                System.Web.HttpContext.Current.Application["lastCheckedOrderIdS"] = db.Orders.ToList().Last().Id;
            }
            
            return View();
        }


        [HttpPost]
        public ActionResult GetList()
        {
            var orderdetails = (from od in db.OrderDetails
                                group od by new { OrderId = od.OrderId } into g
                                select new
                                {
                                    Id = g.Key.OrderId,
                                    TotalQuantity = g.Sum(od => od.Quantity),
                                    Total = g.Sum(od => od.Quantity * od.Price),
                                }).ToList();
            var orders = (from o in db.Orders.ToList()
                          join od in orderdetails on o.Id equals od.Id into temp
                          from od in temp.DefaultIfEmpty(new { o.Id, TotalQuantity = 0, Total = 0})
                          select new
                          {
                              Id = o.Id,
                              CustomerName = o.Customer.Name,
                              CustomerId = o.CustomerId,
                              PayType = (o.PayByBank) ? "Trực tuyến" : "Tiền mặt",
                              Status = (o.Deleted) ? "Đã hủy" : ((o.Completed) ? "Đã giao" : "Đang giao"),
                              Datetime = o.Date,
                              TotalQuantity = od.TotalQuantity,
                              Total = od.Total,
                          }).ToList();
            int totalRows = orders.ToList().Count;
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            if (!string.IsNullOrEmpty(searchValue))
            {
                orders = orders.Where(x =>
                x.Id.ToString().Contains(searchValue) ||
                x.CustomerName.ToLower().Contains(searchValue.ToLower()) ||
                x.PayType.Contains(searchValue) ||
                x.Datetime.ToString().ToLower().Contains(searchValue.ToLower()) ||
                x.Status.ToLower().Contains(searchValue.ToLower())).ToList();
            }
            int totalRowFiltered = orders.ToList().Count;
            orders = orders.OrderBy(sortColumnName + " " + sortDirection).ToList();
            if (length > 0)
                orders = orders.Skip(start).Take(length).ToList();
            else
                orders = orders.Skip(start).ToList();

            return Json(new { data = orders, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRowFiltered }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Complete(int id)
        {
            string eror = "";
            var order = db.Orders.Find(id);
            if (order == null)
                eror = "Bản ghi không tồn tại";           
            else if (order.Deleted)
                eror = "Đơn hàng đã hủy";
            else if (order.Completed)
                eror = "Đơn hàng đã giao";
            else
            {
                order.Completed = true;
                db.SaveChanges();
            }           
            return Content(eror);
        }


        public ActionResult Details (int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public ActionResult PrintOrder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


    }
}