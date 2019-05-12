using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Sarif_Industries.Data_Access_Layer;
using Sarif_Industries.Models;
using Sarif_Industries.View_Models;
using Sarif_Industries.PartialModels;
using System;
using System.Globalization;
using Sarif_Industries.Security;

namespace Sarif_Industries.Controllers
{
    public class UsersController : ApplicationController
    {
        private UserRegistration userRegistration = new UserRegistration();
        private User_PM userPM = new User_PM();

        public ActionResult UserProfile()
        {
            AccountProfile accountProfile = new AccountProfile();


            if (Session["UserId"] != null)
                accountProfile.User = ModelContext.Users.Find(UserIdSession);
            else
                return RedirectToAction("Login", "Users");


            accountProfile.Orders = ModelContext.Orders.Where(o => o.UserId == UserIdSession);

            accountProfile.Products = (from p in ModelContext.Products
                                       join o in ModelContext.Orders
                                       on p.ProductId equals o.ProductId
                                       select p);

            accountProfile.ProductImage = (from p in ModelContext.ProductImages
                                           join o in ModelContext.Orders
                                           on p.ProductId equals o.ProductId
                                           where p.IsDefaultImage == true
                                           select p);

            accountProfile.CountryList = Country_PM.GetCountries().FirstOrDefault();

            accountProfile.CountryList = (from c in Country_PM.GetCountries()
                                          join u in ModelContext.Users
                                          on c.CountryID equals u.Country
                                          where u.UserId == UserIdSession
                                          select c).FirstOrDefault();



            return View(accountProfile);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = ModelContext.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Register()
        {
            userRegistration.Country = Country_PM.GetCountries();

            return View(userRegistration);
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserId,Email,Password,Forename,Surname,Address,Phone,PostCode,Country")] User user)
        {
            userRegistration.Country = Country_PM.GetCountries();


            if (ModelState.IsValid)
            {
                userPM.AddUser(user);

                Session["UserID"] = user.UserId.ToString();
                Session["Name"] = user.Forename.ToString();

                if (Session[UserIDKey] != null)
                {
                    int userId = Convert.ToInt32(Session[UserIDKey]);
                    Cart_PM.MigrateCart(userId);
                }


                return RedirectToAction("Index", "Home");
            }


            return View(userRegistration);
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            User userEmail = ModelContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();

            if (userEmail != null)
            {
                CustomPasswordHasher customPasswordHasher = new CustomPasswordHasher();

                bool pass = customPasswordHasher.CustomVerifyHashedPassword(userEmail.Password, user.Password);

                User userCredentials = ModelContext.Users.Where(a => a.Email.Equals(user.Email)).FirstOrDefault();

                if (userCredentials != null && pass)
                {
                    Session["UserID"] = userCredentials.UserId.ToString();
                    Session["Name"] = userCredentials.Forename.ToString();

                    if (Session[UserIDKey] != null)
                    {
                        int userId = Convert.ToInt32(Session[UserIDKey]);
                        Cart_PM.MigrateCart(userId);
                    }


                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Message = "Incorrect Credentials";
            return View(user);
        }

        public JsonResult CheckEmail(string userEmail)
        {
            System.Threading.Thread.Sleep(200);
            var SearchData = ModelContext.Users.Where(x => x.Email == userEmail).SingleOrDefault();

            if (SearchData != null)
                return Json(1);     // email exists
            else
                return Json(0);     // email does not exist 

        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Remove("UserID");
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Edit/5
        public ActionResult EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            userRegistration.User = ModelContext.Users.Find(id);
            userRegistration.Country = Country_PM.GetCountries();

            if (userRegistration.User == null)
            {
                return HttpNotFound();
            }
            return View(userRegistration);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "UserId,Email,Password,Forename,Surname,Address,Phone,PostCode,Country")] User user)
        {
            if (ModelState.IsValid)
            {
                userPM.EditUser(user);
                return RedirectToAction("UserProfile");
            }

            return View(userRegistration);
        }

        public ActionResult DeleteUser()
        {
            User user = ModelContext.Users.Find(UserIdSession);

            if (user == null)
            {
                return RedirectToAction("UserProfile", "Users");
            }
            else
            {
                ModelContext.Users.Remove(user);
                ModelContext.SaveChanges();

                return RedirectToAction("LogOut", "Users");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ModelContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
