using System.Data;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace HealthyGuide
{
    public class FilterCreate
    {
        private string filterValue;
        private string filterColumn;

        public string TextValue { get => filterValue; private set => filterValue = value; }
        public string SelectColumn { get => filterColumn; set => filterColumn = value; }

        public Window Creator(DataTable currentTable)
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
                TextValue = keyWords.Text;
                SelectColumn = columnNameList.SelectedValue.ToString();
                window.Close();
            };

            stackPanel.Children.Add(textLabel);
            stackPanel.Children.Add(keyWords);
            stackPanel.Children.Add(listLabel);
            stackPanel.Children.Add(columnNameList);
            stackPanel.Children.Add(filterAccept);

            return window;
        }
    }
}
