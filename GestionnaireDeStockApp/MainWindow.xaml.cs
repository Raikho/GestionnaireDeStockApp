using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool MenuClosed = true;

        public static LoginWindow loginWindow { get; set; }

        static MainWindow mainWindow;
        public MainWindow()
        {
            mainWindow = this;

            InitializeComponent();

            LeftMenu.IsEnabled = false;
            HideAllItems();

            ConnectToSession();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MenuClosed)
            {
                Storyboard openMenu = (Storyboard)Button.FindResource("OpenMenu");
                openMenu.Begin();

                LogoTxtBlock.FontSize = 16;
                ConnectedCircle.Width = 270;
            }
            else
            {
                Storyboard closeMenu = (Storyboard)Button.FindResource("CloseMenu");
                closeMenu.Begin();

                LogoTxtBlock.FontSize = 5;
                ConnectedCircle.Width = 30;
            }

            MenuClosed = !MenuClosed;
        }

        private void ConnectToSession()
        {
            loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            LeftMenu.IsEnabled = LoginWindow.connectionState;
            if (LeftMenu.IsEnabled == true)
            {
                ShowAllItems();
                ShowCurrentUserName(LoginWindow.Username);
                MainFrame.Content = new ArticlesListManagementPage();
            }
        }

        public static void ShowCurrentUserName(string name)
        {
            mainWindow.WelcomeTxtBlock.Text = $"{name}";
            mainWindow.ConnectedCircle.Foreground = new SolidColorBrush(Colors.Green);
            mainWindow.SuggestToConnect.Text = string.Empty;
        }

        private void TopGridBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous quitter l'application?", "Gestionnaire de stock", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void ReduceWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ResizeWindow_Click(object sender, RoutedEventArgs e)
        {
            if (IsMaximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }
        bool IsMaximized
        {
            get
            {
                if (WindowState == WindowState.Maximized)
                    return true;
                else
                    return false;
            }
        }

        private void AlertButton_Click(object sender, RoutedEventArgs e)
        {
            AlertWindow alertWindow = new AlertWindow();
            alertWindow.Show();
        }

        public void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectToSession();
        }

        private void ShowArticleListManagement_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ArticlesListManagementPage();
        }

        private void SellAnArticle_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SalesManagementPage();
        }

        private void InventoryButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EditAnArticlePage();
        }

        private void HideAllItems()
        {
            AlertButton.Visibility = Visibility.Hidden;
            ShowArticleListManagement.Visibility = Visibility.Hidden;
            SellAnArticle.Visibility = Visibility.Hidden;
            InventoryButton.Visibility = Visibility.Hidden;
            DashboardButton.Visibility = Visibility.Hidden;
        }

        private void ShowAllItems()
        {
            AlertButton.Visibility = Visibility.Visible;
            ShowArticleListManagement.Visibility = Visibility.Visible;
            SellAnArticle.Visibility = Visibility.Visible;
            InventoryButton.Visibility = Visibility.Visible;
            DashboardButton.Visibility = Visibility.Visible;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (LoginWindow.connectionState == false)
            {
                if (e.Key == Key.F6)
                    ConnectToSession();
                else if (e.Key == Key.Escape)
                {
                    if (MessageBox.Show("Voulez-vous quitter l'application?", "Gestionnaire de stock", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }
                }
                else
                    MessageBox.Show("Veuillez vous connecter.");
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    if (MessageBox.Show("Voulez-vous quitter l'application?", "Gestionnaire de stock", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }
                }

                if (e.Key == Key.F1)
                    MainFrame.Content = new ArticlesListManagementPage();

                if (e.Key == Key.F2)
                {
                    MainFrame.Content = new SalesManagementPage();
                }

                if (e.Key == Key.F3)
                    MainFrame.Content = new EditAnArticlePage();

                if (e.Key == Key.F5)
                {
                    AlertWindow alertWindow = new AlertWindow();
                    alertWindow.Show();
                }

                if (e.Key == Key.F6)
                    ConnectToSession();

                if (e.Key == Key.F7)
                {
                    AccountCreationWindow accountCreationWindow = new AccountCreationWindow();
                    accountCreationWindow.ShowDialog();
                }
            }
        }

        private void DashBoardButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
