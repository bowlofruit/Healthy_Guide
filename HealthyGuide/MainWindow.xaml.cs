using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HealthyGuide
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=localhost; Port=5432; Database=Culinary_guide_for_healthy_eating; User Id=postgres; Password=1234;";
        private string selectedTable;
        private DataTable currentTable;

        public MainWindow()
        {
            InitializeComponent();
            LoadTables();
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
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
                    {
                        foreach (DataColumn column in selectedRow.Row.Table.Columns)
                        {
                            command.Parameters.AddWithValue($"@{column.ColumnName}", selectedRow[column.ColumnName]);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            TableView();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            DataRow newRow = currentTable.NewRow();
            int foreignKeyCount = 0;

            string query = $"INSERT INTO {selectedTable} ({string.Join(", ", currentTable.Columns.Cast<DataColumn>().Where(c => c.ColumnName != "id").Select(c => c.ColumnName))}) VALUES";

            foreach (DataColumn column in currentTable.Columns)
            {
                if (column.ColumnName != "id")
                {
                    if (column.DataType == typeof(int) || column.DataType == typeof(double))
                    {
                        if (column.ColumnName.Substring(column.ColumnName.Length - 2) == "id")
                        {
                            list.Add($"{column.ColumnName.Substring(0, column.ColumnName.Length - 2)}");
                            foreignKeyCount++;
                        }
                        else
                        {
                            newRow[column.ColumnName] = 1;
                        }
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

            if (foreignKeyCount > 0)
            {
                string choosenTables, choosenColumns;
                if (foreignKeyCount == currentTable.Columns.Count)
                {
                    choosenTables = string.Join(", ", list);
                    choosenColumns = string.Join(", ", list.Select(c => $"{c}.id"));
                }
                else
                {
                    choosenTables = $"{selectedTable}, {string.Join(", ", list.Select(c => c))}";
                    choosenColumns = string.Join(", ", currentTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                }

                DataTable resultTable = GetResultTable(list, choosenTables, choosenColumns);

                if (resultTable.Rows.Count > 0)
                {
                    DataRow dataRow = resultTable.Rows[0];
                    int count = 0;
                    foreach (DataColumn column in currentTable.Columns)
                    {
                        newRow[column.ColumnName] = dataRow[count];
                        count++;
                    }
                }
                else
                {
                    MessageBox.Show("Error: there are no free values ​​for foreign keys");
                    return;
                }
            }

            currentTable.Rows.Add(newRow);

            query += $"({string.Join(", ", currentTable.Columns.Cast<DataColumn>().Where(c => c.ColumnName != "id").Select(c => $"@{c.ColumnName}"))})";
            InsertData(newRow, query);
            TableView();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            if (selectedRow != null)
            {
                string query = $"DELETE FROM {selectedTable} WHERE {string.Join(" AND ", currentTable.Columns.Cast<DataColumn>().Select(c => $"@{c.ColumnName} = {c.ColumnName}"))}";
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
                    {
                        foreach (DataColumn column in currentTable.Columns)
                        {
                            command.Parameters.AddWithValue($"@{column.ColumnName}", selectedRow[column.ColumnName]);
                        }
                        command.ExecuteNonQuery();
                    }
                }
                TableView();
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterCreate filter = new FilterCreate();
            Window window = filter.Creator(currentTable);

            window.ShowDialog();

            TableView(filter.SelectColumn, filter.TextValue);
        }

        private void TableView(string filterColumn = null, string filterValue = null)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                NpgsqlDataAdapter adapter;
                DataTable table = new DataTable();

                if (filterColumn != null && filterValue != null)
                {
                    adapter = new NpgsqlDataAdapter($"SELECT * FROM {selectedTable} WHERE {filterColumn} IN ({filterValue})", conn);
                    adapter.Fill(table);
                }
                else
                {
                    adapter = new NpgsqlDataAdapter($"SELECT * FROM {selectedTable}", conn);
                    adapter.Fill(table);
                    currentTable = table;
                }

                currentTable.DefaultView.Sort = currentTable.Columns[0].ColumnName + " ASC";
                dataGrid.ItemsSource = table.DefaultView;
            }
        }

        private void LoadTables()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                DataTable schema = conn.GetSchema("Tables");
                tableList.ItemsSource = schema.Select().Select(row => row[2].ToString()).ToList();
                tableList.SelectedIndex = 0;
            }
        }

        private void InsertData(DataRow newRow, string query)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, conn))
                {
                    foreach (DataColumn column in currentTable.Columns)
                    {
                        command.Parameters.AddWithValue($"@{column.ColumnName}", newRow[column.ColumnName]);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        private DataTable GetResultTable(List<string> list, string choosenTables, string choosenColumns)
        {
            var resultTable = new DataTable();

            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string columnFill = $"SELECT {choosenColumns} FROM {choosenTables} WHERE NOT EXISTS (SELECT * FROM {selectedTable} WHERE {string.Join(" AND ", list.Select(c => $"{selectedTable}.{c}id = {c}.id"))})";
                using (NpgsqlCommand command = new NpgsqlCommand(columnFill, conn))
                {
                    using (var adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(resultTable);
                    }
                }
            }
            return resultTable;
        }
    }
}