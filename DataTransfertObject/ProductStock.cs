using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    public class ProductStock
    {
        [Key]
        public int ProductStockId { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }
    }
}
