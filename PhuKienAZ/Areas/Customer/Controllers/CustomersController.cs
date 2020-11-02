using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhuKienAZ.Areas.Admin.Models.DataModel;
using PhuKienAZ.Security;

namespace PhuKienAZ.Areas.Customer.Controllers
{
    public class CustomersController : Controller
    {
        private PhuKienAZEntities db = new PhuKienAZEntities();

        // GET: Customer/Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin.Models.DataModel.Customer customer, string RePassword)
        {
            if (db.Customers.SingleOrDefault(x => x.Username == customer.Username) != null)
            {
                ViewBag.UsernameEror = "Tài khoản đã tồn tại";
                return View(customer);
            }
            if (RePassword != customer.Password)
            {
                ViewBag.UsernameEror = "Xác nhận mật khẩu không chính xác";
                return View(customer);
            }
            customer.Id = Guid.NewGuid().ToString().Replace("-", string.Empty);

            customer.Password = Encryptor.MD5Hash(customer.Password);

            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Login", "Login");
        }

        // GET: Customer/Customers/Edit/5
        public ActionResult Edit()
        {           
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var customer = db.Customers.Find(((Admin.Models.DataModel.Customer)Session["customer"]).Id);
            return View(customer);
        }

        // POST: Customer/Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Admin.Models.DataModel.Customer customer)
        {
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var oldCustomers = db.Customers.Find(customer.Id);
            if (oldCustomers == null)
            {
                return View(oldCustomers);
            }
            oldCustomers.Name = customer.Name;
            oldCustomers.Phone = customer.Phone;
            oldCustomers.BankAccountCode = customer.BankAccountCode;
            oldCustomers.Email = customer.Email;
            oldCustomers.Address = customer.Address;
            oldCustomers.Male = customer.Male;
            db.SaveChanges();
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
