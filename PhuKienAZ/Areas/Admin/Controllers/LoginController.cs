using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PhuKienAZ.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            PhuKienAZEntities db = new PhuKienAZEntities();
            string md5Pass = Encryptor.MD5Hash(password);
            var user = db.Users.SingleOrDefault(x => x.Username == username && x.Password == md5Pass);
            if (user != null)
            {
                Session["user"] = user;
                FormsAuthentication.SetAuthCookie(username, false);
                if (user.Manager)
                    return Redirect(Request["ReturnUrl"] ?? "/Admin/Home/Index");
                else
                    return Redirect(Request["ReturnUrl"] ?? "/Admin/Orders/Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View();
            }
        }
    }
}