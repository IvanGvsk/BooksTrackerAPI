namespace BookTrackerApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public required string PasswordHash { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
