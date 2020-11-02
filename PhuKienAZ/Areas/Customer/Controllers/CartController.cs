using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Areas.Customer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhuKienAZ.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        PhuKienAZEntities db = new PhuKienAZEntities();
        // GET: Customer/Cart
        public ActionResult Index()
        {
            var cart = (List<CartItem>)Session["cart"] ?? new List<CartItem>();
            return View(cart);
        }

        [HttpPost]
        public ActionResult UseCart(string productId, int quantity, bool isAdd)
        {
            var product = db.Products.Find(productId);
            if (product == null)
                return Content("Sản phẩm không tồn tại !");
            var cart = (List<CartItem>)Session["cart"] ?? new List<CartItem>();
            CartItem cartItem = cart.SingleOrDefault(x => x.ProductId == productId);
            if (isAdd)
            {
                if (quantity == 0)
                {
                    return Content("Số lượng sản phẩm phải lớn hơn 0 !");
                }
                if (cartItem == null)
                {
                    cart.Add(new CartItem
                    {
                        ProductId = productId,
                        CategoryName = product.Category.Name,
                        Price = product.Price,
                        Picture = product.Picture,
                        ProductName = product.Name,
                        Quantity = quantity
                    });
                }
                else
                {
                    cartItem.Quantity += quantity;
                }
            }
            else
            {
                if (cartItem == null)
                {
                    return Content("Giỏ hàng không có sản phẩm này !");
                }
                else
                {
                    if (quantity == 0)
                    {
                        cart.Remove(cartItem);
                    }
                    else
                    {
                        cartItem.Quantity = quantity;
                    }
                }
            }
            int totalItem = cart.Sum(x => x.Quantity);
            Session["cart"] = cart;
            Session["totalCartQuantity"] = totalItem;
            return Content(totalItem + "");
        }
    }
}