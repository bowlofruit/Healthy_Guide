using System;

namespace HealthGuide.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public float Score { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
}
