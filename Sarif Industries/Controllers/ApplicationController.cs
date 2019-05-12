using System;
using System.Linq;
using System.Web.Mvc;
using Sarif_Industries.Data_Access_Layer;
using System.Data.Entity;
using Sarif_Industries.Models;
using Sarif_Industries.PartialModels;

namespace Sarif_Industries.Controllers
{
    public abstract class ApplicationController : Controller
    {
        public ModelContext ModelContext { get; } = new ModelContext();
        public ShoppingCart_PM Cart_PM { get; } = new ShoppingCart_PM();

        private const string UserIdKey = "UserID";
        private const string CartKey = "cart";

        public static string UserIDKey => UserIdKey;
        public static string CartSessionKey => CartKey;

        public int UserIdSession { get; set; }

        public ApplicationController()
        {

            // if user is logged in
            if (System.Web.HttpContext.Current.Session[UserIdKey] != null)
            {
                UserIdSession = Convert.ToInt32(System.Web.HttpContext.Current.Session[UserIdKey]);

                Cart_PM.UserId = UserIdSession;

                ViewData["cart"] = Cart_PM.GetCartDb();

                ViewData["cartitemscount"] = Cart_PM.GetCartItemCountDb();

                ViewData["grandtotal"] = Cart_PM.GetGrandTotalDb();

                ViewData["product"] = Cart_PM.GetProductDb();

                ViewData["productimage"] = Cart_PM.GetProductImageDb();


            }
            else if (System.Web.HttpContext.Current.Session[CartSessionKey] != null)
            {
                ViewData["cart"] = Cart_PM.GetCartSession();

                ViewData["cartitemscount"] = Cart_PM.GetCartItemCountSession();

                ViewData["grandtotal"] = Cart_PM.GetGrandTotalSession();

                ViewData["product"] = Cart_PM.GetProductSession();

                ViewData["productimage"] = Cart_PM.GetProductImageSession();

            }
            else
            {
                ViewData["cart"] = null;

                ViewData["cartitemscount"] = 0;
            }


            // For page footer
            ViewData["categories"] = from pc in ModelContext.ProductCategories
                                     select pc;
        }
    }
}