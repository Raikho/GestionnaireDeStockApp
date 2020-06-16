using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Ticket
    {
        [Key]
        public string TicketRef { get; set; }

        public string NameSeller { get; set; }

        public double Recipe { get; set; }

        public double Discount { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime CreationDate { get; set; }

        public override string ToString() => $"{TicketRef} {NameSeller} {Recipe}";
    }
}
