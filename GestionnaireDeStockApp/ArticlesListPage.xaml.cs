using DataLayer;
using System.Collections.Generic;
using System.Windows.Controls;
namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour ArticlesListPage.xaml
    /// </summary>
    public partial class ArticlesListPage : Page
    {
        public ArticlesListPage()
        {
            InitializeComponent();
            AddProductsInRows();
        }

        public void AddProductsInRows()
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
    }
}
