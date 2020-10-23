using BusinessLogicLayer;
using System.Windows;
using System.Windows.Input;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        readonly AlertWindowManager AlertWindowManager = new AlertWindowManager();

        public AlertWindow()
        {
            InitializeComponent();
            productsDataGrid.ItemsSource = AlertWindowManager.AddProductsInRows();
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.F1)
            {
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.F5)
            {
                Close();
            }
        }
    }
}
