using BusinessLogicLayer;
using DataLayer;
using DataTransfertObject;
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
        bool NameTxtBoxClick, SurNameTxtBoxClick, CreateIDTxtBoxClick, CreatePWTxtBoxClick, ConfirmPWTxtBoxClick;

        public AccountCreationWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LoginManager._loginSession.ConnectionState == true)
            {
                Close();
            }
            else
            {
                if (MessageBox.Show("Voulez-vous quitter la création de profil?", "Gestionnaire de stock", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Close();
                    MainWindow mainWindow = new MainWindow();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (LoginManager._loginSession.ConnectionState == true)
            {
                if (e.Key == Key.Escape || e.Key == Key.F7)
                {
                    Close();
                }
            }
            else
            {
                if (e.Key == Key.Escape || e.Key == Key.F7)
                {
                    if (MessageBox.Show("Voulez-vous quitter la création de profil?", "Gestionnaire de stock", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Close();
                        MainWindow mainWindow = new MainWindow();
                    }
                }
            }
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

        private void NameTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CreateAnAccount();
            }
        }

        private void SurNameTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CreateAnAccount();
            }
        }

        private void CreateIDTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CreateAnAccount();
            }
        }

        private void CreatePWTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CreateAnAccount();
            }
        }

        private void ConfirmPWTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CreateAnAccount();
            }
        }

        private void CreatePWTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CreatePWTxtBoxClick = true;
            DesignPasswordBox(CreatePWTxtBox, CreatePWTxtBox_GotFocus);
        }

        private void ConfirmPWTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ConfirmPWTxtBoxClick = true;
            DesignPasswordBox(ConfirmPWTxtBox, ConfirmPWTxtBox_GotFocus);
        }

        private void CreationButton_Click(object sender, RoutedEventArgs e)
        {
            CreateAnAccount();
        }

        private void CreateAnAccount()
        {
            try
            {
                if (NameTxtBox.Text == ""
                    || SurNameTxtBox.Text == ""
                    || CreateIDTxtBox.Text == ""
                    || CreatePWTxtBox.Password == ""
                    || ConfirmPWTxtBox.Password == ""
                    || NameTxtBoxClick == false
                    || SurNameTxtBoxClick == false
                    || CreateIDTxtBoxClick == false
                    || CreatePWTxtBoxClick == false
                    || ConfirmPWTxtBoxClick == false)
                    MessageBox.Show("Merci de remplir tous les champs.");
                else if (CreatePWTxtBox.Password != ConfirmPWTxtBox.Password)
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
                            Password = CreatePWTxtBox.Password
                        };
                        users.Add(newUser);
                        dbContext.SaveChanges();

                        MessageBox.Show("Profil crée avec succés!");
                        Close();
                        MainWindow mainWindow = new MainWindow();
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

        private void DesignPasswordBox(PasswordBox passwordBox, RoutedEventHandler routedEventHandler)
        {
            passwordBox.Password = string.Empty;
            passwordBox.Foreground = new SolidColorBrush(Colors.White);
            passwordBox.Opacity = 1;
            passwordBox.GotFocus += routedEventHandler;
        }
    }
}
