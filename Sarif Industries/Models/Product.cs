using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sarif_Industries.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public ProductCategory ProductCategory { get; set; }

        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public ProductState ProductState { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}