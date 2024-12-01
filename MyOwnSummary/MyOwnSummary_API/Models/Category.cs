using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_API.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public required string Name { get; set; }

        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
