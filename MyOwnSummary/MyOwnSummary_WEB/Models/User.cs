using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_WEB.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string? Token { get; set; }
    }

    public class CreateUser
    {
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
