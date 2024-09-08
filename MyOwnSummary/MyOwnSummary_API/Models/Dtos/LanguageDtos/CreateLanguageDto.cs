using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_API.Models.Dtos.LanguageDtos
{
    public class CreateLanguageDto
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
