using HealthyGuide.Blogs;
using HealthyGuide.Recipes;
using HealthyGuide.Reviews;
using HealthyGuide.Users;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HealthyGuide
{
    public partial class MainWindow : Window
    {
        private readonly string connectionString = "Server=localhost; Port=5432; Database=Culinary_guide; User Id=postgres; Password=1234;";
        private string tableName;
        StackPanel addContentContainer;
        DataGrid currentGrid;
        ComboBox filterBox;
        string filter;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GridCollapsed()
        {
            reviewDataGrid.Visibility = Visibility.Collapsed;
            usersDataGrid.Visibility = Visibility.Collapsed;
            blogsDataGrid.Visibility = Visibility.Collapsed;
            categoryDataGrid.Visibility = Visibility.Collapsed;
        }

        private void LoadData<T>(string title, List<T> data)
        {
            GridCollapsed();

            contentInfo.Text = title;
            DataGrid dataGrid = null;
            switch (typeof(T))
            {
                case Type t when t == typeof(Category):
                    dataGrid = categoryDataGrid;
                    tableName = "recipe";
                    break;
                case Type t when t == typeof(User):
                    dataGrid = usersDataGrid;
                    tableName = "users";
                    break;
                case Type t when t == typeof(Blog):
                    dataGrid = blogsDataGrid;
                    tableName = "blog";
                    break;
                case Type t when t == typeof(Review):
                    dataGrid = reviewDataGrid;
                    tableName = "review";
                    break;
            }

            currentGrid = dataGrid;

            currentGrid.Visibility = Visibility.Visible;
            currentGrid.ItemsSource = data;
        }

        private void Recipe_Click(object sender, RoutedEventArgs e)
        {
            CategoryCreator categories = new CategoryCreator(connectionString, filter);
            LoadData("Recipe categories", categories.Categories);
        }

        private void User_Click(object sender, RoutedEventArgs e)
        {
            UserCreator userCreator = new UserCreator(connectionString, filter);
            LoadData("Users info", userCreator.Users);
            filterBox = userCreator.ComboBox;
        }

        private void Blogs_Click(object sender, RoutedEventArgs e)
        {
            BlogCreator blogCreator = new BlogCreator(connectionString, filter);
            LoadData("Blogs list", blogCreator.Blogs);
            filterBox = blogCreator.BlogFilterBox;
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            ReviewCreator reviews = new ReviewCreator(connectionString, filter);
            LoadData("Review categories", reviews.Reviews);
            filterBox = reviews.ComboBox;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window { Height = 400, Width = 300 };
            addContentContainer = new StackPanel();
            addContentContainer.Background = new SolidColorBrush(Colors.Gray);

            Button button = new Button
            {
                Height = 30,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Content = "OK",
                Margin = new Thickness(10)
            };

            button.Click += (s, args) =>
            {
                var fieldNames = "";
                var fieldValues = "";
                foreach (var child in addContentContainer.Children)
                {
                    if (child is TextBox textBox)
                    {
                        fieldNames += $"{textBox.Name}, ";
                        fieldValues += $"'{textBox.Text}', ";
                    }
                }
                fieldNames = fieldNames.TrimEnd(' ', ',');
                fieldValues = fieldValues.TrimEnd(' ', ',');

                var insertQuery = $"INSERT INTO {tableName} ({fieldNames}) VALUES ({fieldValues})";

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new NpgsqlCommand(insertQuery, connection);
                    command.ExecuteNonQuery();
                }

                window.Close();
            };

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var command = new NpgsqlCommand($"SELECT column_name FROM information_schema.columns WHERE table_name='{tableName}'", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var fieldName = reader.GetString(0);
                    if (fieldName != "id")
                    {
                        var label = new Label
                        {
                            Content = fieldName,
                            Foreground = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        var textBox = new TextBox
                        {
                            Name = fieldName,
                            Width = 100,
                            Margin = new Thickness(10)
                        };
                        addContentContainer.Children.Add(label);
                        addContentContainer.Children.Add(textBox);
                    }
                }
            }

            addContentContainer.Children.Add(button);
            window.Content = addContentContainer;
            window.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRows = currentGrid.SelectedItems;

            if (selectedRows.Count > 0)
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (NpgsqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (object row in selectedRows)
                            {
                                var idProperty = row.GetType().GetProperty("Id");
                                if (idProperty != null && idProperty.PropertyType == typeof(int))
                                {
                                    int idToDelete = (int)idProperty.GetValue(row);

                                    string sql = $"DELETE FROM {tableName} WHERE id = @id";
                                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                                    {
                                        cmd.Parameters.AddWithValue("id", idToDelete);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            tran.Commit();
                            currentGrid.Items.Refresh();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            MessageBox.Show("Помилка при видаленні запису: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Виділіть запис для видалення");
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window { Height = 400, Width = 300 };
            addContentContainer = new StackPanel();
            addContentContainer.Background = new SolidColorBrush(Colors.Gray);

            var labelComboBox = new Label
            {
                Content = "Column",
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var labelTextBox = new Label
            {
                Content = "Column",
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var textBox = new TextBox
            {
                Width = 100,
                Margin = new Thickness(10)
            };

            Button button = new Button
            {
                Height = 30,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Content = "OK",
                Margin = new Thickness(10)
            };

            button.Click += (s, args) =>
            {
                filter = $"WHERE '{textBox.Text}' IN ({filterBox.SelectedItem})";
                window.Close();
                currentGrid.Items.Refresh();
            };

            addContentContainer.Children.Add(labelComboBox);
            addContentContainer.Children.Add(filterBox);
            addContentContainer.Children.Add(labelTextBox);
            addContentContainer.Children.Add(textBox);
            addContentContainer.Children.Add(button);

            window.Content = addContentContainer;
            window.Show();
        }

        private void FilterClear_Click(object sender, RoutedEventArgs e)
        {
            filter = string.Empty;
        }
    }
}