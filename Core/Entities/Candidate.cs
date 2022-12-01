using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    [Table("Candidates")]
    public class Candidate
    {
        [Key]
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
        [MinLength(1)]
        public string FirstName { get; set; } = null!;
        [MinLength(1)]
        public string LastName { get; set; } = null!;
        [Phone]
        public string? PhoneNumber { get; set; }
        public string? TimeInterval { get; set; }
        [Url]
        public string? LinkedInProfileUrl { get; set; }
        [Url]
        public string? GitHubProfileUrl { get; set; }
        [MinLength(1)]
        public string TextComment { get; set; } = null!;
        [JsonIgnore]
        public bool IsModified { get; set; }

    }
}
