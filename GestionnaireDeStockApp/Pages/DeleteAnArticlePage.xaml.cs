using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour DeleteAnArticlePage.xaml
    /// </summary>
    public partial class DeleteAnArticlePage : Page
    {
        public DeleteAnArticlePage()
        {
            InitializeComponent();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBlock();
            RemoveAnArticleByReference();
        }

        private void DeleteRefTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DeleteRefTxtBox.Text = string.Empty;
            ClearTheBlock();
            DeleteRefTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            DeleteRefTxtBox.GotFocus += DeleteRefTxtBox_GotFocus;
        }

        private void ClearTheBlock()
        {
            DeleteRefArticleTxtBlock.Text = string.Empty;
            DeleteRefTxtBlockCount.Text = string.Empty;
            DeleteRefTxtBlockError.Text = string.Empty;
            DeleteRefArticleTxtBlock.Foreground = new SolidColorBrush(Colors.GreenYellow);
        }

        /// <summary>
        /// Supprime un article via sa référence.
        /// </summary>
        public void RemoveAnArticleByReference()
        {
            try
            {
                string reference = DeleteRefTxtBox.Text;
                if (!Regex.IsMatch(reference, @"^[a-zA-Z0-9, ]+$"))
                {
                    DeleteRefTxtBlockError.Text = "Une erreur est survenue. Veuillez effectuer une saisie alphanumérique.";
                }
                else
                {
                    int articleCounter = 0;
                    bool duplicate = false;
                    Product articleToDelete = null;

                    using (var dbContext = new StockContext())
                    {
                        articleToDelete = dbContext.Products.Where(c => c.Reference.ToLower() == reference.ToLower()).FirstOrDefault();
                        dbContext.Remove(articleToDelete);
                        dbContext.SaveChanges();
                    }
                    if (articleToDelete != null)
                    {
                        articleCounter++;
                        duplicate = true;
                        DeleteRefArticleTxtBlock.Text = $"L'article suivant a été supprimé:\n\n" +
                                                        $"Référence: { articleToDelete.Reference}\n" +
                                                        $"Nom: {articleToDelete.Name}\n" +
                                                        $"Prix: {articleToDelete.Price}\n" +
                                                        $"Quantité: {articleToDelete.Quantity}\n\n";
                        DeleteRefTxtBlockCount.Text = $"Le nombre d'articles supprimé est: {articleCounter}";
                    }
                    if (!duplicate)
                    {
                        DeleteRefArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Orange);
                        DeleteRefArticleTxtBlock.Text = "Article introuvable";
                    }
                }
            }
            catch (Exception except)
            {
                DeleteRefTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}.";
            }
        }
    }
}
