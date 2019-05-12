using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sarif_Industries.Models
{
    public class Cart
    {
        [Key]
        public int cartId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string Colour { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}