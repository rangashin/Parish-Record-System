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
    /// Interaction logic for Page2G.xaml
    /// </summary>
    public partial class Page2G : Page
    {
        public Page2G()
        {
            InitializeComponent();
            loadGrid();
        }

        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=parish;Integrated Security=True");
        string tempID = null;

        public void loadGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM baptismal", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            baptismalGrid.ItemsSource = dt.DefaultView;
        }

        public bool isValid()
        {
            DateTime? selectedDate = dateOfBirth.SelectedDate;
            if (textBoxCFirstName.Text == string.Empty && textBoxCLastName.Text == string.Empty && textBoxCMiddleName.Text == string.Empty && !selectedDate.HasValue && textBoxCPlace.Text == string.Empty && (radBMale.IsChecked == false && radBFemale.IsChecked == false) && textBoxFLastName.Text == string.Empty && textBoxFFirstName.Text == string.Empty && textBoxFMiddleName.Text == string.Empty && textBoxMLastName.Text == string.Empty && textBoxMFirstName.Text == string.Empty && textBoxMMiddleName.Text == string.Empty)
            {
                MessageBox.Show("Please select a record to modify.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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
            if (textBoxFLastName.Text == string.Empty)
            {
                MessageBox.Show("Father's last name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxFFirstName.Text == string.Empty)
            {
                MessageBox.Show("Father's first name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxFMiddleName.Text == string.Empty)
            {
                MessageBox.Show("Father's middle name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxMLastName.Text == string.Empty)
            {
                MessageBox.Show("Mother's last name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxMFirstName.Text == string.Empty)
            {
                MessageBox.Show("Mother's first name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textBoxMMiddleName.Text == string.Empty)
            {
                MessageBox.Show("Mother's middle name is required.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
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
            textBoxFLastName.Clear();
            textBoxFFirstName.Clear();
            textBoxFMiddleName.Clear();
            textBoxMLastName.Clear();
            textBoxMFirstName.Clear();
            textBoxMMiddleName.Clear();
        }

        private void baptismalGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                tempID = selectedRow["id"].ToString();
                textBoxCFirstName.Text = selectedRow["firstname"].ToString();
                textBoxCLastName.Text = selectedRow["lastname"].ToString();
                textBoxCMiddleName.Text = selectedRow["middlename"].ToString();
                dateOfBirth.Text = selectedRow["birthdate"].ToString();
                textBoxCPlace.Text = selectedRow["birthplace"].ToString();
                if (radBMale.Content.ToString() == selectedRow["sex"].ToString())
                    radBMale.IsChecked = true;
                else
                    radBFemale.IsChecked = true;
                textBoxFLastName.Text = selectedRow["fatherlastname"].ToString();
                textBoxFFirstName.Text = selectedRow["fatherfirstname"].ToString();
                textBoxFMiddleName.Text = selectedRow["fathermiddlename"].ToString();
                textBoxMLastName.Text = selectedRow["motherlastname"].ToString();
                textBoxMFirstName.Text = selectedRow["motherfirstname"].ToString();
                textBoxMMiddleName.Text = selectedRow["mothermiddlename"].ToString();
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
                    SqlCommand cmd = new SqlCommand("SELECT * FROM baptismal WHERE id = " + search, conn);
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    conn.Close();
                    baptismalGrid.ItemsSource = dt.DefaultView;
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
            if(isValid())
            {
                conn.Open();
                DateTime date = (DateTime)dateOfBirth.SelectedDate;
                string sex = null;
                if (radBFemale.IsChecked == true)
                    sex = radBFemale.Content.ToString();
                else
                    sex = radBMale.Content.ToString();
                SqlCommand cmd = new SqlCommand("UPDATE baptismal SET firstname = '" + textBoxCFirstName.Text +
                    "', lastname = '" + textBoxCLastName.Text +
                    "', middlename = '" + textBoxCMiddleName.Text +
                    "', birthdate = '" + date.ToString("yyyy/MM/dd") +
                    "', birthplace = '" + textBoxCPlace.Text +
                    "', sex = '" + sex +
                    "', fatherlastname = '" + textBoxFLastName.Text +
                    "', fatherfirstname = '" + textBoxFFirstName.Text +
                    "', fathermiddlename = '" + textBoxFMiddleName.Text +
                    "', motherlastname = '" + textBoxMLastName.Text +
                    "', motherfirstname = '" + textBoxMFirstName.Text +
                    "', mothermiddlename = '" + textBoxMMiddleName.Text + "' WHERE id = '" + tempID + "'", conn);
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
                    clearBaptismalData();
                    loadGrid();
                    tempID = null;
                }
            } 
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM baptismal WHERE id = " + tempID, conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                conn.Close();
                clearBaptismalData();
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
