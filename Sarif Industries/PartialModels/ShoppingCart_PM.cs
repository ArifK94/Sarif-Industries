using System;
using System.Linq;
using System.Web;
using Sarif_Industries.Data_Access_Layer;
using Sarif_Industries.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace Sarif_Industries.PartialModels
{
    public partial class ShoppingCart_PM
    {
        public const string CartSessionKey = "cart";
        private object cartSession;
        private List<Cart> carts;

        private int userId;

        private int currentCartQuantity;

        private ModelContext modelContext = new ModelContext();

        public int UserId { get => userId; set => userId = value; }

        private int count = 0;

        public ShoppingCart_PM()
        {
            cartSession = HttpContext.Current.Session[CartSessionKey];

            carts = cartSession as List<Cart>;
        }


        public IQueryable<Product> GetProductDb()
        {
            return (from d in modelContext.Products
                    join f in modelContext.Carts
                    on d.ProductId equals f.ProductId
                    select d).Distinct();

        }

        public IQueryable<ProductImage> GetProductImageDb()
        {

            return modelContext.ProductImages
                .Join(modelContext.Carts, d => d.ProductId, f => f.ProductId,
                (d, f) => new
                {
                    d = d,
                    f = f
                })
                .Where(temp0 => ((temp0.d.IsDefaultImage == true) && (temp0.f.UserId == UserId)))
                .Select(temp0 => temp0.d);

        }


        // Get Cart item from database
        public IEnumerable<Cart> GetCartDb()
        {
            var cart = modelContext.Carts
                .Include(c => c.Product)
                .Include(c => c.User)
                .Where(c => c.UserId == UserId);

            return cart;
        }

        // Get total number of cart items from database
        public int GetCartItemCountDb()
        {
            count = modelContext.Carts
                .Include(c => c.Product)
                .Include(c => c.User)
                .Where(c => c.UserId == UserId)
                .Count();

            return count;
        }

        // Get grand total of cart from database
        public decimal GetGrandTotalDb()
        {
            decimal total = 0;

            if (count > 0)
            {
                total = modelContext.Carts
                    .Include(c => c.Product)
                    .Include(c => c.User)
                    .Where(c => c.UserId == UserId)
                    .Select(c => c.Price * c.Quantity)
                    .Sum();
            }
            return total;
        }

        //-------------------------------------------------------------------------------------------- Session
        public List<Cart> GetCartSession()
        {
            return carts;
        }


        public List<Cart> GetCart()
        {
            return (List<Cart>)HttpContext.Current.Session[CartSessionKey];
        }


        public IQueryable<Product> GetProductSession()
        {
            return (from d in modelContext.Products
                    select d).Distinct();

        }

        public IQueryable<ProductImage> GetProductImageSession()
        {
            return modelContext.ProductImages.Where(p => p.IsDefaultImage == true);

        }

        public int GetCartItemCountSession()
        {
            return carts.Count();
        }

        public decimal GetGrandTotalSession()
        {
            return carts
                .Select(c => c.Price * c.Quantity)
                .Sum();
        }



        public Cart AddToCartDb(Cart cart, int userId)
        {
            // get product ID from cart
            var cartItem = modelContext.Carts
                .Where(c => c.ProductId == cart.ProductId)
                .Where(c => c.Colour == cart.Colour)
                .Where(c => c.UserId == userId)
                .SingleOrDefault();

            // get product ID from products
            var product = modelContext.Products
                .Where(p => p.ProductId == cart.ProductId)
                .SingleOrDefault();


            int stock = product.Stock;
            int inputQuantity = cart.Quantity;

            cart.UserId = userId;

            // if product does not exist in the cart, then add to cart
            if (cartItem == null)
                modelContext.Carts.Add(cart);
            else
            {
                UpdateQuantityDb(cartItem, inputQuantity, stock);
            }
            modelContext.SaveChanges();

            return cart;
        }

        private void UpdateQuantityDb(Cart cartItem, int inputQuantity, int stock)
        {
            int cartQuantity = cartItem.Quantity;
            cartQuantity += inputQuantity; // increase cart quantity of the product

            // if the cart quantity is less than the product stock level
            if (cartQuantity <= stock)
                cartItem.Quantity = cartQuantity;     // assign new quantity to the cart
            else
                cartQuantity = 0;
        }

        public List<Cart> AddtoCartSession(Cart cart)
        {
            var product = modelContext.Products
                .Where(p => p.ProductId == cart.ProductId)
                .SingleOrDefault();

            int stock = product.Stock;
            int inputQuantity = cart.Quantity;

            if (cartSession == null)
            {
                List<Cart> carts = new List<Cart>();
                carts.Add(cart);

                cartSession = carts;
                HttpContext.Current.Session[CartSessionKey] = cartSession;
            }
            else
            {
                List<Cart> carts = (List<Cart>)cartSession;

                int index = isExist(cart.ProductId, cart.Colour);
                if (index != -1)
                {

                    int cartQuantity = currentCartQuantity;
                    cartQuantity += inputQuantity; // increase cart quantity of the product

                    // if the cart quantity is less than the product stock level
                    if (cartQuantity <= stock)
                        currentCartQuantity = cartQuantity;     // assign new quantity to the cart
                    else
                        cartQuantity = 0;

                    setQuantity(cart.ProductId, currentCartQuantity);
                }
                else
                {
                    carts.Add(cart);
                }

                cartSession = carts;
                HttpContext.Current.Session[CartSessionKey] = cartSession;

            }
            return carts;
        }

        private int isExist(int id, string colour)
        {
            List<Cart> cart = (List<Cart>)HttpContext.Current.Session[CartSessionKey];

            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ProductId == id && cart[i].Colour.Equals(colour))
                {
                    currentCartQuantity = cart[i].Quantity;
                    return 1;
                }

            return -1;
        }

        private void setQuantity(int id, int quantity)
        {
            List<Cart> cart = (List<Cart>)HttpContext.Current.Session[CartSessionKey];

            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ProductId == id)
                    cart[i].Quantity = quantity;

        }


        public void Remove(int id)
        {
            List<Cart> cart = (List<Cart>)HttpContext.Current.Session[CartSessionKey];

            int index = 0;

            // Get the item index from cart
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ProductId == id)
                    index = cart.IndexOf(cart[i]);

            cart.RemoveAt(index);

            cartSession = cart;
            HttpContext.Current.Session[CartSessionKey] = cartSession;

        }

        public void EditCartSession(Cart cart, int index)
        {
            List<Cart> cartSession = (List<Cart>)HttpContext.Current.Session[CartSessionKey];

            cartSession[index].Quantity = cart.Quantity;
            cartSession[index].Colour = cart.Colour;

        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their userId
        public void MigrateCart(int UserId)
        {
            List<Cart> cart = (List<Cart>)HttpContext.Current.Session[CartSessionKey];

            if (cartSession != null)
            {
                foreach (Cart item in cart)
                {
                    AddToCartDb(item, UserId);
                }

                cartSession = null;
                HttpContext.Current.Session[CartSessionKey] = cartSession;

                modelContext.SaveChanges();
            }
        }
    }
}