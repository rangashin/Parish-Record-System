using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LoginForm.Pages;

namespace LoginForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
 

        private void AddForm_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new addformpage();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove(); 

        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new confirmationpage();
        }

        private void AboutUs_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AboutUs();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
    }
}
