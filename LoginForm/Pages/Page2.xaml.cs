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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=parish;Integrated Security=True");

        public bool isValid()
        {
            DateTime? selectedDate = dateOfBirth.SelectedDate;
            if (textBoxCFirstName.Text == string.Empty)
            {
                MessageBox.Show("Child's first name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxCLastName.Text == string.Empty)
            {
                MessageBox.Show("Child's last name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxCMiddleName.Text == string.Empty)
            {
                MessageBox.Show("Child's middle name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!selectedDate.HasValue)
            {
                MessageBox.Show("Child's date of birth is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxCPlace.Text == string.Empty)
            {
                MessageBox.Show("Child's place of birth is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (radBMale.IsChecked == false && radBFemale.IsChecked == false)
            {
                MessageBox.Show("Child's sex is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxFBLastName.Text == string.Empty)
            {
                MessageBox.Show("Father's last name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxFBFirstName.Text == string.Empty)
            {
                MessageBox.Show("Father's first name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxFBMiddleName.Text == string.Empty)
            {
                MessageBox.Show("Father's middle name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxMBLastName.Text == string.Empty)
            {
                MessageBox.Show("Mother's last name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxMBFirstName.Text == string.Empty)
            {
                MessageBox.Show("Mother's first name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxMBMiddleName.Text == string.Empty)
            {
                MessageBox.Show("Mother's middle name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    DateTime date = (DateTime)dateOfBirth.SelectedDate;
                    SqlCommand cmd = new SqlCommand("INSERT INTO baptismal VALUES (@firstname, @lastname, @middlename, @birthdate, @birthplace, @sex, @fatherlastname, @fatherfirstname, @fathermiddlename, @motherlastname, @motherfirstname, @mothermiddlename)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@firstname", textBoxCFirstName.Text);
                    cmd.Parameters.AddWithValue("@lastname", textBoxCLastName.Text);
                    cmd.Parameters.AddWithValue("@middlename", textBoxCMiddleName.Text);
                    cmd.Parameters.AddWithValue("@birthdate", date.ToString("yyyy/MM/dd"));
                    cmd.Parameters.AddWithValue("@birthplace", textBoxCPlace.Text);
                    if (radBFemale.IsChecked == true)
                        cmd.Parameters.AddWithValue("@sex", radBFemale.Content);
                    else
                        cmd.Parameters.AddWithValue("@sex", radBMale.Content);
                    cmd.Parameters.AddWithValue("@fatherlastname", textBoxFBLastName.Text);
                    cmd.Parameters.AddWithValue("@fatherfirstname", textBoxFBFirstName.Text);
                    cmd.Parameters.AddWithValue("@fathermiddlename", textBoxFBMiddleName.Text);
                    cmd.Parameters.AddWithValue("@motherlastname", textBoxMBLastName.Text);
                    cmd.Parameters.AddWithValue("@motherfirstname", textBoxMBFirstName.Text);
                    cmd.Parameters.AddWithValue("@mothermiddlename", textBoxMBMiddleName.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully registered.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearBaptismalData();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Exception error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void clearBaptismalData()
        {
            textBoxCFirstName.Clear();
            textBoxCLastName.Clear();
            textBoxCMiddleName.Clear();
            dateOfBirth.SelectedDate = null;
            textBoxCPlace.Clear();
            radBFemale.IsChecked = false;
            radBMale.IsChecked = false;
            textBoxFBLastName.Clear();
            textBoxFBFirstName.Clear();
            textBoxFBMiddleName.Clear();
            textBoxMBLastName.Clear();
            textBoxMBFirstName.Clear();
            textBoxMBMiddleName.Clear();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clearBaptismalData();
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new addformpage());
        }
    }
}