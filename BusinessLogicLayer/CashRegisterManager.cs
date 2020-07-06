﻿using DataTransfertObject;
using DataTransfertObject.DataGridView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLogicLayer
{
    public class CashRegisterManager
    {
        public ObservableCollection<InvoiceView> invoiceViewsList = new ObservableCollection<InvoiceView>();
        public ObservableCollection<ProductLine> productLinesList = new ObservableCollection<ProductLine>();
        public ObservableCollection<Discount> totalDiscountsList = new ObservableCollection<Discount>();
        public ObservableCollection<PaymentMethod> paymentMethodsList = new ObservableCollection<PaymentMethod>();

        public void MakeASalesCycle(ProductView productView,
                                    CashRegisterManager cashRegisterManager,
                                    InvoiceManager invoiceManager,
                                    ProductLineManager productLineManager,
                                    DiscountManager discountManager,
                                    double quantity,
                                    double pourcentDTxtBox,
                                    double discountTxtBox)
        {
            invoiceManager.MakeAInvoice(productView, cashRegisterManager, productLineManager, discountManager, quantity, pourcentDTxtBox, discountTxtBox);
            productLineManager.SetAProductLine(invoiceManager.Ticket, productLinesList);
            discountManager.SetADiscount(invoiceManager.Ticket, totalDiscountsList);
            JoinProductLineWithDiscount();
        }

        public void JoinProductLineWithDiscount()
        {
            InvoiceViewManager invoiceViewManager = new InvoiceViewManager();
            var Join = from d in totalDiscountsList
                       join p in productLinesList
                       on d.DiscountJoinId equals p.ProductLineJoinId
                       select new InvoiceView()
                       {
                           Name = p.Product.Name,
                           Price = Math.Round(p.Product.Price, 2),
                           Quantity = Math.Round(p.Quantity, 2),
                           TotalDiscount = Math.Round(-d.TotalDiscount, 2),
                           FinalTotalPrice = Math.Round(p.FinalTotalPrice, 2)
                       };
            invoiceViewManager.SetAProductLine(Join, invoiceViewsList);
        }

        public double CalculateAPrice(ProductView productView, double quantity)
        {
            double sum = productView.Price * quantity;
            return sum;
        }

        public double CalculateAPourcentDiscount(double pourcentDTxtBox, double sum)
        {
            double pourcentDiscountValue = sum * (Convert.ToDouble(pourcentDTxtBox) / 100);
            return pourcentDiscountValue;
        }

        public double CalculateADiscount(double discountTxtBox)
        {
            double discount = Convert.ToDouble(discountTxtBox);
            return discount;
        }

        public double CalculateTotalPourcentDAndDiscount(double pourcentDiscount, double discount)
        {
            var totalDiscount = pourcentDiscount + discount;
            return totalDiscount;
        }

        public double CalculateADiscountPrice(double totalToPay, double globalDiscount)
        {
            double totalDiscountPrice = totalToPay - globalDiscount;
            return totalDiscountPrice;
        }

        public double CalculateTheTotalInvoiceDiscount(Invoice ticket)
        {
            double totalInvoiceDiscountSum = 0;

            foreach (var productLineToAttribute in ticket.ProductLines)
            {
                foreach (var discount in productLineToAttribute.Discounts)
                {
                    totalInvoiceDiscountSum += discount.TotalDiscount;
                }
            }
            return totalInvoiceDiscountSum;
        }

        public void DeleteProductToSell(InvoiceView invoiceView, Invoice ticket)
        {
            foreach (var productLineToDelete in invoiceViewsList)
            {
                if (productLineToDelete.InvoiceId == invoiceView.InvoiceId)
                {
                    ticket.Recipe -= productLineToDelete.FinalTotalPrice;
                    ticket.TotalToPay -= productLineToDelete.FinalTotalPrice;
                    RemoveInvoiceProductLine(invoiceView, ticket);
                    RemoveProductLineFromProductLinesList(invoiceView);
                    RemoveDiscountFromDiscountsList(invoiceView);
                    invoiceViewsList.Remove(productLineToDelete);
                    break;
                }
            }
        }

        private static void RemoveInvoiceProductLine(InvoiceView invoiceView, Invoice ticket)
        {
            //var invoiceProductLine = from p in ticket.ProductLines
            //                         where p.Product.ProductId == invoiceView.InvoiceId
            //                         select p;

            foreach (var invoiceProductLine in ticket.ProductLines)
            {
                if (invoiceProductLine.ProductLineId == invoiceView.InvoiceId)
                {
                    ticket.ProductLines.Remove(invoiceProductLine);
                    break;
                }
            }
        }

        private void RemoveProductLineFromProductLinesList(InvoiceView invoiceView)
        {
            foreach (var productLine in productLinesList)
            {
                if (productLine.ProductLineId == invoiceView.InvoiceId)
                {
                    productLinesList.Remove(productLine);
                    break;
                }
            }
        }

        private void RemoveDiscountFromDiscountsList(InvoiceView invoiceView)
        {
            foreach (var discount in totalDiscountsList)
            {
                if (discount.DiscountId == invoiceView.InvoiceId)
                {
                    totalDiscountsList.Remove(discount);
                    break;
                }
            }
        }
    }
}
