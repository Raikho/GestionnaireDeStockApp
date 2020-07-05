using BusinessLogicLayer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour EditAnArticleWindow.xaml
    /// </summary>
    public partial class EditAnArticleWindow : Window
    {
        public EditAnArticleWindow()
        {
            InitializeComponent();
            SelectAnArticle();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBLock();
            EditAnArticle();
        }

        private void EditRefTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditRefTxtBox.GotFocus += EditRefTxtBox_GotFocus;
        }

        private void EditRefTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            EditRefTxtBox.Foreground = new SolidColorBrush(Colors.DarkGreen);
            if (e.Key == Key.Enter)
            {
                EditAnArticle();
            }
        }

        private void EditRefTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(EditRefTxtBox);
        }

        private void EditRefTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(EditRefTxtBox);
        }

        private void EditNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditNameTxtBox.GotFocus += EditNameTxtBox_GotFocus;
        }

        private void EditNameTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            EditNameTxtBox.Foreground = new SolidColorBrush(Colors.DarkGreen);
            if (e.Key == Key.Enter)
            {
                EditAnArticle();
            }
        }

        private void EditNameTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(EditNameTxtBox);
        }

        private void EditNameTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(EditNameTxtBox);
        }

        private void EditPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditPriceTxtBox.GotFocus += EditPriceTxtBox_GotFocus;
        }

        private void EditPriceTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            EditPriceTxtBox.Foreground = new SolidColorBrush(Colors.DarkGreen);
            if (e.Key == Key.Enter)
            {
                EditAnArticle();
            }
        }

        private void EditPriceTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(EditPriceTxtBox);
        }

        private void EditPriceTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(EditPriceTxtBox);
        }

        private void EditQuantTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditQuantTxtBox.GotFocus += EditQuantTxtBox_GotFocus;
        }

        private void EditQuantTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            EditQuantTxtBox.Foreground = new SolidColorBrush(Colors.DarkGreen);
            if (e.Key == Key.Enter)
            {
                EditAnArticle();
            }
        }

        private void EditQuantTxtBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectContentOnFocus(EditQuantTxtBox);
        }

        private void EditQuantTxtBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            SelectContentOnFocus(EditQuantTxtBox);
        }

        private void SelectContentOnFocus(TextBox textBox)
        {
            if (textBox.Text != null)
                textBox.SelectAll();
        }

        private void ClearTheBLock()
        {
            EditAnArticleTxtBlockInfo.Text = string.Empty;

            EditRefTxtBox.Foreground = new SolidColorBrush(Colors.Red);
            EditNameTxtBox.Foreground = new SolidColorBrush(Colors.Red);
            EditPriceTxtBox.Foreground = new SolidColorBrush(Colors.Red);
            EditQuantTxtBox.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void SelectAnArticle()
        {
            try
            {
                var selectedItem = ProductViewManager.CurrentItemSelected;

                EditRefTxtBox.Text = selectedItem.Reference;
                EditNameTxtBox.Text = selectedItem.Name;
                EditPriceTxtBox.Text = selectedItem.Price.ToString();
                EditQuantTxtBox.Text = selectedItem.Quantity.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void EditAnArticle()
        {
            try
            {
                if (MessageBox.Show("Etes-vous sûr de vouloir modifié cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var checkedChar = CheckInputService.CheckAllCharacteristics(EditRefTxtBox, EditNameTxtBox, EditPriceTxtBox, EditQuantTxtBox, EditAnArticleTxtBlockInfo);
                    if (checkedChar == false)
                    {
                        MessageBox.Show("Une erreur de saisie est survenue.");
                        SelectAnArticle();
                    }
                    else
                    {
                        ProductViewManager.CurrentItemSelected.Reference = EditRefTxtBox.Text;
                        ProductViewManager.CurrentItemSelected.Name = EditNameTxtBox.Text;
                        ProductViewManager.CurrentItemSelected.Price = Convert.ToDouble(EditPriceTxtBox.Text);
                        ProductViewManager.CurrentItemSelected.Quantity = Convert.ToDouble(EditQuantTxtBox.Text);

                        EditAnArticleTxtBlockInfo.Foreground = new SolidColorBrush(Colors.GreenYellow);
                        EditAnArticleTxtBlockInfo.Text = "Le produit a été modifié avec succès";

                        ProductManager.UpdateAProduct(ProductViewManager.CurrentItemSelected);
                        Close();
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