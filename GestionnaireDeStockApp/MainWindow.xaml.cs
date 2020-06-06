using DataLayer;
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
        LoginWindow loginWindow = new LoginWindow();
        AlertWindow alertWindow = new AlertWindow();

        public MainWindow()
        {
            InitializeComponent();

            LeftMenu.IsEnabled = false;
            HideAllButtons();
        }

        private void TopGridBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
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
            alertWindow = new AlertWindow();
            alertWindow.Show();
        }

        public void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            LeftMenu.IsEnabled = loginWindow.connectionState;
            if (LeftMenu.IsEnabled == true)
            {
                ShowAllButtons();

                WelcomeTxtBlock.Foreground = new SolidColorBrush(Colors.GreenYellow);
                WelcomeTxtBlock.Text = $"{loginWindow.Username} est connecté";
            }   
        }

        private void AddAnArticleButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddAnArticlePage();
        }

        private void SearchInAllDataBaseButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SearchInAllDataBasePage();
        }

        private void DeleteAnArticle_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DeleteAnArticlePage();
        }

        private void EditAnArticleButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EditAnArticlePage();
        }

        private void ShowAllArticlesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ShowAllArticlesPage();
        }

        private void HideAllButtons()
        {
            AlertButton.Visibility = Visibility.Hidden;
            AddAnArticleButton.Visibility = Visibility.Hidden;
            SearchInAllDataBaseButton.Visibility = Visibility.Hidden;
            DeleteAnArticle.Visibility = Visibility.Hidden;
            EditAnArticleButton.Visibility = Visibility.Hidden;
            ShowAllArticlesButton.Visibility = Visibility.Hidden;
        }

        private void ShowAllButtons()
        {
            AlertButton.Visibility = Visibility.Visible;
            AddAnArticleButton.Visibility = Visibility.Visible;
            SearchInAllDataBaseButton.Visibility = Visibility.Visible;
            DeleteAnArticle.Visibility = Visibility.Visible;
            EditAnArticleButton.Visibility = Visibility.Visible;
            ShowAllArticlesButton.Visibility = Visibility.Visible;
        }
    }
}
