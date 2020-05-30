using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public class User
        {
            public string Nom { get; set; }
            public string Prénom { get; set; }
            public string Tel { get; set; }
        }

        readonly ObservableCollection<User> users = new ObservableCollection<User>
        {
            new User()
            {
                Nom = "Hammana",
                Prénom = "Charif"
            }
        };

        public MainWindow()
        {
            InitializeComponent();

            GridView.DataContext = users;
        }

        private void TopGridBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
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

        private void AddAnArticleButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddAnArticlePage(@"DataBase\Liste des articles.txt");
        }

        private void SearchByRefButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SearchByReferencePage(@"DataBase\Liste des articles.txt");
        }

        private void SearchByNameButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SearchByNamePage(@"DataBase\Liste des articles.txt");
        }

        private void SearchByPrice_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SearchByPricePage(@"DataBase\Liste des articles.txt");
        }

        private void SearchInAllDataBaseButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SearchInAllDataBasePage(@"DataBase\Liste des articles.txt");
        }

        private void DeleteAnArticle_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DeleteAnArticlePage(@"DataBase\Liste des articles.txt");
        }

        private void EditAnArticleButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EditAnArticlePage(@"DataBase\Liste des articles.txt");
        }

        private void ShowAllArticlesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ShowAllArticlesPage(@"DataBase\Liste des articles.txt");
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
