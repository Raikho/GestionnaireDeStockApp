using BusinessLogicLayer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour CreditCardPaymentWindow.xaml
    /// </summary>
    public partial class MoneyPaymentWindow : Window
    {
        public MoneyPaymentWindow(InvoiceManager invoiceManager)
        {
            InitializeComponent();

            MoneyTxtBox.Text = Math.Round(invoiceManager.Ticket.TotalToPay, 2).ToString();
            MoneyTxtBox.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            SetAMoneyAmount();
            Close();
        }

        private void MoneyTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void MoneyTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetAMoneyAmount();
                Close();
            }
        }

        private void MoneyTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(MoneyTxtBox);
        }

        private void MoneyTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(MoneyTxtBox);
        }

        private void SelectContentOnFocus(TextBox textBox)
        {
            if (textBox.Text != null)
                textBox.SelectAll();
        }

        private double SetAMoneyAmount()
        {
            try
            {
                CheckInputService.CheckDoubleTypeInput(MoneyTxtBox);
                if (CheckInputService.CorrectPickedChara == false || MoneyTxtBox.Text == "")
                    return 0;
                else
                {
                    return SalesManagementPage.Payment.MoneyPayment = Convert.ToDouble(MoneyTxtBox.Text);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return 0;
            }
        }
    }
}
