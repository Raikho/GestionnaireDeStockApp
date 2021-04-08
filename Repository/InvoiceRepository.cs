using DataLayer;
using DataTransfertObject;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class InvoiceRepository : Repository<Invoice>
    {
        public new IEnumerable<Invoice> GetAll()
        {
            var dbContext = new StockContext();
            return dbContext.Invoices.Select(i => new Invoice
            {
                InvoiceId = i.InvoiceId,
                CreationDate = i.CreationDate,
                NameSeller = i.NameSeller,
                PaymentMethods = i.PaymentMethods,
                Recipe = i.Recipe,
                TicketRef = i.TicketRef,
                TotalToPay = i.TotalToPay
            });
        }
    }
}
