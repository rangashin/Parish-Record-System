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
    /// Interaction logic for Page3G.xaml
    /// </summary>
    public partial class Page3G : Page
    {
        public Page3G()
        {
            InitializeComponent();
            loadGrid();
        }

        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=parish;Integrated Security=True");
        string tempID = null;

        public void loadGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM death", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            deathGrid.ItemsSource = dt.DefaultView;
        }

        public bool isValid()
        {
            DateTime? selectedDate = dateOfDeath.SelectedDate;
            if (textBoxDFirstName.Text == string.Empty && textBoxDMiddleName.Text == string.Empty && textBoxDLastName.Text == string.Empty && !selectedDate.HasValue && (radMale.IsChecked == false && radFemale.IsChecked == false) && textBoxDCause.Text == string.Empty && textBoxDCivil.Text == string.Empty && textBoxDAddress.Text == string.Empty && textBoxDCity.Text == string.Empty && textBoxDProvince.Text == string.Empty)
            {
                MessageBox.Show("Please select a record to modify.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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

        private void deathGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                tempID = selectedRow["id"].ToString();
                textBoxDFirstName.Text = selectedRow["firstname"].ToString();
                textBoxDLastName.Text = selectedRow["lastname"].ToString();
                textBoxDMiddleName.Text = selectedRow["middlename"].ToString();
                dateOfDeath.Text = selectedRow["deathdate"].ToString();
                textBoxDPlace.Text = selectedRow["deathplace"].ToString();
                if (radMale.Content.ToString() == selectedRow["sex"].ToString())
                    radMale.IsChecked = true;
                else
                    radFemale.IsChecked = true;
                textBoxDAddress.Text = selectedRow["address"].ToString();
                textBoxDCivil.Text = selectedRow["civilstatus"].ToString();
                textBoxDCause.Text = selectedRow["causeofdeath"].ToString();
                textBoxDProvince.Text = selectedRow["province"].ToString();
                textBoxDCity.Text = selectedRow["citymunicipality"].ToString();      
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string search = SearchIDTextBox.Text.ToString();
                if (search == "")
                    loadGrid();
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM death WHERE id = " + search, conn);
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    conn.Close();
                    deathGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Search by using id only.", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                SearchIDTextBox.Clear();
            }
            finally
            {
                conn.Close();
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                conn.Open();
                DateTime date = (DateTime)dateOfDeath.SelectedDate;
                string sex = null;
                if (radFemale.IsChecked == true)
                    sex = radFemale.Content.ToString();
                else
                    sex = radMale.Content.ToString();
                SqlCommand cmd = new SqlCommand("UPDATE death SET firstname = '" + textBoxDFirstName.Text +
                    "', lastname = '" + textBoxDLastName.Text +
                    "', middlename = '" + textBoxDMiddleName.Text +
                    "', deathdate = '" + date.ToString("yyyy/MM/dd") +
                    "', deathplace = '" + textBoxDPlace.Text +
                    "', sex = '" + sex +
                    "', address = '" + textBoxDAddress.Text +
                    "', civilstatus = '" + textBoxDCivil.Text +
                    "', causeofdeath = '" + textBoxDCause.Text +
                    "', province = '" + textBoxDProvince.Text +
                    "', citymunicipality = '" + textBoxDCity.Text + "' WHERE id = '" + tempID + "'", conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record has been updated successfully.", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Exception error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    conn.Close();
                    clearDeathData();
                    loadGrid();
                    tempID = null;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM death WHERE id = " + tempID, conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                conn.Close();
                clearDeathData();
                loadGrid();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Please select a record to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                conn.Close();
                tempID = null;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new confirmationpage());
        }
    }
}
