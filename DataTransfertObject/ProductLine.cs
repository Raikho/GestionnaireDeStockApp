using System.Collections;
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

        public double PourcentDiscount { get; set; }

        public double Discount { get; set; }

        public double TotalDiscount { get; set; }

        public double FinalTotalPrice { get; set; }

        //public ICollection<Discount> Discounts { get; set; }
    }
}
