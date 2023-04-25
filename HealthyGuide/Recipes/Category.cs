using System.Collections.Generic;

namespace HealthyGuide
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RecipeInfo> RecipeInfo { get; set; }

        public Category(int id, string name, List<RecipeInfo> recipeInfo)
        {
            Id = id;
            Name = name;
            RecipeInfo = recipeInfo;
        }
    }
}