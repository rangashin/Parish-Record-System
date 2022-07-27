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

namespace LoginForm.Pages
{
    /// <summary>
    /// Interaction logic for confirmationpage.xaml
    /// </summary>
    public partial class confirmationpage : Page
    {
        public confirmationpage()
        {
            InitializeComponent();
        }
        private void Baptismal_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page2G();
            Baptismal.Visibility = Visibility.Hidden;
            Marriage.Visibility = Visibility.Hidden;
            Death.Visibility = Visibility.Hidden;
            EditDocuments.Visibility = Visibility.Hidden;   
        }
        private void Marriage_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page1G();
            Baptismal.Visibility = Visibility.Hidden;
            Marriage.Visibility = Visibility.Hidden;
            Death.Visibility = Visibility.Hidden;
            EditDocuments.Visibility = Visibility.Hidden;
        }
        private void Death_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page3G();
            Baptismal.Visibility = Visibility.Hidden;
            Marriage.Visibility = Visibility.Hidden;
            Death.Visibility = Visibility.Hidden;
            EditDocuments.Visibility = Visibility.Hidden;
        }
    }
}
