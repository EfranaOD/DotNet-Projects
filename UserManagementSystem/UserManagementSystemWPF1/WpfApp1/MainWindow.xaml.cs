using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> users = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtAge.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            if ( (txtName.Text).Length < 3)
            {
                MessageBox.Show("Minimum letters in name cannot be less than 3");
                return;
            }

            if (txtName.Text.Any( c => char.IsDigit(c)))
            {
                MessageBox.Show("Name cannot contain numbers");
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text, "^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$"))
            {
                MessageBox.Show("Invalid email pattern");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int intAge))
            {
                MessageBox.Show("Age must be a valid number");
                return;
            }

            if (intAge < 1 || intAge > 100)
            {
                MessageBox.Show("Age must be between 1 and 100");
                return;
            }

            if (txtPassword.Password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long");
                return;
            }

            User user = new User();
            user.Id = users.Count + 1;
            user.Name = txtName.Text;
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Password;

            int age = Convert.ToInt32(txtAge.Text);
            user.Age = age;

            users.Add(user);
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtPassword.Password = string.Empty;
                        
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {

            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = users;
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( dgUsers.SelectedItems is User selectedUser)
            {
                txtName.Text = selectedUser.Name;
                txtEmail.Text= selectedUser.Email;
                txtAge.Text = selectedUser.Age.ToString();
                txtPassword.Password = selectedUser.Password;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItems is User selectedUser)
            {
                selectedUser.Name = txtName.Text;
                selectedUser.Email = txtEmail.Text;
                selectedUser.Age = Convert.ToInt32(txtAge.Text);
                selectedUser.Password = txtPassword.Password;

                dgUsers.ItemsSource = null;
                dgUsers.ItemsSource = users;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if ( dgUsers.SelectedItem is User selectedUser)
            {
                users.Remove(selectedUser);

                dgUsers.ItemsSource = null;
                dgUsers.ItemsSource = users;
            }
        }
    }
}
