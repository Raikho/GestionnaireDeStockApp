using DataTransfertObject;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionnaireDeStockApp.Windows
{
    /// <summary>
    /// Logique d'interaction pour GiftChequeWindow.xaml
    /// </summary>
    public partial class GiftChequeWindow : Window
    {
        public bool CloseWithPayment { get; set; }

        public GiftChequeWindow(Payment payment)
        {
            InitializeComponent();

            AmountGiftChqTxtBox.Text = Math.Round(payment.TotalToPay, 2).ToString();
            AmountGiftChqTxtBox.Focus();
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
            CloseWithPayment = true;
            SetAGiftChqAmount();
            Close();
        }

        private void GiftChqTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(AmountGiftChqTxtBox);
        }

        private void GiftChqTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(AmountGiftChqTxtBox);
        }

        private void GiftChqTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void GiftChqTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CloseWithPayment = true;
                SetAGiftChqAmount();
                Close();
            }
        }

        private void SelectContentOnFocus(TextBox textBox)
        {
            if (textBox.Text != null)
                textBox.SelectAll();
        }

        private double SetAGiftChqAmount()
        {
            try
            {
                CheckInputService.CheckDoubleTypeInput(AmountGiftChqTxtBox);
                if (CheckInputService.CorrectPickedChara == false || AmountGiftChqTxtBox.Text == "")
                    return 0;
                else
                {
                    return SalesManagementPage.Payment.GiftChequePayment = Convert.ToDouble(AmountGiftChqTxtBox.Text);
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
