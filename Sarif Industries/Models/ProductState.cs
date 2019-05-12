using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sarif_Industries.Models
{
    public class ProductState
    {
        [Key]
        public int StateId { get; set; }

        public string State { get; set; }
    }
}