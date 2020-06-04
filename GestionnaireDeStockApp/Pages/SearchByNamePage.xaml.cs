using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour SearchByNamePage.xaml
    /// </summary>
    public partial class SearchByNamePage : Page
    {
        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        public SearchByNamePage(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(path);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBlock();
            SearchForAnArticleByName();
        }

        private void SearchNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchNameTxtBox.Text = string.Empty;
            ClearTheBlock();
            SearchNameTxtBox.GotFocus += SearchNameTxtBox_GotFocus;
            SearchNameTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ClearTheBlock()
        {
            SearchNameArticleTxtBlock.Text = string.Empty;
            SearchNameTxtBlockError.Text = string.Empty;
            SearchNameTxtBlockCount.Text = string.Empty;
            SearchNameArticleTxtBlock.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Recherche via le nom de l'article recherché.
        /// </summary>
        public void SearchForAnArticleByName()
        {
            try
            {
                string input = SearchNameTxtBox.Text;

                if (Regex.IsMatch(input, @"^[a-zA-Z ]+$"))
                {
                    int articleCounter = 0;
                    bool duplicate = false;
                    foreach (var article in Articles)
                    {
                        if (article.Name.ToLower().Contains(input.ToString().ToLower()))
                        {
                            duplicate = true;
                            SearchNameArticleTxtBlock.Text += $"{article}";
                            articleCounter++;
                        }
                    }
                    SearchNameTxtBlockCount.Text = $"Nombre d'articles trouvés: {articleCounter}";
                    if (!duplicate)
                    {
                        SearchNameArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Orange);
                        SearchNameArticleTxtBlock.Text = "Article introuvable";
                    }
                }
                else
                    throw new ArgumentException();
            }
            catch (ArgumentException argExcept)
            {
                SearchNameTxtBlockError.Text = $"L'erreur suivante est survenue: {argExcept.Message}. Veuillez saisir une entrée alphabétique.";
            }
        }
    }
}
