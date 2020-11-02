using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhuKienAZ.Areas.Customer.Controllers
{
    public class LoginController : Controller
    {
        PhuKienAZEntities db = new PhuKienAZEntities();
        // GET: Customer/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login (string username, string password)
        {
            string md5Pass = Encryptor.MD5Hash(password);
            var customer = db.Customers.SingleOrDefault(x => x.Username == username && x.Password == md5Pass);
            if(customer == null)
            {
                ViewBag.Eror = "<script>alert('Sai tài khoản hoặc mật khẩu')</script>";
                return View();
            } else
            {
                Session["customer"] = customer;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Logout()
        {
            Session["customer"] = null;
            return RedirectToAction("Login", "Login");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword (string oldPassword, string newPassword, string reNewPassword)
        {
            var customer = db.Customers.Find(((Admin.Models.DataModel.Customer)Session["customer"]).Id);
            if (customer.Password != Encryptor.MD5Hash(oldPassword))
            {
                ViewBag.Eror = "<script>alert('Sai mật khẩu')</script>";
                return View();
            }
            else if (reNewPassword != newPassword)
            {
                ViewBag.Eror = "<script>alert('Xác nhận mật khẩu mới không chính xác')</script>";
                return View();
            }
            else if(oldPassword == newPassword)
            {
                ViewBag.Eror = "<script>alert('Mật khẩu mới trùng mật khẩu cũ')</script>";
                return View();
            } else
            {
                customer.Password = Encryptor.MD5Hash(newPassword);
                db.SaveChanges();
                ViewBag.Eror = "<script>alert('Đổi mật khẩu thành công')</script>";
                return View();
            }
        }
    }
}