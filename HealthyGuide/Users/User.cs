namespace HealthyGuide.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string PhoneNumber { get; set; }

        public User(int id, string email, string login, string phoneNumber)
        {
            Id = id;
            Email = email;
            Login = login;
            PhoneNumber = phoneNumber;
        }
    }
}