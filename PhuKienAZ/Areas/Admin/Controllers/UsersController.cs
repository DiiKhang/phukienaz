using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Areas.Admin.Utilities;
using PhuKienAZ.Security;

namespace PhuKienAZ.Areas.Admin.Controllers
{
    
    public class UsersController : Controller
    { 
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Admin/Users
        [AreaAuthorize("Admin", Roles = "Manager")]
        public ActionResult Index()
        {
            return View(db.Users.OrderByDescending(x => x.Id).ToList().Where(x => !x.Manager));
        }

        // GET: Admin/Users/Details/5
        [AreaAuthorize("Admin", Roles = "Manager")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        [AreaAuthorize("Admin", Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AreaAuthorize("Admin", Roles = "Manager")]
        public ActionResult Create(User user, string RePassword, HttpPostedFileBase pictureFile)
        {
            //kt tài khoản 
            if (db.Users.SingleOrDefault(x => x.Username == user.Username) != null)
            {
                ViewBag.UsernameEror = "Tên tài khoản đã được sử dụng !";
                return View(user);
            }
            //kt xác nhận mật khẩu 
            if (RePassword != user.Password)
            {
                ViewBag.RePasswordEror = "Xác nhận mật khẩu không chính xác";
                return View(user);
            }
            //kt file ảnh 
            if (pictureFile == null)
            {
                ViewBag.PictureEror = "Chưa chọn ảnh";
                return View(user);
            }
            else if (!".jpeg.png.jpg".Contains(pictureFile.FileName.Split('.').Last().ToLower()))
            {
                ViewBag.PictureEror = "Vui lòng chọn định dạng ảnh JPEG, JPG, PNG";
                return View(user);
            }
            pictureFile.SaveAs(Server.MapPath("~/Areas/Admin/Content/images/user/" + user.Username + ".jpg"));
            user.Picture = "/Areas/Admin/Content/images/user/" + user.Username + ".jpg";

            user.Id = Guid.NewGuid().ToString().Replace("-", string.Empty);

            user.Password = Encryptor.MD5Hash(user.Password);

            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Users/Edit/5
        [AreaAuthorize("Admin", Roles = "Manager")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                var manager = (User)Session["user"];
                return View(manager);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AreaAuthorize("Admin", Roles = "Manager")]
        public ActionResult Edit(User user, HttpPostedFileBase pictureFile)
        {
            var oldUser = db.Users.Find(user.Id);
            if (oldUser == null)
            {
                return View(user);
            }
            if (pictureFile == null)
            {
                ViewBag.PictureEror = "Chưa chọn ảnh";
                return View(user);
            }
            else if (!".jpeg.png.jpg".Contains(pictureFile.FileName.Split('.').Last().ToLower()))
            {
                ViewBag.PictureEror = "Vui lòng chọn định dạng ảnh JPEG, JPG, PNG";
                return View(user);
            }
            pictureFile.SaveAs(Server.MapPath("~/Areas/Admin/Content/images/user/" + oldUser.Username + ".jpg"));
            oldUser.Name = user.Name;
            oldUser.Phone = user.Phone;
            oldUser.Email = user.Email;
            oldUser.Male = user.Male;
            oldUser.Birthday = user.Birthday;
            oldUser.Address = user.Address;
 
            db.SaveChanges();
            SystemLog.Add("07", "Sửa", user.Id);
            return RedirectToAction("Index");

            //return RedirectToAction("Index");
        }

        // POST: Admin/Users/Delete/5
        [HttpPost]
        [AreaAuthorize("Admin", Roles = "Manager")]
        public ActionResult UpdateStatus(string id, bool disabled)
        {
            string eror = "";
            User user = db.Users.Find(id);
            if(user == null)
            {
                eror = "Tài khoản không hợp lệ";
            }
            else if(user.Disabled == disabled)
            {
                eror = (disabled) ? "Tài khoản đang bị vô hiệu hóa":"Tài khoản đang được kích hoạt";
            } else
            {
                user.Disabled = disabled;
                db.SaveChanges();
                SystemLog.Add("07", disabled ? "Vô hiệu hóa" : "Kích hoạt", id);
                db.SaveChanges();       
            }
            return Content(eror);
        }

        [AreaAuthorize("Admin")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AreaAuthorize("Admin")]
        public ActionResult ChangePassword(string oldPass, string newPass, string reNewPass)
        {
            var user = db.Users.Find(((User)Session["user"]).Id);
            if (Encryptor.MD5Hash(oldPass) != user.Password)
            {
                TempData["erorMessageAlert"] = "<script>alert('Mật khẩu cũ không đúng')</script>";
            }
            else if (newPass != reNewPass)
            {
                TempData["erorMessageAlert"] = "<script>alert('Xác nhận mật khẩu không đúng')</script>";
            }
            else if (newPass == oldPass)
            {
                TempData["erorMessageAlert"] = "<script>alert('Mật khẩu mới phải khác mật khẩu cũ')</script>";
            }
            else
            {
                TempData["erorMessageAlert"] = "<script>alert('Đổi mật khẩu thành công')</script>";
                user.Password = Encryptor.MD5Hash(newPass);
                db.SaveChanges();
            }
            return View();
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
