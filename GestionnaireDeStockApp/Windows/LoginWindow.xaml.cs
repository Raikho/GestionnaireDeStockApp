using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DataLayer;

namespace GestionnaireDeStockApp
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool connectionState = false;

        bool UserNameTxtBoxClick, PasswordTxtBoxClick = false;

        public string Username { get; private set; }

        public LoginWindow()
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

        private void UserNameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UserNameTxtBoxClick = true;
            ClearTheUserNameTxtBoxBlock();
        }

        private void PasswordTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordTxtBoxClick = true;
            ClearThePasswordTxtBoxBlock();
        }

                private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            AccountCreationWindow accountCreationWindow = new AccountCreationWindow();
            accountCreationWindow.Show();
            this.Close();
        }

        private void ConnexionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserNameTxtBox.Text == ""
                    || PasswordTxtBox.Password == ""
                    || UserNameTxtBoxClick == false
                    || PasswordTxtBoxClick == false)
                    MessageBox.Show("Veuillez renseigner tous les champs.");
                else
                {
                    using (var dbContext = new StockContext())
                    {
                        var users = dbContext.Users;

                        User newUserConnectionTry = null;
                        foreach (var user in users)
                        {
                            newUserConnectionTry = user;
                            if (newUserConnectionTry.Username == UserNameTxtBox.Text && newUserConnectionTry.Password == PasswordTxtBox.Password)
                            {
                                connectionState = true;
                                MessageBox.Show("Connexion réussie.");
                                Username = user.Username;
                                this.Close();
                                break;
                            }
                            else
                            {
                                newUserConnectionTry = null;
                            }    
                        }
                        if (newUserConnectionTry == null)
                        {
                            MessageBox.Show("Connexion échouée, veuillez réessayer.");
                        }
                    }
                }
            }
            catch (System.Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        private void ClearTheUserNameTxtBoxBlock()
        {
            if (UserNameTxtBoxClick == true)
            {
                UserNameTxtBox.Text = string.Empty;
                UserNameTxtBox.Foreground = new SolidColorBrush(Colors.White);
                UserNameTxtBox.Opacity = 1;
                UserNameTxtBox.GotFocus += UserNameTxtBox_GotFocus;
            }
        }

        private void ClearThePasswordTxtBoxBlock()
        {
            if (PasswordTxtBoxClick == true)
            {
                PasswordTxtBox.Clear();
                PasswordTxtBox.Foreground = new SolidColorBrush(Colors.White);
                PasswordTxtBox.Opacity = 1;
                PasswordTxtBox.GotFocus += PasswordTxtBox_GotFocus;
            }
        }
    }
}
