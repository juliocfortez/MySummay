using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_WEB.Models.Dtos.NoteDtos
{
    public class NoteDto
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

        public string? Pronunciation { get; set; }

        public string? Translate { get; set; }
    }
}
