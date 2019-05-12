using Sarif_Industries.Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sarif_Industries.Models;
using System.Web.Mvc;

namespace Sarif_Industries.PartialModels
{
    public partial class Product_PM
    {
        private ModelContext modelContext = new ModelContext();

        private const string price_LowToHigh = "low to high";
        private const string price_HighToLow = "high to low";

        private const string priceRangeOne = "£0 - £1,500";
        private const string priceRangeTwo = "£1,500 - £3,000";
        private const string priceRangeThree = "£3,000 - £8,000";
        private const string priceRangeFour = "£8,000 - £12,000";
        private const string priceRangeFive = "> £12,000";


        public List<SelectListItem> CreatePriceSort(List<SelectListItem> sort)
        {
            sort = new List<SelectListItem>();
            sort.Add(new SelectListItem() { Text = price_LowToHigh, Value = "1" });
            sort.Add(new SelectListItem() { Text = price_HighToLow, Value = "2" });

            return sort;
        }


        public List<SelectListItem> CreatePriceRange(List<SelectListItem> range)
        {
            range = new List<SelectListItem>();
            range.Add(new SelectListItem() { Text = priceRangeOne, Value = "1" });
            range.Add(new SelectListItem() { Text = priceRangeTwo, Value = "2" });
            range.Add(new SelectListItem() { Text = priceRangeThree, Value = "3" });
            range.Add(new SelectListItem() { Text = priceRangeFour, Value = "4" });
            range.Add(new SelectListItem() { Text = priceRangeFive, Value = "5" });

            return range;
        }

        public List<Product> FilterPrice(List<Product> products, string price)
        {
            if (!string.IsNullOrEmpty(price))
            {
                switch (price)
                {
                    case price_LowToHigh:
                        products = modelContext.Products.OrderBy(p => p.Price).Where(p => p.StateId != 3).ToList();
                        break;
                    case price_HighToLow:
                        products = modelContext.Products.OrderByDescending(p => p.Price).Where(p => p.StateId != 3).ToList();
                        break;
                    case priceRangeOne:
                        products = FilterRange(0, 1500);
                        break;
                    case priceRangeTwo:
                        products = FilterRange(1500, 3000);
                        break;
                    case priceRangeThree:
                        products = FilterRange(3000, 8000);
                        break;
                    case priceRangeFour:
                        products = FilterRange(8000, 12000);
                        break;
                    case priceRangeFive:
                        products = modelContext.Products.Where(p => p.Price >= 12000).Where(p => p.StateId != 3).ToList();
                        break;
                }
            }

            return products;
        }

        private List<Product> FilterRange(int min, int max)
        {
            return modelContext.Products
                .Where(p => p.Price >= min && p.Price <= max)
                .Where(p => p.StateId != 3)
                .ToList();
        }


        public List<Product> FilterColour(List<Product> products, string colour)
        {
            List<ProductColour> colours = (from p in modelContext.ProductColours
                                           select p).ToList();

            if (colour != null)
            {
                for (int i = 0; i < colours.Count; i++)
                {
                    if (colours[i].Colour == colour)
                    {
                        int colourId = colours[i].ColourId;

                        products = (from p in modelContext.Products
                                    join pI in modelContext.ProductImages
                                    on p.ProductId equals pI.ProductId
                                    where pI.ColourId == colourId && p.StateId != 3
                                    select p).Distinct().ToList();
                    }
                }
            }
            return products;
        }


        public List<Product> FilterCategory(List<Product> products, string category)
        {
            List<ProductCategory> categories = (from c in modelContext.ProductCategories
                                                select c).ToList();

            if (category != null)
            {

                for (int i = 0; i < categories.Count; i++)
                {
                    if (categories[i].Category == category)
                    {
                        int catId = categories[i].CategoryId;

                        products = products.Where(p => p.CategoryId == catId && p.StateId != 3).ToList();
                    }
                }
            }
            return products;
        }
    }
}