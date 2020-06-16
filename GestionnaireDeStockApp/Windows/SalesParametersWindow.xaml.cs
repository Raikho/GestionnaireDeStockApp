using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour SalesParametersWindow.xaml
    /// </summary>
    public partial class SalesParametersWindow : Window
    {
        public static double Quantity { get; private set; }

        public static double PourcentDiscount { get; private set; }

        public static double Discount { get; private set; }

        public SalesParametersWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            SetAParamPack();
        }

        private void QuantParamTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            QuantParamTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            QuantParamTxtBox.GotFocus += QuantParamTxtBox_GotFocus;
        }

        private void QuantParamTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetAParamPack();
            }
        }

        private void PourcentDiscountTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PourcentDiscountTxtBox.Text = string.Empty;

            PourcentDiscountTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            PourcentDiscountTxtBox.GotFocus += PourcentDiscountTxtBox_GotFocus;
        }

        private void PourcentDiscountTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetAParamPack();
            }
        }

        private void DiscountTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DiscountTxtBox.Text = string.Empty;

            DiscountTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            DiscountTxtBox.GotFocus += DiscountTxtBox_GotFocus;
        }

        private void DiscountTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SetAParamPack();
            }
        }

        private void QuantParamTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(QuantParamTxtBox);
        }

        private void QuantParamTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(QuantParamTxtBox);
        }

        private void SelectContentOnFocus(TextBox textBox)
        {
            if (textBox.Text != null)
                textBox.SelectAll();
        }

        private void SetAParamPack()
        {
            Quantity = 0;
            PourcentDiscount = 0;
            Discount = 0;

            SetQuantityParameter();
            SetPourcentDiscountParamater();
            SetDiscountParameter();
            SalesManagementPage.CalculateTheTicketPrice();
            SalesManagementPage.LoadDataBaseProducts();
            if (Quantity != 0)
                Close();
        }

        private double SetQuantityParameter()
        {
            ControlInputService.CheckDoubleTypeInput(QuantParamTxtBox);
            if (ControlInputService.CorrectPickedChara == false || QuantParamTxtBox.Text == "")
                return 0;
            else
                return Quantity = Convert.ToInt32(QuantParamTxtBox.Text);
        }

        private double SetPourcentDiscountParamater()
        {
            ControlInputService.CheckDoubleTypeInput(PourcentDiscountTxtBox);
            if (ControlInputService.CorrectPickedChara == false || PourcentDiscountTxtBox.Text == "")
                return 0;
            else
                return PourcentDiscount = Convert.ToInt32(PourcentDiscountTxtBox.Text);
        }

        private double SetDiscountParameter()
        {
            ControlInputService.CheckDoubleTypeInput(DiscountTxtBox);
            if (ControlInputService.CorrectPickedChara == false || DiscountTxtBox.Text == "")
                return 0;
            else
                return Discount = Convert.ToInt32(DiscountTxtBox.Text);
        }
    }
}
