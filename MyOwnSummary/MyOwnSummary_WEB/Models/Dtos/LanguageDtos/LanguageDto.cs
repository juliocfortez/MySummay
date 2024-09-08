using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_WEB.Models.Dtos.LanguageDtos
{
    public class LanguageDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
