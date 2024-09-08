using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_WEB.Models.Dtos.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
