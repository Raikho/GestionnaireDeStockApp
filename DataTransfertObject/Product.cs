using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public double ExclTaxPrice { get; set; }
        public double Price { get; set; }
        public ICollection<ProductStock> ProductStocks { get; set; }
        private readonly HashSet<Provider> _providers = new HashSet<Provider>();
        public ICollection<Provider> Providers { get => _providers; }
    }
}
