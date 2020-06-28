using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    public class MethodPayment
    {
        [Key]
        public int MethodPaymentId { get; set; }

        public Invoice Ticket { get; set; }

        public double CB { get; set; }

        public double Money { get; set; }

        public double Cheque { get; set; }

        public double GiftCheque { get; set; }

        public double Other { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
