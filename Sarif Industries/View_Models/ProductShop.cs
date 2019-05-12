using Sarif_Industries.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sarif_Industries.View_Models
{
    public class ProductShop
    {
        public List<Product> Product { get; set; }

        public IQueryable<ProductCategory> ProductCategories { get; set; }
        public int TotalCategoryCount { get; set; }

        public IQueryable<ProductColour> ProductColours { get; set; }

        public List<ProductSale> ProductSalePrice { get; set; }

        public List<ProductImage> ProductThumbnail { get; set; }

        public List<SelectListItem> PriceSort { get; set; }
        public List<SelectListItem> PriceRange { get; set; }

        public string SelectedPrice { get; set; }
        public string SelectedColour { get; set; }
        public string SelectedCategory { get; set; }
        public string Search { get; set; }

    }
}