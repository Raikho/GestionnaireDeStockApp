using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour SearchByPricePage.xaml
    /// </summary>
    public partial class SearchByPricePage : Page
    {
        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        public SearchByPricePage(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(path);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBlock();
            SearchForAnArticleByPriceRange();
        }

        private void SearchMinPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchMinPriceTxtBox.Text = string.Empty;
            SearchMinPriceTxtBox.GotFocus += SearchMinPriceTxtBox_GotFocus;
        }

        private void SearchMaxPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchMaxPriceTxtBox.Text = string.Empty;
            ClearTheBlock();
            SearchMaxPriceTxtBox.GotFocus += SearchMaxPriceTxtBox_GotFocus;
            SearchMinPriceTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            SearchMaxPriceTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ClearTheBlock()
        {
            SearchPriceArticleTxtBlock.Text = string.Empty;
            SearchPriceTxtBlockError.Text = string.Empty;
            SearchPriceTxtBlockCount.Text = string.Empty;
            SearchPriceArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Green);
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
                    SearchPriceTxtBlockError.Foreground = new SolidColorBrush(Colors.Orange);
                    SearchPriceTxtBlockError.Text = "La saisie ne correspond pas à une saisie chiffrée.";
                }
                else
                {
                    int itemCounter = 0;
                    bool duplicate = false;
                    foreach (var article in Articles)
                    {
                        if (article.Price > priceMin && article.Price < priceMax)
                        {
                            duplicate = true;
                            SearchPriceArticleTxtBlock.Text = $"{article}";
                            itemCounter++;
                        }
                    }
                    SearchPriceTxtBlockCount.Text = $"Nombre d'articles trouvés: {itemCounter}";
                    if (!duplicate)
                    {
                        SearchPriceArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Orange);
                        SearchPriceArticleTxtBlock.Text = "Aucun article trouvé";
                    }
                }
            }
            catch (Exception except)
            {
                SearchPriceTxtBlockError.Foreground = new SolidColorBrush(Colors.Red);
                SearchPriceTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}";
            }
        }
    }
}
