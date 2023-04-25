using System.Collections.Generic;

namespace HealthyGuide
{
    public class RecipeInfo
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Instruction { get; set; }
        public int Kcal { get; set; }
        public int Servings { get; set; }
        public int CookingTime { get; set; }
        public List<Ingredients> Ingredients { get; set; }

        public RecipeInfo(int id, string headline, string instruction, int kcal, List<Ingredients> ingredients, int servings, int cookingTime)
        {
            Id = id;
            Headline = headline;
            Instruction = instruction;
            Kcal = kcal;
            Ingredients = ingredients;
            Servings = servings;
            CookingTime = cookingTime;
        }
    }
}