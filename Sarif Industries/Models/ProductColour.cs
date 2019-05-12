using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sarif_Industries.Models
{
    public class ProductColour
    {
        [Key]
        public int ColourId { get; set; }

        public string Colour { get; set; }
    }
}