using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_API.Models
{
    public class Note
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int LanguageId { get; set; }
        public required int CategoryId { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public required string SourceText { get; set; }

        [MaxLength(100)]
        public required string Translate { get; set; }

        [MaxLength(100)]
        public string? Pronunciation { get; set; }

        public required int Repetition { get; set; }

        public virtual required Category Category { get; set; }
        public virtual required User User { get; set; }
        public virtual required Language Language { get; set; }
    }

    
}
