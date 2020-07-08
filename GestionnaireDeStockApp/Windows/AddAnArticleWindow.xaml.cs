using BusinessLogicLayer;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour AddAnArticleWindow.xaml
    /// </summary>
    public partial class AddAnArticleWindow : Window
    {
        ArticlesListManagementPage ArticlesListManagementPage = new ArticlesListManagementPage();

        public AddAnArticleWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTheBLock();
            CreateNewArticle();
        }

        private void AddRefTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearTheBLock();
                CreateNewArticle();
            }

        }

        private void AddNameTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearTheBLock();
                CreateNewArticle();
            }
        }

        private void AddPriceTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearTheBLock();
                CreateNewArticle();
            }
        }

        private void AddQuantTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ClearTheBLock();
                CreateNewArticle();
            }
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
            AddAnArticleTxtBlockInfo.Text = string.Empty;
            AddAnArticleTxtBlockInfo.Text = string.Empty;
            AddAnArticleTxtBlockInfo.FontSize = 10;
            AddAnArticleTxtBlockInfo.Foreground = new SolidColorBrush(Colors.GreenYellow);
        }

        private void CreateNewArticle()
        {
            try
            {
                var checkedChar = CheckInputService.CheckAllCharacteristics(AddRefTxtBox, AddNameTxtBox, AddPriceTxtBox, AddQuantTxtBox, AddAnArticleTxtBlockInfo);
                if (checkedChar == true)
                {
                    if (MessageBox.Show("Etes-vous sûr de vouloir ajouté cet article au stock?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var newProduct = ProductManager.AddANewProductByRefChecking(AddRefTxtBox.Text, AddNameTxtBox.Text, AddPriceTxtBox.Text, AddQuantTxtBox.Text);
                        if (newProduct == null)
                        {
                            AddAnArticleTxtBlockInfo.FontSize = 12;
                            AddAnArticleTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Yellow);
                            AddAnArticleTxtBlockInfo.Text = "L'article existe déjà";
                        }
                        else
                        {
                            AddAnArticleTxtBlockInfo.FontSize = 12;
                            AddAnArticleTxtBlockInfo.Text = "Le nouveau produit a été intégré au stock";
                        }
                        ArticlesListManagementPage.ReloadDataGrid();
                    }
                }
            }
            catch (Exception except)
            {
                AddAnArticleTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Orange);
                AddAnArticleTxtBlockInfo.Text = $"L'erreur suivante est survenue: {except.Message}";
            }
        }
    }
}