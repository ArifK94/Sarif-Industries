using System.Collections.Generic;
using System.Linq;
using Sarif_Industries.Models;

namespace Sarif_Industries.View_Models
{
    public class ShoppingCart
    {
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IQueryable<ProductColour> ProductColours { get; set; }

        public decimal Subtotal { get; set; }

        public int CartSessionIndex { get; set; }

    }
}