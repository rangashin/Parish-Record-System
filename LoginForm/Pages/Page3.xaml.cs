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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=parish;Integrated Security=True");

        public bool isValid()
        {
            DateTime? selectedDate = dateOfDeath.SelectedDate;
            if (textBoxDFirstName.Text == string.Empty)
            {
                MessageBox.Show("Death's first name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxDMiddleName.Text == string.Empty)
            {
                MessageBox.Show("Death's middle name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxDLastName.Text == string.Empty)
            {
                MessageBox.Show("Death's last name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!selectedDate.HasValue)
            {
                MessageBox.Show("Date of death is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxDPlace.Text == string.Empty)
            {
                MessageBox.Show("Place of death is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (radMale.IsChecked == false && radFemale.IsChecked == false)
            {
                MessageBox.Show("Death's sex is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxDCause.Text == string.Empty)
            {
                MessageBox.Show("Cause of death is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxDCivil.Text == string.Empty)
            {
                MessageBox.Show("Death's civil status is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxDAddress.Text == string.Empty)
            {
                MessageBox.Show("Death's address is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxDCity.Text == string.Empty)
            {
                MessageBox.Show("Death's city or municipality is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxDProvince.Text == string.Empty)
            {
                MessageBox.Show("Death's province is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    DateTime date = (DateTime)dateOfDeath.SelectedDate;
                    SqlCommand cmd = new SqlCommand("INSERT INTO death VALUES (@firstname, @lastname, @middlename, @deathdate, @deathplace, @sex, @address, @civilstatus, @causeofdeath, @province, @citymunicipality)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@firstname", textBoxDFirstName.Text);
                    cmd.Parameters.AddWithValue("@lastname", textBoxDMiddleName.Text);
                    cmd.Parameters.AddWithValue("@middlename", textBoxDLastName.Text);
                    cmd.Parameters.AddWithValue("@deathdate", date.ToString("yyyy/MM/dd"));
                    cmd.Parameters.AddWithValue("@deathplace", textBoxDPlace.Text);
                    if (radFemale.IsChecked == true)
                        cmd.Parameters.AddWithValue("@sex", radFemale.Content);
                    else
                        cmd.Parameters.AddWithValue("@sex", radMale.Content);
                    cmd.Parameters.AddWithValue("@address", textBoxDAddress.Text);
                    cmd.Parameters.AddWithValue("@civilstatus", textBoxDCivil.Text);
                    cmd.Parameters.AddWithValue("@causeofdeath", textBoxDCause.Text);
                    cmd.Parameters.AddWithValue("@province", textBoxDProvince.Text);
                    cmd.Parameters.AddWithValue("@citymunicipality", textBoxDCity.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully registered.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearDeathData();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Exception error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void clearDeathData()
        {
            textBoxDFirstName.Clear();
            textBoxDMiddleName.Clear();
            textBoxDLastName.Clear();
            dateOfDeath.SelectedDate = null;
            textBoxDPlace.Clear();
            radFemale.IsChecked = false;
            radMale.IsChecked = false;
            textBoxDCause.Clear();
            textBoxDCivil.Clear();
            textBoxDAddress.Clear();
            textBoxDCity.Clear();
            textBoxDProvince.Clear();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clearDeathData();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new addformpage());
        }
    }
}
