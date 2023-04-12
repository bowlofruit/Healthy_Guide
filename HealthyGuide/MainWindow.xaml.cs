using Npgsql;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HealthyGuide
{
    public partial class MainWindow : Window
    {
        private string connString = "Server=localhost; Port=5432; Database=Culinary_guide_for_healthy_eating; User Id=postgres; Password=1234;";
        private NpgsqlConnection conn;
        private string selectedTable;
        private DataTable currentTable;
        private string filterValue = string.Empty;
        private string filterColumn;

        public MainWindow()
        {
            InitializeComponent();
            conn = new NpgsqlConnection(connString);
            LoadTables();
        }

        private void LoadTables()
        {
            DataTable schema = conn.GetSchema("Tables");
            tableList.ItemsSource = schema.Select().Select(row => row[2].ToString()).ToList();
        }

        private void TableList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTable = (string)tableList.SelectedItem;
            TableView();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow != null)
            {
                string query = $"UPDATE {selectedTable} SET {string.Join(", ", selectedRow.Row.Table.Columns.Cast<DataColumn>().Select(c => $"{c.ColumnName} = @{c.ColumnName}"))} WHERE ";
                if (currentTable.Columns.Contains("id"))
                {
                    query += $"@id = id";
                }
                else
                {
                    query += string.Join(" AND ", selectedRow.Row.Table.Columns.Cast<DataColumn>().Select(c => $"@{c.ColumnName} = {c.ColumnName}"));
                }
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                foreach (DataColumn column in selectedRow.Row.Table.Columns)
                {
                    command.Parameters.AddWithValue($"@{column.ColumnName}", selectedRow[column.ColumnName]);
                }
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

            TableView();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DataRow newRow = currentTable.NewRow();
            foreach (DataColumn column in currentTable.Columns)
            {
                if(column.ColumnName != "id")
                {
                    if (column.DataType == typeof(int) || column.DataType == typeof(double))
                    {
                        newRow[column.ColumnName] = 1;
                    }
                    else if (column.DataType == typeof(DateTime))
                    {
                        newRow[column.ColumnName] = DateTime.Now;
                    }
                    else if (column.DataType == typeof(string))
                    {
                        newRow[column.ColumnName] = "default value";
                    }
                }
            }
            currentTable.Rows.Add(newRow);

            NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO {selectedTable} ({string.Join(", ", currentTable.Columns.Cast<DataColumn>().Where(c => c.ColumnName != "id").Select(c => c.ColumnName))}) VALUES ({string.Join(", ", currentTable.Columns.Cast<DataColumn>().Where(c => c.ColumnName != "id").Select(c => $"@{c.ColumnName}"))})", conn);
            foreach (DataColumn column in currentTable.Columns)
            {
                command.Parameters.AddWithValue($"@{column.ColumnName}", newRow[column.ColumnName]);
            }
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

            TableView();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow != null)
            {
                NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM {selectedTable} WHERE {string.Join(" AND ", selectedRow.Row.Table.Columns.Cast<DataColumn>().Select(c => $"@{c.ColumnName} = {c.ColumnName}"))}", conn);
                foreach (DataColumn column in currentTable.Columns)
                {
                    command.Parameters.AddWithValue($"@{column.ColumnName}", selectedRow[column.ColumnName]);
                }
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                TableView();
            }
        }

        private void TableView()
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter($"SELECT * FROM {selectedTable}", conn);
            currentTable = new DataTable();
            adapter.Fill(currentTable);
            dataGrid.ItemsSource = currentTable.DefaultView;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            conn.Close();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = new StackPanel
            {
                Background = new SolidColorBrush(Colors.Gray),
            };

            Window window = new Window
            {
                Title = "Filter",
                Width = 400,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = stackPanel
            };

            Label textLabel = new Label
            {
                Content = "Enter key words",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            TextBox keyWords = new TextBox
            {
                Height = 20,
                Width = 100,
                HorizontalAlignment = HorizontalAlignment.Center, 
                VerticalAlignment = VerticalAlignment.Center
            };

            Label listLabel = new Label
            {
                Content = "Choose column",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            ComboBox columnNameList = new ComboBox
            {
                Height = 20,
                Width = 100,
                SelectedIndex = 0,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            foreach (DataColumn column in currentTable.Columns)
            {
                columnNameList.Items.Add(column.ColumnName);
            }

            Button filterAccept = new Button
            {
                Height = 30,
                Width = 120,
                Content = "OK",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            filterAccept.Click += (s, args) =>
            {
                filterValue = keyWords.Text;
                filterColumn = columnNameList.SelectedValue.ToString();
                window.Close();
            };

            stackPanel.Children.Add(textLabel);
            stackPanel.Children.Add(keyWords);
            stackPanel.Children.Add(listLabel);
            stackPanel.Children.Add(columnNameList);
            stackPanel.Children.Add(filterAccept);

            window.ShowDialog();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter($"SELECT * FROM {selectedTable} WHERE {filterColumn} IN ({filterValue})", conn);
            DataTable filterTable = new DataTable();
            adapter.Fill(filterTable);
            dataGrid.ItemsSource = filterTable.DefaultView;
        }
    }
}