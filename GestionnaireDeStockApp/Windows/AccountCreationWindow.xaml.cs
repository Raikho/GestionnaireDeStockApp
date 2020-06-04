using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour AccountCreationWindow.xaml
    /// </summary>
    public partial class AccountCreationWindow : Window
    {
        public AccountCreationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void NameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DesignTextBox(NameTxtBox, NameTxtBox_GotFocus);
        }

        private void SurNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DesignTextBox(SurNameTxtBox, SurNameTxtBox_GotFocus);
        }

        private void CreateIDTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DesignTextBox(CreateIDTxtBox, CreateIDTxtBox_GotFocus);
        }

        private void CreatePWTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DesignTextBox(CreatePWTxtBox, CreatePWTxtBox_GotFocus);
        }

        private void ConfirmPWTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DesignTextBox(ConfirmPWTxtBox, ConfirmPWTxtBox_GotFocus);
        }

        private void CreationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameTxtBox.Text == "" || SurNameTxtBox.Text == "" || CreateIDTxtBox.Text == "" || CreatePWTxtBox.Text == "")
                    MessageBox.Show("Merci de remplir tous les champs.");
                else if (CreatePWTxtBox.Text != ConfirmPWTxtBox.Text)
                    MessageBox.Show("Le mot de passe n'est pas identique.");
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LoginDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;"))
                    {
                        sqlConnection.Open();

                        SqlCommand sqlCommand = new SqlCommand("UserAdd", sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Name", NameTxtBox.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@SurName", SurNameTxtBox.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@UserName", CreateIDTxtBox.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@Password", CreatePWTxtBox.Text.Trim());
                        sqlCommand.ExecuteNonQuery();
                        MessageBox.Show("Profil crée avec succés!");
                        Clear();

                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.ShowDialog();
                        this.Close();
                    } 
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void DesignTextBox(TextBox textBox, RoutedEventHandler routedEventHandler)
        {
            textBox.Text = string.Empty;
            textBox.Foreground = new SolidColorBrush(Colors.White);
            textBox.Opacity = 1;
            textBox.GotFocus += routedEventHandler;
        }

        private void Clear()
        {
            NameTxtBox.Text = SurNameTxtBox.Text = CreateIDTxtBox.Text = CreatePWTxtBox.Text = ConfirmPWTxtBox.Text = string.Empty;
        }
    }
}
