using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static LoginWindow currentLgWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            LeftMenu.IsEnabled = false;
            HideAllItems();

            //ConnectAccount();
        }

        private void ConnectAccount()
        {
            do
            {
                LoginWindow loginWindow = new LoginWindow();
                currentLgWindow = loginWindow;
                currentLgWindow = new LoginWindow();
                currentLgWindow.ShowDialog();
            }
            while (LoginWindow.connectionState == false);
            MainFrame.Content = new ArticlesListManagementPage();
        }

        private void TopGridBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ReduceWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ResizeWindow_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void AlertButton_Click(object sender, RoutedEventArgs e)
        {
            AlertWindow alertWindow = new AlertWindow();
            alertWindow.Show();
        }

        public void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            currentLgWindow = new LoginWindow();
            currentLgWindow.ShowDialog();

            LeftMenu.IsEnabled = LoginWindow.connectionState;
            if (LeftMenu.IsEnabled == true)
            {
                ShowAllItems();

                WelcomeTxtBlock.Foreground = new SolidColorBrush(Colors.GreenYellow);
                WelcomeTxtBlock.Text = $"{currentLgWindow.Username} est connecté";
            }
        }

        private void ShowArticleListManagement_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ArticlesListManagementPage();
        }

        private void SearchInAllDataBaseButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SearchInAllDataBasePage();
        }

        private void EditAnArticleButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EditAnArticlePage();
        }

        private void HideAllItems()
        {
            AlertButton.Visibility = Visibility.Hidden;
            ShowArticleListManagement.Visibility = Visibility.Hidden;
            SearchInAllDataBaseButton.Visibility = Visibility.Hidden;
            EditAnArticleButton.Visibility = Visibility.Hidden;
        }

        private void ShowAllItems()
        {
            AlertButton.Visibility = Visibility.Visible;
            ShowArticleListManagement.Visibility = Visibility.Visible;
            SearchInAllDataBaseButton.Visibility = Visibility.Visible;
            EditAnArticleButton.Visibility = Visibility.Visible;
        }
    }
}
