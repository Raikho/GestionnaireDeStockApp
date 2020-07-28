using BusinessLogicLayer;
using DataTransfertObject;
using GestionnaireDeStockApp.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GestionnaireDeStockApp.Pages
{
    /// <summary>
    /// Logique d'interaction pour InventoryManagementPage.xaml
    /// </summary>
    public partial class InventoryManagementPage : Page
    {
        public InventoryManagementPage()
        {
            InitializeComponent();
            SearchTextBox.Focus();
            ProductsDataGrid.ItemsSource = ProductViewManager.JoinProductAndProductStockTables();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadDataGrid();
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            SearchTextBox.Foreground = new SolidColorBrush(Colors.White);
            SearchTextBox.GotFocus += SearchTextBox_GotFocus;
            if (SearchTextBox.Text == string.Empty)
            {
                ReloadDataGrid();
            }
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchAnArticle();
            }
        }

        private void SearchMinPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchMinPriceTxtBox.Text = string.Empty;
            SearchMinPriceTxtBox.Foreground = new SolidColorBrush(Colors.White);
            SearchMinPriceTxtBox.GotFocus += SearchMinPriceTxtBox_GotFocus;
            if (SearchMinPriceTxtBox.Text == string.Empty)
                ReloadDataGrid();
        }

        private void SearchMaxPriceTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchMaxPriceTxtBox.Text = string.Empty;
            SearchMaxPriceTxtBox.Foreground = new SolidColorBrush(Colors.White);
            SearchMaxPriceTxtBox.GotFocus += SearchMaxPriceTxtBox_GotFocus;
            if (SearchMaxPriceTxtBox.Text == string.Empty)
                ReloadDataGrid();
        }

        private void PriceSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchByPriceInterval();
        }

        private void SearchMinPriceTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchByPriceInterval();
            }
        }

        private void SearchMaxPriceTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchByPriceInterval();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchAnArticle();
        }

        public void ReloadDataGrid()
        {
            ProductsDataGrid.ItemsSource = ProductViewManager.JoinProductAndProductStockTables();
        }

        private void SearchAnArticle()
        {
            try
            {
                var input = CheckInputService.CheckStringTypeInput(SearchTextBox);
                if (input == true)
                {
                    ProductsDataGrid.ItemsSource = ProductManager.GetProductByGlobalResearch(SearchTextBox.Text);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void SearchByPriceInterval()
        {
            try
            {
                var priceChecked = CheckInputService.CheckDoubleIntervalNumber(SearchMinPriceTxtBox, SearchMaxPriceTxtBox);
                if (priceChecked == true)
                {
                    ProductsDataGrid.ItemsSource = ProductManager.GetProductByPriceInterval(SearchMinPriceTxtBox.Text, SearchMaxPriceTxtBox.Text);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void EditAProductQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            ProductViewManager productViewManager = new ProductViewManager();
            productViewManager.SelectAProductByRow((ProductView)ProductsDataGrid.CurrentCell.Item);
            InventoryAdjustmentWindow inventoryAdjustmentWindow = new InventoryAdjustmentWindow();
            inventoryAdjustmentWindow.ShowDialog();
        }

        private void ValidateAQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException("Méthode a implémenter");
        }
    }
}
