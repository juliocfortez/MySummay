using MyOwnSummary_WEB.Models.Dtos.CategoryDtos;
using MyOwnSummary_WEB.Models.Dtos.LanguageDtos;
using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_WEB.Models
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
        public string SourceText { get; set; } = string.Empty;

        public string Translate { get; set; } = string.Empty;

        public string? Pronunciation { get; set; }
    }

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
        [MaxLength(100)]
        public string SourceText { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Pronunciation { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Translate { get; set; }

        public int Repetition { get; set; }
    }

    public class CreateNoteDto
    {
        public int UserId { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public string SourceText { get; set; } = string.Empty;

        public string Translate { get; set; } = string.Empty;

        public string? Pronunciation { get; set; }
    }

    public class NoteViewDto
    {
        public int UserId { get; set; }
        public List<NoteDto> Notes { get; set; } = new();

        public List<LanguageDto> Languages { get; set; } = new();

        public List<CategoryDto> Categories { get; set; } = new();
    }
}
