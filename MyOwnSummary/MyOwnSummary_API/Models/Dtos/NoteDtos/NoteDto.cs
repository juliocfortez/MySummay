using MyOwnSummary_API.Models.Dtos.CategoryDtos;
using MyOwnSummary_API.Models.Dtos.LanguageDtos;
using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_API.Models.Dtos.NoteDtos
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
        public string SourceText { get; set; } = "";

        public string? Pronunciation { get; set; }

        public string? Translate { get; set; }
    }

    public class NoteViewDto
    {
        public int UserId { get; set; }
        public List<NoteDto> Notes { get; set; } = new();

        public List<LanguageDto> Languages { get; set; } = new();

        public List<CategoryDto> Categories { get; set; } = new();
    }
}
