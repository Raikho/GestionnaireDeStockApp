using DataTransfertObject;
using System;
using System.Linq;

namespace BusinessLogicLayer
{
    public class MethodPaymentManager
    {
        public double CalculACBPayment(InvoiceManager invoiceManager, Payment payment)
        {
            var totalToPay = invoiceManager.Ticket.TotalToPay;
            var restToPay = totalToPay - payment.CBPayment;
            return invoiceManager.Ticket.TotalToPay = restToPay;
        }

        public double CalculAMoneyPayment(InvoiceManager invoiceManager, Payment payment)
        {
            var totalToPay = invoiceManager.Ticket.TotalToPay;
            var restToPay = totalToPay - payment.MoneyPayment;
            return invoiceManager.Ticket.TotalToPay = restToPay;
        }

        public double CalculAChequePayment(InvoiceManager invoiceManager, Payment payment)
        {
            var totalToPay = invoiceManager.Ticket.TotalToPay;
            var restToPay = totalToPay - payment.ChequePayment;
            return invoiceManager.Ticket.TotalToPay = restToPay;
        }

        public double SetThePaymentMethod(InvoiceManager invoiceManager, Payment payment)
        {
            invoiceManager.Ticket.PaymentMethods = invoiceManager.paymentMethods;

            MakeACBPayment(invoiceManager, payment);
            MakeAMoneyPayment(invoiceManager, payment);
            MakeAChequePayment(invoiceManager, payment);

            payment.TotalPayment = payment.CBPayment + payment.MoneyPayment + payment.ChequePayment;

            return payment.TotalPayment;
        }

        private void MakeACBPayment(InvoiceManager invoiceManager, Payment payment)
        {
            invoiceManager.Ticket.PaymentMethods.Add(new PaymentMethod()
            {
                PaymentMethodtId = SetTheProductLineId(invoiceManager.Ticket),
                Type = PaymentMethodType.CB,
                Value = payment.CBPayment,
                CreationDate = DateTime.Now
            });
        }

        private void MakeAMoneyPayment(InvoiceManager invoiceManager, Payment payment)
        {
            invoiceManager.Ticket.PaymentMethods.Add(new PaymentMethod()
            {
                PaymentMethodtId = SetTheProductLineId(invoiceManager.Ticket),
                Type = PaymentMethodType.Money,
                Value = payment.MoneyPayment,
                CreationDate = DateTime.Now
            });
        }

        private void MakeAChequePayment(InvoiceManager invoiceManager, Payment payment)
        {
            invoiceManager.Ticket.PaymentMethods.Add(new PaymentMethod()
            {
                PaymentMethodtId = SetTheProductLineId(invoiceManager.Ticket),
                Type = PaymentMethodType.Cheque,
                Value = payment.ChequePayment,
                CreationDate = DateTime.Now
            });
        }

        public int SetTheProductLineId(Invoice ticket)
        {
            int value;
            if (ticket.PaymentMethods.Count == 0)
                value = 0;
            else
            {
                value = ticket.PaymentMethods.Last().PaymentMethodtId + 1;
            }
            return value;
        }
    }
}
