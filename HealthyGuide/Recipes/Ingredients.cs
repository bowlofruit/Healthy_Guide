namespace HealthyGuide
{
    public class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }
        public int Proteins { get; set; }

        public Ingredients(int id, string name, int carbohydrates, int fats, int proteins)
        {
            Id = id;
            Name = name;
            Carbohydrates = carbohydrates;
            Fats = fats;
            Proteins = proteins;
        }
    }
}