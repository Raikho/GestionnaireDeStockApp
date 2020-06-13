using DataLayer;
using System;
using System.Text.RegularExpressions;
using System.Windows;
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
            EditRefTxtBox.Text = string.Empty;
            ClearTheBLock();
            EditRefTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            EditRefTxtBox.GotFocus += EditRefTxtBox_GotFocus;
        }

        private void EditRefTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearTheBLock();
                EditAnArticle();
            }
        }

        private void EditNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditNameTxtBox.Text = string.Empty;
            ClearTheBLock();
            EditNameTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            EditNameTxtBox.GotFocus += EditNameTxtBox_GotFocus;
        }

        private void EditNameTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearTheBLock();
                EditAnArticle();
            }
        }

        private void EditPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditPriceTxtBox.Text = string.Empty;
            ClearTheBLock();
            EditPriceTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            EditPriceTxtBox.GotFocus += EditPriceTxtBox_GotFocus;
        }

        private void EditPriceTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearTheBLock();
                EditAnArticle();
            }
        }

        private void EditQuantTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditQuantTxtBox.Text = string.Empty;
            ClearTheBLock();
            EditQuantTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            EditQuantTxtBox.GotFocus += EditQuantTxtBox_GotFocus;
        }

        private void EditQuantTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearTheBLock();
                EditAnArticle();
            }
        }

        private void ClearTheBLock()
        {
            RefTxtBlockEditInfo.Text = string.Empty;
            NameTxtBlockEditInfo.Text = string.Empty;
            PriceTxtBlockEditInfo.Text = string.Empty;
            QuantTxtBlockEditInfo.Text = string.Empty;
            EditTxtBlockInfo.Text = string.Empty;
            RefTxtBlockEditConfirm.Text = string.Empty;
            NameTxtBlockEditConfirm.Text = string.Empty;
            PriceTxtBlockEditConfirm.Text = string.Empty;
            QuantTxtBlockEditConfirm.Text = string.Empty;
        }

        private void SelectAnArticle()
        {
            try
            {
                using (var dbContext = new StockContext())
                {
                    var products = dbContext.Products;
                    var selectedItem = ArticlesListManagementPage.CurrentItemSelected;

                    foreach (var product in products)
                    {
                        Product articleToEdit = selectedItem;
                        if (articleToEdit.ToString().ToLower() == product.ToString().ToLower())
                        {
                            EditRefTxtBox.Text = articleToEdit.Reference;
                            EditNameTxtBox.Text = articleToEdit.Name;
                            EditPriceTxtBox.Text = articleToEdit.Price.ToString(); ;
                            EditQuantTxtBox.Text = articleToEdit.Quantity.ToString();
                        }
                    }
                }
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
                using (var dbContext = new StockContext())
                {
                    var products = dbContext.Products;
                    var selectedItem = ArticlesListManagementPage.CurrentItemSelected;

                    if (selectedItem != null)
                    {
                        if (MessageBox.Show("Etes-vous sûr de vouloir modifié cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            var checkedChar = CheckAllCharacteristics();
                            if (checkedChar == "ERROR")
                            {
                                MessageBox.Show("Une erreur de saisie est survenue.");
                                SelectAnArticle();
                            }
                            else
                            {
                                selectedItem.Reference = EditRefTxtBox.Text;
                                selectedItem.Name = EditNameTxtBox.Text;
                                selectedItem.Price = Convert.ToDouble(EditPriceTxtBox.Text);
                                selectedItem.Quantity = Convert.ToInt32(EditQuantTxtBox.Text);

                                RefTxtBlockEditConfirm.Text = $"{selectedItem.Reference}";
                                NameTxtBlockEditConfirm.Text = selectedItem.Name;
                                PriceTxtBlockEditConfirm.Text = selectedItem.Price.ToString();
                                QuantTxtBlockEditConfirm.Text = selectedItem.Quantity.ToString();

                                EditTxtBlockInfo.Text = "Le produit a été modifié avec succès:";

                                dbContext.Update(selectedItem);
                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private string CheckAllCharacteristics()
        {
            var refChecked = ReWriteAReference();
            var nameChecked = ReWriteAName();
            var priceChecked = ReWriteAPrice();
            var quantChecked = ReWriteAQuantity();
            if (refChecked == "null"
                || nameChecked == "null"
                || priceChecked == 0
                || quantChecked == 0)
            {
                return "ERROR";
            }
            else
                return "OK";
        }

        /// <summary>
        /// Ajoute une "référence" à un article en création.
        /// </summary>
        /// <returns></returns>
        string ReWriteAReference()
        {
            try
            {
                string newInput = EditRefTxtBox.Text;
                if (!Regex.IsMatch(newInput, @"^[a-zA-Z0-9, ]+$"))
                {
                    RefTxtBlockEditInfo.Text = "Référence: veuillez effectuer une saisie alphanumérique.\n";
                    newInput = "null";
                }
                return newInput;
            }
            catch (Exception except)
            {
                RefTxtBlockEditInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
                return "null";
            }
        }

        /// <summary>
        /// Ajoute une "nom" à un article en création.
        /// </summary>
        /// <returns></returns>
        string ReWriteAName()
        {
            try
            {
                string name = EditNameTxtBox.Text;
                if (!Regex.IsMatch(name, @"^[a-zA-Z0-9, ]+$"))
                {
                    NameTxtBlockEditInfo.Text = $"Nom: veuillez effectuer une saisie alphanumérique.\n";
                    name = "null";
                }
                return name;
            }
            catch (Exception except)
            {
                NameTxtBlockEditInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
                return "null";
            }
        }

        /// <summary>
        /// Ajoute une "prix" à un article en création.
        /// </summary>
        /// <returns></returns>
        double ReWriteAPrice()
        {
            try
            {
                string newInput = EditPriceTxtBox.Text;
                bool correctNum = double.TryParse(newInput, out double price);
                if (!correctNum)
                {
                    PriceTxtBlockEditInfo.Text = "Prix: veuillez saisir un prix chiffré.\n";
                    price = 0;
                }
                return price;
            }
            catch (Exception except)
            {
                PriceTxtBlockEditInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
                return 0;
            }
        }

        /// <summary>
        /// Ajoute une "quantité" à un article en création.
        /// </summary>
        /// <returns></returns>
        int ReWriteAQuantity()
        {
            try
            {
                string newInput = EditQuantTxtBox.Text;
                bool correctNum = int.TryParse(newInput, out int quantity);
                if (!correctNum)
                {
                    QuantTxtBlockEditInfo.Text = "Quantité: veuillez saisir une quantité chiffrée.\n";
                    quantity = 0;
                }
                return quantity;
            }
            catch (Exception except)
            {
                QuantTxtBlockEditInfo.Text = $"L'erreur suivante est survenue: {except}";
                return 0;
            }
        }
    }
}
