using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        public Product Product { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public long PostalCode { get; set; }
        public long PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
