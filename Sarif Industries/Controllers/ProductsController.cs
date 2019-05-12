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
using Sarif_Industries.PartialModels;

namespace Sarif_Industries.Controllers
{
    public class ProductsController : ApplicationController
    {
        private ProductShop productShop = new ProductShop();


        // GET: Products
        public ActionResult Shop(string category, string searchString, string colour, string price)
        {

            Product_PM product_PM = new Product_PM();

            List<Product> products =  ModelContext.Products.Where (p => p.StateId != 3).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            // Category filtering
            productShop.Product = product_PM.FilterCategory(products, category);

            // Colour filtering
            productShop.Product = product_PM.FilterColour(productShop.Product, colour);

            productShop.ProductCategories = ModelContext.ProductCategories;

            productShop.TotalCategoryCount = ModelContext.Products.Where(p => p.StateId != 3).Count();

            productShop.ProductColours = ModelContext.ProductColours;

            productShop.ProductSalePrice = (from d in ModelContext.ProductSales
                                            join f in ModelContext.Products
                                            on d.ProductId equals f.ProductId
                                            select d).ToList();

            productShop.ProductThumbnail = (from d in ModelContext.ProductImages
                                            join f in ModelContext.Products
                                            on d.ProductId equals f.ProductId
                                            where d.IsDefaultImage == true
                                            select d).ToList();


            // Create price sorting & range list
            productShop.PriceSort = product_PM.CreatePriceSort(productShop.PriceRange);
            productShop.PriceRange = product_PM.CreatePriceRange(productShop.PriceRange);

            // Price filtering
            productShop.Product = product_PM.FilterPrice(productShop.Product, price);

            productShop.SelectedPrice = !string.IsNullOrEmpty(price) ? "Price: " + price : "Price: All";
            productShop.SelectedCategory = !string.IsNullOrEmpty(category) ? "Category: " + category : "Category: All";
            productShop.SelectedColour = !string.IsNullOrEmpty(colour) ? "Colour: " + colour : "Colour: All";
            productShop.Search = !string.IsNullOrEmpty(searchString) ? "Searching: " + searchString : "Searching: All";


            return View(productShop);
        }

        public ActionResult Sale()
        {

            productShop.Product = ModelContext.Products.Where (p => p.StateId == 2).ToList();

            productShop.ProductSalePrice = (from s in ModelContext.ProductSales
                                            join p in ModelContext.Products
                                            on s.ProductId equals p.ProductId
                                            select s).ToList();

            productShop.ProductThumbnail = (from i in ModelContext.ProductImages
                                            join p in ModelContext.Products
                                            on i.ProductId equals p.ProductId
                                            where i.IsDefaultImage == true
                                            select i).ToList();

            return View(productShop);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductDetail productDetail = new ProductDetail();

            // find product details by id
            productDetail.Product = ModelContext.Products.Find(id);

            productDetail.ProductCategory = (from d in ModelContext.ProductCategories
                                             join f in ModelContext.Products
                                             on d.CategoryId equals f.CategoryId
                                             where f.ProductId == id
                                             select d).SingleOrDefault();

            productDetail.ProductSalePrice = (from d in ModelContext.ProductSales
                                              join f in ModelContext.Products
                                              on d.ProductId equals f.ProductId
                                              where f.ProductId == id
                                              select d).ToList();

            // find the product images by id
            productDetail.ProductImages = ModelContext.ProductImages
                .Where(p => p.ProductId == id)
                .ToList();

            productDetail.ProductColours = (from d in ModelContext.ProductColours
                                            join f in ModelContext.ProductImages
                                            on d.ColourId equals f.ColourId
                                            where f.ProductId == id
                                            select d).Distinct();

            productDetail.Cart = new Cart();


            productDetail.Reviews = (from r in ModelContext.Reviews
                                     join p in ModelContext.Products
                                     on r.ProductId equals p.ProductId
                                     where p.ProductId == id
                                     select r).Distinct();

            productDetail.Review = new Review();

            productDetail.TotalReviews = productDetail.Reviews.Count();

            productDetail.Users = (from u in ModelContext.Users
                                   join r in ModelContext.Reviews
                                   on u.UserId equals r.UserId
                                   where u.UserId == r.UserId
                                   select u).Distinct();


            if (productDetail == null)
            {
                return HttpNotFound();
            }

            return View(productDetail);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview([Bind(Include = "ReviewId,UserId,ProductId,Comment,ReviewDate")] Review review)
        {
            if (Session[UserIDKey] != null)
            {
                if (ModelState.IsValid)
                {
                    review.UserId = UserIdSession;
                    review.ReviewDate = DateTime.Now;

                    ModelContext.Reviews.Add(review);
                    ModelContext.SaveChanges();

                    return RedirectToAction("Details", new { id = review.ProductId });
                }
            }

            return RedirectToAction("Details", new { id = review.ProductId });
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
