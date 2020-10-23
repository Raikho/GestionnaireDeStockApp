using DataTransfertObject;
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
        private static SalesParameter salesParameter = new SalesParameter();
        public static SalesParameter SalesParameter { get => salesParameter; set => salesParameter = value; }
        public bool rightParameters { get; set; }

        public SalesParametersWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
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
            {
                textBox.SelectAll();
            }
        }

        public bool SetAParamPack()
        {
            SalesParameter.Quantity = 0;
            SalesParameter.PourcentDiscount = 0;
            SalesParameter.Discount = 0;

            SetQuantityParameter();
            SetPourcentDiscountParamater();
            SetDiscountParameter();

            var paramBool = SetSaleParameter();
            if (paramBool == true && SalesParameter.Quantity != 0)
            {
                Close();
                return rightParameters = true;
            }
            else
            {
                return rightParameters = false;
            }
        }

        private bool SetSaleParameter()
        {
            bool paramater = false;
            if (CheckInputService.CorrectPickedChara == false || SalesParameter.Quantity == 0)
            {
                MessageBox.Show("Veuillez saisir une quantité.");
                paramater = false;
            }
            else
            {
                if (MessageBox.Show("Etes-vous sûr de vouloir ajouter cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    paramater = true;
                }
            }
            return paramater;
        }

        private double SetQuantityParameter()
        {
            CheckInputService.CheckDoubleTypeInput(QuantParamTxtBox);
            if (CheckInputService.CorrectPickedChara == false || QuantParamTxtBox.Text == "")
            {
                return 0;
            }
            else
            {
                return SalesParameter.Quantity = Convert.ToInt32(QuantParamTxtBox.Text);
            }
        }

        private double SetPourcentDiscountParamater()
        {
            CheckInputService.CheckDoubleTypeInput(PourcentDiscountTxtBox);
            if (CheckInputService.CorrectPickedChara == false || PourcentDiscountTxtBox.Text == "")
            {
                return 0;
            }
            else
            {
                return SalesParameter.PourcentDiscount = Convert.ToInt32(PourcentDiscountTxtBox.Text);
            }
        }

        private double SetDiscountParameter()
        {
            CheckInputService.CheckDoubleTypeInput(DiscountTxtBox);
            if (CheckInputService.CorrectPickedChara == false || DiscountTxtBox.Text == "")
            {
                return 0;
            }
            else
            {
                return SalesParameter.Discount = Convert.ToInt32(DiscountTxtBox.Text);
            }
        }
    }
}
