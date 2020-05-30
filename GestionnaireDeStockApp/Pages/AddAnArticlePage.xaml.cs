using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour AddAnArticlePage.xaml
    /// </summary>
    public partial class AddAnArticlePage : Page
    {
        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        public AddAnArticlePage(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(Path);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBLock();
            CreateNewArticle();
        }

        private void AddRefTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            AddRefTxtBox.Text = string.Empty;
            ClearTheBLock();
            AddRefTxtBox.Foreground = new SolidColorBrush(Colors.Black);
            AddRefTxtBox.GotFocus += AddRefTxtBox_GotFocus;
        }

        private void AddNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            AddNameTxtBox.Text = string.Empty;
            ClearTheBLock();
            AddNameTxtBox.GotFocus += AddNameTxtBox_GotFocus;
            AddNameTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void AddPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            AddPriceTxtBox.Text = string.Empty;
            ClearTheBLock();
            AddPriceTxtBox.GotFocus += AddPriceTxtBox_GotFocus;
            AddPriceTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void AddQuantTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            AddQuantTxtBox.Text = string.Empty;
            ClearTheBLock();
            AddQuantTxtBox.GotFocus += AddQuantTxtBox_GotFocus;
            AddQuantTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ClearTheBLock()
        {
            RefTxtBlockInfo.Text = string.Empty;
            NameTxtBlockInfo.Text = string.Empty;
            PriceTxtBlockInfo.Text = string.Empty;
            QuantTxtBlockInfo.Text = string.Empty;
            CreateTxtBlockInfo.Text = string.Empty;
            RefTxtBlockConfirm.Text = string.Empty;
            NameTxtBlockConfirm.Text = string.Empty;
            PriceTxtBlockConfirm.Text = string.Empty;
            QuantTxtBlockConfirm.Text = string.Empty;
            CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Green);
        }

        void CreateNewArticle()
        {
            try
            {
                var reference = AddAReference();

                if (reference == 0)
                {
                    CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Red);
                    CreateTxtBlockInfo.Text = "La saisie est incorrecte";
                }
                else
                {
                    bool duplicate = false;
                    foreach (var article in Articles)
                    {
                        if (article.Reference == reference)
                        {
                            duplicate = true;
                            CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Orange);
                            CreateTxtBlockInfo.Text = "L'article existe déjà";
                            break;
                        }
                    }

                    if (!duplicate)
                    {
                        string name = AddAName();
                        double price = AddAPrice();
                        int quantity = AddAQuantity();

                        if (name == "null" || price == 0 || quantity == 0)
                        {
                            CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Red);
                            CreateTxtBlockInfo.Text = "La saisie est incorrecte";
                        }
                        else
                        {
                            var newArticle = new Article(reference, name, price, quantity);

                            Articles.Add(newArticle);

                            Write();

                            CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Green);
                            ShowAnArticle(newArticle);
                            CreateTxtBlockInfo.Text = $"Le nouveau produit a été intégré au stock: ";
                        }
                    }
                }


            }
            catch (Exception except)
            {
                CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Red);
                CreateTxtBlockInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
            }

        }

        void ShowAnArticle(Article article)
        {
            RefTxtBlockConfirm.Text = $"Numéro: {article.Reference}";
            NameTxtBlockConfirm.Text = $"Nom: {article.Name}";
            PriceTxtBlockConfirm.Text = $"Prix: {article.Price}";
            QuantTxtBlockConfirm.Text = $"Quantité: {article.Quantity}";
        }

        /// <summary>
        /// Appel la fonction d'écriture de la classe "Article" et écrit dans le fichier.
        /// </summary>
        void Write()
        {
            Article.WriteAFile(Articles, Path);
        }

        /// <summary>
        /// Ajoute une "référence" à un article en création.
        /// </summary>
        /// <returns></returns>
        int AddAReference()
        {
            try
            {
                //RefTxtBlockInfo.Text = "Veuillez saisir le numéro de référence de l'article :";
                string newInput = AddRefTxtBox.Text;
                bool correctNum = int.TryParse(newInput, out int reference);
                if (!correctNum)
                {
                    RefTxtBlockInfo.Text = "Une erreur est survenue. Veuillez saisir une référence chiffrée.\n";
                    //return AddAReference();
                }
                return reference;
            }
            catch (Exception except)
            {
                RefTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Orange);
                RefTxtBlockInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
                return 0;
            }
        }

        /// <summary>
        /// Ajoute une "nom" à un article en création.
        /// </summary>
        /// <returns></returns>
        string AddAName()
        {
            try
            {
                //NameTxtBlockInfo.Text = "Veuillez saisir le nom de l'article:";
                string name = AddNameTxtBox.Text;
                if (!Regex.IsMatch(name, @"^[a-zA-Z ]+$"))
                    throw new ArgumentException();
                return name;
            }
            catch (ArgumentException argExcept)
            {
                NameTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Red);
                NameTxtBlockInfo.Text = $"L'erreur suivante est survenue :{argExcept.Message}. Veuillez saisir un nom alphabétique.\n";
                return "null";
            }
        }

        /// <summary>
        /// Ajoute une "prix" à un article en création.
        /// </summary>
        /// <returns></returns>
        double AddAPrice()
        {
            try
            {
                //PriceTxtBlockInfo.Text = "Veuillez saisir le prix de l'article:";
                string newInput = AddPriceTxtBox.Text;
                bool correctNum = double.TryParse(newInput, out double price);
                if (!correctNum)
                {
                    PriceTxtBlockInfo.Text = "Une erreur est survenue. Veuillez saisir une référence chiffrée.\n";
                    //return AddAPrice();
                }
                return price;
            }
            catch (Exception except)
            {
                PriceTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Red);
                PriceTxtBlockInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
                return 0;
            }
        }

        /// <summary>
        /// Ajoute une "quantité" à un article en création.
        /// </summary>
        /// <returns></returns>
        int AddAQuantity()
        {
            try
            {
                string newInput = AddQuantTxtBox.Text;
                bool correctNum = int.TryParse(newInput, out int quantity);
                if (!correctNum)
                {
                    QuantTxtBlockInfo.Text = "Une erreur est survenue. Veuillez saisir une référence chiffrée.\n";
                }
                return quantity;
            }
            catch (Exception except)
            {
                QuantTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Red);
                QuantTxtBlockInfo.Text = $"L'erreur suivante est survenue: {except}";
                return 0;
            }
        }
    }
}
