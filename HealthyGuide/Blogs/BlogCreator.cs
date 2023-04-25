using Npgsql;
using System.Collections.Generic;

namespace HealthyGuide.Blogs
{
    public class BlogCreator
    {
        private readonly string connectionString;
        public List<Blog> Blogs;

        public BlogCreator(string connectionString)
        {
            this.connectionString = connectionString;
            Blogs = FillReview();
        }

        public List<Blog> FillReview()
        {
            List<Blog> reviews = new List<Blog>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"SELECT b.id, b.Name, bc.Content, u.Login
                            FROM Blog b 
                            JOIN Blog_Content bc ON b.ID = bc.BlogID 
                            JOIN Users u ON b.UserID = u.ID;";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string description = reader.GetString(2);
                            string login = reader.GetString(3);

                            reviews.Add(new Blog(id, name, description, login));
                        }
                    }
                }
            }
            return reviews;
        }
    }
}