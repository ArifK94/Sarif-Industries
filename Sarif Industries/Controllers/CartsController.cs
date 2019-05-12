using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sarif_Industries.Data_Access_Layer;
using Sarif_Industries.Models;
using Sarif_Industries.View_Models;
using System.Globalization;
using Sarif_Industries.PartialModels;

namespace Sarif_Industries.Controllers
{
    public class CartsController : ApplicationController
    {


        // GET: Carts
        public ActionResult Cart()
        {
            return View();
        }

        public JsonResult AddToCart([Bind(Include = "cartId,UserId,ProductId,Colour,Quantity,Price")] Cart cart)
        {

            bool result = false;
            int userId = Convert.ToInt32(Session["UserId"]);

            if (ModelState.IsValid)
            {
                if (Session["UserId"] != null)
                {
                    Cart_PM.AddToCartDb(cart, userId);
                    result = true;

                }
                else
                {
                    Cart_PM.AddtoCartSession(cart);
                    result = true;
                }

            }

            return Json(result);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cart cart = new Cart();
            ShoppingCart shoppingCart = new ShoppingCart();


            // get cart either by session or from database
            if (Session[UserIDKey] != null)
                cart = ModelContext.Carts.Find(id);
            else
            {
                int index = id.GetValueOrDefault();
                cart = Cart_PM.GetCart().ElementAt(index);

                shoppingCart.CartSessionIndex = index;
            }


            if (cart == null)
            {
                return HttpNotFound();
            }
            else
            {

                shoppingCart.Cart = cart;

                shoppingCart.Product = ModelContext.Products.Where(p => p.ProductId == cart.ProductId).SingleOrDefault();

                shoppingCart.ProductColours = (from d in ModelContext.ProductColours
                                               join f in ModelContext.ProductImages
                                               on d.ColourId equals f.ColourId
                                               where f.ProductId == cart.ProductId
                                               select d).Distinct();

                shoppingCart.ProductImages = ModelContext.ProductImages
                    .Where(p => p.ProductId == cart.ProductId)
                    .ToList();

                return View(shoppingCart);
            }

        }

        // POST: Carts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cartId,UserId,ProductId,Colour,Quantity,Price")] Cart cart)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            if (Session[UserIDKey] != null)
            {
                if (ModelState.IsValid)
                {
                    cart.UserId = userId;

                    ModelContext.Entry(cart).State = EntityState.Modified;
                    ModelContext.SaveChanges();
                    return RedirectToAction("Cart");
                }
            }

            return View(cart);
        }

        public JsonResult EditCartSession([Bind(Include = "cartId,UserId,ProductId,Colour,Quantity,Price")] Cart cart, int id)
        {

            bool result = false;

            if (Session[CartSessionKey] != null)
            {
                Cart_PM.EditCartSession(cart, id);
                result = true;
            }

            return Json(result);
        }


        public ActionResult Checkout()
        {
            Checkout checkout = new Checkout();
            int userId = Convert.ToInt32(Session["UserId"]);

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Users");
            }

            var items = ModelContext.Carts
                .Where(c => c.UserId == userId)
                .Count();

            // redirect to carts page if no items in shopping cart
            if (items <= 0)
            {
                return RedirectToAction("Cart");
            }

            checkout.User = ModelContext.Users.Find(userId);
            ViewBag.Forename = checkout.User.Forename;
            ViewBag.Surname = checkout.User.Surname;
            ViewBag.Address = checkout.User.Address;
            ViewBag.PostCode = checkout.User.PostCode;
            ViewBag.Phone = checkout.User.Phone;


            checkout.Cart = ModelContext.Carts
                    .Include(c => c.Product)
                    .Include(c => c.User)
                    .Where(c => c.UserId == userId)
                    .ToList();


            checkout.Orders = new List<Order> { new Order
                            {
                                FirstName = checkout.User.Forename,
                                LastName = checkout.User.Surname,
                                Address = checkout.User.Address,
                                Country = checkout.User.Country,
                                PostCode = checkout.User.PostCode
                            }};

            checkout.Product = (from d in ModelContext.Products
                                join f in ModelContext.Carts
                                on d.ProductId equals f.ProductId
                                select d).ToList();


            checkout.CountryList = Country_PM.GetCountries();




            return View(checkout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(List<Order> orders)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var product = (from d in ModelContext.Products
                           select d).ToList();

            var cart = ModelContext.Carts
                      .Where(c => c.UserId == userId)
                      .ToList();

            var user = ModelContext.Users.Find(userId);


            if (ModelState.IsValid)
            {
                foreach (var cartItem in cart)
                {
                    orders = new List<Order> { new Order
                    {
                        // add values to Orders table
                        UserId = UserIdSession,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Colour = cartItem.Colour,
                        Total = cartItem.Price,
                        FirstName = user.Forename,
                        LastName = user.Surname,
                        Address = user.Address,
                        Country = user.Country,
                        PostCode = user.PostCode,
                        OrderDate = DateTime.Now
                    } };

                    foreach (var prodItem in product)
                    {
                        if (prodItem.ProductId == cartItem.ProductId)
                        {
                            foreach (var order in orders)
                            {
                                ModelContext.Orders.Add(order);
                            }


                            // Update product stock
                            prodItem.Stock -= cartItem.Quantity;
                            ModelContext.Entry(prodItem).State = EntityState.Modified;

                            // remove product from Carts table
                            ModelContext.Carts.Remove(cartItem);

                        }
                    }
                }

                ModelContext.SaveChanges();
                ModelState.Clear();

                return RedirectToAction("Cart");
            }

            return View();
        }


        public JsonResult DeleteConfirmed(int id)
        {
            var cart = ModelContext.Carts
                .Where(x => x.ProductId == id && (x.UserId == UserIdSession))
                .SingleOrDefault();

            bool result = false;

            if (Session[UserIDKey] != null && cart != null)
            {
                ModelContext.Carts.Remove(cart);
                ModelContext.SaveChanges();
                result = true;
            }

            else if (Session[CartSessionKey] != null && cart != null)
            {
                Cart_PM.Remove(id);
                result = true;
            }

            return Json(result);
        }

        public ActionResult Order()
        {

            if (Session[UserIDKey] == null)
                return RedirectToAction("Login", "Users");

            AccountProfile accountProfile = new AccountProfile();

            accountProfile.User = ModelContext.Users
                .Where(u => u.UserId == UserIdSession).SingleOrDefault();

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

            return View(accountProfile);
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
