using MyOwnSummary_API.Models.Dtos.CategoryDtos;
using MyOwnSummary_API.Models.Dtos.LanguageDtos;
using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_API.Models.Dtos.NoteDtos
{
    public class CreateNoteDto
    {
        public required int UserId { get; set; }
        public required int LanguageId { get; set; }
        public required int CategoryId { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
        public required string SourceText { get; set; }

        public required string Translate { get; set; }

        public string? Pronunciation { get; set; }
    }

    public class ShowCreateNoteView
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public List<LanguageDto> Languages { get; set; } = new();
        [Required]
        public List<CategoryDto> Categories { get; set; } = new();
    }
}
