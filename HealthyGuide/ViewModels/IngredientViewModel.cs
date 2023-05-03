using HealthGuide.Models;
using Npgsql;
using System.Collections.Generic;

namespace HealthGuide.ViewModels
{
    public class IngredientViewModel
    {
        private string connString = "Server=localhost; Port=5432; Database=Culinary_guide; User Id=postgres; Password=1234;";

        public List<Ingredient> GetIngredientsForRecipe(int recipeId)
        {
            string query = $"select i.id, i.name, carbohydrates, fats, proteins from ingredient i inner join recipe_ingredient ri ON i.id = ri.ingredientid inner join recipe r ON r.id = ri.recipeid and r.id = {recipeId};";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (NpgsqlCommand commIngredientsQuery = new NpgsqlCommand(query, conn))
                {
                    List<Ingredient> recipeIngredients = new List<Ingredient>();

                    using (NpgsqlDataReader ingredientsReader = commIngredientsQuery.ExecuteReader())
                    {
                        while (ingredientsReader.Read())
                        {
                            Ingredient ingredient = new Ingredient()
                            {
                                Id = ingredientsReader.GetInt32(0),
                                Name = ingredientsReader.GetString(1),
                                Carbohydrates = ingredientsReader.GetInt32(2),
                                Fats = ingredientsReader.GetInt32(3),
                                Proteins = ingredientsReader.GetInt32(4)
                            };
                            recipeIngredients.Add(ingredient);
                        }
                    }
                    return recipeIngredients;
                }
            }
        }
    }
}