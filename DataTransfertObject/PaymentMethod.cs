using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransfertObject
{
    public enum PaymentMethodType { CB, Money, Cheque, GiftCheque, Other }

    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodtId { get; set; }
        public Invoice Ticket { get; set; }
        public double Value { get; set; }
        public PaymentMethodType Type { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
