using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product
    {
        [Key]
        public string Reference { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        //public override string ToString() => $"{Reference} {Name}";
    }
}
