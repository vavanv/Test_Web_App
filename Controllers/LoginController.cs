using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.AppDbContext;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var db = new WebAppDbContext();
            try
            {
                var email = new SqlParameter("@email", model.Email.Trim());
                var password = new SqlParameter("@password", model.Password.Trim());
                var isValidUser = new SqlParameter("@isValidUser", System.Data.SqlDbType.Bit);
                isValidUser.Direction = System.Data.ParameterDirection.Output;

                db.Database.ExecuteSqlCommand("EXEC CheckUserCredentials @email, @password , @isValidUser OUT", email, password, isValidUser);
                var isvalid = (bool)isValidUser.Value;

                if (!isvalid)
                {
                    ModelState.AddModelError("", "Cannot login. Please check email and/or password");
                    return View(model);
                }

                Contact contact = db.Contacts.Where(c => c.Email == model.Email).FirstOrDefault();

                TempData["contact"] = contact;
                return RedirectToAction("Details");

            } finally
            {
                db.Dispose();
            }
        }

        public ActionResult Details (Contact model)
        {
            var contact = (Contact)TempData["contact"];
            return View(contact);
        }
    }
}