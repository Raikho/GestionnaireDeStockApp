using DataLayer;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Windows.Data;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour ArticlesListPage.xaml
    /// </summary>
    public partial class ArticlesListManagementPage : Page
    {
        public ArticlesListManagementPage()
        {
            InitializeComponent();
            LoadDataBaseProducts();
        }

        private void AddANewArticleButton_Click(object sender, RoutedEventArgs e)
        {
            AddAnArticleWindow addAnArticleWindow = new AddAnArticleWindow();
            addAnArticleWindow.Show();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDataBaseProducts();
        }

        private void LoadDataBaseProducts()
        {
            using (var dbContext = new StockContext())
            {
                List<Product> productAdded = new List<Product>();

                var products = dbContext.Products;

                foreach (var product in products)
                {
                    productAdded.Add(new Product()
                    {
                        Reference = product.Reference,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity
                    });
                }
                productsDataGrid.ItemsSource = productAdded;
            }
        }

        private void EditAnArticleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dbContext = new StockContext();
                Product productRow = productsDataGrid.SelectedItem as Product;
                string m = productRow.Reference;
                Product product = (from p in dbContext.Products
                                     where p.Reference == productRow.Reference
                                     select p).SingleOrDefault();
                product.Reference = productRow.Reference;
                product.Name = productRow.Name;
                product.Price = productRow.Price;
                product.Quantity = productRow.Quantity;
                dbContext.Entry(product);
                dbContext.SaveChanges();
                MessageBox.Show("Produit modifié avec succés!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void DeleteAnArticleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var dbContext = new StockContext())
                {
                    var selectedRow = productsDataGrid.CurrentCell.Item;
                    if (selectedRow != DBNull.Value)
                    {
                        if(MessageBox.Show("Etes-vous sûr de vouloir supprimer cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            dbContext.Remove(selectedRow);
                            dbContext.SaveChanges();
                        }
                    }

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            SearchTextBox.Foreground = new SolidColorBrush(Colors.White);
            SearchTextBox.GotFocus += SearchTextBox_GotFocus;
            if (SearchTextBox.Text == string.Empty)
            {
                LoadDataBaseProducts();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchAnArticle();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SearchAnArticle();
            }
        }

        private void SearchAnArticle()
        {
            try
            {
                string input = SearchTextBox.Text;
                if (!Regex.IsMatch(input, @"^[a-zA-Z0-9, ]+$"))
                {
                    MessageBox.Show("Une erreur est survenue. Veuillez effectuer une saisie alphanumérique.");
                }
                else
                {
                    List<Product> productAdded = new List<Product>();
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

                                productAdded.Add(new Product()
                                {
                                    Reference = product.Reference,
                                    Name = product.Name,
                                    Price = product.Price,
                                    Quantity = product.Quantity
                                });
                            }
                        }
                        productsDataGrid.ItemsSource = productAdded;
                    }
                    if (articleToFind == null)
                    {
                        MessageBox.Show("Article introuvable");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void SearchMinPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchMinPriceTxtBox.Text = string.Empty;
            SearchMinPriceTxtBox.Foreground = new SolidColorBrush(Colors.White);
            SearchMinPriceTxtBox.GotFocus += SearchMinPriceTxtBox_GotFocus;
            if (SearchMinPriceTxtBox.Text == string.Empty)
                LoadDataBaseProducts();
        }

        private void SearchMaxPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchMaxPriceTxtBox.Text = string.Empty;
            SearchMaxPriceTxtBox.Foreground = new SolidColorBrush(Colors.White);
            SearchMaxPriceTxtBox.GotFocus += SearchMaxPriceTxtBox_GotFocus;
            if (SearchMaxPriceTxtBox.Text == string.Empty)
                LoadDataBaseProducts();
        }

        private void PriceSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByPriceInterval();
        }

        private void SearchMinPriceTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SearchByPriceInterval();
            }
        }

        private void SearchMaxPriceTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SearchByPriceInterval();
            }
        }

        private void SearchByPriceInterval()
        {
            try
            {
                string newPriceMinInput = SearchMinPriceTxtBox.Text;
                bool correctMinNum = double.TryParse(newPriceMinInput, out double priceMin);

                string newPriceMaxInput = SearchMaxPriceTxtBox.Text;
                bool correctMaxNum = double.TryParse(newPriceMaxInput, out double priceMax);
                if (!correctMinNum || !correctMaxNum)
                {
                    MessageBox.Show("La saisie ne correspond pas à une saisie chiffrée.");
                }
                else
                {
                    List<Product> productAdded = new List<Product>();
                    Product articleToFind = null;
                    using (var dbContext = new StockContext())
                    {
                        var products = dbContext.Products;

                        foreach (var product in products)
                        {
                            if (product.Price >= priceMin && product.Price <= priceMax)
                            {
                                articleToFind = product;

                                productAdded.Add(new Product()
                                {
                                    Reference = product.Reference,
                                    Name = product.Name,
                                    Price = product.Price,
                                    Quantity = product.Quantity
                                });
                            }
                        }
                        productsDataGrid.ItemsSource = productAdded;
                    }
                    if (articleToFind == null)
                    {
                        MessageBox.Show("Aucun article trouvé");
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
