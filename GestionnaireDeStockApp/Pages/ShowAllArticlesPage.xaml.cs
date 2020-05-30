using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour ShowAllArticlesPage.xaml
    /// </summary>
    public partial class ShowAllArticlesPage : Page
    {
        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        public ShowAllArticlesPage(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(path);
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
                var articles = Article.GetAllCharacteristics(Path);
                foreach (var article in articles)
                {
                    duplicate = true;
                    ShowAllArtTxtBlock.Text = $"{article}";
                    articleCounter++;
                }
                ShowAllArtTxtBlockCount.Text = $"Le nombre total d'articles trouvé est de {articleCounter}.";
                if (!duplicate)
                {
                    ShowAllArtTxtBlock.Text = "Il n'y a pas d'article dans le stock";
                }
            }
            catch (Exception except)
            {
                ShowAllArtTxtBlockError.Foreground = new SolidColorBrush(Colors.Red);
                ShowAllArtTxtBlockError.Text = $"L'erreur suivante est survenue: {except.Message}";
            }
        }
    }
}
