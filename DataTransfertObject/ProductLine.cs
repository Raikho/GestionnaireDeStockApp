using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    public class ProductLine
    {
        [Key]
        public int ProductLineId { get; set; }
        public Invoice Ticket { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public double FinalTotalPrice { get; set; }
        public ICollection<Discount> Discounts { get; set; }
    }
}
