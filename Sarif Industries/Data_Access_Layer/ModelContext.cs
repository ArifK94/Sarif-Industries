using Sarif_Industries.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sarif_Industries.Data_Access_Layer
{
    public class ModelContext : DbContext
    {
        public ModelContext() : base("name=ModelContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductColour> ProductColours { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductState> ProductStates { get; set; }
        public DbSet<ProductSale> ProductSales { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Review> Reviews { get; set; }

    }
}