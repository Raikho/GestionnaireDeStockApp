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
        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        public SearchInAllDataBasePage(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(path);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBlock();
            SearchInAllDataBase();
        }

        private void SearchDataBaseTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchDataBaseTxtBox.Text = string.Empty;
            ClearTheBlock();
            SearchDataBaseTxtBox.GotFocus += SearchDataBaseTxtBox_GotFocus;
            SearchDataBaseTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ClearTheBlock()
        {
            SearchDBArticleTxtBlock.Text = string.Empty;
            SearchDBTxtBlockError.Text = string.Empty;
            SearchDBTxtBlockCount.Text = string.Empty;
            SearchDBArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Green);
        }

        /// <summary>
        /// Recherche dans l'ensemble du fichier, une correspondance, quelque soit la saisie.
        /// </summary>
        public void SearchInAllDataBase()
        {
            try
            {
                string input = SearchDataBaseTxtBox.Text;

                if (Regex.IsMatch(input, @"^[a-zA-Z0-9,]+$"))
                {
                    int articleCounter = 0;
                    bool duplicate = false;
                    foreach (var article in Articles)
                    {
                        if (article.ToString().ToLower().Contains(input))
                        {
                            duplicate = true;
                            SearchDBArticleTxtBlock.Text = $"{article}";
                            articleCounter++;
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
                SearchDBTxtBlockError.Foreground = new SolidColorBrush(Colors.Red);
                SearchDBTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}. La saisie est incorrecte.";
            }
        }
    }
}
