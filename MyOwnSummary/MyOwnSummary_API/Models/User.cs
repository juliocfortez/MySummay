using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_API.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Email { get; set; }

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

        public virtual ICollection<UserLanguage> Languages { get; set;} = new List<UserLanguage>();
    }
}
