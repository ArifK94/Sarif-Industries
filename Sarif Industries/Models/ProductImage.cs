using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sarif_Industries.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int ColourId { get; set; }
        [ForeignKey("ColourId")]
        public ProductColour ProductColour { get; set; }

        public string ImageName { get; set; }
        public string FilePath { get; set; }
        public bool IsDefaultImage { get; set; }
    }
}