using DataTransfertObject;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour CreditCardPaymentWindow.xaml
    /// </summary>
    public partial class CreditCardPaymentWindow : Window
    {
        private double cbAmount;

        public bool CloseWithPayment { get; set; }
        public double CbAmount { get => cbAmount; set => cbAmount = value; }

        public CreditCardPaymentWindow(Payment payment)
        {
            InitializeComponent();

            CBTxtBox.Text = Math.Round(payment.TotalToPay, 2).ToString();
            CBTxtBox.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            CloseWithPayment = true;
            SetACbAmount();
            Close();
        }

        private void CBTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CloseWithPayment = true;
                SetACbAmount();
                Close();
            }
        }

        private void CBTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(CBTxtBox);
        }

        private void CBTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(CBTxtBox);
        }

        private void SelectContentOnFocus(TextBox textBox)
        {
            if (textBox.Text != null)
            {
                textBox.SelectAll();
            }
        }

        private void SetACbAmount()
        {
            try
            {
                CheckInputService.CheckDoubleTypeInput(CBTxtBox);
                if (CheckInputService.CorrectPickedChara == false || CBTxtBox.Text == "")
                {
                    CbAmount = 0;
                }
                else
                {
                    SalesManagementPage.Payment.CBPayment = Convert.ToDouble(CBTxtBox.Text);
                    CbAmount = Convert.ToDouble(CBTxtBox.Text);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void CBTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //Not yet implemented
        }
    }
}
