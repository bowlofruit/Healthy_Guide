using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;

namespace HealthyGuide.Users
{
    public class UserCreator
    {
        private readonly string connectionString;
        private string filter;
        public List<User> Users { get; set; }
        public ComboBox ComboBox { get; set; }

        public UserCreator(string connectionString, string filter)
        {
            this.connectionString = connectionString;
            this.filter = filter;
            Users = FillReview();
        }

        public List<User> FillReview()
        {
            ComboBox = new ComboBox();
            List<User> users = new List<User>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = $@"select * from users {filter}";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string email = reader.GetString(1);
                            string phoneNumber = reader.GetString(2);
                            string login = reader.GetString(3);

                            users.Add(new User(id, email, phoneNumber, login));
                        }

                        var schemaTable = reader.GetSchemaTable();
                        var columns = schemaTable.Rows.Cast<DataRow>()
                            .Select(row => row["ColumnName"].ToString())
                            .ToList();

                        foreach (var column in columns)
                        {
                            ComboBox.Items.Add(column);
                        }
                    }
                }
            }
            return users;
        }
    }
}