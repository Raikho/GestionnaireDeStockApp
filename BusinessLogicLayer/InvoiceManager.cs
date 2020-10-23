using DataLayer;
using DataTransfertObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLogicLayer
{
    public class InvoiceManager
    {
        private ObservableCollection<PaymentMethod> paymentMethods = new ObservableCollection<PaymentMethod>();
        private StockContext invoices = new StockContext();
        private Invoice ticket = new Invoice();

        public ObservableCollection<PaymentMethod> PaymentMethods { get => paymentMethods; set => paymentMethods = value; }
        public StockContext Invoices { get => invoices; set => invoices = value; }
        public Invoice Ticket { get => ticket; set => ticket = value; }

        public List<Invoice> LoadInvoiceDataBase()
        {
            List<Invoice> invoicesList = new List<Invoice>();
            var dbContext = Invoices;
            var invoicesStore = dbContext.Invoices;
            foreach (var invoice in invoicesStore)
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

            Ticket.NameSeller = LoginManager.LoginSession.UserName;
            Ticket.Recipe += totalDiscountPriceValue;
            Ticket.TotalToPay += totalDiscountPriceValue;
            Ticket.CreationDate = DateTime.Now;

            Ticket.ProductLines.Add(new ProductLine
            {
                ProductLineJoinId = productLineManager.SetTheProductLineId(Ticket),
                Product = new Product
                {
                    Name = productView.Name,
                    Price = productView.Price,
                },
                Quantity = quantity,
                FinalTotalPrice = totalDiscountPriceValue,
                Discounts = new List<Discount>
                {
                    new Discount
                    {
                        DiscountJoinId = discountManager.SetTheDiscountId(Ticket),
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
            cashRegisterManager.InvoiceViewsList.Clear();
            cashRegisterManager.ProductLinesList.Clear();
            cashRegisterManager.TotalDiscountsList.Clear();
            cashRegisterManager.PaymentMethodsList.Clear();
        }
    }
}
