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
    /// Interaction logic for Page1G.xaml
    /// </summary>
    public partial class Page1G : Page
    {
        public Page1G()
        {
            InitializeComponent();
            loadGrid();
        }

        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=parish;Integrated Security=True");
        string tempID = null;

        public void loadGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM marriage", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            marriageGrid.ItemsSource = dt.DefaultView;
        }

        public bool isValid()
        {
            if (textBoxHFirstName.Text == string.Empty && textBoxHLastName.Text == string.Empty && textBoxHMiddleName.Text == string.Empty && textBoxHBirth.Text == string.Empty && textBoxHAddress.Text == string.Empty && textBoxWFirstName.Text == string.Empty && textBoxWLastName.Text == string.Empty && textBoxWMiddleName.Text == string.Empty && textBoxWBirth.Text == string.Empty && textBoxWAddress.Text == string.Empty && textBoxPlaceMarriage.Text == string.Empty && textBoxCity.Text == string.Empty && textBoxProvince.Text == string.Empty)
            {
                MessageBox.Show("Please select a record to modify.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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

        private void marriageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                tempID = selectedRow["id"].ToString();
                textBoxHFirstName.Text = selectedRow["husbandfirstname"].ToString();
                textBoxHLastName.Text = selectedRow["husbandlastname"].ToString();
                textBoxHMiddleName.Text = selectedRow["husbandmiddlename"].ToString();
                textBoxHBirth.Text = selectedRow["husbandbirthplace"].ToString();
                textBoxHAddress.Text = selectedRow["husbandaddress"].ToString();
                textBoxWFirstName.Text = selectedRow["wifefirstname"].ToString();
                textBoxWLastName.Text = selectedRow["wifelastname"].ToString();
                textBoxWMiddleName.Text = selectedRow["wifemiddlename"].ToString();
                textBoxWBirth.Text = selectedRow["wifebirthplace"].ToString();
                textBoxWAddress.Text = selectedRow["wifeaddress"].ToString();
                textBoxPlaceMarriage.Text = selectedRow["marriageplace"].ToString();
                textBoxCity.Text = selectedRow["citymunicipality"].ToString();
                textBoxProvince.Text = selectedRow["province"].ToString();
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
                    SqlCommand cmd = new SqlCommand("SELECT * FROM marriage WHERE id = " + search, conn);
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    conn.Close();
                    marriageGrid.ItemsSource = dt.DefaultView;
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
                SqlCommand cmd = new SqlCommand("UPDATE marriage SET husbandfirstname = '" + textBoxHFirstName.Text +
                    "', husbandlastname = '" + textBoxHLastName.Text +
                    "', husbandmiddlename = '" + textBoxHMiddleName.Text +
                    "', husbandbirthplace = '" + textBoxHBirth.Text +
                    "', husbandaddress = '" + textBoxHAddress.Text +
                    "', wifefirstname = '" + textBoxWFirstName.Text +
                    "', wifelastname = '" + textBoxWLastName.Text +
                    "', wifemiddlename = '" + textBoxWMiddleName.Text +
                    "', wifebirthplace = '" + textBoxWBirth.Text + 
                    "', wifeaddress = '" + textBoxWAddress.Text +
                    "', marriageplace = '" + textBoxPlaceMarriage.Text + 
                    "', province = '" + textBoxProvince.Text + 
                    "', citymunicipality = '" + textBoxCity.Text + "' WHERE ID = '" + tempID + "'", conn);
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
                    clearMarriageData();
                    loadGrid();
                    tempID = null;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM marriage WHERE id = " + tempID, conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                conn.Close();
                clearMarriageData();
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
