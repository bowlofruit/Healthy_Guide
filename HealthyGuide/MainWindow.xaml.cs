using HealthGuide.ViewModels;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace HealthGuide

{
    public partial class MainWindow : Window
    {
        public ViewModelInit MainViewModel { get; private set; }

        public MainWindow()
        {
            MainViewModel = new ViewModelInit();
            DataContext = MainViewModel;
            InitializeComponent();
        }

        private void ShowGrids(bool recipesGridIsVisible, bool usersGridIsVisible, bool blogsGridIsVisible, bool reviewsGridIsVisible, INotifyPropertyChanged activeViewModel)
        {
            RecipeDataGrid.Visibility = recipesGridIsVisible ? Visibility.Visible : Visibility.Collapsed;
            UserDataGrid.Visibility = usersGridIsVisible ? Visibility.Visible : Visibility.Collapsed;
            BlogDataGrid.Visibility = blogsGridIsVisible ? Visibility.Visible : Visibility.Collapsed;
            ReviewDataGrid.Visibility = reviewsGridIsVisible ? Visibility.Visible : Visibility.Collapsed;

            buttonDelete.DataContext = activeViewModel;
            buttonFilter.DataContext = activeViewModel;
            buttonAdd.DataContext = activeViewModel;
            buttonFilterClear.DataContext = activeViewModel;
        }


        private void ButtonRecipes_Click(object sender, RoutedEventArgs e)
        {
            ShowGrids(true, false, false, false, MainViewModel.RecipeViewModel);
        }

        private void ButtonUsers_Click(object sender, RoutedEventArgs e)
        {
            ShowGrids(false, true, false, false, MainViewModel.UserViewModel);
        }

        private void ButtonBlogs_Click(object sender, RoutedEventArgs e)
        {
            ShowGrids(false, false, true, false, MainViewModel.BlogViewModel);
        }

        private void ButtonReviews_Click(object sender, RoutedEventArgs e)
        {
            ShowGrids(false, false, false, true, MainViewModel.ReviewViewModel);
        }
    }
}