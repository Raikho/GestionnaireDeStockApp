using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour EditAnArticleSubMenu.xaml
    /// </summary>
    public partial class EditAnArticleSubMenu : Page
    {
        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        readonly EditAnArticlePage editAnArticlePage;
        readonly Article article;

        public EditAnArticleSubMenu(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(path);

            editAnArticlePage = new EditAnArticlePage(Path);
        }

        private void EditValidateButton_Click(object sender, RoutedEventArgs e)
        {
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

        void EditACharacteristic()
        {
            try
            {
                int articleReference = EditAnArticlePage.characToDelete.Reference;
                if (EditRefTxtBox.Text == "zone de saisie..." || EditRefTxtBox.Text == "")
                {
                    editAnArticlePage.RefTxtBlockConfirm.Text = string.Empty;
                    editAnArticlePage.RefTxtBlockConfirm.Text = "Caractéristique inchangée";
                }
                else
                {
                    string newArtRefInput = EditRefTxtBox.Text;
                    bool correctArtRefNum = int.TryParse(newArtRefInput, out articleReference);
                    if (!correctArtRefNum)
                    {
                        editAnArticlePage.EditAnArticleTxtBlockError.Text = "Une erreur est survenue. Veuillez saisir une référence chiffrée.";
                    }
                    else
                    {
                        EditAnArticlePage.characToDelete.Reference = articleReference;
                        Write();
                        editAnArticlePage.EditAnArticleTxtBlockShow.Text = $"La référence de l'article a été modifiée avec succès:";
                    }
                }
            }
            catch (Exception except)
            {
                editAnArticlePage.EditAnArticleTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}.";
            }
            try
            {
                string articleName = EditAnArticlePage.characToDelete.Name;
                if (EditNameTxtBox.Text == "zone de saisie..." || EditNameTxtBox.Text == "")
                {
                    editAnArticlePage.NameTxtBlockConfirm.Text = string.Empty;
                    editAnArticlePage.NameTxtBlockConfirm.Text = "Caractéristique inchangée";
                }
                else
                {
                    articleName = EditNameTxtBox.Text;
                    if (Regex.IsMatch(articleName, @"^[a-zA-Z ]+$"))
                    {
                        Write();
                        editAnArticlePage.EditAnArticleTxtBlockShow.Text = $"Le nom de l'article a été modifié avec succès:";
                    }
                    else
                        throw new ArgumentException();
                }
            }
            catch (ArgumentException argExcept)
            {
                editAnArticlePage.EditAnArticleTxtBlockError.Text = $"L'erreur suivante est survenue: {argExcept.Message}. Veuillez saisir une entrée alphabétique.";
            }
            try
            {
                double articlePrice = EditAnArticlePage.characToDelete.Price;
                if (EditPriceTxtBox.Text == "zone de saisie..." || EditPriceTxtBox.Text == "")
                {
                    editAnArticlePage.PriceTxtBlockConfirm.Text = string.Empty;
                    editAnArticlePage.PriceTxtBlockConfirm.Text = "Caractéristique inchangée";
                }
                else
                {
                    string newArtPriceInput = EditPriceTxtBox.Text;
                    bool correctArtPriceNum = double.TryParse(newArtPriceInput, out articlePrice);
                    if (!correctArtPriceNum)
                    {
                        editAnArticlePage.EditAnArticleTxtBlockError.Text = "Une erreur est survenue. La saisie ne correspond pas à un prix.";
                    }
                    Write();
                    editAnArticlePage.EditAnArticleTxtBlockShow.Text = $"Le prix de l'article a été modifié avec succès:";
                }
            }
            catch (Exception except)
            {
                editAnArticlePage.EditAnArticleTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}.";
            }
            try
            {
                int articleQuantity = EditAnArticlePage.characToDelete.Quantity;
                if (EditQuantTxtBox.Text == "zone de saisie..." || EditQuantTxtBox.Text == "")
                {
                   editAnArticlePage.QuantTxtBlockConfirm.Text = string.Empty;
                   editAnArticlePage.QuantTxtBlockConfirm.Text = "Caractéristique inchangée";
                }
                else
                {
                    string newArtQuantInput = EditQuantTxtBox.Text;
                    bool correctArtQuantNum = int.TryParse(newArtQuantInput, out articleQuantity);
                    if (!correctArtQuantNum)
                    {
                        editAnArticlePage.EditAnArticleTxtBlockError.Text = "Une erreur est survenue. La saisie ne correspond pas à une quantité chiffrée.";
                    }
                    Write();
                    editAnArticlePage.EditAnArticleTxtBlockShow.Text = $"La quantité de l'article a été modifiée avec succès:";
                }
            }
            catch (Exception except)
            {
                editAnArticlePage.EditAnArticleTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}.";
            }
        }

        void Write()
        {
            Article.WriteAFile(Articles, Path);
        }

        void ShowAnArticle()
        {
            editAnArticlePage.RefTxtBlockConfirm.Text = $"Numéro: {article.Reference}";
            editAnArticlePage.NameTxtBlockConfirm.Text = $"Nom: {article.Name}";
            editAnArticlePage.PriceTxtBlockConfirm.Text = $"Prix: {article.Price}";
            editAnArticlePage.QuantTxtBlockConfirm.Text = $"Quantité: {article.Quantity}";
        }
    }
}
