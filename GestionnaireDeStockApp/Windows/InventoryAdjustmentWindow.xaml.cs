using BusinessLogicLayer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionnaireDeStockApp.Windows
{
    /// <summary>
    /// Logique d'interaction pour InventoryAdjustmentWindow.xaml
    /// </summary>
    public partial class InventoryAdjustmentWindow : Window
    {
        public InventoryAdjustmentWindow()
        {
            InitializeComponent();
            QuantityTxtBox.Text = ProductViewManager.CurrentItemSelected.Quantity.ToString();
            QuantityTxtBox.Focus();
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

        private void QuantityTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(QuantityTxtBox);
        }

        private void QuantityTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(QuantityTxtBox);
        }

        private void QuantityTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Close();
            }
        }

        private void DefectiveTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(DefectiveTxtBox);
        }

        private void DefectiveTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(DefectiveTxtBox);
        }

        private void DefectiveTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Close();
            }
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectContentOnFocus(TextBox textBox)
        {
            if (textBox.Text != null)
            {
                textBox.SelectAll();
            }
        }
    }
}
