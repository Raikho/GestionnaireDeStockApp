using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    public enum DiscountType { Amount, Percent, SubTotalDiscount }

    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        public ProductLine ProductLine { get; set; }
        public double Value { get; set; }
        public double TotalDiscount { get; set; }
        public DiscountType Type { get; set; }
    }
}