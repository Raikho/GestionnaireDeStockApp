using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class ProductLine
    {
        [Key]
        public int ProductLineId { get; set; }

        public Invoice Ticket { get; set; }

        public Product Product { get; set; }

        public float Quantity { get; set; }
    }
}
