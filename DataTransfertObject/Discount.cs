using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    //public class Discount
    //{
    //    [Key]
    //    public string ProductToSellName { get; set; }

    //    public double ProductToSellPrice { get; set; }

    //    public double ProductToSellQuant { get; set; }

    //    public double ProductToSellSubTotal { get; set; }
    //}

    public enum DiscountType { Amount, Percent }

    public class Discount
    {
        [Key]
        public ProductLine ProductLine { get; set; }
        public double Value { get; set; }
        public DiscountType Type { get; set; }
    }
}