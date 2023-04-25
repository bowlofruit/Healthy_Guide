namespace HealthyGuide.Reviews
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public string ReviewType { get; set; }
        public string ReviewName { get; set; }
        public string Login { get; set; }

        public Review(int id, string description, int score, string reviewType, string reviewName, string login)
        {
            Id = id;
            Description = description;
            Score = score;
            ReviewType = reviewType;
            ReviewName = reviewName;
            Login = login;
        }
    }
}