using DataLayer;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour SearchInAllDataBasePage.xaml
    /// </summary>
    public partial class SearchInAllDataBasePage : Page
    {
        bool ValidateButtonClick, SearchDataBaseTxtBoxClick, PriceValidateButtonClick, SearchMaxPriceTxtBoxClick = false;

        public SearchInAllDataBasePage()
        {
            InitializeComponent();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateButtonClick = true;
            ClearBlocks();
            SearchInAllDataBase();
        }

        private void SearchDataBaseTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchDataBaseTxtBoxClick = true;
            SearchDataBaseTxtBox.Text = string.Empty;
            ClearBlocks();
            SearchDataBaseTxtBox.GotFocus += SearchDataBaseTxtBox_GotFocus;
            SearchDataBaseTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void PriceValidateButton_Click(object sender, RoutedEventArgs e)
        {
            PriceValidateButtonClick = true;
            ClearBlocks();
            SearchForAnArticleByPriceRange();
        }

        private void SearchMinPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchMinPriceTxtBox.Text = string.Empty;
            SearchMinPriceTxtBox.GotFocus += SearchMinPriceTxtBox_GotFocus;
        }

        private void SearchMaxPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchMaxPriceTxtBoxClick = true;
            SearchMaxPriceTxtBox.Text = string.Empty;
            ClearBlocks();
            SearchMaxPriceTxtBox.GotFocus += SearchMaxPriceTxtBox_GotFocus;
            SearchMinPriceTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            SearchMaxPriceTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Recherche dans l'ensemble du fichier, une correspondance, quelque soit la saisie.
        /// </summary>
        public void SearchInAllDataBase()
        {
            try
            {
                string input = SearchDataBaseTxtBox.Text;

                if (Regex.IsMatch(input, @"^[a-zA-Z0-9, ]+$"))
                {
                    int articleCounter = 0;
                    bool duplicate = false;
                    using (var dbContext = new StockContext())
                    {
                        var products = dbContext.Products;

                        foreach (var product in products)
                        {
                            if (product.ToString().ToLower().Contains(input.ToLower()))
                            {
                                duplicate = true;
                                SearchDBArticleTxtBlock.Text += $"Référence: {product.Reference}\n" +
                                                                $"Nom: {product.Name}\n" +
                                                                $"Prix: {product.Price}\n" +
                                                                $"Quantité: {product.Quantity}\n\n";
                                articleCounter++;
                            }
                        }
                    }
                    SearchDBTxtBlockCount.Text = $"Nombre d'articles trouvés: {articleCounter}";
                    if (!duplicate)
                    {
                        SearchDBArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Orange);
                        SearchDBArticleTxtBlock.Text = "Article introuvable";
                    }
                }
                else
                    throw new Exception();
            }
            catch (Exception except)
            {
                SearchDBTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}. La saisie est incorrecte.";
            }
        }

        /// <summary>
        /// Recherche via une intervalle de prix minimum et maximum, le ou les articles compris dedans.
        /// </summary>
        public void SearchForAnArticleByPriceRange()
        {
            try
            {
                string newPriceMinInput = SearchMinPriceTxtBox.Text;
                bool correctMinNum = double.TryParse(newPriceMinInput, out double priceMin);

                string newPriceMaxInput = SearchMaxPriceTxtBox.Text;
                bool correctMaxNum = double.TryParse(newPriceMaxInput, out double priceMax);
                if (!correctMinNum || !correctMaxNum)
                {
                    SearchDBTxtBlockError.Text = "La saisie ne correspond pas à une saisie chiffrée.";
                }
                else
                {
                    int itemCounter = 0;
                    bool duplicate = false;
                    using (var dbContext = new StockContext())
                    {
                        var products = dbContext.Products;

                        foreach (var product in products)
                        {
                            if (Convert.ToDouble(product.Price) >= priceMin && Convert.ToDouble(product.Price) <= priceMax)
                            {
                                duplicate = true;
                                SearchDBArticleTxtBlock.Text += $"Référence: {product.Reference}\n" +
                                                                $"Nom: {product.Name}\n" +
                                                                $"Prix: {product.Price}\n" +
                                                                $"Quantité: {product.Quantity}\n\n";
                                itemCounter++;
                            }
                        }
                    }
                    SearchDBTxtBlockCount.Text = $"Nombre d'articles trouvés: {itemCounter}";
                    if (!duplicate)
                    {
                        SearchDBArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Orange);
                        SearchDBArticleTxtBlock.Text = "Aucun article trouvé";
                    }
                }
            }
            catch (Exception except)
            {
                SearchDBTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}";
            }
        }

        private void ClearBlocks()
        {
            if (SearchDataBaseTxtBoxClick == true || ValidateButtonClick == true)
            {
                SearchDBArticleTxtBlock.Text = string.Empty;
                SearchDBTxtBlockError.Text = string.Empty;
                SearchDBTxtBlockCount.Text = string.Empty;
                SearchDBArticleTxtBlock.Foreground = new SolidColorBrush(Colors.White);
            }
            if (PriceValidateButtonClick == true || SearchMaxPriceTxtBoxClick == true)
            {
                SearchDBArticleTxtBlock.Text = string.Empty;
                SearchDBTxtBlockError.Text = string.Empty;
                SearchDBTxtBlockCount.Text = string.Empty;
                SearchDBArticleTxtBlock.Foreground = new SolidColorBrush(Colors.White);
            }
        }
    }
}
