using DataLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public AlertWindow()
        {
            InitializeComponent();
            AddProductsInRows();
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.F1)
                Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void AddProductsInRows()
        {
            using (var dbContext = new StockContext())
            {
                var products = dbContext.Products;

                List<Product> productAdded = new List<Product>();

                foreach (var product in products)
                {
                    if (product.Quantity <= 10)
                    {
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
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.F5)
                Close();
        }
    }
}
