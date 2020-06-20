using DataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour SalesManagementPage.xaml
    /// </summary>
    public partial class SalesManagementPage : Page
    {
        public static List<double> totalSumList = new List<double>();

        public static double FinalTotal { get; private set; }

        public static double CBPayment { get; private set; }
        public static double MoneyPayment { get; private set; }
        public static double ChequePayment { get; private set; }

        public static string TicketRef { get; private set; }
        public static double Discount { get; private set; }
        public static string PaymentMethod { get; private set; }

        static ObservableCollection<ProductToSell> productToSells = new ObservableCollection<ProductToSell>();

        static SalesManagementPage salesManagementPage;
        public SalesManagementPage()
        {
            salesManagementPage = this;

            InitializeComponent();
            SearchAnArticleToSellTxtBox.Focus();
            LoadDataBaseProducts();
            ShowSellerNameOnTicket();
            ShowDateOnTicket();
            CalculateTicketNumber();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchAnArticleToSell();
        }

        private void SearchAnArticle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchAnArticleToSell();
        }

        private void ShowSellerNameOnTicket()
        {
            SellerNameTxtBox.Text = $"Vendeur: {LoginWindow.Username}";
        }

        private void ShowDateOnTicket()
        {
            DateTxtBox.Text = DateTime.Now.ToLongDateString();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDataBaseProducts();
        }

        private void SearchAnArticleToSellTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchAnArticleToSellTxtBox.Text = string.Empty;
            SearchAnArticleToSellTxtBox.Foreground = new SolidColorBrush(Colors.White);
            SearchAnArticleToSellTxtBox.GotFocus += SearchAnArticleToSellTxtBox_GotFocus;
            if (SearchAnArticleToSellTxtBox.Text == string.Empty)
            {
                LoadDataBaseProducts();
            }
        }

        private void AddToSell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SalesParametersWindow salesParametersWindow = new SalesParametersWindow();
                salesParametersWindow.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void CBButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreditCardPaymentWindow creditCardPaymentWindow = new CreditCardPaymentWindow();
                creditCardPaymentWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void MoneyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MoneyPaymentWindow moneyPaymentWindow = new MoneyPaymentWindow();
                moneyPaymentWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ChequeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChequePaymentWindow chequePaymentWindow = new ChequePaymentWindow();
                chequePaymentWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void PresentChqButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void OtherPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void PaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous valider l'encaissement?", "Caisse", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (FinalTotal > 0)
                        MessageBox.Show("L'encaissement est incomplet. Veuillez procéder au paiement.");
                    else
                    {
                        using var dbContext = new StockContext();
                        var tickets = dbContext.Invoices;

                        var ticket = new Invoice()
                        {
                            TicketRef = TicketRef,
                            NameSeller = LoginWindow.Username,
                            Recipe = CalculateRecipe(),
                            Discount = Discount,
                            PaymentMethod = ShowPaymentMethod(),
                            CreationDate = DateTime.Now.Date
                        };
                        dbContext.Add(ticket);
                        dbContext.SaveChanges();
                        MessageBox.Show("Vente terminée! Le ticket a été validé.");
                        ResetTheTicket();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void CancelPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Etes-vous sûr de vouloir annuler le ticket?", "Ticket", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                ResetTheTicket();
        }

        private void ResetTheTicket()
        {
            productToSells.Clear();

            TotalTxtBlock.Text = string.Empty;
            RestToPayTxtBlock.Text = string.Empty;
            PaymentTxtBlock.Text = string.Empty;
            PaymentMethodTxtBlock.Text = string.Empty;
            TicketNumTxtBox.Text = CalculateTicketNumber();

            FinalTotal = 0;
            totalSumList.Clear();
        }

        private void SearchAnArticleToSell()
        {
            try
            {
                string input = SearchAnArticleToSellTxtBox.Text;
                if (!Regex.IsMatch(input, @"^[a-zA-Z0-9, ]+$"))
                {
                    MessageBox.Show("La saisie ne correspond pas à une saisie alphanumérique.");
                }
                else
                {
                    List<Product> productAdded = new List<Product>();
                    Product articleToFind = null;

                    using (var dbContext = new StockContext())
                    {
                        var products = dbContext.Products;

                        foreach (var product in products)
                        {
                            if (product.Reference.ToString().ToLower().Contains(input.ToString().ToLower())
                                || product.Name.ToString().ToLower().Contains(input.ToString().ToLower())
                                || product.Price.ToString().ToLower().Contains(input.ToString().ToLower())
                                || product.Quantity.ToString().ToLower().Contains(input.ToString().ToLower()))
                            {
                                articleToFind = product;

                                productAdded.Add(new Product()
                                {
                                    Reference = product.Reference,
                                    Name = product.Name,
                                    Price = product.Price,
                                    Quantity = product.Quantity
                                });
                            }
                        }
                        ArticleToSellDataGrid.ItemsSource = productAdded;
                    }
                    if (articleToFind == null)
                    {
                        MessageBox.Show("Article introuvable");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static void CalculateTheTicketPrice()
        {
            try
            {
                using var dbContext = new StockContext();
                var products = dbContext.Products;
                var selectedRow = salesManagementPage.ArticleToSellDataGrid.CurrentCell.Item;
                Product articleToSell = (Product)selectedRow;

                double sum;
                double totalSum = 0;

                if (ControlInputService.CorrectPickedChara == false || SalesParametersWindow.Quantity == 0)
                    MessageBox.Show("Veuillez saisir une quantité.");
                else
                {
                    if (MessageBox.Show("Etes-vous sûr de vouloir ajouter cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        sum = articleToSell.Price * SalesParametersWindow.Quantity;
                        productToSells.Add(new ProductToSell()
                        {
                            ProductToSellName = articleToSell.Name,
                            ProductToSellPrice = articleToSell.Price,
                            ProductToSellQuant = SalesParametersWindow.Quantity,
                            ProductToSellSubTotal = sum
                        });
                        salesManagementPage.InvoiceDataGrid.ItemsSource = productToSells;

                        var tempTotal = salesManagementPage.CalculateADiscountPrice(sum);

                        totalSumList.Add(tempTotal);
                    }
                }
                foreach (var item in totalSumList)
                {
                    totalSum += item;
                }
                FinalTotal = totalSum;
                salesManagementPage.TotalTxtBlock.Text = $"{Math.Round(FinalTotal, 2)}€ TTC";
                salesManagementPage.RestToPayTxtBlock.Text = $"{Math.Round(FinalTotal, 2)}€";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private double CalculateADiscountPrice(double totalSum)
        {
            double pourcentDiscount = SalesParametersWindow.PourcentDiscount / 100;
            double discount = SalesParametersWindow.Discount;

            Discount = pourcentDiscount + discount;
            double pourcentDiscountPrice = totalSum - totalSum * pourcentDiscount;
            double discountPrice = pourcentDiscountPrice - discount;
            double totalDiscount = totalSum - discountPrice;

            if (totalDiscount == 0)
                return discountPrice;
            else
            {
                //InvoiceDataGrid.BindingGroup.Name += "Remise\n";
                //salesManagementPage.SubTotalTxtBlock.Text += $"-{Math.Round(totalDiscount, 2)}\n";
            }
            return discountPrice;
        }

        private string CalculateTicketNumber()
        {
            List<Invoice> invoicesList = new List<Invoice>();
            var dbContext = new StockContext();
            var invoices = dbContext.Invoices;

            foreach (var item in invoices)
            {
                invoicesList.Add(item);
            }

            var lastTicket = invoicesList.Last();

            var refToSum = lastTicket.TicketRef.Substring(11);

            int newTicketRef = Convert.ToInt32(refToSum) + 1;

            string numFormat = "0000.##";
            return TicketRef = TicketNumTxtBox.Text = $"{DateTime.Now.ToShortDateString()}/{newTicketRef.ToString(numFormat)}";
        }

        public static void LoadDataBaseProducts()
        {
            using var dbContext = new StockContext();
            List<Product> productAdded = new List<Product>();

            var products = dbContext.Products;

            foreach (var product in products)
            {
                productAdded.Add(new Product()
                {
                    ProductId = product.ProductId,
                    Reference = product.Reference,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                });
            }
            salesManagementPage.ArticleToSellDataGrid.ItemsSource = productAdded;
        }

        public static void MakeACBPayment()
        {
            try
            {
                var totalToPay = FinalTotal;
                CBPayment = CreditCardPaymentWindow.CreditCardPayment;

                var restToPay = totalToPay - CBPayment;

                salesManagementPage.PaymentMethodTxtBlock.Text += "Paiement CB:\n";
                salesManagementPage.PaymentTxtBlock.Text += $"{Math.Round(CBPayment, 2)}€\n";

                FinalTotal = restToPay;
                salesManagementPage.RestToPayTxtBlock.Text = $"{Math.Round(FinalTotal, 2)}€";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static void MakeAMoneyPayment()
        {
            try
            {
                var totalToPay = FinalTotal;
                MoneyPayment = MoneyPaymentWindow.MoneyPayment;

                var restToPay = totalToPay - MoneyPayment;

                salesManagementPage.PaymentMethodTxtBlock.Text += "Paiement espèces:\n";
                salesManagementPage.PaymentTxtBlock.Text += $"{Math.Round(MoneyPayment, 2)}€\n";

                FinalTotal = restToPay;
                salesManagementPage.RestToPayTxtBlock.Text = $"{Math.Round(FinalTotal, 2)}€";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static void MakeAChequePayment()
        {
            try
            {
                var totalToPay = FinalTotal;
                ChequePayment = ChequePaymentWindow.ChequePayment;

                var restToPay = totalToPay - ChequePayment;

                salesManagementPage.PaymentMethodTxtBlock.Text += "Paiement chèque:\n";
                salesManagementPage.PaymentTxtBlock.Text += $"{Math.Round(ChequePayment, 2)}€\n";

                FinalTotal = restToPay;
                salesManagementPage.RestToPayTxtBlock.Text = $"{Math.Round(FinalTotal, 2)}€";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static string ShowPaymentMethod()
        {
            if (CreditCardPaymentWindow.CreditCardPayment > 0 && MoneyPaymentWindow.MoneyPayment == 0 && ChequePaymentWindow.ChequePayment == 0)
                return "CB";
            else if (CreditCardPaymentWindow.CreditCardPayment == 0 && MoneyPaymentWindow.MoneyPayment > 0 && ChequePaymentWindow.ChequePayment == 0)
                return "ESPECES";
            else if (CreditCardPaymentWindow.CreditCardPayment == 0 && MoneyPaymentWindow.MoneyPayment == 0 && ChequePaymentWindow.ChequePayment > 0)
                return "CHEQUE";
            else if (CreditCardPaymentWindow.CreditCardPayment > 0 && MoneyPaymentWindow.MoneyPayment > 0 && ChequePaymentWindow.ChequePayment == 0)
                return "CB/ESPECES";
            else if (CreditCardPaymentWindow.CreditCardPayment == 0 && MoneyPaymentWindow.MoneyPayment == 0 && ChequePaymentWindow.ChequePayment > 0)
                return "CB/CHEQUE";
            else if (CreditCardPaymentWindow.CreditCardPayment == 0 && MoneyPaymentWindow.MoneyPayment > 0 && ChequePaymentWindow.ChequePayment > 0)
                return "ESPECES/CHEQUE";
            else
                return "CB/ESPECES/CHEQUE";
        }

        private double CalculateRecipe()
        {
            return CBPayment + MoneyPayment + ChequePayment;
        }

        private void DeleteProductToSell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = InvoiceDataGrid.CurrentCell.Item;
                ProductToSell currentProdToSell = (ProductToSell)selectedRow;

                if (selectedRow != DBNull.Value)
                {
                    if (MessageBox.Show("Etes-vous sûr de vouloir supprimer cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        foreach (var productToSell in productToSells)
                        {
                            if (productToSell.ProductToSellName.ToLower() == currentProdToSell.ProductToSellName.ToLower())
                            {
                                FinalTotal -= productToSell.ProductToSellSubTotal;
                                totalSumList.Remove(productToSell.ProductToSellSubTotal);
                                productToSells.Remove(productToSell);
                                break;
                            }
                        }
                        salesManagementPage.TotalTxtBlock.Text = $"{Math.Round(FinalTotal, 2)}€ TTC";
                        salesManagementPage.RestToPayTxtBlock.Text = $"{Math.Round(FinalTotal, 2)}€";
                        salesManagementPage.InvoiceDataGrid.ItemsSource = productToSells;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}