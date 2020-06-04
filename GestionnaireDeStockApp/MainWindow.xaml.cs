using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginWindow loginWindow = new LoginWindow();

        public MainWindow()
        {
            InitializeComponent();

            LeftMenu.IsEnabled = false;
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

        public void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            LeftMenu.IsEnabled = loginWindow.connectionState;
        }

        private void AddAnArticleButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginWindow.connectionState == false)
                MessageBox.Show("Veuillez vous connecter à une session");
            else
            MainFrame.Content = new AddAnArticlePage(@"DataBase\Liste des articles.txt");
        }

        private void SearchByRefButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginWindow.connectionState == false)
                MessageBox.Show("Veuillez vous connecter à une session");
            else
                MainFrame.Content = new SearchByReferencePage(@"DataBase\Liste des articles.txt");
        }

        private void SearchByNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginWindow.connectionState == false)
                MessageBox.Show("Veuillez vous connecter à une session");
            else
                MainFrame.Content = new SearchByNamePage(@"DataBase\Liste des articles.txt");
        }

        private void SearchByPrice_Click(object sender, RoutedEventArgs e)
        {
            if (loginWindow.connectionState == false)
                MessageBox.Show("Veuillez vous connecter à une session");
            else
                MainFrame.Content = new SearchByPricePage(@"DataBase\Liste des articles.txt");
        }

        private void SearchInAllDataBaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginWindow.connectionState == false)
                MessageBox.Show("Veuillez vous connecter à une session");
            else
                MainFrame.Content = new SearchInAllDataBasePage(@"DataBase\Liste des articles.txt");
        }

        private void DeleteAnArticle_Click(object sender, RoutedEventArgs e)
        {
            if (loginWindow.connectionState == false)
                MessageBox.Show("Veuillez vous connecter à une session");
            else
                MainFrame.Content = new DeleteAnArticlePage(@"DataBase\Liste des articles.txt");
        }

        private void EditAnArticleButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginWindow.connectionState == false)
                MessageBox.Show("Veuillez vous connecter à une session");
            else
                MainFrame.Content = new EditAnArticlePage(@"DataBase\Liste des articles.txt");
        }

        private void ShowAllArticlesButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginWindow.connectionState == false)
                MessageBox.Show("Veuillez vous connecter à une session");
            else
              MainFrame.Content = new ShowAllArticlesPage(@"DataBase\Liste des articles.txt");
        }
    }
}
