using DataLayer;
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
            AddAnArticleTxtBlockError.Text = string.Empty;
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
                using (var dbContext = new StockContext())
                {
                    var products = dbContext.Products;
                    bool duplicate = false;

                    foreach (var product in products)
                    {
                        if (product.Reference.ToLower() == AddRefTxtBox.Text.ToLower())
                        {
                            duplicate = true;
                            CreateTxtBlockInfo.Foreground = new SolidColorBrush(Colors.Yellow);
                            CreateTxtBlockInfo.Text = "L'article existe déjà";
                            break;
                        }
                    }

                    if (!duplicate)
                    {
                        if (MessageBox.Show("Etes-vous sûr de vouloir ajouté cet article au stock?", "DataGridView", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            var checkedChar = ControlInputService.CheckAllCharacteristics(AddRefTxtBox, AddNameTxtBox, AddPriceTxtBox, AddQuantTxtBox, AddAnArticleTxtBlockError);
                            if (checkedChar == false)
                            {
                                MessageBox.Show("Une erreur de saisie est survenue.");
                            }
                            else
                            {
                                var product = new Product()
                                {
                                    Reference = AddRefTxtBox.Text,
                                    Name = AddNameTxtBox.Text,
                                    Price = Convert.ToDouble(AddPriceTxtBox.Text),
                                    Quantity = Convert.ToInt32(AddQuantTxtBox.Text)
                                };
                                products.Add(product);
                                dbContext.SaveChanges();

                                CreateTxtBlockInfo.Text = "Le nouveau produit a été intégré au stock:";
                                ShowAnArticle(product.Reference, product.Name, product.Price, product.Quantity);
                                ArticlesListManagementPage.LoadDataBaseProducts();
                            }
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
    }
}
