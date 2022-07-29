using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace LoginForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        int attempts = 3; string txtUser; string txtPass;
        MainWindow _MainWindow = new MainWindow();
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();
            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void LoginClick_Click(object sender, RoutedEventArgs e)
        {
            string un = "admin";
            string pw = "admin";

            txtUser = username.Text;
            txtPass = password.Password;
            
            
            if (txtUser == un && txtPass == pw)
            {
                this.Close();
                _MainWindow.Show();
            }
            else
            {
                attempts--;
                MessageBox.Show("Incorrect username or password.\nYou have " + attempts + " attempts.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                username.Clear();
                password.Clear();
            }

            if(attempts == 0)
            {
                MessageBox.Show("You have reached maximum attempts.", "Maximum attempts", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }
    }
}
