using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour SalesManagementPage.xaml
    /// </summary>
    public partial class SalesManagementPage : Page
    {
        public List<double> totalSumList = new List<double>();

        public SalesManagementPage()
        {
            InitializeComponent();
            ShowSellerNameOnTicket();
            ShowDateOnTicket();
            CalculateTicketNumber();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchAnArticleToSell();
        }

        private void SearchAnArticle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchAnArticleToSell();
        }

        private void ShowSellerNameOnTicket()
        {
            SellerNameTxtBox.Text = $"Vendeur: {MainWindow.currentLgWindow.Username}";
        }

        private void ShowDateOnTicket()
        {
            DateTxtBox.Text = DateTime.Now.ToLongDateString();
        }

        private void AddToSell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var dbContext = new StockContext())
                {
                    var products = dbContext.Products;
                    var selectedRow = ArticleToSellDataGrid.CurrentCell.Item;

                    double sum;
                    double totalSum = 0;

                    foreach (var product in products)
                    {
                        Product articleToSell = (Product)selectedRow;
                        if (articleToSell.ToString().ToLower() == product.ToString().ToLower())
                        {
                            if (MessageBox.Show("Etes-vous sûr de vouloir ajouter cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                NameTxtBlock.Text += $"{articleToSell.Name}\n";
                                PriceTxtBlock.Text += $"{articleToSell.Price}\n";
                                QuantTxtBlock.Text += $"{articleToSell.Quantity}\n";

                                sum = articleToSell.Price * articleToSell.Quantity;
                                SubTotalTxtBlock.Text += $"{Math.Round(sum, 2)}\n";

                                totalSumList.Add(sum);
                            }
                        }
                    }                
                    foreach (var item in totalSumList)
                    {
                        totalSum += item;
                    }
                    TotalTxtBlock.Text = $"{Math.Round(totalSum, 2)}€ TTC";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void SearchAnArticleToSell()
        {
            try
            {
                string input = SearchAnArticleToSellTxtBox.Text;
                if (!Regex.IsMatch(input, @"^[a-zA-Z0-9, ]+$"))
                {
                    MessageBox.Show("La saisie ne correspond pas à une saisie alphanumérique.");
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
                        ArticleToSellDataGrid.ItemsSource = productAdded;
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

        private void CalculateTicketNumber()
        {
            TicketNumTxtBox.Text = $"Ticket no: {DateTime.Now.ToShortDateString()}/001";
        }
    }
}