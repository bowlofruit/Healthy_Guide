using Npgsql;
using System.Collections.Generic;

namespace HealthyGuide.Recipes
{
    public class CategoryCreator
    {
        private readonly string connectionString;
        public List<Category> Categories { get; set; }

        public CategoryCreator(string connectionString)
        {
            this.connectionString = connectionString;
            Categories = FillCategory();
        }

        private List<Category> FillCategory()
        {
            List<Category> categories = GetCategories();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                foreach (Category category in categories)
                {
                    List<RecipeInfo> recipes = GetRecipesByCategoryId(category.Id);
                    category.RecipeInfo.AddRange(recipes);
                }
            }
            return categories;
        }

        private List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string categoryQuery = "SELECT * from category";
                using (NpgsqlCommand commCategoryQuery = new NpgsqlCommand(categoryQuery, conn))
                {
                    using (NpgsqlDataReader categoryReader = commCategoryQuery.ExecuteReader())
                    {
                        while (categoryReader.Read())
                        {
                            Category category = new Category(categoryReader.GetInt32(0), categoryReader.GetString(1), new List<RecipeInfo>());
                            categories.Add(category);
                        }
                    }
                }
            }
            return categories;
        }

        private List<RecipeInfo> GetRecipesByCategoryId(int categoryId)
        {
            List<RecipeInfo> recipes = new List<RecipeInfo>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string recipeInfoQuery = $"SELECT R.id, R.name, RI.instruction, RI.servings, RI.cookingtime, RI.kcal, C.name FROM recipe_info RI INNER JOIN recipe R ON R.id = RI.recipeid INNER JOIN recipe_category RC ON RC.recipeid = R.id INNER JOIN category C ON C.id = RC.categoryid WHERE C.id = {categoryId};";
                using (NpgsqlCommand commRecipeQuery = new NpgsqlCommand(recipeInfoQuery, conn))
                {
                    using (NpgsqlDataReader recipeInfoReader = commRecipeQuery.ExecuteReader())
                    {
                        while (recipeInfoReader.Read())
                        {
                            List<Ingredients> recipeIngredients = GetIngredientsByRecipeId(recipeInfoReader.GetInt32(0));
                            string instruction = recipeInfoReader.GetString(2).Replace(". ", "\n");
                            RecipeInfo recipeInfo = new RecipeInfo(recipeInfoReader.GetInt32(0), recipeInfoReader.GetString(1), instruction, recipeInfoReader.GetInt32(5), recipeIngredients, recipeInfoReader.GetInt32(3), recipeInfoReader.GetInt32(4));
                            recipes.Add(recipeInfo);
                        }
                    }
                }
            }
            return recipes;
        }

        private List<Ingredients> GetIngredientsByRecipeId(int recipeId)
        {
            List<Ingredients> recipeIngredients = new List<Ingredients>();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string ingredientsQuery = $"select ingredients.id, ingredients.name, carbohydrates, fats, proteins from ingredients inner join ingredients_list ON ingredients.id = ingredients_list.ingredientid inner join recipe ON recipe.id = ingredients_list.recipeid and recipe.id = {recipeId};";
                using (NpgsqlCommand commIngredientsQuery = new NpgsqlCommand(ingredientsQuery, conn))
                {
                    using (NpgsqlDataReader ingredientsReader = commIngredientsQuery.ExecuteReader())
                    {
                        while (ingredientsReader.Read())
                        {
                            Ingredients ingredient = new Ingredients(ingredientsReader.GetInt32(0), ingredientsReader.GetString(1), ingredientsReader.GetInt32(2), ingredientsReader.GetInt32(3), ingredientsReader.GetInt32(4));
                            recipeIngredients.Add(ingredient);
                        }
                    }
                }
                return recipeIngredients;
            }
        }
    }
}