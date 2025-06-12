using System.ComponentModel.DataAnnotations;

namespace BookTrackerApi.DTOs
{
    public record AuthDto
    {
        [Required]
        [MinLength(3)]
        public string Username { get; init; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; init; } = string.Empty;
    }
    public record RegisterDto
    {
        [Required]
        [MinLength(3)]
        public string Username { get; init; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; init; } = string.Empty;
    }
}
