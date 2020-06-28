using BusinessLogicLayer;
using DataTransfertObject;
using System;
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
        CashRegisterManager CashRegisterManager = new CashRegisterManager();
        MethodPaymentManager MethodPaymentManager = new MethodPaymentManager();
        SalesParameter SalesParameter = new SalesParameter();

        public SalesManagementPage()
        {
            InitializeComponent();
            SearchAnArticleToSellTxtBox.Focus();
            ArticleToSellDataGrid.ItemsSource = ProductManager.LoadProductsDataBase();
            ShowSellerNameOnTicket();
            ShowDateOnTicket();
            ShowTicketNumber();
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
            try
            {
                SellerNameTxtBox.Text = $"Vendeur: {LoginManager.Username}";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ShowDateOnTicket()
        {
            DateTxtBox.Text = DateTime.Now.ToLongDateString();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ArticleToSellDataGrid.ItemsSource = ProductManager.LoadProductsDataBase();
        }

        private void SearchAnArticleToSellTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchAnArticleToSellTxtBox.Text = string.Empty;
            SearchAnArticleToSellTxtBox.Foreground = new SolidColorBrush(Colors.White);
            SearchAnArticleToSellTxtBox.GotFocus += SearchAnArticleToSellTxtBox_GotFocus;
            if (SearchAnArticleToSellTxtBox.Text == string.Empty)
            {
                ProductManager.LoadProductsDataBase();
            }
        }

        private void AddToSellButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SalesParametersWindow salesParametersWindow = new SalesParametersWindow();
                salesParametersWindow.ShowDialog();

                if (salesParametersWindow.rightParameters == true)
                {
                    CalculateTheTicketPrice();
                    ProductManager.LoadProductsDataBase();
                }    
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

        private void CancelPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Etes-vous sûr de vouloir annuler le ticket?", "Ticket", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                ResetTheTicket();
        }

        private void ResetTheTicket()
        {
            TotalTxtBlock.Text = string.Empty;
            RestToPayTxtBlock.Text = string.Empty;
            PaymentTxtBlock.Text = string.Empty;
            PaymentMethodTxtBlock.Text = string.Empty;
            DiscountTxtBlock.Text = string.Empty;
            TotalDiscountTxtBlock.Text = string.Empty;
            TicketNumTxtBox.Text = ShowTicketNumber();

            CashRegisterManager._ProductLine.FinalTotalPrice = 0;
            CashRegisterManager.productLinesList.Clear();
        }

        private void PaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous valider l'encaissement?", "Caisse", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (CashRegisterManager._ProductLine.FinalTotalPrice > 0)
                        MessageBox.Show("L'encaissement est incomplet. Veuillez procéder au paiement.");
                    else
                    {
                        InvoiceManager.SaveInvoiceToDataBase(CashRegisterManager._invoice.TicketRef, LoginManager.Username, CashRegisterManager.CalculateTheTotalPayment(), CashRegisterManager._ProductLine.TotalDiscount, CashRegisterManager.MakeAPaymentMethod(), DateTime.Now);
                        MessageBox.Show("Vente terminée! Le ticket a été validé.");
                        ProductLineManager.SaveProductLine(CashRegisterManager.productLinesList);
                        MethodPaymentManager.SavePaymentMethod();
                        ResetTheTicket();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void SearchAnArticleToSell()
        {
            try
            {
                var input = CheckInputService.CheckStringTypeInput(SearchAnArticleToSellTxtBox);
                if (input == true)
                {
                    ArticleToSellDataGrid.ItemsSource = ProductManager.GetProductByGlobalResearch(SearchAnArticleToSellTxtBox.Text);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void CalculateTheTicketPrice()
        {
            try
            {
                CashRegisterManager.ExecuteAPriceCalculCycle(ProductManager.SelectAProductByRow(ArticleToSellDataGrid.CurrentCell.Item), SalesParametersWindow.SalesParameter.Quantity, SalesParametersWindow.SalesParameter.PourcentDiscount, SalesParametersWindow.SalesParameter.Discount);
                InvoiceDataGrid.ItemsSource = CashRegisterManager.productLinesList;
                TotalTxtBlock.Text = $"{Math.Round(CashRegisterManager._ProductLine.FinalTotalPrice, 2)}€ TTC";
                RestToPayTxtBlock.Text = $"{Math.Round(CashRegisterManager._ProductLine.FinalTotalPrice, 2)}€";
                if (CashRegisterManager._ProductLine.TotalDiscount > 0)
                {
                    DiscountTxtBlock.Text = "Remise";
                    TotalDiscountTxtBlock.Text = $"-{Math.Round(CashRegisterManager._ProductLine.TotalDiscount, 2)}";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private string ShowTicketNumber()
        {
            var ticketRef = CashRegisterManager.CalculateTicketNumber();

            string numFormat = "0000.##";
            return CashRegisterManager._invoice.TicketRef = TicketNumTxtBox.Text = $"{DateTime.Now.ToShortDateString()}/{ticketRef.ToString(numFormat)}";
        }

        public void ShowACBPayment()
        {
            try
            {
                CashRegisterManager.MakeACBPayment();
                PaymentMethodTxtBlock.Text += "Paiement CB:\n";
                PaymentTxtBlock.Text += $"{Math.Round(CashRegisterManager._methodPayment.CB, 2)}€\n";

                RestToPayTxtBlock.Text = $"{Math.Round(CashRegisterManager._ProductLine.FinalTotalPrice, 2)}€";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void ShowAMoneyPayment()
        {
            try
            {
                CashRegisterManager.MakeAMoneyPayment();
                PaymentMethodTxtBlock.Text += "Paiement espèces:\n";
                PaymentTxtBlock.Text += $"{Math.Round(CashRegisterManager._methodPayment.Money, 2)}€\n";

                RestToPayTxtBlock.Text = $"{Math.Round(CashRegisterManager._ProductLine.FinalTotalPrice, 2)}€";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void ShowAChequePayment()
        {
            try
            {
                CashRegisterManager.MakeAChequePayment();
                PaymentMethodTxtBlock.Text += "Paiement chèque:\n";
                PaymentTxtBlock.Text += $"{Math.Round(CashRegisterManager._methodPayment.Cheque, 2)}€\n";

                RestToPayTxtBlock.Text = $"{Math.Round(CashRegisterManager._ProductLine.FinalTotalPrice, 2)}€";
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void DeleteProductToSell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = ProductManager.SelectAProductToSellByRow(InvoiceDataGrid.CurrentCell.Item);
                if (selectedRow != null)
                {
                    if (MessageBox.Show("Etes-vous sûr de vouloir supprimer cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        CashRegisterManager.DeleteProductToSell();
                        TotalTxtBlock.Text = $"{Math.Round(CashRegisterManager._ProductLine.FinalTotalPrice, 2)}€ TTC";
                        RestToPayTxtBlock.Text = $"{Math.Round(CashRegisterManager._ProductLine.FinalTotalPrice, 2)}€";
                        TotalDiscountTxtBlock.Text = $"-{Math.Round(CashRegisterManager._ProductLine.TotalDiscount, 2)}";
                        InvoiceDataGrid.ItemsSource = CashRegisterManager.productLinesList;
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