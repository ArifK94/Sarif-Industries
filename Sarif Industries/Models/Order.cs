using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sarif_Industries.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string Colour { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Notes { get; set; }

        [Column(TypeName = "datetime2")]
        public System.DateTime OrderDate { get; set; }
    }
}