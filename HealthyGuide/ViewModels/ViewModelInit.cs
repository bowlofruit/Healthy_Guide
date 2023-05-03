using HealthGuide.ViewModels;

namespace HealthGuide
{
    public class ViewModelInit
    {
        public RecipeViewModel RecipeViewModel { get; private set; }
        public UserViewModel UserViewModel { get; private set; }
        public ReviewViewModel ReviewViewModel { get; private set; }
        public BlogViewModel BlogViewModel { get; private set; }

        public ViewModelInit()
        {
            RecipeViewModel = new RecipeViewModel();
            UserViewModel = new UserViewModel();
            ReviewViewModel = new ReviewViewModel();
            BlogViewModel = new BlogViewModel();
        }
    }
}
