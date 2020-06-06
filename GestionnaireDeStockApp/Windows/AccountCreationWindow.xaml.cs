using DataLayer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour AccountCreationWindow.xaml
    /// </summary>
    public partial class AccountCreationWindow : Window
    {
        bool NameTxtBoxClick, SurNameTxtBoxClick, CreateIDTxtBoxClick, CreatePWTxtBoxClick, ConfirmPWTxtBoxClick = false;

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
            NameTxtBoxClick = true;
            DesignTextBox(NameTxtBox, NameTxtBox_GotFocus);
        }

        private void SurNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SurNameTxtBoxClick = true;
            DesignTextBox(SurNameTxtBox, SurNameTxtBox_GotFocus);
        }

        private void CreateIDTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CreateIDTxtBoxClick = true;
            DesignTextBox(CreateIDTxtBox, CreateIDTxtBox_GotFocus);
        }

        private void CreatePWTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CreatePWTxtBoxClick = true;
            DesignTextBox(CreatePWTxtBox, CreatePWTxtBox_GotFocus);
        }

        private void ConfirmPWTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ConfirmPWTxtBoxClick = true;
            DesignTextBox(ConfirmPWTxtBox, ConfirmPWTxtBox_GotFocus);
        }

        private void CreationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NameTxtBox.Text == "" 
                    || SurNameTxtBox.Text == "" 
                    || CreateIDTxtBox.Text == "" 
                    || CreatePWTxtBox.Text == "" 
                    || ConfirmPWTxtBox.Text == "" 
                    || NameTxtBoxClick == false 
                    || SurNameTxtBoxClick == false 
                    || CreateIDTxtBoxClick == false 
                    || CreatePWTxtBoxClick == false 
                    || ConfirmPWTxtBoxClick == false)
                    MessageBox.Show("Merci de remplir tous les champs.");
                else if (CreatePWTxtBox.Text != ConfirmPWTxtBox.Text)
                    MessageBox.Show("Le mot de passe n'est pas identique.");
                else
                {
                    using (var dbContext = new StockContext())
                    {
                        var users = dbContext.Users;

                        var newUser = new User()
                        {
                            Name = NameTxtBox.Text,
                            Surname = SurNameTxtBox.Text,
                            Username = CreateIDTxtBox.Text,
                            Password = CreatePWTxtBox.Text
                        };
                        users.Add(newUser);
                        dbContext.SaveChanges();

                        MessageBox.Show("Profil crée avec succés!");
                        LoginWindow loginWindow = new LoginWindow();
                        this.Close();
                        loginWindow.ShowDialog();
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
    }
}
