using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sarif_Industries.Models;

namespace Sarif_Industries.View_Models
{
    public class HomeProduct
    {
        public IQueryable<Product> Products { get; set; }
        public IQueryable<ProductImage> ProductImages { get; set; }

    }
}