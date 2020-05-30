using System;
using System.Collections.Generic;
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
        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        public DeleteAnArticlePage(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(path);
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
            DeleteRefArticleTxtBlock.Foreground = new SolidColorBrush(Colors.Green);
        }

        /// <summary>
        /// Supprime un article via sa référence.
        /// </summary>
        public void RemoveAnArticleByReference()
        {
            try
            {
                string newInput = DeleteRefTxtBox.Text;
                bool correctNum = int.TryParse(newInput, out int reference);
                if (!correctNum)
                {
                    DeleteRefTxtBlockError.Foreground = new SolidColorBrush(Colors.Orange);
                    DeleteRefTxtBlockError.Text = "Une erreur est survenue. Veuillez saisir une référence chiffrée.";
                }
                else
                {
                    int articleCounter = 0;
                    bool duplicate = false;
                    Article articleToDelete = null;

                    foreach (var art in Articles)
                    {
                        if (art.Reference == reference)
                        {
                            articleToDelete = art;
                            break;
                        }
                    }
                    if (articleToDelete != null)
                    {
                        articleCounter++;
                        duplicate = true;
                        DeleteRefArticleTxtBlock.Text = $"L'article suivant a été supprimé:\n{articleToDelete}";
                        DeleteRefTxtBlockCount.Text = $"Le nombre d'articles supprimé est: {articleCounter}";
                        Articles.Remove(articleToDelete);
                        Write();
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

        /// <summary>
        /// Appel la fonction d'écriture de la classe "Article" et écrit dans le fichier.
        /// </summary>
        void Write()
        {
            Article.WriteAFile(Articles, Path);
        }
    }
}
