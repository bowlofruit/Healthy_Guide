using HealthGuide.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HealthGuide.ViewModels
{
    public class ReviewViewModel : BaseViewModel<Review>
    {
        protected override List<Review> LoadTable()
        {
            string query = "select r.id, r.userid, r.name, ri.score, u.login, ri.date, ri.content from review r inner join review_info ri on ri.reviewid = r.id inner join users u on u.id = r.userid";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    List<Review> reviews = new List<Review>();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Review review = new Review()
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Title = reader.GetString(2),
                                Score = reader.GetFloat(3),
                                Author = reader.GetString(4),
                                Date = reader.GetDateTime(5),
                                Content = reader.GetString(6).Replace(". ", ".\n")
                            };
                            reviews.Add(review);
                        }
                    }
                    return reviews;
                }
            }
        }

        protected override void AddValue(object parameter) => WindowCreator.CreateAddValueWindow(typeof(Review), AddValueToDatabase).ShowDialog();
        protected override void FilterTable(object parameter) => WindowCreator.CreateFilterTableWindow(typeof(Review), FilterTableToDatabase).ShowDialog();

        protected override void AddValueToDatabase(Window window, StackPanel stackPanel)
        {
            string title = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Title").Text;
            string content = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Content").Text;
            string author = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Author").Text;
            string score = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Score").Text;
            string date = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Date").Text;
            int authorId = 0;

            if (!Validator.IsValidTitle(title))
            {
                MessageBox.Show("Invalid title");
                return;
            }

            if (!Validator.IsValidContent(content))
            {
                MessageBox.Show("Invalid content");
                return;
            }

            if (!Validator.IsValidAuthor(author, Items, ref authorId))
            {
                MessageBox.Show("Invalid author");
                return;
            }

            if (!Validator.IsValidScore(score))
            {
                MessageBox.Show("Invalid score");
                return;
            }

            if (!Validator.IsValidDate(date))
            {
                MessageBox.Show("Invalid score");
                return;
            }

            string reviewQuery = "INSERT INTO Review (name, userid) VALUES (@Name, @UserId) RETURNING id";
            string reviewInfoQuery = "INSERT INTO Review_Info (reviewid, content, score, date) VALUES (@ReviewId, @Content, @Score, @Date)";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (NpgsqlCommand reviewCmd = new NpgsqlCommand(reviewQuery, conn))
                {
                    reviewCmd.Parameters.AddWithValue("@Name", title);
                    reviewCmd.Parameters.AddWithValue("@UserId", authorId);

                    int reviewId = (int)reviewCmd.ExecuteScalar();

                    using (NpgsqlCommand reviewInfoCmd = new NpgsqlCommand(reviewInfoQuery, conn))
                    {
                        reviewInfoCmd.Parameters.AddWithValue("@ReviewId", reviewId);
                        reviewInfoCmd.Parameters.AddWithValue("@Content", content);
                        reviewInfoCmd.Parameters.AddWithValue("@Score", double.Parse(score));
                        reviewInfoCmd.Parameters.AddWithValue("@Date", DateTime.Parse(date));

                        reviewInfoCmd.ExecuteNonQuery();
                    }
                }

                Items = LoadTable();
            }

            window.Close();
        }

        protected override void DeleteValue(object parameter)
        {
            if (parameter is Review review)
            {
                string reviewQuery = "DELETE FROM Review WHERE id = @Id";

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    using (NpgsqlCommand reviewCmd = new NpgsqlCommand(reviewQuery, conn))
                    {
                        reviewCmd.Parameters.AddWithValue("@Id", review.Id);
                        reviewCmd.ExecuteNonQuery();
                    }

                    Items = LoadTable();
                }
            }
        }

        protected override void UpdateActiveValue(object parameter)
        {
            if (SelectedItem is Review review)
            {
                MessageBoxResult result = MessageBox.Show("Save changes?", "Update", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    string updateSetQuery = "UPDATE review SET name = @Name WHERE id = @Recipeid";

                    using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        using (NpgsqlCommand updateSetCmd = new NpgsqlCommand(updateSetQuery, conn))
                        {
                            updateSetCmd.Parameters.AddWithValue("@Name", review.Title);
                            updateSetCmd.Parameters.AddWithValue("@Recipeid", review.Id);

                            updateSetCmd.ExecuteNonQuery();
                        }
                    }

                    Items = LoadTable();
                }
            }
        }
    }
}