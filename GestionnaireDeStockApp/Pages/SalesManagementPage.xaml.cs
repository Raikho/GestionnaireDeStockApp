using BusinessLogicLayer;
using DataTransfertObject;
using GestionnaireDeStockApp.Windows;
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
        public static Payment Payment { get; set; } = new Payment();

        CashRegisterManager CashRegisterManager { get; }

        readonly InvoiceManager InvoiceManager = new InvoiceManager();
        readonly MethodPaymentManager MethodPaymentManager = new MethodPaymentManager();
        readonly PaymentMethod PaymentMethod = new PaymentMethod();

        public SalesManagementPage()
        {
            CashRegisterManager = new CashRegisterManager();
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
            {
                SearchAnArticleToSell();
            }
        }

        private void ShowSellerNameOnTicket()
        {
            try
            {
                SellerNameTxtBox.Text = $"Vendeur: {LoginManager.LoginSession.UserName}";
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

                if (salesParametersWindow.RightParameters == true)
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
                CreditCardPaymentWindow creditCardPaymentWindow = new CreditCardPaymentWindow(Payment);
                creditCardPaymentWindow.ShowDialog();

                if (creditCardPaymentWindow.CloseWithPayment == true && Convert.ToDouble(creditCardPaymentWindow.CBTxtBox.Text) > 0)
                {
                    ShowACBPayment();
                }
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
                MoneyPaymentWindow moneyPaymentWindow = new MoneyPaymentWindow(Payment);
                moneyPaymentWindow.ShowDialog();
                if (moneyPaymentWindow.CloseWithPayment == true && Convert.ToDouble(moneyPaymentWindow.MoneyTxtBox.Text) > 0)
                {
                    ShowAMoneyPayment();
                }
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
                ChequePaymentWindow chequePaymentWindow = new ChequePaymentWindow(Payment);
                chequePaymentWindow.ShowDialog();
                if (chequePaymentWindow.CloseWithPayment == true && Convert.ToDouble(chequePaymentWindow.ChqTxtBox.Text) > 0)
                {
                    ShowAChequePayment();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void GiftChqButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GiftChequeWindow giftChequeWindow = new GiftChequeWindow(Payment);
                giftChequeWindow.ShowDialog();
                if (giftChequeWindow.CloseWithPayment == true && Convert.ToDouble(giftChequeWindow.AmountGiftChqTxtBox.Text) > 0)
                {
                    ShowAGiftChqPayment();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OtherPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Not yet implemented
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void CancelPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Etes-vous sûr de vouloir annuler le ticket?", "Ticket", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ResetTheTicket();
            }
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

            Payment.TotalToPay = 0;
            InvoiceManager.Ticket.Recipe = 0;
            InvoiceManager.Ticket.TotalToPay = 0;
            InvoiceManager.Ticket.ProductLines.Clear();

            if (InvoiceManager.Ticket.PaymentMethods != null)
            {
                InvoiceManager.Ticket.PaymentMethods.Clear();
            }

            if (CashRegisterManager.InvoiceViewsList != null)
            {
                CashRegisterManager.InvoiceViewsList.Clear();
            }

            if (CashRegisterManager.ProductLinesList != null)
            {
                CashRegisterManager.ProductLinesList.Clear();
            }

            if (CashRegisterManager.TotalDiscountsList != null)
            {
                CashRegisterManager.TotalDiscountsList.Clear();
            }

            if (CashRegisterManager.PaymentMethodsList != null)
            {
                CashRegisterManager.PaymentMethodsList.Clear();
            }
        }

        private void PaymentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous valider l'encaissement?", "Caisse", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (Math.Round(Payment.TotalToPay, 2) > 0)
                    {
                        MessageBox.Show("L'encaissement est incomplet. Veuillez procéder au paiement.");
                    }
                    else
                    {
                        MessageBox.Show("Vente terminée! Le ticket a été validé.");
                        ProductStockManager ProductStockManager = new ProductStockManager();
                        ProductStockManager.EditProductQuantity(InvoiceManager);
                        MethodPaymentManager.SetThePaymentMethod(InvoiceManager, PaymentMethod, Payment);
                        InvoiceManager.SaveInvoiceToDataBase();
                        ResetTheTicket();
                        ArticleToSellDataGrid.ItemsSource = ProductViewManager.JoinProductAndProductStockTables();
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
                InvoiceDataGrid.ItemsSource = CashRegisterManager.InvoiceViewsList;
                Payment.TotalToPay = Math.Round(InvoiceManager.Ticket.TotalToPay, 2);
                TotalTxtBlock.Text = $"{Math.Round(InvoiceManager.Ticket.Recipe, 2)}€ TTC";
                RestToPayTxtBlock.Text = $"{Math.Round(Payment.TotalToPay, 2)}€";
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
                string CBPaymentType = "Paiement CB:";
                SetAShowPayment(Payment.CBPayment, CBPaymentType);
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
                string MoneyPaymentType = "Paiement espèces:";
                SetAShowPayment(Payment.MoneyPayment, MoneyPaymentType);
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
                string ChequePaymentType = "Paiement chèque:";
                SetAShowPayment(Payment.ChequePayment, ChequePaymentType);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public void ShowAGiftChqPayment()
        {
            try
            {
                string giftChqPaymentType = "Paiement chèque cadeau:";
                SetAShowPayment(Payment.GiftChequePayment, giftChqPaymentType);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public double SetAShowPayment(double payment, string paymentType)
        {
            if (payment > Payment.TotalToPay || payment < 0)
            {
                MessageBox.Show("Le montant est incorrect");
            }
            else
            {
                if (payment > 0)
                {
                    MethodPaymentManager.CalculateAPayment(Payment, payment);
                    PaymentMethodTxtBlock.Text += $"{paymentType}\n";
                    PaymentTxtBlock.Text += $"{Math.Round(payment, 2)}€\n";
                    RestToPayTxtBlock.Text = $"{Math.Round(Payment.TotalToPay, 2)}€";
                }
            }
            return payment;
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
                        InvoiceDataGrid.ItemsSource = CashRegisterManager.InvoiceViewsList;
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