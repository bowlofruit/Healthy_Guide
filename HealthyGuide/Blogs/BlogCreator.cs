using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;

namespace HealthyGuide.Blogs
{
    public class BlogCreator
    {
        private readonly string connectionString;
        public List<Blog> Blogs { get; set; }
        public ComboBox BlogFilterBox { get; set; }
        private string filter;


        public BlogCreator(string connectionString, string filter)
        {
            this.connectionString = connectionString;
            this.filter = filter;
            Blogs = FillReview();
        }

        private List<Blog> FillReview()
        {
            BlogFilterBox = new ComboBox();
            List<Blog> reviews = new List<Blog>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = $@"SELECT b.id as blogID, b.Name, bc.Content, u.Login
                            FROM Blog b 
                            JOIN Blog_Content bc ON b.ID = bc.BlogID 
                            JOIN Users u ON b.UserID = u.ID {filter}";

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

                        var schemaTable = reader.GetSchemaTable();
                        var columns = schemaTable.Rows.Cast<DataRow>()
                            .Select(row => row["ColumnName"].ToString())
                            .ToList();

                        foreach (var column in columns)
                        {
                            BlogFilterBox.Items.Add(column);
                        }
                    }
                }
            }
            return reviews;
        }
    }
}