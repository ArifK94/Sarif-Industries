using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sarif_Industries.Models;

namespace Sarif_Industries.View_Models
{
    public class AccountProfile
    {
        public User User { get; set; }

        public IQueryable<Product> Products { get; set; }
        public IQueryable<ProductImage> ProductImage { get; set; }
        public IQueryable<Order> Orders { get; set; }

        public Country CountryList { get; set; }

    }
}