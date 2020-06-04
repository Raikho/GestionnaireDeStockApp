using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour EditAnArticlePage.xaml
    /// </summary>
    public partial class EditAnArticlePage : Page
    {
        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        public Article characToDelete { get; private set; }

        public EditAnArticlePage(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(path);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBlock();
            ClearTheBox();

            characToDelete = EditAnArticleByReference();
            if (characToDelete == null)
                EditAnArticleTxtBlockShow.Text = "Aucun article trouvé";
        }

        private void EditValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBlock();
            EditACharacteristic();
        }

        private void EditRefTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditRefTxtBox.Text = string.Empty;
            EditRefTxtBox.GotFocus += EditRefTxtBox_GotFocus;
            EditRefTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void EditNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditNameTxtBox.Text = string.Empty;
            EditNameTxtBox.GotFocus += EditNameTxtBox_GotFocus;
            EditNameTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void EditPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditPriceTxtBox.Text = string.Empty;
            EditPriceTxtBox.GotFocus += EditPriceTxtBox_GotFocus;
            EditPriceTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void EditQuantTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditQuantTxtBox.Text = string.Empty;
            EditQuantTxtBox.GotFocus += EditQuantTxtBox_GotFocus;
            EditQuantTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ClearTheBlock()
        {
            EditAnArticleTxtBlockShow.Text = string.Empty;
            EditRefTxtBlockError.Text = string.Empty;
            EditNameTxtBlockError.Text = string.Empty;
            EditPriceTxtBlockError.Text = string.Empty;
            EditQuantTxtBlockError.Text = string.Empty;
            RefTxtBlockConfirm.Text = string.Empty;
            NameTxtBlockConfirm.Text = string.Empty;
            PriceTxtBlockConfirm.Text = string.Empty;
            QuantTxtBlockConfirm.Text = string.Empty;

            RefTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.Orange);
            NameTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.Orange);
            PriceTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.Orange);
            QuantTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.Orange);
        }

        private void ClearTheBox()
        {
            EditRefTxtBox.Text = string.Empty;
            EditNameTxtBox.Text = string.Empty;
            EditPriceTxtBox.Text = string.Empty;
            EditQuantTxtBox.Text = string.Empty;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditAnArticleTxtBox.Text = string.Empty;
            ClearTheBox();
            EditAnArticleTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            EditAnArticleTxtBox.GotFocus += TextBox_GotFocus;
        }

        /// <summary>
        /// Cible un article en fonction de la référence saisie, et propose un menu pour éditer une caractéristique, au choix, de l'article.
        /// </summary>
        /// <returns></returns>
        public Article EditAnArticleByReference()
        {
            try
            {
                characToDelete = null;
                string newInput = EditAnArticleTxtBox.Text;
                bool correctNum = long.TryParse(newInput, out long reference);
                if (!correctNum)
                {
                    EditRefTxtBlockError.Text = "Une erreur est survenue. Veuillez saisir une référence chiffrée.";
                }

                foreach (var item in Articles)
                {
                    if (item.Reference == reference)
                    {
                        characToDelete = item;
                        EditAnArticleTxtBlockShow.Text = $"L'article séléctionné est:";
                        ShowAnArticle(characToDelete);
                        break;
                    }
                }
            }
            catch (Exception except)
            {
                EditRefTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}.";
            }
            return characToDelete;
        }

        void EditACharacteristic()
        {
            try
            {
                long articleReference = characToDelete.Reference;
                if (EditRefTxtBox.Text == "zone de saisie..." || EditRefTxtBox.Text == "")
                {
                    RefTxtBlockConfirm.Text = string.Empty;
                    RefTxtBlockConfirm.Text = "Référence: caractéristique inchangée";
                }
                else
                {
                    string newArtRefInput = EditRefTxtBox.Text;
                    bool correctArtRefNum = long.TryParse(newArtRefInput, out articleReference);
                    if (!correctArtRefNum)
                    {
                        RefTxtBlockConfirm.Text = string.Empty;
                        RefTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.OrangeRed);
                        RefTxtBlockConfirm.Text = $"Erreur de saisie";
                        EditRefTxtBlockError.Text = "Une erreur est survenue. Veuillez saisir une référence chiffrée.";
                    }
                    else
                    {
                        characToDelete.Reference = articleReference;
                        RefTxtBlockConfirm.Text = string.Empty;
                        RefTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.GreenYellow);
                        RefTxtBlockConfirm.Text = $"Référence: \"{articleReference}\"";
                        EditAnArticleTxtBlockShow.Text += $"La référence de l'article a été modifiée avec succès\n";
                    }
                }
            }
            catch (Exception except)
            {
                EditRefTxtBlockError.Text = $"L'erreur suivante est survenue: \"{except.Message}\".";
            }
            try
            {
                string articleName = characToDelete.Name;
                if (EditNameTxtBox.Text == "zone de saisie..." || EditNameTxtBox.Text == "")
                {
                    NameTxtBlockConfirm.Text = string.Empty;
                    NameTxtBlockConfirm.Text = "Nom: caractéristique inchangée";
                }
                else
                {
                    articleName = EditNameTxtBox.Text;
                    if (Regex.IsMatch(articleName, @"^[a-zA-Z ]+$"))
                    {
                        characToDelete.Name = articleName;
                        NameTxtBlockConfirm.Text = string.Empty;
                        NameTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.GreenYellow);
                        NameTxtBlockConfirm.Text = $"Nom: \"{articleName}\"";
                        EditAnArticleTxtBlockShow.Text += $"Le nom de l'article a été modifié avec succès\n";
                    }
                    else
                        throw new ArgumentException();
                }
            }
            catch (ArgumentException argExcept)
            {
                NameTxtBlockConfirm.Text = string.Empty;
                NameTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.OrangeRed);
                NameTxtBlockConfirm.Text = $"Erreur de saisie";
                EditNameTxtBlockError.Text = $"L'erreur suivante est survenue: \"{argExcept.Message}\". Veuillez saisir une entrée alphabétique.";
            }
            try
            {
                double articlePrice = characToDelete.Price;
                if (EditPriceTxtBox.Text == "zone de saisie..." || EditPriceTxtBox.Text == "")
                {
                    PriceTxtBlockConfirm.Text = string.Empty;
                    PriceTxtBlockConfirm.Text = "Prix: caractéristique inchangée";
                }
                else
                {
                    string newArtPriceInput = EditPriceTxtBox.Text;
                    bool correctArtPriceNum = double.TryParse(newArtPriceInput, out articlePrice);
                    if (!correctArtPriceNum)
                    {
                        PriceTxtBlockConfirm.Text = string.Empty;
                        PriceTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.OrangeRed);
                        PriceTxtBlockConfirm.Text = $"Erreur de saisie";
                        EditPriceTxtBlockError.Text = "Une erreur est survenue. La saisie ne correspond pas à un prix.";
                    }
                    else
                    {
                        characToDelete.Price = articlePrice;
                        PriceTxtBlockConfirm.Text = string.Empty;
                        PriceTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.GreenYellow);
                        PriceTxtBlockConfirm.Text = $"Prix: \"{articlePrice}\"";
                        EditAnArticleTxtBlockShow.Text += $"Le prix de l'article a été modifié avec succès\n";
                    }
                }
            }
            catch (Exception except)
            {
                EditPriceTxtBlockError.Text = $"L'erreur suivante est survenue: \"{except.Message}\".";
            }
            try
            {
                int articleQuantity = characToDelete.Quantity;
                if (EditQuantTxtBox.Text == "zone de saisie..." || EditQuantTxtBox.Text == "")
                {
                    QuantTxtBlockConfirm.Text = string.Empty;
                    QuantTxtBlockConfirm.Text = "Quantité: caractéristique inchangée";
                }
                else
                {
                    string newArtQuantInput = EditQuantTxtBox.Text;
                    bool correctArtQuantNum = int.TryParse(newArtQuantInput, out articleQuantity);
                    if (!correctArtQuantNum)
                    {
                        QuantTxtBlockConfirm.Text = string.Empty;
                        QuantTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.OrangeRed);
                        QuantTxtBlockConfirm.Text = $"Erreur de saisie";
                        EditQuantTxtBlockError.Text = "Une erreur est survenue. La saisie ne correspond pas à une quantité chiffrée.";
                    }
                    else
                    {
                        characToDelete.Quantity = articleQuantity;
                        QuantTxtBlockConfirm.Text = string.Empty;
                        QuantTxtBlockConfirm.Foreground = new SolidColorBrush(Colors.GreenYellow);
                        QuantTxtBlockConfirm.Text = $"Quantité: \"{articleQuantity}\"";
                        EditAnArticleTxtBlockShow.Text += $"La quantité de l'article a été modifiée avec succès\n";
                    }
                }
                Write();
            }
            catch (Exception except)
            {
                EditQuantTxtBlockError.Text = $"L'erreur suivante est survenue: \"{except.Message}\".";
            }
        }

        void Write()
        {
            Article.WriteAFile(Articles, Path);
        }

        void ShowAnArticle(Article article)
        {
            RefTxtBlockConfirm.Text = $"Numéro: {article.Reference}";
            NameTxtBlockConfirm.Text = $"Nom: {article.Name}";
            PriceTxtBlockConfirm.Text = $"Prix: {article.Price}";
            QuantTxtBlockConfirm.Text = $"Quantité: {article.Quantity}";
        }
    }
}
