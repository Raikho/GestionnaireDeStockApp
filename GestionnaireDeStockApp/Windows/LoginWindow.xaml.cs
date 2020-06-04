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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool connectionState = false;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void UserNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UserNameTxtBox.Text = string.Empty;
            UserNameTxtBox.Foreground = new SolidColorBrush(Colors.White);
            UserNameTxtBox.Opacity = 1;
            UserNameTxtBox.GotFocus += UserNameTxtBox_GotFocus;
        }

        private void PasswordTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordTxtBox.Clear();
            PasswordTxtBox.Foreground = new SolidColorBrush(Colors.White);
            PasswordTxtBox.Opacity = 1;
            PasswordTxtBox.GotFocus += PasswordTxtBox_GotFocus;
        }

        private void ConnexionButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LoginDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                string query = "SELECT COUNT(Username) FROM [UserIDTable] WHERE Username=@UserName AND Password=@Password";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@UserName", UserNameTxtBox.Text);
                sqlCommand.Parameters.AddWithValue("@Password", PasswordTxtBox.Password);
                
                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                if (count == 1)
                {
                    MessageBox.Show("Connexion réussie!");
                    MainWindow mainWindow = new MainWindow();
                    connectionState = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("L'identifiant ou le mot de passe sont incorrectes. Veuillez réessayer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            AccountCreationWindow accountCreationWindow = new AccountCreationWindow();
            accountCreationWindow.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
