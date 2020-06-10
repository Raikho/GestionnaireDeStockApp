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
            AddProductsInRows();
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

        public void AddProductsInRows()
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
    }
}
