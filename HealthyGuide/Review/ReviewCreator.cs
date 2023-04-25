using Npgsql;
using System.Collections.Generic;

namespace HealthyGuide.Reviews
{
    public class ReviewCreator
    {
        private readonly string connectionString;
        public List<Review> Reviews { get; set; }

        public ReviewCreator(string connectionString)
        {
            this.connectionString = connectionString;
            Reviews = FillReview();
        }

        public List<Review> FillReview()
        {
            List<Review> reviews = new List<Review>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                SELECT Review.ID, Review.Content, Review.Score,
                CASE
                    WHEN Review_Recipe.RecipeID IS NOT NULL THEN 'Recipe'
                    WHEN Review_Blog.BlogID IS NOT NULL THEN 'Blog'
                END AS Review_Type,
                CASE
                    WHEN Review_Recipe.RecipeID IS NOT NULL THEN Recipe.Name
                    WHEN Review_Blog.BlogID IS NOT NULL THEN Blog.Name
                END AS Review_Item_Name,
                Users.Login
                FROM Review
                INNER JOIN Users ON Review.UserID = Users.ID
                LEFT JOIN Review_Recipe ON Review.ID = Review_Recipe.ReviewID
                LEFT JOIN Review_Blog ON Review.ID = Review_Blog.ReviewID
                LEFT JOIN Recipe ON Review_Recipe.RecipeID = Recipe.ID
                LEFT JOIN Blog ON Review_Blog.BlogID = Blog.ID;";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string description = reader.GetString(1);
                            int score = reader.GetInt32(2);
                            string reviewType = reader.GetString(3);
                            string reviewName = reader.GetString(4);
                            string login = reader.GetString(5);

                            reviews.Add(new Review(id, description, score, reviewType, reviewName, login));
                        }
                    }
                }
            }
            return reviews;
        }
    }
}