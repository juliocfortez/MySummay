using MyOwnSummary_WEB.Models.Dtos.CategoryDtos;
using MyOwnSummary_WEB.Models.Dtos.LanguageDtos;
using System.ComponentModel.DataAnnotations;
namespace MyOwnSummary_WEB.Models

{
    public class DataForCreateNote
    {
            [Required]
            public int UserId { get; set; }
            [Required]
            public List<LanguageDto> Languages { get; set; }
            [Required]
            public List<CategoryDto> Categories { get; set; }
    }
}
