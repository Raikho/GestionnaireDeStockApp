using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour SearchByReferencePage.xaml
    /// </summary>
    public partial class SearchByReferencePage : Page
    {
        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        public SearchByReferencePage(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(path);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBlock();
            SearchForAnArticleByReference();
        }

        private void SearchRefTexBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchRefTexBox.Text = string.Empty;
            ClearTheBlock();
            SearchRefTexBox.GotFocus += SearchRefTexBox_GotFocus;
            SearchRefTexBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ClearTheBlock()
        {
            SearchRefArticleTxtBlock.Text = string.Empty;
            SearchRefTxtBlockError.Text = string.Empty;
            SearchRefTxtBlockCount.Text = string.Empty;
            SearchRefArticleTxtBlock.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Recherche via la référence de l'article recherché.
        /// </summary>
        public void SearchForAnArticleByReference()
        {
            try
            {
                //consoleWrite($"Veuillez entrez votre recherche par référence :");
                string newInput = SearchRefTexBox.Text;
                bool correctNUm = long.TryParse(newInput, out long searchReference);

                if (!correctNUm)
                {
                    SearchRefTxtBlockError.Text = "Une erreur est survenue. Veuillez saisir une référence chiffrée.";
                }
                else
                {
                    int articleCounter = 0;
                    bool duplicate = false;
                    foreach (var article in Articles)
                    {
                        if (article.Reference.ToString() == searchReference.ToString())
                        {
                            duplicate = true;
                            SearchRefArticleTxtBlock.Text = $"{article}";
                            articleCounter++;
                        }
                    }
                    SearchRefTxtBlockCount.Text = $"Nombre d'articles trouvés: {articleCounter}";
                    if (!duplicate)
                    {
                        SearchRefArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Orange);
                        SearchRefArticleTxtBlock.Text = "Article introuvable";
                    }
                }
            }

            catch (Exception except)
            {
                SearchRefTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}";
            }
        }
    }
}
