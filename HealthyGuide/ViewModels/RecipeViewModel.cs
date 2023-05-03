using HealthGuide.Models;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HealthGuide.ViewModels
{
    public class RecipeViewModel : BaseViewModel<Recipe>, INotifyPropertyChanged
    {
        protected override List<Recipe> LoadTable()
        {
            string query = "SELECT * FROM recipe r, recipe_info ri WHERE r.id = ri.recipeid";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    List<Recipe> recipes = new List<Recipe>();

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Recipe recipe = new Recipe()
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Instruction = reader.GetString(2).Replace(". ", "\n"),
                                CookingTime = reader.GetInt32(3),
                                Servings = reader.GetInt32(4),
                                Kcal = reader.GetInt32(5),
                                Ingredient = new IngredientViewModel().GetIngredientsForRecipe(reader.GetInt32(0))
                            };
                            recipes.Add(recipe);
                        }
                    }
                    return recipes;
                }
            }
        }

        protected override void AddValue(object parameter) => WindowCreator.CreateAddValueWindow(typeof(Recipe), AddValueToDatabase).ShowDialog();
        protected override void FilterTable(object parameter) => WindowCreator.CreateFilterTableWindow(typeof(Recipe), FilterTableToDatabase).ShowDialog();

        protected override void AddValueToDatabase(Window window, StackPanel stackPanel)
        {
            string title = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Title").Text;
            string instruction = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Instruction").Text;
            string kcal = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Kcal").Text;
            string servings = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "Servings").Text;
            string cookingTime = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "CookingTime").Text;

            if (!Validator.IsValidTitle(title))
            {
                MessageBox.Show("Invalid title");
                return;
            }

            if (!Validator.IsValidContent(instruction))
            {
                MessageBox.Show("Invalid instruction");
                return;
            }

            if (!Validator.IsValidKcal(kcal))
            {
                MessageBox.Show("Invalid kcal");
            }

            if (!Validator.IsValidServing(servings))
            {
                MessageBox.Show("Invalid servings");
            }

            if (!Validator.IsValidTime(cookingTime))
            {
                MessageBox.Show("Invalid cooking time");
            }

            List<Ingredient> ingredients = new List<Ingredient>();

            string recipeQuery = "INSERT INTO recipe (name) VALUES (@Name) RETURNING id";
            string recipeInfoQuery = "INSERT INTO recipe_info (recipeid, instruction, cookingtime, servings, kcal) VALUES (@Recipeid, @Instruction, @CookingTime, @Servings, @Kcal)";
            string recipeIngredientQuery = "INSERT INTO recipe_ingredient (recipeid, ingredientid) VALUES (@Recipeid, @Ingredientid)";

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (NpgsqlCommand recipeCmd = new NpgsqlCommand(recipeQuery, conn))
                {
                    recipeCmd.Parameters.AddWithValue("@Name", title);

                    int recipeid = (int)recipeCmd.ExecuteScalar();

                    using (NpgsqlCommand recipeInfoCmd = new NpgsqlCommand(recipeInfoQuery, conn))
                    {
                        recipeInfoCmd.Parameters.AddWithValue("Recipeid", recipeid);
                        recipeInfoCmd.Parameters.AddWithValue("@Instruction", instruction);
                        recipeInfoCmd.Parameters.AddWithValue("@CookingTime", int.Parse(cookingTime));
                        recipeInfoCmd.Parameters.AddWithValue("@Servings", int.Parse(servings));
                        recipeInfoCmd.Parameters.AddWithValue("@Kcal", int.Parse(kcal));

                        recipeInfoCmd.ExecuteNonQuery();
                    }

                    using (NpgsqlCommand recipeIngredientCmd = new NpgsqlCommand(recipeIngredientQuery, conn))
                    {
                        recipeIngredientCmd.Parameters.AddWithValue("@Recipeid", recipeid);

                        foreach (var ingredient in ingredients)
                        {
                            recipeIngredientCmd.Parameters.Clear();
                            recipeIngredientCmd.Parameters.AddWithValue("@Recipeid", recipeid);
                            recipeIngredientCmd.Parameters.AddWithValue("@Ingredientid", 1);

                            recipeIngredientCmd.ExecuteNonQuery();
                        }
                    }
                }

                Items = LoadTable();
            }
            window.Close();
        }


        protected override void DeleteValue(object parameter)
        {
            if (parameter is Recipe recipe)
            {
                string query = "DELETE FROM recipe WHERE Id = @Id";

                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", recipe.Id);
                        cmd.ExecuteNonQuery();
                    }

                    Items = LoadTable();
                }
            }
        }
    }
}