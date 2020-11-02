using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Areas.Customer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PhuKienAZ.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        PhuKienAZEntities db = new PhuKienAZEntities();

        public ActionResult Index()
        {
            var customer = (Admin.Models.DataModel.Customer)Session["customer"];
            
            if (customer == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var orders = db.Orders.Where(x => x.CustomerId == customer.Id).ToList();
            return View(orders);
        }

        public ActionResult Details(int id)
        {
            var customer = (Admin.Models.DataModel.Customer)Session["customer"];
            var order = db.Orders.Find(id);
            if (customer == null)
            {
                return RedirectToAction("Index", "Home");
            } else if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(order);
        }

        public ActionResult Delete(int id)
        {
            var customer = (Admin.Models.DataModel.Customer)Session["customer"];
            var order = db.Orders.Find(id);
            if (customer == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (order == null || order.Deleted || order.Completed)
            {
                return RedirectToAction("Index", "Home");
            } else
            {
                TempData["eror"] = "<script>alert('Hủy đơn hàng thành công')</script>";
                order.Deleted = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult PlaceOrder()
        {
            if (Session["customer"] == null || Session["cart"] == null)
                return RedirectToAction("Index", "Home");
            ViewBag.payByBank = false;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PlaceOrder(string description, string destination, bool? payByBank)
        {
            var customer = (Admin.Models.DataModel.Customer)Session["customer"];
            var cartItems = (List<CartItem>)Session["cart"];

            

            if (Session["cart"] == null || customer == null)
                return RedirectToAction("Index", "Home");
            if (payByBank.Value)
            {                
                string tranferResult = await TranferMoney(customer.BankAccountCode, cartItems.Sum(x => x.Quantity * x.Price));
                if (tranferResult != "null")
                {
                    TempData["eror"] = "<script>alert('"+ tranferResult + "')</script>";
                    ViewBag.payByBank = payByBank.Value;
                    return View();
                }
            }
            description = (description.Length == 0) ? null : description;
            destination = (destination.Length == 0) ? customer.Address : destination;
            //add new order
            var newOrder = new Order()
            {
                CustomerId = customer.Id,
                Description = description,
                Destination = destination,
                PayByBank = payByBank.Value,
                Date = DateTime.Now
            };
            db.Orders.Add(newOrder);
            db.SaveChanges();
            //add new orderdetails
            int newOrderId = newOrder.Id;
            foreach (var item in cartItems)
            {
                db.OrderDetails.Add(new OrderDetails
                {
                    OrderId = newOrderId,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }
            db.SaveChanges();
            //reset cart
            Session["cart"] = null;
            Session["totalCartQuantity"] = 0;
            Session["newOrderId"] = newOrderId;
            return RedirectToAction("Success");
        }

        private async Task<string> TranferMoney(string customerAccountCode, int total)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost:50903/HDBank/chuyen-khoan/" + customerAccountCode + "/1234567891/" + total);
                var response = await client.GetAsync(uri);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public ActionResult Success()
        {
            if (Session["newOrderId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.newOrderId = Session["newOrderId"];
            Session["newOrderId"] = null;
            return View();
        }
    }
}