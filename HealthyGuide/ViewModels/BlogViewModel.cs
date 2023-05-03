using HealthGuide.Models;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace HealthGuide.ViewModels
{
    public class BlogViewModel : BaseViewModel<Blog>, INotifyPropertyChanged
    {
        protected override List<Blog> LoadTable()
        {
            string query = "select b.id, b.userid, b.name, u.login, bi.content from blog b inner join blog_info bi on bi.blogid = b.id inner join users u on b.userid = u.id";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    List<Blog> blogs = new List<Blog>();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Blog blog = new Blog()
                            {
                                Id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                Title = reader.GetString(2),
                                Author = reader.GetString(3),
                                Content = reader.GetString(4).Replace(". ", ".\n")
                            };
                            blogs.Add(blog);
                        }
                    }
                    return blogs;
                }
            }
        }

        protected override void AddValue(object parameter) => WindowCreator.CreateAddValueWindow(typeof(Blog), AddValueToDatabase).ShowDialog();
        protected override void FilterTable(object parameter) => WindowCreator.CreateFilterTableWindow(typeof(Blog), FilterTableToDatabase).ShowDialog();

        protected override void AddValueToDatabase(Window window, StackPanel stackPanel)
        {
            string title = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Title").Text;
            string content = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Content").Text;
            string author = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Author").Text;
            int authorId = 0;

            if (!Validator.IsValidAuthor(author, Items, ref authorId))
            {
                MessageBox.Show("Invalid author");
                return;
            }

            if (!Validator.IsValidContent(content))
            {
                MessageBox.Show("Invalid content");
                return;
            }

            if (!Validator.IsValidTitle(title))
            {
                MessageBox.Show("Invalid title");
                return;
            }

            string queryBlog = "INSERT INTO blog (userid, name) VALUES (@UserId, @Name) RETURNING id";
            string queryBlogInfo = "INSERT INTO blog_info (blogid, content) VALUES (@BlogId, @Content)";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(queryBlog, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", authorId);
                    cmd.Parameters.AddWithValue("@Name", title);

                    int blogId = (int)cmd.ExecuteScalar();

                    using (NpgsqlCommand cmdBlogInfo = new NpgsqlCommand(queryBlogInfo, conn))
                    {
                        cmdBlogInfo.Parameters.AddWithValue("@BlogId", blogId);
                        cmdBlogInfo.Parameters.AddWithValue("@Content", content);

                        cmdBlogInfo.ExecuteNonQuery();
                    }
                }

                Items = LoadTable();
            }

            window.Close();
        }

        protected override void DeleteValue(object parameter)
        {
            if (parameter is Blog blog)
            {
                string query = "DELETE FROM blog WHERE id = @Id";

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", blog.Id);
                        cmd.ExecuteNonQuery();
                    }

                    Items = LoadTable();
                }
            }
        }
    }
}