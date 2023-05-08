using HealthGuide.Models;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HealthGuide.ViewModels
{
    public class UserViewModel : BaseViewModel<User>
    {
        public ICommand SelectionChangedCommand { get; private set; }

        public UserViewModel() 
        {
            SelectionChangedCommand = new RelayCommand(UpdateActiveValue);
        }

        protected override List<User> LoadTable()
        {
            string query = "SELECT * FROM Users";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    List<User> users = new List<User>();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User()
                            {
                                Id = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                PhoneNumber = reader.GetString(2),
                                Login = reader.GetString(3)
                            };
                            users.Add(user);
                        }
                    }
                    return users;
                }
            }
        }

        protected override void AddValue(object parameter) => WindowCreator.CreateAddValueWindow(typeof(User), AddValueToDatabase).ShowDialog();
        protected override void FilterTable(object parameter) => WindowCreator.CreateFilterTableWindow(typeof(User), FilterTableToDatabase).ShowDialog();


        protected override void AddValueToDatabase(Window window, StackPanel stackPanel)
        {
            string emailText = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Email").Text;
            string loginText = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Login").Text;
            string phoneText = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "PhoneNumber").Text;

            if (!Validator.IsValidEmail(emailText, Items))
            {
                MessageBox.Show("Invalid email");
                return;
            }

            if (!Validator.IsValidPhoneNumber(phoneText, Items))
            {
                MessageBox.Show("Invalid phone number");
                return;
            }

            if (!Validator.IsValidLogin(loginText, Items))
            {
                MessageBox.Show("Invalid login: must be more than 5 characters");
                return;
            }

            string query = "INSERT INTO Users (email, login, PhoneNumber) VALUES (@Email, @Login, @PhoneNumber)";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", emailText);
                    cmd.Parameters.AddWithValue("@Login", loginText);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneText);

                    cmd.ExecuteNonQuery();
                }

                Items = LoadTable();
            }
            window.Close();
        }

        protected override void UpdateActiveValue(object parameter)
        {
            if (SelectedItem is User user)
            {
                MessageBoxResult result = MessageBox.Show("Save changes?", "Update", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    string query = "UPDATE Users SET Email = @Email, Login = @Login, PhoneNumber = @PhoneNumber WHERE Id = @Id";

                    using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();

                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Email", user.Email);
                            cmd.Parameters.AddWithValue("@Login", user.Login);
                            cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                            cmd.Parameters.AddWithValue("@Id", user.Id);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        protected override void DeleteValue(object parameter)
        {
            if (parameter is User user)
            {
                string query = "DELETE FROM Users WHERE Id = @Id";

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", user.Id);
                        cmd.ExecuteNonQuery();
                    }

                    Items = LoadTable();
                }
            }
        }
    }
}