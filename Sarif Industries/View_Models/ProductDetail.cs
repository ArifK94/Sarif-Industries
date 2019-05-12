using Sarif_Industries.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sarif_Industries.View_Models
{
    public class ProductDetail
    {
        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public List<ProductSale> ProductSalePrice { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public IQueryable<ProductColour> ProductColours { get; set; }

        public Cart Cart { get; set; }

        public IQueryable<Review> Reviews { get; set; }
        public Review Review { get; set; }
        public int TotalReviews { get; set; }

        public IQueryable<User> Users { get; set; }
    }
}