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
        public static double CreditCardPayment { get; private set; }

        public CreditCardPaymentWindow()
        {
            InitializeComponent();

            CBTxtBox.Text = SalesManagementPage.FinalTotal.ToString();
            CBTxtBox.Focus();
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
            SetACbAmount();
            SalesManagementPage.MakeACBPayment();
            Close();
        }

        private void CBTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void CBTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetACbAmount();
                SalesManagementPage.MakeACBPayment();
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
                textBox.SelectAll();
        }

        private double SetACbAmount()
        {
            try
            {
                ControlInputService.CheckDoubleTypeInput(CBTxtBox);
                if (ControlInputService.CorrectPickedChara == false || CBTxtBox.Text == "")
                    return 0;
                else
                    return CreditCardPayment = Convert.ToDouble(CBTxtBox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return 0;
            }

        }
    }
}
