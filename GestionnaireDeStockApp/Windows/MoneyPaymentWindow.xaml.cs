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
        public static double MoneyPayment { get; private set; }

        public MoneyPaymentWindow()
        {
            InitializeComponent();

            MoneyTxtBox.Text = SalesManagementPage.FinalTotal.ToString();
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
            SalesManagementPage.MakeAMoneyPayment();
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
                SalesManagementPage.MakeAMoneyPayment();
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
                ControlInputService.CheckDoubleTypeInput(MoneyTxtBox);
                if (ControlInputService.CorrectPickedChara == false || MoneyTxtBox.Text == "")
                    return 0;
                else
                    return MoneyPayment = Convert.ToDouble(MoneyTxtBox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return 0;
            }

        }
    }
}
