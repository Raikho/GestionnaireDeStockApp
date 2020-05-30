using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour EditACharacteristicPage.xaml
    /// </summary>
    public partial class EditACharacteristicPage : Page
    {
        public static string InputEditing { get; private set; }

        public string Path { get; private set; }

        List<Article> Articles { get; set; }

        public EditACharacteristicPage(string path)
        {
            InitializeComponent();

            Path = path;
            Articles = Article.GetAllCharacteristics(path);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            EditACharaTxtBox.Text = InputEditing;
        }

        private void EditACharaTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EditACharaTxtBox.Text = string.Empty;
            EditACharaTxtBox.GotFocus += EditACharaTxtBox_GotFocus;
            EditACharaTxtBox.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}


