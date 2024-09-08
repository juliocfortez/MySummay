using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_WEB.Models.Dtos.LanguageDtos
{
    public class CreateLanguageDto
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
