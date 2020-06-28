using DataLayer;
using DataTransfertObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLogicLayer
{
    public class CashRegisterManager
    {
        public ObservableCollection<ProductLine> productLinesList = new ObservableCollection<ProductLine>();
        public ProductLine _ProductLine = new ProductLine();
        public MethodPayment _methodPayment = new MethodPayment();
        public Invoice _invoice = new Invoice();


        public void ExecuteAPriceCalculCycle(Product product, double quantity, double pourcentDTxtBox, double discountTxtBox)
        {
            CalculateAPrice(product, quantity);
            CalculateAPourcentDiscount(pourcentDTxtBox);
            CalculateADiscount(discountTxtBox);
            CalculateAGlobalDiscount();
            CalculateADiscountPrice();

            productLinesList.Add(_ProductLine);
        }

        private double CalculateAPrice(Product product, double quantity)
        {
            double sum = product.Price * quantity;

            _ProductLine.Product = new Product() { Name = product.Name, Price = product.Price };
            _ProductLine.Quantity = quantity;
            _ProductLine.Ticket = new Invoice() { Recipe = sum };

            return sum;
        }

        private double CalculateAPourcentDiscount(double pourcentDTxtBox)
        {
            var totalSum = _ProductLine.Ticket.Recipe;
            double pourcentDiscount = totalSum * (Convert.ToDouble(pourcentDTxtBox) / 100);

            _ProductLine.PourcentDiscount = pourcentDiscount;


            return pourcentDiscount;
        }

        private double CalculateADiscount(double discountTxtBox)
        {
            double discount = Convert.ToDouble(discountTxtBox);
            _ProductLine.Discount = discount;

            return discount;
        }

        private double CalculateAGlobalDiscount()
        {
            var pourcentDiscount = _ProductLine.PourcentDiscount;
            var discount = _ProductLine.Discount;

            double totalDiscount = pourcentDiscount + discount;

            _ProductLine.TotalDiscount = totalDiscount;


            return -totalDiscount;
        }

        private double CalculateADiscountPrice()
        {
            var totalPrice = _ProductLine.Ticket.Recipe;
            var totalDiscount = _ProductLine.TotalDiscount;

            double totalDiscountPrice = totalPrice - totalDiscount;
            _ProductLine.FinalTotalPrice = totalDiscountPrice;

            return totalDiscountPrice;
        }

        public int CalculateTicketNumber()
        {
            var invoicesList = InvoiceManager.LoadInvoiceDataBase();
            var lastTicket = invoicesList.Last();
            var refToSum = lastTicket.TicketRef.Substring(11);
            int newTicketRef = Convert.ToInt32(refToSum) + 1;
            _invoice.TicketRef = newTicketRef.ToString();
            return newTicketRef;
        }

        public void MakeACBPayment()
        {
            var totalToPay = _ProductLine.FinalTotalPrice;
            var restToPay = totalToPay - _methodPayment.CB;
            _ProductLine.FinalTotalPrice = restToPay;
        }

        public void MakeAMoneyPayment()
        {
            var totalToPay = _ProductLine.FinalTotalPrice;
            var restToPay = totalToPay - _methodPayment.Money;
            _ProductLine.FinalTotalPrice = restToPay;
        }

        public void MakeAChequePayment()
        {
            var totalToPay = _ProductLine.FinalTotalPrice;
            var restToPay = totalToPay - _methodPayment.Cheque;
            _ProductLine.FinalTotalPrice = restToPay;
        }

        public double CalculateTheTotalPayment()
        {
            return _ProductLine.FinalTotalPrice = _methodPayment.CB +_methodPayment.Money + _methodPayment.Cheque;
        }

        public string MakeAPaymentMethod()
        {
            return $"CB: {_methodPayment.CB}\nESP: {_methodPayment.Money}\nCHQ: {_methodPayment.Cheque}";
        }

        public void DeleteProductToSell()
        { 
            foreach (var productLineToDelete in productLinesList)
            {
                if (productLineToDelete.Product.Name.ToLower() == ProductManager.selectedProductToSell.Product.Name.ToLower())
                {
                    _ProductLine.FinalTotalPrice -= productLineToDelete.FinalTotalPrice;
                    _ProductLine.TotalDiscount -= productLineToDelete.TotalDiscount;
                    productLinesList.Remove(productLineToDelete);
                    break;
                }
            }
        }
    }
}
