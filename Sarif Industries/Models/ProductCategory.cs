using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sarif_Industries.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryId { get; set; }

        public string Category { get; set; }
    }
}