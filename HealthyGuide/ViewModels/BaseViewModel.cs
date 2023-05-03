using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace HealthGuide.ViewModels
{
    public abstract class BaseViewModel<T> : INotifyPropertyChanged where T : class
    {
        protected string connString = "Server=localhost; Port=5432; Database=Culinary_guide; User Id=postgres; Password=1234;";

        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand FilterCommand { get; private set; }
        public ICommand FilterClearCommand { get; private set; }

        private List<T> _items;
        public List<T> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }

        private T _selectedItem;
        public T SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public BaseViewModel()
        {
            Items = LoadTable();
            AddCommand = new RelayCommand(AddValue);
            DeleteCommand = new RelayCommand(DeleteValue, CanDeleteValue);
            FilterCommand = new RelayCommand(FilterTable);
            FilterClearCommand = new RelayCommand(FiterTableClear);
        }

        protected abstract List<T> LoadTable();
        protected abstract void AddValueToDatabase(Window window, StackPanel stackPanel);
        protected abstract void AddValue(object parameter);
        protected abstract void DeleteValue(object parameter);
        protected abstract void FilterTable(object parameter);
        protected bool CanDeleteValue(object parameter) => parameter != null;
        protected void FiterTableClear(object parameter) => Items = LoadTable();

        protected void FilterTableToDatabase(Window window, StackPanel stackPanel)
        {
            string selectedColumn = (string)((ComboBox)stackPanel.Children[3]).SelectedItem;

            string filterValue = ((TextBox)stackPanel.Children[1]).Text;

            Items = LoadTable().Where(d => d.GetType().GetProperty(selectedColumn).GetValue(d, null).ToString().Contains(filterValue)).ToList();

            window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
