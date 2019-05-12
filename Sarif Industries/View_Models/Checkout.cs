using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sarif_Industries.Models;

namespace Sarif_Industries.View_Models
{
    public class Checkout
    {
        public User User { get; set; }

        public IEnumerable<Cart> Cart { get; set; }

        public IEnumerable<Product> Product { get; set; }

        public IEnumerable<Country> CountryList { get; set; }

        public List<Order> Orders { get; set; }


    }
}