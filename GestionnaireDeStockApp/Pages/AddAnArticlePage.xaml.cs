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
    /// Logique d'interaction pour AddAnArticlePage.xaml
    /// </summary>
    public partial class AddAnArticlePage : Page
    {
        public AddAnArticlePage()
        {
            InitializeComponent();
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
            CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.GreenYellow);
        }

        void CreateNewArticle()
        {
            try
            {
                var reference = AddAReference();

                if (reference == "null")
                {
                    CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Orange);
                    CreateTxtBlockInfo.Text = "La saisie est incorrecte";
                }
                else
                {
                    bool duplicate = false;

                    using (var dbContext = new StockContext())
                    {
                        var products = dbContext.Products;
                        foreach (var product in products)
                        {
                            if (product.Reference.ToLower() == reference.ToLower())
                            {
                                duplicate = true;
                                CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Orange);
                                CreateTxtBlockInfo.Text = "L'article existe déjà";
                                break;
                            }
                        }
                    }

                    if (!duplicate)
                    {
                        string name = AddAName();
                        double price = AddAPrice();
                        int quantity = AddAQuantity();

                        if (name == "null" || price == 0 || quantity == 0)
                        {
                            CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Orange);
                            CreateTxtBlockInfo.Text = "La saisie est incorrecte";
                        }
                        else
                        {
                            using (var dbContext = new StockContext())
                            {
                                var products = dbContext.Products;

                                var product = new Product()
                                {
                                    Reference = reference,
                                    Name = name,
                                    Price = price,
                                    Quantity = quantity,
                                };
                                products.Add(product);
                                dbContext.SaveChanges();
                            }
                            CreateTxtBlockInfo.Text = $"Le nouveau produit a été intégré au stock: ";
                            ShowAnArticle(reference, name, price, quantity);
                        }
                    }
                }
            }
            catch (Exception except)
            {
                CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Orange);
                CreateTxtBlockInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
            }

        }

        void ShowAnArticle(string reference, string name, double price, int quantity)
        {
            RefTxtBlockConfirm.Text = $"{reference}";
            NameTxtBlockConfirm.Text = $"{name}";
            PriceTxtBlockConfirm.Text = $"{price}";
            QuantTxtBlockConfirm.Text = $"{quantity}";
        }

        /// <summary>
        /// Ajoute une "référence" à un article en création.
        /// </summary>
        /// <returns></returns>
        string AddAReference()
        {
            try
            {
                string newInput = AddRefTxtBox.Text;
                if (!Regex.IsMatch(newInput, @"^[a-zA-Z0-9, ]+$"))
                {
                    RefTxtBlockInfo.Text = "Référence: veuillez effectuer une saisie alphanumérique.\n";
                    newInput = "null";
                }
                return newInput;
            }
            catch (Exception except)
            {
                RefTxtBlockInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
                return "null";
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
                string name = AddNameTxtBox.Text;
                if (!Regex.IsMatch(name, @"^[a-zA-Z0-9, ]+$"))
                {
                    NameTxtBlockInfo.Text = $"Nom: veuillez effectuer une saisie alphanumérique.\n";
                    name = "null";
                }
                return name;
            }
            catch (Exception except)
            {
                NameTxtBlockInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
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
                string newInput = AddPriceTxtBox.Text;
                bool correctNum = double.TryParse(newInput, out double price);
                if (!correctNum)
                {
                    PriceTxtBlockInfo.Text = "Prix: veuillez saisir un prix chiffré.\n";
                    price = 0;
                }
                return price;
            }
            catch (Exception except)
            {
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
                    QuantTxtBlockInfo.Text = "Quantité: veuillez saisir une quantité chiffrée.\n";
                    quantity = 0;
                }
                return quantity;
            }
            catch (Exception except)
            {
                QuantTxtBlockInfo.Text = $"L'erreur suivante est survenue: {except}";
                return 0;
            }
        }
    }
}
