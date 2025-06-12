namespace BookTrackerApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int? Year { get; set; } 
        public int Rating { get; set; } = 0; 
        public bool IsRead { get; set; } = false;
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public User? User { get; set; }
    }
}
