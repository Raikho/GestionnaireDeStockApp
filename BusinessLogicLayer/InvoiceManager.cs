using DataLayer;
using DataTransfertObject;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class InvoiceManager
    {
        public static List<Invoice> LoadInvoiceDataBase()
        {
            using (var dbContext = new StockContext())
            {
                List<Invoice> invoicesList = new List<Invoice>();

                var invoices = dbContext.Invoices;

                foreach (var item in invoices)
                {
                    invoicesList.Add(item);
                }
                return invoicesList;
            }
        }

        public static Invoice SaveInvoiceToDataBase(string ticketRef, string nameSeller, double recipe, double discount, string paymentMethod, DateTime creationDate)
        {
            using (var dbContext = new StockContext())
            {
                var tickets = dbContext.Invoices;

                var ticket = new Invoice()
                {
                    TicketRef = ticketRef,
                    NameSeller = nameSeller,
                    Recipe = recipe,
                    Discount = discount,
                    PaymentMethod = paymentMethod,
                    CreationDate = creationDate,
                };
                tickets.Add(ticket);
                dbContext.SaveChanges();
                return ticket;
            }
        }
    }
}
