using MyOwnSummary_WEB.Models.Dtos.CategoryDtos;
using MyOwnSummary_WEB.Models.Dtos.LanguageDtos;
using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_WEB.Models.Dtos.NoteDtos
{
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
        public string SourceText { get; set; }

        public string? Translate { get; set; }

        public string? Pronunciation { get; set; }
    }

    public class ShowCreateNoteView
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public List<LanguageDto> Languages { get; set; }
        [Required]
        public List<CategoryDto> Categories { get; set; }
    }
}
