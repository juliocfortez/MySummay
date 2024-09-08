using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_API.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public string SourceText { get; set; }

        public string? Translate { get; set; }

        public string? Pronunciation { get; set; }
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual Language Language { get; set; }
    }
}
