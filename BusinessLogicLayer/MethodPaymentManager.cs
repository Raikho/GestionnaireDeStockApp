using DataTransfertObject;
using System;
using System.Linq;

namespace BusinessLogicLayer
{
    public class MethodPaymentManager
    {
        public double CalculateAPayment(Payment payment, double currentPayment)
        {
            return payment.TotalToPay -= currentPayment;
        }

        public double SetThePaymentMethod(InvoiceManager invoiceManager, PaymentMethod paymentType, Payment payment)
        {
            invoiceManager.Ticket.PaymentMethods = invoiceManager.PaymentMethods;

            MakeACBPayment(invoiceManager, paymentType, payment);
            MakeAMoneyPayment(invoiceManager, paymentType, payment);
            MakeAChequePayment(invoiceManager, paymentType, payment);
            MakeAGiftChequePayment(invoiceManager, paymentType, payment);

            payment.TotalPayment = payment.CBPayment + payment.MoneyPayment + payment.ChequePayment;

            return payment.TotalPayment;
        }

        private void MakeACBPayment(InvoiceManager invoiceManager, PaymentMethod paymentType, Payment payment)
        {
            paymentType.Type = PaymentMethodType.CB;
            MakeAPayment(invoiceManager, paymentType.Type, payment.CBPayment);
        }

        private void MakeAMoneyPayment(InvoiceManager invoiceManager, PaymentMethod paymentType, Payment payment)
        {
            paymentType.Type = PaymentMethodType.Money;
            MakeAPayment(invoiceManager, paymentType.Type, payment.MoneyPayment);
        }

        private void MakeAChequePayment(InvoiceManager invoiceManager, PaymentMethod paymentType, Payment payment)
        {
            paymentType.Type = PaymentMethodType.Cheque;
            MakeAPayment(invoiceManager, paymentType.Type, payment.ChequePayment);
        }

        private void MakeAGiftChequePayment(InvoiceManager invoiceManager, PaymentMethod paymentType, Payment payment)
        {
            paymentType.Type = PaymentMethodType.GiftCheque;
            MakeAPayment(invoiceManager, paymentType.Type, payment.GiftChequePayment);
        }

        private void MakeAPayment(InvoiceManager invoiceManager, PaymentMethodType type, double payment)
        {
            invoiceManager.Ticket.PaymentMethods.Add(new PaymentMethod
            {
                PaymentMethodJoinId = SetTheProductLineJoinId(invoiceManager.Ticket),
                Type = type,
                Value = payment,
                CreationDate = DateTime.Now
            });
        }

        public int SetTheProductLineJoinId(Invoice ticket)
        {
            int value;
            if (ticket.PaymentMethods.Count == 0)
            {
                value = 0;
            }
            else
            {
                value = ticket.PaymentMethods.Last().PaymentMethodJoinId + 1;
            }
            return value;
        }
    }
}
