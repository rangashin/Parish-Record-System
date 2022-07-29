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
using System.Data.SqlClient;
using System.Data;

namespace LoginForm.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        addformpage _addformpage = new addformpage();
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=parish;Integrated Security=True");

        public bool isValid()
        {
            if (textBoxHFirstName.Text == string.Empty)
            {
                MessageBox.Show("Husband's first name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxHLastName.Text == string.Empty)
            {
                MessageBox.Show("Husband's last name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxHMiddleName.Text == string.Empty)
            {
                MessageBox.Show("Husband's middle name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxHBirth.Text == string.Empty)
            {
                MessageBox.Show("Husband's place of birth is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxHAddress.Text == string.Empty)
            {
                MessageBox.Show("Husband's address is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxWFirstName.Text == string.Empty)
            {
                MessageBox.Show("Wife's first name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxWLastName.Text == string.Empty)
            {
                MessageBox.Show("Wife's last name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxWMiddleName.Text == string.Empty)
            {
                MessageBox.Show("Wife's middle name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxWBirth.Text == string.Empty)
            {
                MessageBox.Show("Wife's place of birth is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxWAddress.Text == string.Empty)
            {
                MessageBox.Show("Wife's address is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxPlaceMarriage.Text == string.Empty)
            {
                MessageBox.Show("Place of marriage is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxCity.Text == string.Empty)
            {
                MessageBox.Show("City or municipality is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxProvince.Text == string.Empty)
            {
                MessageBox.Show("Province is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO marriage VALUES (@husbandfirstname, @husbandlastname, @husbandmiddlename, @husbandbirthplace, @husbandaddress, @wifefirstname, @wifelastname, @wifemiddlename, @wifebirthplace, @wifeaddress, @marriageplace, @province, @citymunicipality)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@husbandfirstname", textBoxHFirstName.Text);
                    cmd.Parameters.AddWithValue("@husbandlastname", textBoxHLastName.Text);
                    cmd.Parameters.AddWithValue("@husbandmiddlename", textBoxHMiddleName.Text);
                    cmd.Parameters.AddWithValue("@husbandbirthplace", textBoxHBirth.Text);
                    cmd.Parameters.AddWithValue("@husbandaddress", textBoxHAddress.Text);
                    cmd.Parameters.AddWithValue("@wifefirstname", textBoxWFirstName.Text);
                    cmd.Parameters.AddWithValue("@wifelastname", textBoxHLastName.Text);
                    cmd.Parameters.AddWithValue("@wifemiddlename", textBoxHMiddleName.Text);
                    cmd.Parameters.AddWithValue("@wifebirthplace", textBoxHBirth.Text);
                    cmd.Parameters.AddWithValue("@wifeaddress", textBoxHAddress.Text);
                    cmd.Parameters.AddWithValue("@marriageplace", textBoxPlaceMarriage.Text);
                    cmd.Parameters.AddWithValue("@province", textBoxProvince.Text);
                    cmd.Parameters.AddWithValue("@citymunicipality", textBoxCity.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully registered.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearMarriageData();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Exception error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void clearMarriageData()
        {
            textBoxHFirstName.Clear();
            textBoxHLastName.Clear();
            textBoxHMiddleName.Clear();
            textBoxHBirth.Clear();
            textBoxHAddress.Clear();
            textBoxWFirstName.Clear();
            textBoxWLastName.Clear();
            textBoxWMiddleName.Clear();
            textBoxWBirth.Clear();
            textBoxWAddress.Clear();
            textBoxPlaceMarriage.Clear();
            textBoxCity.Clear();
            textBoxProvince.Clear();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            clearMarriageData();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new addformpage());
        }
    }
}