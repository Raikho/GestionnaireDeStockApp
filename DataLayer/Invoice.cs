using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        public string TicketRef { get; set; }

        public string NameSeller { get; set; }

        public double Recipe { get; set; }

        public double Discount { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime CreationDate { get; set; }

        private readonly HashSet<ProductLine> _productLines = new HashSet<ProductLine>();

        public ICollection<ProductLine> ProductLines { get => _productLines; }

        public override string ToString() => $"{TicketRef} {NameSeller} {Recipe}";
    }
}
