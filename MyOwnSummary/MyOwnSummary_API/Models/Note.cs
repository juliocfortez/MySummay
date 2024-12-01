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
        public required string SourceText { get; set; }

        public required string Translate { get; set; }

        public string? Pronunciation { get; set; }

        public virtual required Category Category { get; set; }
        public virtual required User User { get; set; }
        public virtual required Language Language { get; set; }
    }
}
