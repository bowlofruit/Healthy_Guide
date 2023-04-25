using Npgsql;
using System.Collections.Generic;

namespace HealthyGuide.Users
{
    public class UserCreator
    {
        private readonly string connectionString;
        public List<User> Users { get; set; }

        public UserCreator(string connectionString)
        {
            this.connectionString = connectionString;
            Users = FillReview();
        }

        public List<User> FillReview()
        {
            List<User> users = new List<User>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"select * from users";

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
                    }
                }
            }
            return users;
        }
    }
}
