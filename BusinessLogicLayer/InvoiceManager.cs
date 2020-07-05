using DataLayer;
using DataTransfertObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLogicLayer
{
    public class InvoiceManager
    {
        public ObservableCollection<PaymentMethod> paymentMethods = new ObservableCollection<PaymentMethod>();
        public StockContext Invoices = new StockContext();
        public Invoice Ticket = new Invoice();

        public List<Invoice> LoadInvoiceDataBase()
        {
            List<Invoice> invoicesList = new List<Invoice>();
            var dbContext = new StockContext();
            var invoices = dbContext.Invoices;
            foreach (var invoice in invoices)
            {
                invoicesList.Add(invoice);
            }
            return invoicesList;
        }

        public Invoice SaveInvoiceToDataBase()
        {
            var dbContext = Invoices;
            var invoiceList = dbContext.Invoices;
            invoiceList.Add(Ticket);
            dbContext.SaveChanges();
            return Ticket;
        }

        public Invoice MakeAInvoice(ProductView productView,
                                    CashRegisterManager cashRegisterManager,
                                    ProductLineManager productLineManager,
                                    DiscountManager discountManager,
                                    double quantity,
                                    double pourcentDTxtBox,
                                    double discountTxtBox)
        {
            ClearAllInvoiceSetup(cashRegisterManager);

            var sum = cashRegisterManager.CalculateAPrice(productView, quantity);
            var pourcentDiscountValue = cashRegisterManager.CalculateAPourcentDiscount(pourcentDTxtBox, sum);
            var discountValue = cashRegisterManager.CalculateADiscount(discountTxtBox);
            var totalDiscountValue = cashRegisterManager.CalculateTotalPourcentDAndDiscount(pourcentDiscountValue, discountValue);
            var totalDiscountPriceValue = cashRegisterManager.CalculateADiscountPrice(sum, totalDiscountValue);

            Ticket.InvoiceId = SetTheInvoiceId();
            Ticket.NameSeller = LoginManager._loginSession.UserName;
            Ticket.Recipe += totalDiscountPriceValue;
            Ticket.TotalToPay += totalDiscountPriceValue;
            Ticket.CreationDate = DateTime.Now;

            Ticket.ProductLines.Add(new ProductLine()
            {
                ProductLineId = productLineManager.SetTheProductLineId(Ticket),
                Product = new Product()
                {
                    Name = productView.Name,
                    Price = productView.Price,
                },
                Quantity = quantity,
                FinalTotalPrice = totalDiscountPriceValue,
                Discounts = new List<Discount>()
                {
                    new Discount()
                    {
                        DiscountId = discountManager.SetTheDiscountId(Ticket),
                        Type = DiscountType.SubTotalDiscount,
                        Value = totalDiscountValue,
                        TotalDiscount = totalDiscountValue
                    }
                }
            });
            return Ticket;
        }

        public void MakeTheTicketPaymentsMethod()
        {
            foreach (var paymentMethod in paymentMethods)
            {
                Ticket.PaymentMethods.Add(paymentMethod);
            }
        }

        public int CalculateTicketNumber()
        {
            var invoicesList = LoadInvoiceDataBase();
            var lastTicket = invoicesList.Last();
            var refToSum = lastTicket.TicketRef.Substring(11);
            int newTicketRef = Convert.ToInt32(refToSum) + 1;
            Ticket.TicketRef = newTicketRef.ToString();
            return newTicketRef;
        }

        public void ClearAllInvoiceSetup(CashRegisterManager cashRegisterManager)
        {
            cashRegisterManager.invoiceViewsList.Clear();
            cashRegisterManager.productLinesList.Clear();
            cashRegisterManager.totalDiscountsList.Clear();
            cashRegisterManager.paymentMethodsList.Clear();
        }

        public int SetTheInvoiceId()
        {
            int value;
            if (Invoices.Invoices.Count() == 0)
                value = 0;
            else
            {
                var dbContext = Invoices;
                var invoiceList = dbContext.Invoices;
                value = invoiceList.ToList().Last().InvoiceId + 1;
            }
            return value;
        }
    }
}
