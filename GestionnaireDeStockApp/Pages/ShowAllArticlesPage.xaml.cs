using DataLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour ShowAllArticlesPage.xaml
    /// </summary>
    public partial class ShowAllArticlesPage : Page
    {
        public ShowAllArticlesPage()
        {
            InitializeComponent();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ShowAllItems();
        }

        /// <summary>
        /// Affiche l'ensemble des articles compris dans le fichier texte.
        /// </summary>
        public void ShowAllItems()
        {
            try
            {
                int articleCounter = 0;
                bool duplicate = false;
                ShowAllArtTxtBlock.Text = string.Empty;
                using (var dbContext = new StockContext())
                {
                    var products = dbContext.Products;

                    foreach (var product in products)
                    {
                        duplicate = true;
                        ShowAllArtTxtBlock.Text += $"Référence: {product.Reference}\n" +
                                                   $"Nom: {product.Name}\n" +
                                                   $"Prix: {product.Price}\n" +
                                                   $"Quantité: {product.Quantity}\n\n";
                        articleCounter++;
                    }
                }
                ShowAllArtTxtBlockCount.Text = $"Le nombre total d'articles trouvé est de {articleCounter}.";
                if (!duplicate)
                {
                    ShowAllArtTxtBlock.Text = "Il n'y a pas d'article dans le stock";
                }
            }
            catch (Exception except)
            {
                ShowAllArtTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}";
            }
        }
    }
}
