namespace HealthyGuide.Blogs
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserLogin { get; set; }

        public Blog(int id, string name, string description, string userLogin)
        {
            Id = id;
            Name = name;
            Description = description;
            UserLogin = userLogin;
        }
    }
}