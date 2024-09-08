using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_WEB.Models.Dtos.CategoryDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
