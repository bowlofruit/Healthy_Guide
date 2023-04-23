using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;

namespace HealthyGuide
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Server=localhost; Port=5432; Database=Culinary_guide; User Id=postgres; Password=Helper44x44;";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Recipe_Click(object sender, RoutedEventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string categoryQuery = "SELECT * from category";
                using (NpgsqlCommand commCategoryQuery = new NpgsqlCommand(categoryQuery))
                {
                    using (NpgsqlDataReader reader = commCategoryQuery.ExecuteReader())
                    {
                        List<Category> categories = new List<Category>();
                        while (reader.Read())
                        {
                            string recipeInfoQuery = $"SELECT RI.instruction, RI.headline, R.servings, R.cookingtime, R.kcal, C.name FROM recipeinfo ri, recipe r, recipecategory rc, category c WHERE r.id = ri.recipeid and rc.categoryid = c.id and c.id = {} and rc.recipeid = r.id "
                            using (NpgsqlCommand commRecipeQuery = new NpgsqlCommand())
                            {

                            }
                        }
                    }
                }
            }
        }
    }

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

    public class RecipeInfo
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Instruction { get; set; }
        public string Details { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public int Services { get; set; }
        public int CookingTime { get; set; }

        public RecipeInfo(int id, string headline, string instruction, string details, List<Ingredients> ingredients, int services, int cookingTime)
        {
            Id = id;
            Headline = headline;
            Instruction = instruction;
            Details = details;
            Ingredients = ingredients;
            Services = services;
            CookingTime = cookingTime;
        }
    }

    public class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }
        public int Proteins { get; set; }

        public Ingredients (int id, string name, int carbohydrates, int fats, int proteins)
        {
            Id = id;
            Name = name;
            Carbohydrates = carbohydrates;
            Fats = fats;
            Proteins = proteins;
        }
    }
}