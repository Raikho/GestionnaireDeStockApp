using DataLayer;
using System;
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
                if (!Regex.IsMatch(input, @"^[a-zA-Z0-9, ]+$"))
                {
                    SearchDBTxtBlockError.Text = "Une erreur est survenue. Veuillez effectuer une saisie alphanumérique.";
                }
                else
                {
                    int articleCounter = 0;
                    Product articleToFind = null;

                    using (var dbContext = new StockContext())
                    {
                        var products = dbContext.Products;

                        foreach (var product in products)
                        {

                            if (product.Reference.ToString().ToLower().Contains(input.ToString().ToLower())
                                || product.Name.ToString().ToLower().Contains(input.ToString().ToLower())
                                || product.Price.ToString().ToLower().Contains(input.ToString().ToLower())
                                || product.Quantity.ToString().ToLower().Contains(input.ToString().ToLower()))
                            {
                                articleToFind = product;
                                articleCounter++;
                                SearchDBArticleTxtBlock.Text += $"Référence: {articleToFind.Reference}\n" +
                                                                $"Nom: {articleToFind.Name}\n" +
                                                                $"Prix: {articleToFind.Price}\n" +
                                                                $"Quantité: {articleToFind.Quantity}\n\n";
                            }
                        }
                    }
                    if (articleToFind != null)
                        SearchDBTxtBlockCount.Text = $"Nombre d'articles trouvés: {articleCounter}";
                    else
                    {
                        SearchDBArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Orange);
                        SearchDBArticleTxtBlock.Text = "Article introuvable";
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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
                    Product articleToFind = null;
                    using (var dbContext = new StockContext())
                    {
                        var products = dbContext.Products;

                        foreach (var product in products)
                        {
                            if (product.Price >= priceMin && product.Price <= priceMax)
                            {
                                articleToFind = product;
                                itemCounter++;
                                SearchDBArticleTxtBlock.Text += $"Référence: {articleToFind.Reference}\n" +
                                                                $"Nom: {articleToFind.Name}\n" +
                                                                $"Prix: {articleToFind.Price}\n" +
                                                                $"Quantité: {articleToFind.Quantity}\n\n";
                            }
                        }
                    }
                    if (articleToFind != null)
                        SearchDBTxtBlockCount.Text = $"Nombre d'articles trouvés: {itemCounter}";
                    else
                    {
                        SearchDBArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Orange);
                        SearchDBArticleTxtBlock.Text = "Aucun article trouvé";
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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
