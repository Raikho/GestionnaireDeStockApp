using DataLayer;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        static SalesManagementPage salesManagementPage;
        public SalesManagementPage()
        {
            salesManagementPage = this;

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
            SellerNameTxtBox.Text = $"Vendeur: {LoginWindow.Username}";
        }

        private void ShowDateOnTicket()
        {
            DateTxtBox.Text = DateTime.Now.ToLongDateString();
        }

        private void AddToSell_Click(object sender, RoutedEventArgs e)
        {
            SalesParametersWindow salesParametersWindow = new SalesParametersWindow();
            salesParametersWindow.Show();
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

        public static void CalculateTheTicketPrice()
        {
            try
            {
                using (var dbContext = new StockContext())
                {
                    var products = dbContext.Products;
                    var selectedRow = salesManagementPage.ArticleToSellDataGrid.CurrentCell.Item;
                    Product articleToSell = (Product)selectedRow;

                    double sum;
                    double totalSum = 0;

                    if (ControlInputService.CorrectPickedChara == false)
                        MessageBox.Show("La saisie est incorrecte.");
                    else
                    {
                        if (MessageBox.Show("Etes-vous sûr de vouloir ajouter cet article?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            salesManagementPage.NameTxtBlock.Text += $"{articleToSell.Name}\n";
                            salesManagementPage.PriceTxtBlock.Text += $"{articleToSell.Price}\n";
                            salesManagementPage.QuantTxtBlock.Text += $"{SalesParametersWindow.Quantity}\n";

                            sum = articleToSell.Price * SalesParametersWindow.Quantity;
                            salesManagementPage.SubTotalTxtBlock.Text += $"{Math.Round(sum, 2)}\n";

                            var tempTotal = salesManagementPage.CalculateADiscountPrice(sum);

                            salesManagementPage.totalSumList.Add(tempTotal);
                        }
                    }
                    foreach (var item in salesManagementPage.totalSumList)
                    {
                        totalSum += item;
                    }
                    var finalTotal = totalSum;
                    salesManagementPage.TotalTxtBlock.Text = $"{Math.Round(finalTotal, 2)}€ TTC";
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private double CalculateADiscountPrice(double totalSum)
        {
            double pourcentDiscount = SalesParametersWindow.PourcentDiscount / 100;
            double discount = SalesParametersWindow.Discount;

            double pourcentDiscountPrice = 0;
            double discountPrice = 0;

            pourcentDiscountPrice = totalSum - (totalSum * pourcentDiscount);
            discountPrice = pourcentDiscountPrice - discount;

            double totalDiscount = totalSum - discountPrice;

            if (totalDiscount == 0)
                return 0;
            else
            {
                salesManagementPage.NameTxtBlock.Text += "Remise\n";
                salesManagementPage.SubTotalTxtBlock.Text += $"-{Math.Round(totalDiscount, 2)}\n";
            }
            return discountPrice;
        }

        private void CalculateTicketNumber()
        {
            TicketNumTxtBox.Text = $"Ticket no: {DateTime.Now.ToShortDateString()}/001";
        }
    }
}