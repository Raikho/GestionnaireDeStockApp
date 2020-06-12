using DataLayer;
using System.Windows.Controls;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour EditAnArticlePage.xaml
    /// </summary>
    public partial class EditAnArticlePage : Page
    {
        bool EditNameTxtBoxClick, EditRefTxtBoxClick, EditPriceTxtBoxClick, EditQuantTxtBoxClick = false;

        public Product characToEdit { get; private set; }

        public EditAnArticlePage()
        {
            InitializeComponent();
        }

        //private void ValidateButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ClearTheBlock();
        //    ClearTheBox();

        //    characToEdit = EditAnArticleByReference();
        //    if (characToEdit == null)
        //        EditAnArticleTxtBlockShow.Text = "Aucun article trouvé";
        //}

        //private void EditValidateButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ClearTheBlock();
        //    EditACharacteristic();
        //}

        //private void EditRefTxtBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    EditRefTxtBoxClick = true;
        //    EditRefTxtBox.Text = string.Empty;
        //    EditRefTxtBox.GotFocus += EditRefTxtBox_GotFocus;
        //    EditRefTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        //}

        //private void EditNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    EditNameTxtBoxClick = true;
        //    EditNameTxtBox.Text = string.Empty;
        //    EditNameTxtBox.GotFocus += EditNameTxtBox_GotFocus;
        //    EditNameTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        //}

        //private void EditPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    EditPriceTxtBoxClick = true;
        //    EditPriceTxtBox.Text = string.Empty;
        //    EditPriceTxtBox.GotFocus += EditPriceTxtBox_GotFocus;
        //    EditPriceTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        //}

        //private void EditQuantTxtBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    EditQuantTxtBoxClick = true;
        //    EditQuantTxtBox.Text = string.Empty;
        //    EditQuantTxtBox.GotFocus += EditQuantTxtBox_GotFocus;
        //    EditQuantTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        //}

        //private void ClearTheBlock()
        //{
        //    EditAnArticleTxtBlockShow.Text = string.Empty;
        //    EditRefTxtBlockError.Text = string.Empty;
        //    EditNameTxtBlockError.Text = string.Empty;
        //    EditPriceTxtBlockError.Text = string.Empty;
        //    EditQuantTxtBlockError.Text = string.Empty;
        //    RefTxtBlockConfirm.Text = string.Empty;
        //    NameTxtBlockConfirm.Text = string.Empty;
        //    PriceTxtBlockConfirm.Text = string.Empty;
        //    QuantTxtBlockConfirm.Text = string.Empty;

        //    RefTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.Orange);
        //    NameTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.Orange);
        //    PriceTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.Orange);
        //    QuantTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.Orange);
        //}

        //private void ClearTheBox()
        //{
        //    EditRefTxtBox.Text = string.Empty;
        //    EditNameTxtBox.Text = string.Empty;
        //    EditPriceTxtBox.Text = string.Empty;
        //    EditQuantTxtBox.Text = string.Empty;
        //}

        //private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    EditAnArticleTxtBox.Text = string.Empty;
        //    ClearTheBox();
        //    EditAnArticleTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        //    EditAnArticleTxtBox.GotFocus += TextBox_GotFocus;
        //}

        ///// <summary>
        ///// Cible un article en fonction de la référence saisie, et propose un menu pour éditer une caractéristique, au choix, de l'article.
        ///// </summary>
        ///// <returns></returns>
        //public Product EditAnArticleByReference()
        //{
        //    try
        //    {
        //        characToEdit = null;
        //        string reference = EditAnArticleTxtBox.Text;
        //        if (!Regex.IsMatch(reference, @"^[a-zA-Z0-9 ]+$"))
        //        {
        //            EditRefTxtBlockError.Text = "Une erreur est survenue. Veuillez effectuer une saisie alphanumérique.";
        //        }
        //        else
        //        {
        //            using (var dbContext = new StockContext())
        //            {
        //                var products = dbContext.Products;
        //                foreach (var product in products)
        //                {
        //                    if (product.Reference.ToLower() == reference.ToLower())
        //                    {
        //                        characToEdit = product;
        //                        EditAnArticleTxtBlockShow.Text = $"L'article séléctionné est:";
        //                        ShowAnArticle(characToEdit);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception except)
        //    {
        //        EditRefTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}.";
        //    }
        //    return characToEdit;
        //}

        //void EditACharacteristic()
        //{
        //    string reference = string.Empty;
        //    string name = string.Empty;
        //    double price = 0;
        //    int quantity = 0;

        //    try
        //    {
        //        reference = characToEdit.Reference;
        //        if (EditRefTxtBoxClick == false || EditRefTxtBox.Text == "")
        //        {
        //            RefTxtBlockConfirm.Text = string.Empty;
        //            RefTxtBlockConfirm.Text = "Référence: caractéristique inchangée";
        //        }
        //        else
        //        {
        //            reference = EditRefTxtBox.Text;
        //            if (!Regex.IsMatch(reference, @"^[a-zA-Z0-9 ]+$"))
        //            {
        //                RefTxtBlockConfirm.Text = string.Empty;
        //                RefTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.OrangeRed);
        //                RefTxtBlockConfirm.Text = $"Erreur de saisie";
        //                EditRefTxtBlockError.Text = "Une erreur est survenue. Veuillez effectuer une saisie alphanumérique.";
        //            }
        //            else
        //            {
        //                characToEdit.Reference = reference;
        //                RefTxtBlockConfirm.Text = string.Empty;
        //                RefTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.GreenYellow);
        //                RefTxtBlockConfirm.Text = $"Référence: \"{reference}\"";
        //                EditAnArticleTxtBlockShow.Text += $"La référence de l'article a été modifiée avec succès\n";
        //            }
        //        }
        //    }
        //    catch (Exception except)
        //    {
        //        EditRefTxtBlockError.Text = $"L'erreur suivante est survenue: \"{except.Message}\".";
        //    }
        //    try
        //    {
        //        name = characToEdit.Name;
        //        if (EditNameTxtBoxClick == false || EditNameTxtBox.Text == "")
        //        {
        //            NameTxtBlockConfirm.Text = string.Empty;
        //            NameTxtBlockConfirm.Text = "Nom: caractéristique inchangée";
        //        }
        //        else
        //        {
        //            name = EditNameTxtBox.Text;
        //            if (!Regex.IsMatch(name, @"^[a-zA-Z0-9 ]+$"))
        //            {
        //                NameTxtBlockConfirm.Text = string.Empty;
        //                NameTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.OrangeRed);
        //                NameTxtBlockConfirm.Text = $"Erreur de saisie";
        //                EditNameTxtBlockError.Text = "Une erreur est survenue. Veuillez effectuer une saisie alphanumérique.";
        //            }
        //            else
        //            {
        //                NameTxtBlockConfirm.Text = string.Empty;
        //                NameTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.GreenYellow);
        //                NameTxtBlockConfirm.Text = $"Nom: \"{name}\"";
        //                EditAnArticleTxtBlockShow.Text += $"Le nom de l'article a été modifié avec succès\n";
        //            }
        //        }
        //    }
        //    catch (Exception except)
        //    {
        //        EditNameTxtBlockError.Text = $"L'erreur suivante est survenue: \"{except.Message}\".";
        //    }
        //    try
        //    {
        //        price = Convert.ToDouble(characToEdit.Price);
        //        if (EditPriceTxtBoxClick == false || EditPriceTxtBox.Text == "")
        //        {
        //            PriceTxtBlockConfirm.Text = string.Empty;
        //            PriceTxtBlockConfirm.Text = "Prix: caractéristique inchangée";
        //        }
        //        else
        //        {
        //            string newArtPriceInput = EditPriceTxtBox.Text;
        //            bool correctArtPriceNum = double.TryParse(newArtPriceInput, out price);
        //            if (!correctArtPriceNum)
        //            {
        //                PriceTxtBlockConfirm.Text = string.Empty;
        //                PriceTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.OrangeRed);
        //                PriceTxtBlockConfirm.Text = $"Erreur de saisie";
        //                EditPriceTxtBlockError.Text = "Une erreur est survenue. La saisie ne correspond pas à un prix.";
        //            }
        //            else
        //            {
        //                PriceTxtBlockConfirm.Text = string.Empty;
        //                PriceTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.GreenYellow);
        //                PriceTxtBlockConfirm.Text = $"Prix: \"{price}\"";
        //                EditAnArticleTxtBlockShow.Text += $"Le prix de l'article a été modifié avec succès\n";
        //            }
        //        }
        //    }
        //    catch (Exception except)
        //    {
        //        EditPriceTxtBlockError.Text = $"L'erreur suivante est survenue: \"{except.Message}\".";
        //    }
        //    try
        //    {
        //        quantity = Convert.ToInt32(characToEdit.Quantity);
        //        if (EditQuantTxtBoxClick == false || EditQuantTxtBox.Text == "")
        //        {
        //            QuantTxtBlockConfirm.Text = string.Empty;
        //            QuantTxtBlockConfirm.Text = "Quantité: caractéristique inchangée";
        //        }
        //        else
        //        {
        //            string newArtQuantInput = EditQuantTxtBox.Text;
        //            bool correctQuantNum = int.TryParse(newArtQuantInput, out quantity);
        //            if (!correctQuantNum)
        //            {
        //                QuantTxtBlockConfirm.Text = string.Empty;
        //                QuantTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.OrangeRed);
        //                QuantTxtBlockConfirm.Text = $"Erreur de saisie";
        //                EditQuantTxtBlockError.Text = "Une erreur est survenue. La saisie ne correspond pas à une quantité chiffrée.";
        //            }
        //            else
        //            {
        //                QuantTxtBlockConfirm.Text = string.Empty;
        //                QuantTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.GreenYellow);
        //                QuantTxtBlockConfirm.Text = $"Quantité: \"{quantity}\"";
        //                EditAnArticleTxtBlockShow.Text += $"La quantité de l'article a été modifiée avec succès\n";
        //            }
        //        }
        //    }
        //    catch (Exception except)
        //    {
        //        EditQuantTxtBlockError.Text = $"L'erreur suivante est survenue: \"{except.Message}\".";
        //    }

        //    Update(reference, name, price, quantity);
        //}

        //public void UpdateAnArticle(string reference, string name, double price, int quantity)
        //{
        //    try
        //    {
        //        using (var dbContext = new StockContext())
        //        {
        //            var products = dbContext.Products;

        //            characToEdit = new Product()
        //            {
        //                Reference = reference,
        //                Name = name,
        //                Price = price,
        //                Quantity = quantity,
        //            };
        //            products.Update(characToEdit);
        //            dbContext.SaveChanges();
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBox.Show(exception.Message);
        //    }
        //}

        //void Update(string reference, string name, double price, int quantity)
        //{
        //    UpdateAnArticle(reference, name, price, quantity);
        //}

        //void ShowAnArticle(Product product)
        //{
        //    RefTxtBlockConfirm.Text = $"Numéro: {product.Reference}";
        //    NameTxtBlockConfirm.Text = $"Nom: {product.Name}";
        //    PriceTxtBlockConfirm.Text = $"Prix: {product.Price}";
        //    QuantTxtBlockConfirm.Text = $"Quantité: {product.Quantity}";
        //}
    }
}
