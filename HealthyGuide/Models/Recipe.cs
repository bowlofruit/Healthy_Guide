using System.Collections.Generic;

namespace HealthGuide.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instruction { get; set; }
        public int Kcal { get; set; }
        public int Servings { get; set; }
        public int CookingTime { get; set; }
        public List<Ingredient> Ingredient { get; set; }
    }
}
