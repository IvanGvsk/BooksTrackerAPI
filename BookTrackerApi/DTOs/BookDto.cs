using System.ComponentModel.DataAnnotations;

namespace BookTrackerApi.DTOs
{
    public class BookDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Author { get; set; } = string.Empty;
        public int? Year { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        public bool IsRead { get; set; }
    }
}
