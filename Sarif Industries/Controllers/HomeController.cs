using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sarif_Industries.Models;
using Sarif_Industries.View_Models;


namespace Sarif_Industries.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            HomeProduct homeProduct = new HomeProduct();

            homeProduct.Products = ModelContext.Products.Where(p => p.StateId == 3);

            homeProduct.ProductImages = (from pI in ModelContext.ProductImages
                                         join p in ModelContext.Products
                                         on pI.ProductId equals p.ProductId
                                         where pI.IsDefaultImage == true
                                         select pI);

            return View(homeProduct);
        }

        public ActionResult Product()
        {
            return View();
        }
    }
}