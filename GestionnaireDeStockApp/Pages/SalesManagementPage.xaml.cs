using BusinessLogicLayer;
using DataLayer;
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
        public static Payment Payment = new Payment();
        CashRegisterManager CashRegisterManager = new CashRegisterManager();
        InvoiceManager InvoiceManager = new InvoiceManager();
        MethodPaymentManager MethodPaymentManager = new MethodPaymentManager();

        public SalesManagementPage()
        {
            InitializeComponent();
            SearchAnArticleToSellTxtBox.Focus();
            ArticleToSellDataGrid.ItemsSource = ProductViewManager.JoinProductAndProductStockTables();
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
                SellerNameTxtBox.Text = $"Vendeur: {LoginManager._loginSession.UserName}";
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
            ArticleToSellDataGrid.ItemsSource = ProductViewManager.JoinProductAndProductStockTables();
        }

        private void SearchAnArticleToSellTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchAnArticleToSellTxtBox.Text = string.Empty;
            SearchAnArticleToSellTxtBox.Foreground = new SolidColorBrush(Colors.White);
            SearchAnArticleToSellTxtBox.GotFocus += SearchAnArticleToSellTxtBox_GotFocus;
            if (SearchAnArticleToSellTxtBox.Text == string.Empty)
            {
                ProductViewManager.JoinProductAndProductStockTables();
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
                    ProductViewManager.JoinProductAndProductStockTables();
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
                CreditCardPaymentWindow creditCardPaymentWindow = new CreditCardPaymentWindow(InvoiceManager);
                creditCardPaymentWindow.ShowDialog();
                if (Convert.ToDouble(creditCardPaymentWindow.CBTxtBox.Text) > 0)
                    ShowACBPayment();
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
                MoneyPaymentWindow moneyPaymentWindow = new MoneyPaymentWindow(InvoiceManager);
                moneyPaymentWindow.ShowDialog();
                if (Convert.ToDouble(moneyPaymentWindow.MoneyTxtBox.Text) > 0)
                    ShowAMoneyPayment();
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
                ChequePaymentWindow chequePaymentWindow = new ChequePaymentWindow(InvoiceManager);
                chequePaymentWindow.ShowDialog();
                if (Convert.ToDouble(chequePaymentWindow.ChqTxtBox.Text) > 0)
                    ShowAChequePayment();
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

            InvoiceManager.Ticket.Recipe = 0;
            InvoiceManager.Ticket.TotalToPay = 0;
            InvoiceManager.Ticket.ProductLines.Clear();
            InvoiceManager.Ticket.PaymentMethods.Clear();
            CashRegisterManager.invoiceViewsList.Clear();
            CashRegisterManager.productLinesList.Clear();
            CashRegisterManager.totalDiscountsList.Clear();
            CashRegisterManager.paymentMethodsList.Clear();
        }

        private void PaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous valider l'encaissement?", "Caisse", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (InvoiceManager.Ticket.TotalToPay > 0)
                        MessageBox.Show("L'encaissement est incomplet. Veuillez procéder au paiement.");
                    else
                    {
                        MessageBox.Show("Vente terminée! Le ticket a été validé.");
                        MethodPaymentManager.SetThePaymentMethod(InvoiceManager, Payment);
                        InvoiceManager.SaveInvoiceToDataBase();
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
                ProductViewManager productViewManager = new ProductViewManager();
                ProductLineManager productLineManager = new ProductLineManager();
                DiscountManager discountManager = new DiscountManager();
                CashRegisterManager.MakeASalesCycle(productViewManager.SelectAProductByRow(ArticleToSellDataGrid.CurrentCell.Item),
                                                    CashRegisterManager,
                                                    InvoiceManager,
                                                    productLineManager,
                                                    discountManager,
                                                    SalesParametersWindow.SalesParameter.Quantity,
                                                    SalesParametersWindow.SalesParameter.PourcentDiscount,
                                                    SalesParametersWindow.SalesParameter.Discount);
                InvoiceDataGrid.ItemsSource = CashRegisterManager.invoiceViewsList;
                TotalTxtBlock.Text = $"{Math.Round(InvoiceManager.Ticket.Recipe, 2)}€ TTC";
                RestToPayTxtBlock.Text = $"{Math.Round(InvoiceManager.Ticket.TotalToPay, 2)}€";
                if (CashRegisterManager.CalculateTheTotalInvoiceDiscount(InvoiceManager.Ticket) > 0)
                {
                    DiscountTxtBlock.Text = "Remise";
                    TotalDiscountTxtBlock.Text = $"-{Math.Round(CashRegisterManager.CalculateTheTotalInvoiceDiscount(InvoiceManager.Ticket), 2)}";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private string ShowTicketNumber()
        {
            var ticketRef = InvoiceManager.CalculateTicketNumber();

            string numFormat = "0000.##";
            return InvoiceManager.Ticket.TicketRef = TicketNumTxtBox.Text = $"{DateTime.Now.ToShortDateString()}/{ticketRef.ToString(numFormat)}";
        }


        public void ShowACBPayment()
        {
            try
            {
                MethodPaymentManager.CalculACBPayment(InvoiceManager, Payment);
                PaymentMethodTxtBlock.Text += "Paiement CB:\n";
                PaymentTxtBlock.Text += $"{Math.Round(Payment.CBPayment, 2)}€\n";

                RestToPayTxtBlock.Text = $"{Math.Round(InvoiceManager.Ticket.TotalToPay, 2)}€";
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
                MethodPaymentManager.CalculAMoneyPayment(InvoiceManager, Payment);
                PaymentMethodTxtBlock.Text += "Paiement espèces:\n";
                PaymentTxtBlock.Text += $"{Math.Round(Payment.MoneyPayment, 2)}€\n";

                RestToPayTxtBlock.Text = $"{Math.Round(InvoiceManager.Ticket.TotalToPay, 2)}€";
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
                MethodPaymentManager.CalculAChequePayment(InvoiceManager, Payment);
                PaymentMethodTxtBlock.Text += "Paiement chèque:\n";
                PaymentTxtBlock.Text += $"{Math.Round(Payment.ChequePayment, 2)}€\n";

                RestToPayTxtBlock.Text = $"{Math.Round(InvoiceManager.Ticket.TotalToPay, 2)}€";
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
                var selectedRow = ProductManager.SelectAselectedProductLine(InvoiceDataGrid.CurrentCell.Item);
                if (selectedRow != null)
                {
                    if (MessageBox.Show("Etes-vous sûr de vouloir supprimer cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        CashRegisterManager.DeleteProductToSell(selectedRow, InvoiceManager.Ticket);
                        TotalTxtBlock.Text = $"{Math.Round(InvoiceManager.Ticket.Recipe, 2)}€ TTC";
                        RestToPayTxtBlock.Text = $"{Math.Round(InvoiceManager.Ticket.TotalToPay, 2)}€";
                        TotalDiscountTxtBlock.Text = $"-{Math.Round(CashRegisterManager.CalculateTheTotalInvoiceDiscount(InvoiceManager.Ticket), 2)}";
                        InvoiceDataGrid.ItemsSource = CashRegisterManager.invoiceViewsList;
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